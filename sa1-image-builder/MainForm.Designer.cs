
namespace GUIPatcher
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.modDescription = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabMods = new System.Windows.Forms.TabPage();
            this.modListView = new System.Windows.Forms.ListView();
            this.columnName = new System.Windows.Forms.ColumnHeader();
            this.columnAuthor = new System.Windows.Forms.ColumnHeader();
            this.columnVersion = new System.Windows.Forms.ColumnHeader();
            this.tabCodes = new System.Windows.Forms.TabPage();
            this.codesListView = new System.Windows.Forms.ListView();
            this.tabOptions = new System.Windows.Forms.TabPage();
            this.labelGameCheckResult = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxOriginalPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabBuild = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonBrowseOutput = new System.Windows.Forms.Button();
            this.textBoxOutputPath = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonSaveSettings = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabMods.SuspendLayout();
            this.tabCodes.SuspendLayout();
            this.tabOptions.SuspendLayout();
            this.tabBuild.SuspendLayout();
            this.SuspendLayout();
            // 
            // modDescription
            // 
            this.modDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.modDescription.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.modDescription.Location = new System.Drawing.Point(3, 562);
            this.modDescription.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.modDescription.Multiline = true;
            this.modDescription.Name = "modDescription";
            this.modDescription.ReadOnly = true;
            this.modDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.modDescription.Size = new System.Drawing.Size(758, 238);
            this.modDescription.TabIndex = 4;
            this.modDescription.TabStop = false;
            this.modDescription.Text = "Mod description.";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMods);
            this.tabControl1.Controls.Add(this.tabCodes);
            this.tabControl1.Controls.Add(this.tabOptions);
            this.tabControl1.Controls.Add(this.tabBuild);
            this.tabControl1.Location = new System.Drawing.Point(13, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 842);
            this.tabControl1.TabIndex = 8;
            // 
            // tabMods
            // 
            this.tabMods.BackColor = System.Drawing.SystemColors.Control;
            this.tabMods.Controls.Add(this.modListView);
            this.tabMods.Controls.Add(this.modDescription);
            this.tabMods.Location = new System.Drawing.Point(4, 34);
            this.tabMods.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMods.Name = "tabMods";
            this.tabMods.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabMods.Size = new System.Drawing.Size(764, 804);
            this.tabMods.TabIndex = 0;
            this.tabMods.Text = "Mods";
            // 
            // modListView
            // 
            this.modListView.CheckBoxes = true;
            this.modListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnAuthor,
            this.columnVersion});
            this.modListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.modListView.FullRowSelect = true;
            this.modListView.HideSelection = false;
            this.modListView.Location = new System.Drawing.Point(3, 4);
            this.modListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modListView.MultiSelect = false;
            this.modListView.Name = "modListView";
            this.modListView.Size = new System.Drawing.Size(758, 544);
            this.modListView.TabIndex = 6;
            this.modListView.UseCompatibleStateImageBehavior = false;
            this.modListView.View = System.Windows.Forms.View.Details;
            this.modListView.SelectedIndexChanged += new System.EventHandler(this.modListView_SelectedIndexChanged);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            this.columnName.Width = 400;
            // 
            // columnAuthor
            // 
            this.columnAuthor.Text = "Author";
            this.columnAuthor.Width = 145;
            // 
            // columnVersion
            // 
            this.columnVersion.Text = "Version";
            this.columnVersion.Width = 99;
            // 
            // tabCodes
            // 
            this.tabCodes.BackColor = System.Drawing.SystemColors.Control;
            this.tabCodes.Controls.Add(this.codesListView);
            this.tabCodes.Location = new System.Drawing.Point(4, 34);
            this.tabCodes.Name = "tabCodes";
            this.tabCodes.Padding = new System.Windows.Forms.Padding(3);
            this.tabCodes.Size = new System.Drawing.Size(764, 804);
            this.tabCodes.TabIndex = 3;
            this.tabCodes.Text = "Codes";
            // 
            // codesListView
            // 
            this.codesListView.CheckBoxes = true;
            this.codesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codesListView.FullRowSelect = true;
            this.codesListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.codesListView.HideSelection = false;
            this.codesListView.Location = new System.Drawing.Point(3, 3);
            this.codesListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.codesListView.MultiSelect = false;
            this.codesListView.Name = "codesListView";
            this.codesListView.Size = new System.Drawing.Size(758, 798);
            this.codesListView.TabIndex = 7;
            this.codesListView.UseCompatibleStateImageBehavior = false;
            this.codesListView.View = System.Windows.Forms.View.List;
            // 
            // tabOptions
            // 
            this.tabOptions.BackColor = System.Drawing.SystemColors.Control;
            this.tabOptions.Controls.Add(this.labelGameCheckResult);
            this.tabOptions.Controls.Add(this.buttonBrowse);
            this.tabOptions.Controls.Add(this.textBoxOriginalPath);
            this.tabOptions.Controls.Add(this.label3);
            this.tabOptions.Location = new System.Drawing.Point(4, 34);
            this.tabOptions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabOptions.Name = "tabOptions";
            this.tabOptions.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabOptions.Size = new System.Drawing.Size(764, 804);
            this.tabOptions.TabIndex = 1;
            this.tabOptions.Text = "Options";
            // 
            // labelGameCheckResult
            // 
            this.labelGameCheckResult.AutoSize = true;
            this.labelGameCheckResult.Location = new System.Drawing.Point(18, 211);
            this.labelGameCheckResult.Name = "labelGameCheckResult";
            this.labelGameCheckResult.Size = new System.Drawing.Size(591, 25);
            this.labelGameCheckResult.TabIndex = 3;
            this.labelGameCheckResult.Text = "Browse for a folder to check whether it is the correct version of the game.";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(613, 120);
            this.buttonBrowse.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(113, 46);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxOriginalPath
            // 
            this.textBoxOriginalPath.Location = new System.Drawing.Point(22, 126);
            this.textBoxOriginalPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxOriginalPath.Name = "textBoxOriginalPath";
            this.textBoxOriginalPath.Size = new System.Drawing.Size(574, 31);
            this.textBoxOriginalPath.TabIndex = 1;
            this.textBoxOriginalPath.TextChanged += new System.EventHandler(this.textBoxOriginalPath_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(691, 75);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select the folder containing the original Sonic Adventure (US, 1.005) GDI file:\r\n" +
    "\r\nThis program will create a patched GDI image of Sonic Adventure with selected " +
    "mods.\r\n";
            // 
            // tabBuild
            // 
            this.tabBuild.BackColor = System.Drawing.SystemColors.Control;
            this.tabBuild.Controls.Add(this.label4);
            this.tabBuild.Controls.Add(this.buttonBrowseOutput);
            this.tabBuild.Controls.Add(this.textBoxOutputPath);
            this.tabBuild.Controls.Add(this.labelStatus);
            this.tabBuild.Controls.Add(this.textBoxLog);
            this.tabBuild.Location = new System.Drawing.Point(4, 34);
            this.tabBuild.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabBuild.Name = "tabBuild";
            this.tabBuild.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabBuild.Size = new System.Drawing.Size(764, 804);
            this.tabBuild.TabIndex = 2;
            this.tabBuild.Text = "Build Image";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(373, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Select the output path for the patched image:";
            // 
            // buttonBrowseOutput
            // 
            this.buttonBrowseOutput.Location = new System.Drawing.Point(637, 38);
            this.buttonBrowseOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonBrowseOutput.Name = "buttonBrowseOutput";
            this.buttonBrowseOutput.Size = new System.Drawing.Size(113, 46);
            this.buttonBrowseOutput.TabIndex = 8;
            this.buttonBrowseOutput.Text = "Browse...";
            this.buttonBrowseOutput.UseVisualStyleBackColor = true;
            this.buttonBrowseOutput.Click += new System.EventHandler(this.buttonBrowseOutput_Click);
            // 
            // textBoxOutputPath
            // 
            this.textBoxOutputPath.Location = new System.Drawing.Point(13, 44);
            this.textBoxOutputPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxOutputPath.Name = "textBoxOutputPath";
            this.textBoxOutputPath.Size = new System.Drawing.Size(616, 31);
            this.textBoxOutputPath.TabIndex = 7;
            this.textBoxOutputPath.TextChanged += new System.EventHandler(this.textBoxOutputPath_TextChanged);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(9, 106);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(636, 25);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Click Build to create a modified GD-ROM image. Progress will be shown below.";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(9, 136);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(741, 654);
            this.textBoxLog.TabIndex = 5;
            // 
            // buttonBuild
            // 
            this.buttonBuild.Enabled = false;
            this.buttonBuild.Location = new System.Drawing.Point(639, 865);
            this.buttonBuild.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(142, 46);
            this.buttonBuild.TabIndex = 10;
            this.buttonBuild.Text = "Build Image";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 60;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonSaveSettings
            // 
            this.buttonSaveSettings.Location = new System.Drawing.Point(490, 865);
            this.buttonSaveSettings.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.Size = new System.Drawing.Size(142, 46);
            this.buttonSaveSettings.TabIndex = 9;
            this.buttonSaveSettings.Text = "Save Settings";
            this.buttonSaveSettings.UseVisualStyleBackColor = true;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 921);
            this.Controls.Add(this.buttonBuild);
            this.Controls.Add(this.buttonSaveSettings);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Sonic Adventure Image Builder";
            this.tabControl1.ResumeLayout(false);
            this.tabMods.ResumeLayout(false);
            this.tabMods.PerformLayout();
            this.tabCodes.ResumeLayout(false);
            this.tabOptions.ResumeLayout(false);
            this.tabOptions.PerformLayout();
            this.tabBuild.ResumeLayout(false);
            this.tabBuild.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox modDescription;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabMods;
        private System.Windows.Forms.TabPage tabOptions;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxOriginalPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabBuild;
        private System.Windows.Forms.Button buttonBrowseOutput;
        private System.Windows.Forms.TextBox textBoxOutputPath;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label labelGameCheckResult;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListView modListView;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnAuthor;
        private System.Windows.Forms.ColumnHeader columnVersion;
        private System.Windows.Forms.Button buttonSaveSettings;
        private System.Windows.Forms.TabPage tabCodes;
        private System.Windows.Forms.ListView codesListView;
    }
}

