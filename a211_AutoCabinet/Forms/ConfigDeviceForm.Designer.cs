
namespace a211_AutoCabinet.Forms
{
    partial class ConfigDeviceForm
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
            this.groupBoxAnt = new System.Windows.Forms.GroupBox();
            this.lstAntennas = new System.Windows.Forms.ListView();
            this.colAntEnable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAntSeq = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colAntPowerGain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblClickReaderName = new System.Windows.Forms.Label();
            this.buttonAntEdit = new System.Windows.Forms.Button();
            this.buttonAntDelete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstReaders = new System.Windows.Forms.ListView();
            this.colReaderNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReaderIpAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReaderPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReaderBaudrate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReaderDevType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReaderAntCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReaderDwellTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReaderTxOnTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colReaderTxOffTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonDevAdd = new System.Windows.Forms.Button();
            this.buttonDevDelete = new System.Windows.Forms.Button();
            this.buttonDevEdit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBoxAnt.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxAnt
            // 
            this.groupBoxAnt.Controls.Add(this.lstAntennas);
            this.groupBoxAnt.Controls.Add(this.lblClickReaderName);
            this.groupBoxAnt.Controls.Add(this.buttonAntEdit);
            this.groupBoxAnt.Controls.Add(this.buttonAntDelete);
            this.groupBoxAnt.Location = new System.Drawing.Point(12, 221);
            this.groupBoxAnt.Name = "groupBoxAnt";
            this.groupBoxAnt.Size = new System.Drawing.Size(960, 278);
            this.groupBoxAnt.TabIndex = 30;
            this.groupBoxAnt.TabStop = false;
            this.groupBoxAnt.Text = "Ant";
            // 
            // lstAntennas
            // 
            this.lstAntennas.CheckBoxes = true;
            this.lstAntennas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAntEnable,
            this.colAntSeq,
            this.colAntPowerGain});
            this.lstAntennas.FullRowSelect = true;
            this.lstAntennas.GridLines = true;
            this.lstAntennas.HideSelection = false;
            this.lstAntennas.Location = new System.Drawing.Point(6, 43);
            this.lstAntennas.Name = "lstAntennas";
            this.lstAntennas.OwnerDraw = true;
            this.lstAntennas.Size = new System.Drawing.Size(948, 198);
            this.lstAntennas.TabIndex = 0;
            this.lstAntennas.UseCompatibleStateImageBehavior = false;
            this.lstAntennas.View = System.Windows.Forms.View.Details;
            this.lstAntennas.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstAntennas_ColumnClick);
            this.lstAntennas.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lstAntennas_DrawColumnHeader);
            this.lstAntennas.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lstAntennas_DrawItem);
            this.lstAntennas.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lstAntennas_DrawSubItem);
            // 
            // colAntEnable
            // 
            this.colAntEnable.Text = "";
            this.colAntEnable.Width = 23;
            // 
            // colAntSeq
            // 
            this.colAntSeq.Text = "Antenna Sequence";
            this.colAntSeq.Width = 160;
            // 
            // colAntPowerGain
            // 
            this.colAntPowerGain.Text = "Power Gain";
            this.colAntPowerGain.Width = 100;
            // 
            // lblClickReaderName
            // 
            this.lblClickReaderName.AutoSize = true;
            this.lblClickReaderName.Location = new System.Drawing.Point(6, 21);
            this.lblClickReaderName.Name = "lblClickReaderName";
            this.lblClickReaderName.Size = new System.Drawing.Size(107, 19);
            this.lblClickReaderName.TabIndex = 4;
            this.lblClickReaderName.Text = "Reader Name : ";
            // 
            // buttonAntEdit
            // 
            this.buttonAntEdit.Location = new System.Drawing.Point(798, 247);
            this.buttonAntEdit.Name = "buttonAntEdit";
            this.buttonAntEdit.Size = new System.Drawing.Size(75, 25);
            this.buttonAntEdit.TabIndex = 2;
            this.buttonAntEdit.Text = "Edit";
            this.buttonAntEdit.UseVisualStyleBackColor = true;
            // 
            // buttonAntDelete
            // 
            this.buttonAntDelete.Location = new System.Drawing.Point(879, 247);
            this.buttonAntDelete.Name = "buttonAntDelete";
            this.buttonAntDelete.Size = new System.Drawing.Size(75, 25);
            this.buttonAntDelete.TabIndex = 1;
            this.buttonAntDelete.Text = "Delete";
            this.buttonAntDelete.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstReaders);
            this.groupBox1.Controls.Add(this.buttonDevAdd);
            this.groupBox1.Controls.Add(this.buttonDevDelete);
            this.groupBox1.Controls.Add(this.buttonDevEdit);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(960, 203);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reader";
            // 
            // lstReaders
            // 
            this.lstReaders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colReaderNo,
            this.colReaderName,
            this.colReaderIpAddress,
            this.colReaderPort,
            this.colReaderBaudrate,
            this.colReaderDevType,
            this.colReaderAntCount,
            this.colReaderDwellTime,
            this.colReaderTxOnTime,
            this.colReaderTxOffTime});
            this.lstReaders.FullRowSelect = true;
            this.lstReaders.GridLines = true;
            this.lstReaders.HideSelection = false;
            this.lstReaders.Location = new System.Drawing.Point(6, 22);
            this.lstReaders.Name = "lstReaders";
            this.lstReaders.Size = new System.Drawing.Size(948, 144);
            this.lstReaders.TabIndex = 0;
            this.lstReaders.UseCompatibleStateImageBehavior = false;
            this.lstReaders.View = System.Windows.Forms.View.Details;
            this.lstReaders.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstReaders_MouseClick);
            // 
            // colReaderNo
            // 
            this.colReaderNo.Text = "No";
            // 
            // colReaderName
            // 
            this.colReaderName.Text = "Reader Name";
            this.colReaderName.Width = 130;
            // 
            // colReaderIpAddress
            // 
            this.colReaderIpAddress.Text = "Ip Address";
            this.colReaderIpAddress.Width = 110;
            // 
            // colReaderPort
            // 
            this.colReaderPort.Text = "Port";
            // 
            // colReaderBaudrate
            // 
            this.colReaderBaudrate.Text = "Baudrate";
            this.colReaderBaudrate.Width = 80;
            // 
            // colReaderDevType
            // 
            this.colReaderDevType.Text = "Dev Type";
            this.colReaderDevType.Width = 80;
            // 
            // colReaderAntCount
            // 
            this.colReaderAntCount.Text = "Ant Count";
            this.colReaderAntCount.Width = 100;
            // 
            // colReaderDwellTime
            // 
            this.colReaderDwellTime.Text = "Dwell Time";
            this.colReaderDwellTime.Width = 100;
            // 
            // colReaderTxOnTime
            // 
            this.colReaderTxOnTime.Text = "Tx On Time";
            this.colReaderTxOnTime.Width = 100;
            // 
            // colReaderTxOffTime
            // 
            this.colReaderTxOffTime.Text = "Tx Off Time";
            this.colReaderTxOffTime.Width = 100;
            // 
            // buttonDevAdd
            // 
            this.buttonDevAdd.Location = new System.Drawing.Point(717, 172);
            this.buttonDevAdd.Name = "buttonDevAdd";
            this.buttonDevAdd.Size = new System.Drawing.Size(75, 25);
            this.buttonDevAdd.TabIndex = 6;
            this.buttonDevAdd.Text = "Add";
            this.buttonDevAdd.UseVisualStyleBackColor = true;
            // 
            // buttonDevDelete
            // 
            this.buttonDevDelete.Location = new System.Drawing.Point(879, 172);
            this.buttonDevDelete.Name = "buttonDevDelete";
            this.buttonDevDelete.Size = new System.Drawing.Size(75, 25);
            this.buttonDevDelete.TabIndex = 4;
            this.buttonDevDelete.Text = "Delete";
            this.buttonDevDelete.UseVisualStyleBackColor = true;
            // 
            // buttonDevEdit
            // 
            this.buttonDevEdit.Location = new System.Drawing.Point(798, 172);
            this.buttonDevEdit.Name = "buttonDevEdit";
            this.buttonDevEdit.Size = new System.Drawing.Size(75, 25);
            this.buttonDevEdit.TabIndex = 5;
            this.buttonDevEdit.Text = "Edit";
            this.buttonDevEdit.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(897, 505);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 25);
            this.buttonCancel.TabIndex = 31;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(816, 505);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 25);
            this.buttonSave.TabIndex = 31;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // ConfigDeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxAnt);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ConfigDeviceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfigDeviceForm";
            this.Load += new System.EventHandler(this.ConfigDeviceForm_Load);
            this.groupBoxAnt.ResumeLayout(false);
            this.groupBoxAnt.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxAnt;
        private System.Windows.Forms.ListView lstAntennas;
        private System.Windows.Forms.ColumnHeader colAntSeq;
        private System.Windows.Forms.ColumnHeader colAntPowerGain;
        private System.Windows.Forms.Label lblClickReaderName;
        private System.Windows.Forms.Button buttonAntEdit;
        private System.Windows.Forms.Button buttonAntDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstReaders;
        private System.Windows.Forms.ColumnHeader colReaderNo;
        private System.Windows.Forms.ColumnHeader colReaderName;
        private System.Windows.Forms.ColumnHeader colReaderPort;
        private System.Windows.Forms.ColumnHeader colReaderBaudrate;
        private System.Windows.Forms.ColumnHeader colReaderDevType;
        private System.Windows.Forms.ColumnHeader colReaderAntCount;
        private System.Windows.Forms.ColumnHeader colReaderDwellTime;
        private System.Windows.Forms.ColumnHeader colReaderTxOnTime;
        private System.Windows.Forms.ColumnHeader colReaderTxOffTime;
        private System.Windows.Forms.Button buttonDevAdd;
        private System.Windows.Forms.Button buttonDevDelete;
        private System.Windows.Forms.Button buttonDevEdit;
        private System.Windows.Forms.ColumnHeader colReaderIpAddress;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ColumnHeader colAntEnable;
    }
}