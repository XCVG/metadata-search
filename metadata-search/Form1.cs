using System.Diagnostics;
using System.Windows.Forms;

namespace metadata_search
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource? CancellationTokenSource;
        private SearchResultSet? CurrentResults;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonPickSource_Click(object sender, EventArgs e)
        {
            //hacky, taken from https://stackoverflow.com/questions/11624298/how-to-use-openfiledialog-to-select-a-folder

            OpenFileDialog folderBrowser = new OpenFileDialog();
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                textBoxSource.Text = folderPath;
            }
        }

        private void buttonPickDestination_Click(object sender, EventArgs e)
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                textBoxDestination.Text = folderPath;
            }
        }

        private void SetUIState(bool state)
        {
            groupBoxSource.Enabled = state;
            groupBoxSearch.Enabled = state;
            listBoxResults.Enabled = state;
            buttonOpenSelected.Enabled = state;
            buttonMoveSelected.Enabled = state;
            buttonMoveAll.Enabled = state;
        }

        private async void RunCancellableModal(Func<CancellationToken, Task> task)
        {
            try
            {
                progressBar1.Visible = true;
                progressBar1.Enabled = true;
                buttonStop.Visible = true;
                buttonStop.Enabled = true;

                SetUIState(false);
                CancellationTokenSource = new CancellationTokenSource();
                await task(CancellationTokenSource.Token);
            }
            catch (Exception e)
            {

            }
            finally
            {
                progressBar1.Visible = false;
                progressBar1.Enabled = false;
                buttonStop.Visible = false;
                buttonStop.Enabled = false;
                SetUIState(true);
                CancellationTokenSource = null;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (CancellationTokenSource != null)
            {
                CancellationTokenSource.Cancel();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSource.Text))
                return; //rudimentary check

            RunCancellableModal(async (cancellationToken) =>
            {
                listBoxResults.DataSource = null;
                CurrentResults = null;

                string? excludeFolder = null;
                if(checkBoxExcludeSpecialFolders.Checked)
                    excludeFolder = textBoxDestination.Text;

                SearchInput input = new SearchInput()
                {
                    FolderPath = textBoxSource.Text,
                    Title = textBoxSearchTitle.Text,
                    Artist = textBoxSearchArtist.Text,
                    ChannelId = textBoxSearchChannel.Text,
                    ExcludeSpecialFolders = checkBoxExcludeSpecialFolders.Checked,
                    ExcludePath = excludeFolder
                };

                var resultSet = await Task.Run(() => SearchResultSet.SearchFolder(input, cancellationToken));
                CurrentResults = resultSet;

                listBoxResults.DataSource = resultSet.GetDisplayableSearchResults();
                // query these could be column names.
                listBoxResults.DisplayMember = "FileName";
                listBoxResults.ValueMember = "FilePath";

            });
        }

        private void listBoxResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            // show data in inspector
            if (listBoxResults.SelectedItems.Count == 1)
            {
                var item = listBoxResults.SelectedItems[0] as DisplayableSearchResult;
                string fullName = item.FilePath;
                var searchResult = CurrentResults.GetResult(fullName);
                textBoxInspector.Text = searchResult.Description.Replace("\n", Environment.NewLine); //apparently you need this?!
                labelInspectorTitle.Text = item.FileName;

            }
            else if (listBoxResults.SelectedItems.Count > 1)
            {
                labelInspectorTitle.Text = "<multiple items selected>";
                textBoxInspector.Text = "";
            }
            else
            {
                labelInspectorTitle.Text = "";
                textBoxInspector.Text = "";
            }
        }

        private void buttonOpenSelected_Click(object sender, EventArgs e)
        {
            if (listBoxResults.SelectedItems.Count > 0)
            {
                foreach (object oItem in listBoxResults.SelectedItems)
                {
                    try
                    {
                        //open folder to selected file, based on an answer to https://stackoverflow.com/questions/334630/opening-a-folder-in-explorer-and-selecting-a-file
                        //could be improved but would be pretty annoying to implement see https://stackoverflow.com/questions/13680415/how-to-open-explorer-with-a-specific-file-selected
                        var item = oItem as DisplayableSearchResult;

                        string args = string.Format("/e, /select, \"{0}\"", item.FilePath);

                        ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = "explorer";
                        info.Arguments = args;
                        Process.Start(info);
                    }
                    catch (Exception ex)
                    {
                        //TODO should log
                    }
                }
            }


        }

        private void buttonMoveSelected_Click(object sender, EventArgs e)
        {
            if (listBoxResults.SelectedItems.Count > 0 && !string.IsNullOrEmpty(textBoxDestination.Text))
            {
                RunCancellableModal(async (cancellationToken) =>
                {
                    //listBoxResults.SelectedItems
                    List<string> sourceFiles = new List<string>();
                    foreach (object oItem in listBoxResults.SelectedItems)
                    {
                        var item = oItem as DisplayableSearchResult;
                        sourceFiles.Add(item.FilePath);
                    }

                    string destinationFolder = textBoxDestination.Text;

                    await Task.Run(() => MoveFiles(destinationFolder, sourceFiles, cancellationToken));

                    listBoxResults.ClearSelected();
                    listBoxResults.DataSource = null;
                    CurrentResults = null;
                    labelInspectorTitle.Text = "";
                    textBoxInspector.Text = "";
                });
            }
        }

        private void buttonMoveAll_Click(object sender, EventArgs e)
        {
            if (listBoxResults.DataSource != null && listBoxResults.Items.Count > 0 && !string.IsNullOrEmpty(textBoxDestination.Text))
            {
                RunCancellableModal(async (cancellationToken) =>
                {
                    //listBoxResults.SelectedItems
                    List<string> sourceFiles = new List<string>();
                    foreach (object oItem in listBoxResults.Items)
                    {
                        var item = oItem as DisplayableSearchResult;
                        sourceFiles.Add(item.FilePath);
                    }

                    string destinationFolder = textBoxDestination.Text;

                    await Task.Run(() => MoveFiles(destinationFolder, sourceFiles, cancellationToken));

                    listBoxResults.ClearSelected();
                    listBoxResults.DataSource = null;
                    CurrentResults = null;
                    labelInspectorTitle.Text = "";
                    textBoxInspector.Text = "";
                });
            }
        }

        private async Task MoveFiles(string destinationFolder, IEnumerable<string> sourceFiles, CancellationToken cancellationToken)
        {
            Directory.CreateDirectory(destinationFolder);
            foreach (string sourceFile in sourceFiles)
            {
                try
                {
                    string targetPath = Path.Combine(destinationFolder, Path.GetFileName(sourceFile));

                    var modifiedDate = File.GetLastWriteTime(sourceFile);
                    var createdDate = File.GetCreationTime(sourceFile);

                    File.Move(sourceFile, targetPath);

                    await Task.Delay(100);

                    File.SetCreationTime(targetPath, createdDate);
                    File.SetLastWriteTime(targetPath, modifiedDate);

                }
                catch (Exception ex)
                {
                    //TODO should probably log failures
                }

                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        
    }
}
