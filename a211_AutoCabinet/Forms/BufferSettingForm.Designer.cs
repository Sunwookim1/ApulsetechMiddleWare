namespace a211_AutoCabinet.Forms
{
    partial class BufferSettingForm
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
            this.LVBufferSettings = new System.Windows.Forms.ListView();
            this.Port = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Buffer11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbBulkChange = new System.Windows.Forms.TextBox();
            this.txbBufferSettings = new System.Windows.Forms.TextBox();
            this.txbSelectport = new System.Windows.Forms.TextBox();
            this.lbBulkChange = new System.Windows.Forms.Label();
            this.lbBufferSettings = new System.Windows.Forms.Label();
            this.lbSelectPort = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSetting = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LVBufferSettings
            // 
            this.LVBufferSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LVBufferSettings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Port,
            this.Buffer1,
            this.Buffer2,
            this.Buffer3,
            this.Buffer4,
            this.Buffer5,
            this.Buffer6,
            this.Buffer7,
            this.Buffer8,
            this.Buffer9,
            this.Buffer10,
            this.Buffer11});
            this.LVBufferSettings.FullRowSelect = true;
            this.LVBufferSettings.GridLines = true;
            this.LVBufferSettings.HideSelection = false;
            this.LVBufferSettings.Location = new System.Drawing.Point(12, 12);
            this.LVBufferSettings.Name = "LVBufferSettings";
            this.LVBufferSettings.Size = new System.Drawing.Size(601, 360);
            this.LVBufferSettings.TabIndex = 0;
            this.LVBufferSettings.UseCompatibleStateImageBehavior = false;
            this.LVBufferSettings.View = System.Windows.Forms.View.Details;
            // 
            // Port
            // 
            this.Port.Text = "Port";
            // 
            // Buffer1
            // 
            this.Buffer1.Text = "Buffer1";
            // 
            // Buffer2
            // 
            this.Buffer2.Text = "Buffer2";
            // 
            // Buffer3
            // 
            this.Buffer3.Text = "Buffer3";
            // 
            // Buffer4
            // 
            this.Buffer4.Text = "Buffer4";
            // 
            // Buffer5
            // 
            this.Buffer5.Text = "Buffer5";
            // 
            // Buffer6
            // 
            this.Buffer6.Text = "Buffer6";
            // 
            // Buffer7
            // 
            this.Buffer7.Text = "Buffer7";
            // 
            // Buffer8
            // 
            this.Buffer8.Text = "Buffer8";
            // 
            // Buffer9
            // 
            this.Buffer9.Text = "Buffer9";
            // 
            // Buffer10
            // 
            this.Buffer10.Text = "Buffer10";
            // 
            // Buffer11
            // 
            this.Buffer11.Text = "Buffer11";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txbBulkChange);
            this.groupBox1.Controls.Add(this.txbBufferSettings);
            this.groupBox1.Controls.Add(this.txbSelectport);
            this.groupBox1.Controls.Add(this.lbBulkChange);
            this.groupBox1.Controls.Add(this.lbBufferSettings);
            this.groupBox1.Controls.Add(this.lbSelectPort);
            this.groupBox1.Location = new System.Drawing.Point(619, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(178, 360);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // txbBulkChange
            // 
            this.txbBulkChange.Location = new System.Drawing.Point(10, 167);
            this.txbBulkChange.Name = "txbBulkChange";
            this.txbBulkChange.Size = new System.Drawing.Size(162, 21);
            this.txbBulkChange.TabIndex = 5;
            // 
            // txbBufferSettings
            // 
            this.txbBufferSettings.Location = new System.Drawing.Point(10, 106);
            this.txbBufferSettings.Name = "txbBufferSettings";
            this.txbBufferSettings.Size = new System.Drawing.Size(162, 21);
            this.txbBufferSettings.TabIndex = 4;
            // 
            // txbSelectport
            // 
            this.txbSelectport.Location = new System.Drawing.Point(10, 45);
            this.txbSelectport.Name = "txbSelectport";
            this.txbSelectport.Size = new System.Drawing.Size(162, 21);
            this.txbSelectport.TabIndex = 3;
            // 
            // lbBulkChange
            // 
            this.lbBulkChange.Font = new System.Drawing.Font("굴림", 15F);
            this.lbBulkChange.Location = new System.Drawing.Point(6, 130);
            this.lbBulkChange.Name = "lbBulkChange";
            this.lbBulkChange.Size = new System.Drawing.Size(166, 34);
            this.lbBulkChange.TabIndex = 2;
            this.lbBulkChange.Text = "Bulk Change";
            this.lbBulkChange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbBufferSettings
            // 
            this.lbBufferSettings.Font = new System.Drawing.Font("굴림", 15F);
            this.lbBufferSettings.Location = new System.Drawing.Point(6, 69);
            this.lbBufferSettings.Name = "lbBufferSettings";
            this.lbBufferSettings.Size = new System.Drawing.Size(166, 34);
            this.lbBufferSettings.TabIndex = 1;
            this.lbBufferSettings.Text = "Buffer Settings";
            this.lbBufferSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbSelectPort
            // 
            this.lbSelectPort.Font = new System.Drawing.Font("굴림", 15F);
            this.lbSelectPort.Location = new System.Drawing.Point(6, 17);
            this.lbSelectPort.Name = "lbSelectPort";
            this.lbSelectPort.Size = new System.Drawing.Size(166, 34);
            this.lbSelectPort.TabIndex = 0;
            this.lbSelectPort.Text = "Select Port";
            this.lbSelectPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(629, 378);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(168, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSetting
            // 
            this.btnSetting.Location = new System.Drawing.Point(455, 378);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(168, 23);
            this.btnSetting.TabIndex = 4;
            this.btnSetting.Text = "Setting";
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // BufferSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 406);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LVBufferSettings);
            this.Name = "BufferSettingForm";
            this.Text = "BufferSettingForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView LVBufferSettings;
        private System.Windows.Forms.ColumnHeader Port;
        private System.Windows.Forms.ColumnHeader Buffer1;
        private System.Windows.Forms.ColumnHeader Buffer2;
        private System.Windows.Forms.ColumnHeader Buffer3;
        private System.Windows.Forms.ColumnHeader Buffer4;
        private System.Windows.Forms.ColumnHeader Buffer5;
        private System.Windows.Forms.ColumnHeader Buffer6;
        private System.Windows.Forms.ColumnHeader Buffer7;
        private System.Windows.Forms.ColumnHeader Buffer8;
        private System.Windows.Forms.ColumnHeader Buffer9;
        private System.Windows.Forms.ColumnHeader Buffer10;
        private System.Windows.Forms.ColumnHeader Buffer11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbSelectPort;
        private System.Windows.Forms.Label lbBulkChange;
        private System.Windows.Forms.Label lbBufferSettings;
        private System.Windows.Forms.TextBox txbBulkChange;
        private System.Windows.Forms.TextBox txbBufferSettings;
        private System.Windows.Forms.TextBox txbSelectport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSetting;
    }
}