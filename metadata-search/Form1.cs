namespace metadata_search
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource? CancellationTokenSource;

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
            if(CancellationTokenSource != null)
            {
                CancellationTokenSource.Cancel();
            }
        }
    }
}
