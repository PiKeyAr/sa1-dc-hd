
namespace GUIPatcher
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.patchDescription = new System.Windows.Forms.TextBox();
            this.patchListBox = new System.Windows.Forms.CheckedListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPath = new System.Windows.Forms.TabPage();
            this.labelGameCheckResult = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxOriginalPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPatches = new System.Windows.Forms.TabPage();
            this.tabPageFinish = new System.Windows.Forms.TabPage();
            this.buttonBuild = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonBrowseOutput = new System.Windows.Forms.Button();
            this.textBoxOutputPath = new System.Windows.Forms.TextBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPath.SuspendLayout();
            this.tabPatches.SuspendLayout();
            this.tabPageFinish.SuspendLayout();
            this.SuspendLayout();
            // 
            // patchDescription
            // 
            this.patchDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.patchDescription.Dock = System.Windows.Forms.DockStyle.Right;
            this.patchDescription.Location = new System.Drawing.Point(290, 3);
            this.patchDescription.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.patchDescription.Multiline = true;
            this.patchDescription.Name = "patchDescription";
            this.patchDescription.ReadOnly = true;
            this.patchDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.patchDescription.Size = new System.Drawing.Size(513, 493);
            this.patchDescription.TabIndex = 4;
            this.patchDescription.TabStop = false;
            this.patchDescription.Text = "Patch description.";
            // 
            // patchListBox
            // 
            this.patchListBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.patchListBox.FormattingEnabled = true;
            this.patchListBox.Location = new System.Drawing.Point(3, 3);
            this.patchListBox.Name = "patchListBox";
            this.patchListBox.Size = new System.Drawing.Size(280, 493);
            this.patchListBox.TabIndex = 5;
            this.patchListBox.SelectedIndexChanged += new System.EventHandler(this.patchListBox_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPath);
            this.tabControl1.Controls.Add(this.tabPatches);
            this.tabControl1.Controls.Add(this.tabPageFinish);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(814, 532);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPath
            // 
            this.tabPath.BackColor = System.Drawing.SystemColors.Control;
            this.tabPath.Controls.Add(this.labelGameCheckResult);
            this.tabPath.Controls.Add(this.buttonBrowse);
            this.tabPath.Controls.Add(this.textBoxOriginalPath);
            this.tabPath.Controls.Add(this.label3);
            this.tabPath.Location = new System.Drawing.Point(4, 29);
            this.tabPath.Name = "tabPath";
            this.tabPath.Padding = new System.Windows.Forms.Padding(3);
            this.tabPath.Size = new System.Drawing.Size(806, 499);
            this.tabPath.TabIndex = 1;
            this.tabPath.Text = "Select Original Image";
            // 
            // labelGameCheckResult
            // 
            this.labelGameCheckResult.AutoSize = true;
            this.labelGameCheckResult.Location = new System.Drawing.Point(16, 217);
            this.labelGameCheckResult.Name = "labelGameCheckResult";
            this.labelGameCheckResult.Size = new System.Drawing.Size(521, 20);
            this.labelGameCheckResult.TabIndex = 3;
            this.labelGameCheckResult.Text = "Browse for a folder to check whether it is the correct version of the game.";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(687, 145);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(102, 37);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxOriginalPath
            // 
            this.textBoxOriginalPath.Location = new System.Drawing.Point(20, 149);
            this.textBoxOriginalPath.Name = "textBoxOriginalPath";
            this.textBoxOriginalPath.Size = new System.Drawing.Size(660, 26);
            this.textBoxOriginalPath.TabIndex = 1;
            this.textBoxOriginalPath.TextChanged += new System.EventHandler(this.textBoxOriginalPath_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(605, 100);
            this.label3.TabIndex = 0;
            this.label3.Text = resources.GetString("label3.Text");
            // 
            // tabPatches
            // 
            this.tabPatches.BackColor = System.Drawing.SystemColors.Control;
            this.tabPatches.Controls.Add(this.patchDescription);
            this.tabPatches.Controls.Add(this.patchListBox);
            this.tabPatches.Location = new System.Drawing.Point(4, 29);
            this.tabPatches.Name = "tabPatches";
            this.tabPatches.Padding = new System.Windows.Forms.Padding(3);
            this.tabPatches.Size = new System.Drawing.Size(806, 499);
            this.tabPatches.TabIndex = 0;
            this.tabPatches.Text = "Select Patches";
            // 
            // tabPageFinish
            // 
            this.tabPageFinish.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageFinish.Controls.Add(this.buttonBuild);
            this.tabPageFinish.Controls.Add(this.label4);
            this.tabPageFinish.Controls.Add(this.buttonBrowseOutput);
            this.tabPageFinish.Controls.Add(this.textBoxOutputPath);
            this.tabPageFinish.Controls.Add(this.labelStatus);
            this.tabPageFinish.Controls.Add(this.textBoxLog);
            this.tabPageFinish.Location = new System.Drawing.Point(4, 29);
            this.tabPageFinish.Name = "tabPageFinish";
            this.tabPageFinish.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFinish.Size = new System.Drawing.Size(806, 499);
            this.tabPageFinish.TabIndex = 2;
            this.tabPageFinish.Text = "Create Patched Image";
            // 
            // buttonBuild
            // 
            this.buttonBuild.Enabled = false;
            this.buttonBuild.Location = new System.Drawing.Point(687, 448);
            this.buttonBuild.Name = "buttonBuild";
            this.buttonBuild.Size = new System.Drawing.Size(102, 37);
            this.buttonBuild.TabIndex = 10;
            this.buttonBuild.Text = "Build";
            this.buttonBuild.UseVisualStyleBackColor = true;
            this.buttonBuild.Click += new System.EventHandler(this.buttonBuild_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(330, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Select the output path for the patched image:";
            // 
            // buttonBrowseOutput
            // 
            this.buttonBrowseOutput.Location = new System.Drawing.Point(688, 31);
            this.buttonBrowseOutput.Name = "buttonBrowseOutput";
            this.buttonBrowseOutput.Size = new System.Drawing.Size(102, 37);
            this.buttonBrowseOutput.TabIndex = 8;
            this.buttonBrowseOutput.Text = "Browse...";
            this.buttonBrowseOutput.UseVisualStyleBackColor = true;
            this.buttonBrowseOutput.Click += new System.EventHandler(this.buttonBrowseOutput_Click);
            // 
            // textBoxOutputPath
            // 
            this.textBoxOutputPath.Location = new System.Drawing.Point(12, 35);
            this.textBoxOutputPath.Name = "textBoxOutputPath";
            this.textBoxOutputPath.Size = new System.Drawing.Size(668, 26);
            this.textBoxOutputPath.TabIndex = 7;
            this.textBoxOutputPath.TextChanged += new System.EventHandler(this.textBoxOutputPath_TextChanged);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(8, 85);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(559, 20);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Click Build to create a modified GD-ROM image. Progress will be shown below.";
            // 
            // textBoxLog
            // 
            this.textBoxLog.Location = new System.Drawing.Point(8, 109);
            this.textBoxLog.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(782, 329);
            this.textBoxLog.TabIndex = 5;
            // 
            // timer1
            // 
            this.timer1.Interval = 60;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 555);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Sonic Adventure DC-HD Patcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPath.ResumeLayout(false);
            this.tabPath.PerformLayout();
            this.tabPatches.ResumeLayout(false);
            this.tabPatches.PerformLayout();
            this.tabPageFinish.ResumeLayout(false);
            this.tabPageFinish.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox patchDescription;
        private System.Windows.Forms.CheckedListBox patchListBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPatches;
        private System.Windows.Forms.TabPage tabPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxOriginalPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPageFinish;
        private System.Windows.Forms.Button buttonBrowseOutput;
        private System.Windows.Forms.TextBox textBoxOutputPath;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label labelGameCheckResult;
        private System.Windows.Forms.Button buttonBuild;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
    }
}

