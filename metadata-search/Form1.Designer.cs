namespace metadata_search
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxSource = new GroupBox();
            checkBoxExcludeSpecialFolders = new CheckBox();
            buttonPickSource = new Button();
            textBoxSource = new TextBox();
            groupBoxSearch = new GroupBox();
            buttonSearch = new Button();
            textBoxSearchChannel = new TextBox();
            labelSearchChannel = new Label();
            textBoxSearchArtist = new TextBox();
            labelSearchArtist = new Label();
            textBoxSearchTitle = new TextBox();
            labelSearchTitle = new Label();
            listBoxResults = new ListBox();
            labelInspectorTitle = new Label();
            textBoxInspector = new TextBox();
            buttonOpenSelected = new Button();
            buttonMoveSelected = new Button();
            buttonMoveAll = new Button();
            labelDestination = new Label();
            textBoxDestination = new TextBox();
            buttonPickDestination = new Button();
            buttonStop = new Button();
            progressBar1 = new ProgressBar();
            groupBoxSource.SuspendLayout();
            groupBoxSearch.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxSource
            // 
            groupBoxSource.Controls.Add(checkBoxExcludeSpecialFolders);
            groupBoxSource.Controls.Add(buttonPickSource);
            groupBoxSource.Controls.Add(textBoxSource);
            groupBoxSource.Location = new Point(12, 12);
            groupBoxSource.Name = "groupBoxSource";
            groupBoxSource.Size = new Size(760, 80);
            groupBoxSource.TabIndex = 0;
            groupBoxSource.TabStop = false;
            groupBoxSource.Text = "Source";
            // 
            // checkBoxExcludeSpecialFolders
            // 
            checkBoxExcludeSpecialFolders.AutoSize = true;
            checkBoxExcludeSpecialFolders.Checked = true;
            checkBoxExcludeSpecialFolders.CheckState = CheckState.Checked;
            checkBoxExcludeSpecialFolders.Location = new Point(6, 51);
            checkBoxExcludeSpecialFolders.Name = "checkBoxExcludeSpecialFolders";
            checkBoxExcludeSpecialFolders.Size = new Size(162, 19);
            checkBoxExcludeSpecialFolders.TabIndex = 2;
            checkBoxExcludeSpecialFolders.Text = "Ignore special folders (!__)";
            checkBoxExcludeSpecialFolders.UseVisualStyleBackColor = true;
            // 
            // buttonPickSource
            // 
            buttonPickSource.Location = new Point(706, 22);
            buttonPickSource.Name = "buttonPickSource";
            buttonPickSource.Size = new Size(48, 23);
            buttonPickSource.TabIndex = 1;
            buttonPickSource.Text = "...";
            buttonPickSource.UseVisualStyleBackColor = true;
            buttonPickSource.Click += buttonPickSource_Click;
            // 
            // textBoxSource
            // 
            textBoxSource.Location = new Point(6, 22);
            textBoxSource.Name = "textBoxSource";
            textBoxSource.Size = new Size(694, 23);
            textBoxSource.TabIndex = 0;
            // 
            // groupBoxSearch
            // 
            groupBoxSearch.Controls.Add(buttonSearch);
            groupBoxSearch.Controls.Add(textBoxSearchChannel);
            groupBoxSearch.Controls.Add(labelSearchChannel);
            groupBoxSearch.Controls.Add(textBoxSearchArtist);
            groupBoxSearch.Controls.Add(labelSearchArtist);
            groupBoxSearch.Controls.Add(textBoxSearchTitle);
            groupBoxSearch.Controls.Add(labelSearchTitle);
            groupBoxSearch.Location = new Point(12, 98);
            groupBoxSearch.Name = "groupBoxSearch";
            groupBoxSearch.Size = new Size(760, 155);
            groupBoxSearch.TabIndex = 1;
            groupBoxSearch.TabStop = false;
            groupBoxSearch.Text = "Search";
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(679, 126);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(75, 23);
            buttonSearch.TabIndex = 6;
            buttonSearch.Text = "Search";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += buttonSearch_Click;
            // 
            // textBoxSearchChannel
            // 
            textBoxSearchChannel.Location = new Point(90, 88);
            textBoxSearchChannel.Name = "textBoxSearchChannel";
            textBoxSearchChannel.Size = new Size(393, 23);
            textBoxSearchChannel.TabIndex = 5;
            // 
            // labelSearchChannel
            // 
            labelSearchChannel.AutoSize = true;
            labelSearchChannel.Location = new Point(6, 91);
            labelSearchChannel.Name = "labelSearchChannel";
            labelSearchChannel.Size = new Size(65, 15);
            labelSearchChannel.TabIndex = 4;
            labelSearchChannel.Text = "Channel ID";
            // 
            // textBoxSearchArtist
            // 
            textBoxSearchArtist.Location = new Point(90, 56);
            textBoxSearchArtist.Name = "textBoxSearchArtist";
            textBoxSearchArtist.Size = new Size(393, 23);
            textBoxSearchArtist.TabIndex = 3;
            // 
            // labelSearchArtist
            // 
            labelSearchArtist.AutoSize = true;
            labelSearchArtist.Location = new Point(6, 59);
            labelSearchArtist.Name = "labelSearchArtist";
            labelSearchArtist.Size = new Size(35, 15);
            labelSearchArtist.TabIndex = 2;
            labelSearchArtist.Text = "Artist";
            // 
            // textBoxSearchTitle
            // 
            textBoxSearchTitle.Location = new Point(90, 22);
            textBoxSearchTitle.Name = "textBoxSearchTitle";
            textBoxSearchTitle.Size = new Size(393, 23);
            textBoxSearchTitle.TabIndex = 1;
            // 
            // labelSearchTitle
            // 
            labelSearchTitle.AutoSize = true;
            labelSearchTitle.Location = new Point(6, 25);
            labelSearchTitle.Name = "labelSearchTitle";
            labelSearchTitle.Size = new Size(29, 15);
            labelSearchTitle.TabIndex = 0;
            labelSearchTitle.Text = "Title";
            // 
            // listBoxResults
            // 
            listBoxResults.FormattingEnabled = true;
            listBoxResults.ItemHeight = 15;
            listBoxResults.Location = new Point(12, 259);
            listBoxResults.Name = "listBoxResults";
            listBoxResults.Size = new Size(443, 394);
            listBoxResults.TabIndex = 2;
            // 
            // labelInspectorTitle
            // 
            labelInspectorTitle.AutoSize = true;
            labelInspectorTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelInspectorTitle.Location = new Point(461, 259);
            labelInspectorTitle.Name = "labelInspectorTitle";
            labelInspectorTitle.Size = new Size(111, 15);
            labelInspectorTitle.TabIndex = 3;
            labelInspectorTitle.Text = "labelInspectorTitle";
            // 
            // textBoxInspector
            // 
            textBoxInspector.BackColor = SystemColors.ControlLightLight;
            textBoxInspector.Location = new Point(461, 277);
            textBoxInspector.Multiline = true;
            textBoxInspector.Name = "textBoxInspector";
            textBoxInspector.ReadOnly = true;
            textBoxInspector.Size = new Size(311, 376);
            textBoxInspector.TabIndex = 4;
            // 
            // buttonOpenSelected
            // 
            buttonOpenSelected.Location = new Point(657, 659);
            buttonOpenSelected.Name = "buttonOpenSelected";
            buttonOpenSelected.Size = new Size(109, 23);
            buttonOpenSelected.TabIndex = 5;
            buttonOpenSelected.Text = "Open Selected";
            buttonOpenSelected.UseVisualStyleBackColor = true;
            // 
            // buttonMoveSelected
            // 
            buttonMoveSelected.Location = new Point(12, 659);
            buttonMoveSelected.Name = "buttonMoveSelected";
            buttonMoveSelected.Size = new Size(109, 23);
            buttonMoveSelected.TabIndex = 6;
            buttonMoveSelected.Text = "Move Selected";
            buttonMoveSelected.UseVisualStyleBackColor = true;
            // 
            // buttonMoveAll
            // 
            buttonMoveAll.Location = new Point(127, 659);
            buttonMoveAll.Name = "buttonMoveAll";
            buttonMoveAll.Size = new Size(109, 23);
            buttonMoveAll.TabIndex = 7;
            buttonMoveAll.Text = "Move All";
            buttonMoveAll.UseVisualStyleBackColor = true;
            // 
            // labelDestination
            // 
            labelDestination.AutoSize = true;
            labelDestination.Location = new Point(12, 699);
            labelDestination.Name = "labelDestination";
            labelDestination.Size = new Size(67, 15);
            labelDestination.TabIndex = 8;
            labelDestination.Text = "Destination";
            // 
            // textBoxDestination
            // 
            textBoxDestination.Location = new Point(85, 699);
            textBoxDestination.Name = "textBoxDestination";
            textBoxDestination.Size = new Size(639, 23);
            textBoxDestination.TabIndex = 9;
            // 
            // buttonPickDestination
            // 
            buttonPickDestination.Location = new Point(730, 699);
            buttonPickDestination.Name = "buttonPickDestination";
            buttonPickDestination.Size = new Size(42, 23);
            buttonPickDestination.TabIndex = 3;
            buttonPickDestination.Text = "...";
            buttonPickDestination.UseVisualStyleBackColor = true;
            buttonPickDestination.Click += buttonPickDestination_Click;
            // 
            // buttonStop
            // 
            buttonStop.Enabled = false;
            buttonStop.Location = new Point(333, 335);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(162, 102);
            buttonStop.TabIndex = 10;
            buttonStop.Text = "STOP!";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Visible = false;
            buttonStop.Click += buttonStop_Click;
            // 
            // progressBar1
            // 
            progressBar1.Enabled = false;
            progressBar1.Location = new Point(333, 443);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(162, 23);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 11;
            progressBar1.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 761);
            Controls.Add(progressBar1);
            Controls.Add(buttonStop);
            Controls.Add(buttonPickDestination);
            Controls.Add(textBoxDestination);
            Controls.Add(labelDestination);
            Controls.Add(buttonMoveAll);
            Controls.Add(buttonMoveSelected);
            Controls.Add(buttonOpenSelected);
            Controls.Add(textBoxInspector);
            Controls.Add(labelInspectorTitle);
            Controls.Add(listBoxResults);
            Controls.Add(groupBoxSearch);
            Controls.Add(groupBoxSource);
            Name = "Form1";
            Text = "Metadata Search";
            groupBoxSource.ResumeLayout(false);
            groupBoxSource.PerformLayout();
            groupBoxSearch.ResumeLayout(false);
            groupBoxSearch.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxSource;
        private CheckBox checkBoxExcludeSpecialFolders;
        private Button buttonPickSource;
        private TextBox textBoxSource;
        private GroupBox groupBoxSearch;
        private Label labelSearchTitle;
        private Label labelSearchArtist;
        private TextBox textBoxSearchTitle;
        private Button buttonSearch;
        private TextBox textBoxSearchChannel;
        private Label labelSearchChannel;
        private TextBox textBoxSearchArtist;
        private ListBox listBoxResults;
        private Label labelInspectorTitle;
        private TextBox textBoxInspector;
        private Button buttonOpenSelected;
        private Button buttonMoveSelected;
        private Button buttonMoveAll;
        private Label labelDestination;
        private TextBox textBoxDestination;
        private Button buttonPickDestination;
        private Button buttonStop;
        private ProgressBar progressBar1;
    }
}
