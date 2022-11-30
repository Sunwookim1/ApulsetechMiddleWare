namespace a211_AutoCabinet.Forms
{
    partial class FormSessionSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSessionSetting));
            this.gbInventorySettings = new System.Windows.Forms.GroupBox();
            this.cbxToggleMode = new System.Windows.Forms.CheckBox();
            this.cbTarget = new System.Windows.Forms.ComboBox();
            this.cbSession = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbInventorySettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbInventorySettings
            // 
            resources.ApplyResources(this.gbInventorySettings, "gbInventorySettings");
            this.gbInventorySettings.Controls.Add(this.cbxToggleMode);
            this.gbInventorySettings.Controls.Add(this.cbTarget);
            this.gbInventorySettings.Controls.Add(this.cbSession);
            this.gbInventorySettings.Controls.Add(this.label3);
            this.gbInventorySettings.Controls.Add(this.label2);
            this.gbInventorySettings.Controls.Add(this.label1);
            this.gbInventorySettings.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.gbInventorySettings.Name = "gbInventorySettings";
            this.gbInventorySettings.TabStop = false;
            this.gbInventorySettings.Enter += new System.EventHandler(this.gbInventorySettings_Enter);
            // 
            // cbxToggleMode
            // 
            resources.ApplyResources(this.cbxToggleMode, "cbxToggleMode");
            this.cbxToggleMode.Name = "cbxToggleMode";
            this.cbxToggleMode.UseVisualStyleBackColor = true;
            this.cbxToggleMode.CheckedChanged += new System.EventHandler(this.cbxToggleMode_CheckedChanged);
            // 
            // cbTarget
            // 
            this.cbTarget.FormattingEnabled = true;
            resources.ApplyResources(this.cbTarget, "cbTarget");
            this.cbTarget.Name = "cbTarget";
            // 
            // cbSession
            // 
            this.cbSession.FormattingEnabled = true;
            resources.ApplyResources(this.cbSession, "cbSession");
            this.cbSession.Name = "cbSession";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Name = "label1";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FormSessionSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbInventorySettings);
            this.Name = "FormSessionSetting";
            this.Load += new System.EventHandler(this.FormSelectMaskSetting_Load);
            this.gbInventorySettings.ResumeLayout(false);
            this.gbInventorySettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbInventorySettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbTarget;
        private System.Windows.Forms.ComboBox cbSession;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbxToggleMode;
    }
}