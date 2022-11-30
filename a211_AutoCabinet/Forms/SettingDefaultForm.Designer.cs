
namespace a211_AutoCabinet.Forms
{
    partial class SettingDefaultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDefaultForm));
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAntCount = new System.Windows.Forms.TextBox();
            this.btnDwellTime = new System.Windows.Forms.Button();
            this.btnSelectMask = new System.Windows.Forms.Button();
            this.groupBoxSetting = new System.Windows.Forms.GroupBox();
            this.btnRssiFilterSetting = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxSettingPanelSize = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txbTxOffTime = new System.Windows.Forms.TextBox();
            this.txbTxOnTime = new System.Windows.Forms.TextBox();
            this.textBoxPanelColumn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPanelRow = new System.Windows.Forms.TextBox();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxSetting.SuspendLayout();
            this.groupBoxSettingPanelSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxAntCount
            // 
            resources.ApplyResources(this.textBoxAntCount, "textBoxAntCount");
            this.textBoxAntCount.Name = "textBoxAntCount";
            // 
            // btnDwellTime
            // 
            resources.ApplyResources(this.btnDwellTime, "btnDwellTime");
            this.btnDwellTime.Name = "btnDwellTime";
            this.btnDwellTime.UseVisualStyleBackColor = true;
            this.btnDwellTime.Click += new System.EventHandler(this.btnDwellTime_Click);
            // 
            // btnSelectMask
            // 
            resources.ApplyResources(this.btnSelectMask, "btnSelectMask");
            this.btnSelectMask.Name = "btnSelectMask";
            this.btnSelectMask.UseVisualStyleBackColor = true;
            this.btnSelectMask.Click += new System.EventHandler(this.btnSelectMask_Click);
            // 
            // groupBoxSetting
            // 
            resources.ApplyResources(this.groupBoxSetting, "groupBoxSetting");
            this.groupBoxSetting.Controls.Add(this.btnRssiFilterSetting);
            this.groupBoxSetting.Controls.Add(this.btnDwellTime);
            this.groupBoxSetting.Controls.Add(this.buttonCancel);
            this.groupBoxSetting.Controls.Add(this.btnSelectMask);
            this.groupBoxSetting.Controls.Add(this.buttonOk);
            this.groupBoxSetting.Controls.Add(this.groupBoxSettingPanelSize);
            this.groupBoxSetting.Name = "groupBoxSetting";
            this.groupBoxSetting.TabStop = false;
            // 
            // btnRssiFilterSetting
            // 
            resources.ApplyResources(this.btnRssiFilterSetting, "btnRssiFilterSetting");
            this.btnRssiFilterSetting.Name = "btnRssiFilterSetting";
            this.btnRssiFilterSetting.UseVisualStyleBackColor = true;
            this.btnRssiFilterSetting.Click += new System.EventHandler(this.btnRssiFilterSetting_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            resources.ApplyResources(this.buttonOk, "buttonOk");
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBoxSettingPanelSize
            // 
            resources.ApplyResources(this.groupBoxSettingPanelSize, "groupBoxSettingPanelSize");
            this.groupBoxSettingPanelSize.Controls.Add(this.label5);
            this.groupBoxSettingPanelSize.Controls.Add(this.label4);
            this.groupBoxSettingPanelSize.Controls.Add(this.txbTxOffTime);
            this.groupBoxSettingPanelSize.Controls.Add(this.txbTxOnTime);
            this.groupBoxSettingPanelSize.Controls.Add(this.textBoxAntCount);
            this.groupBoxSettingPanelSize.Controls.Add(this.label3);
            this.groupBoxSettingPanelSize.Controls.Add(this.textBoxPanelColumn);
            this.groupBoxSettingPanelSize.Controls.Add(this.label1);
            this.groupBoxSettingPanelSize.Controls.Add(this.label2);
            this.groupBoxSettingPanelSize.Controls.Add(this.textBoxPanelRow);
            this.groupBoxSettingPanelSize.Name = "groupBoxSettingPanelSize";
            this.groupBoxSettingPanelSize.TabStop = false;
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txbTxOffTime
            // 
            resources.ApplyResources(this.txbTxOffTime, "txbTxOffTime");
            this.txbTxOffTime.Name = "txbTxOffTime";
            // 
            // txbTxOnTime
            // 
            resources.ApplyResources(this.txbTxOnTime, "txbTxOnTime");
            this.txbTxOnTime.Name = "txbTxOnTime";
            // 
            // textBoxPanelColumn
            // 
            resources.ApplyResources(this.textBoxPanelColumn, "textBoxPanelColumn");
            this.textBoxPanelColumn.Name = "textBoxPanelColumn";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // textBoxPanelRow
            // 
            resources.ApplyResources(this.textBoxPanelRow, "textBoxPanelRow");
            this.textBoxPanelRow.Name = "textBoxPanelRow";
            // 
            // columnHeader3
            // 
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // SettingDefaultForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBoxSetting);
            this.Name = "SettingDefaultForm";
            this.Load += new System.EventHandler(this.SettingDefaultForm_Load);
            this.groupBoxSetting.ResumeLayout(false);
            this.groupBoxSettingPanelSize.ResumeLayout(false);
            this.groupBoxSettingPanelSize.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAntCount;
        private System.Windows.Forms.GroupBox groupBoxSetting;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.GroupBox groupBoxSettingPanelSize;
        private System.Windows.Forms.TextBox textBoxPanelColumn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPanelRow;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnDwellTime;
        private System.Windows.Forms.Button btnSelectMask;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbTxOffTime;
        private System.Windows.Forms.TextBox txbTxOnTime;
        private System.Windows.Forms.Button btnRssiFilterSetting;
    }
}