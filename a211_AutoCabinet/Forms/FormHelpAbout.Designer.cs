namespace a211_AutoCabinet.Forms
{
    partial class FormHelpAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormHelpAbout));
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxHelpAboutCopyright = new System.Windows.Forms.GroupBox();
            this.labelCopyrightContent1 = new System.Windows.Forms.Label();
            this.labelCopyrightContent2 = new System.Windows.Forms.Label();
            this.labelAppTitle = new System.Windows.Forms.Label();
            this.groupBoxHelpAboutCopyright.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOk.Location = new System.Drawing.Point(211, 312);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(135, 56);
            this.buttonOk.TabIndex = 75;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // groupBoxHelpAboutCopyright
            // 
            this.groupBoxHelpAboutCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHelpAboutCopyright.Controls.Add(this.labelCopyrightContent1);
            this.groupBoxHelpAboutCopyright.Controls.Add(this.labelCopyrightContent2);
            this.groupBoxHelpAboutCopyright.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxHelpAboutCopyright.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.groupBoxHelpAboutCopyright.Location = new System.Drawing.Point(40, 91);
            this.groupBoxHelpAboutCopyright.Margin = new System.Windows.Forms.Padding(4);
            this.groupBoxHelpAboutCopyright.Name = "groupBoxHelpAboutCopyright";
            this.groupBoxHelpAboutCopyright.Padding = new System.Windows.Forms.Padding(4);
            this.groupBoxHelpAboutCopyright.Size = new System.Drawing.Size(480, 199);
            this.groupBoxHelpAboutCopyright.TabIndex = 74;
            this.groupBoxHelpAboutCopyright.TabStop = false;
            this.groupBoxHelpAboutCopyright.Text = "COPYRIGHT";
            // 
            // labelCopyrightContent1
            // 
            this.labelCopyrightContent1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCopyrightContent1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyrightContent1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.labelCopyrightContent1.Location = new System.Drawing.Point(9, 23);
            this.labelCopyrightContent1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCopyrightContent1.Name = "labelCopyrightContent1";
            this.labelCopyrightContent1.Size = new System.Drawing.Size(400, 23);
            this.labelCopyrightContent1.TabIndex = 7;
            this.labelCopyrightContent1.Text = "Copyright 2022-2024. Aplusetech Co.,Ltd. All rights reserved.";
            // 
            // labelCopyrightContent2
            // 
            this.labelCopyrightContent2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCopyrightContent2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCopyrightContent2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.labelCopyrightContent2.Location = new System.Drawing.Point(8, 58);
            this.labelCopyrightContent2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCopyrightContent2.Name = "labelCopyrightContent2";
            this.labelCopyrightContent2.Size = new System.Drawing.Size(475, 97);
            this.labelCopyrightContent2.TabIndex = 6;
            this.labelCopyrightContent2.Text = resources.GetString("labelCopyrightContent2.Text");
            // 
            // labelAppTitle
            // 
            this.labelAppTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelAppTitle.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAppTitle.ForeColor = System.Drawing.SystemColors.InfoText;
            this.labelAppTitle.Location = new System.Drawing.Point(103, 36);
            this.labelAppTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelAppTitle.Name = "labelAppTitle";
            this.labelAppTitle.Size = new System.Drawing.Size(345, 24);
            this.labelAppTitle.TabIndex = 76;
            this.labelAppTitle.Text = "Apulsetech MiddleWare v1.0.0.0";
            // 
            // FormHelpAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 382);
            this.Controls.Add(this.labelAppTitle);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxHelpAboutCopyright);
            this.Name = "FormHelpAbout";
            this.Text = "FormHelpAbout";
            this.groupBoxHelpAboutCopyright.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBoxHelpAboutCopyright;
        private System.Windows.Forms.Label labelCopyrightContent1;
        private System.Windows.Forms.Label labelCopyrightContent2;
        private System.Windows.Forms.Label labelAppTitle;
    }
}