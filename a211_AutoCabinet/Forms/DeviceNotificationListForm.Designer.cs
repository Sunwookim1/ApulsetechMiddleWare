namespace a211_AutoCabinet.Forms
{
    partial class DeviceNotificationListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceNotificationListForm));
            this.DeviceList = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.GsUriTextValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ApiUriTextValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ListviewDeviceNotify = new System.Windows.Forms.ListView();
            this.DeviceList.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeviceList
            // 
            resources.ApplyResources(this.DeviceList, "DeviceList");
            this.DeviceList.Controls.Add(this.button2);
            this.DeviceList.Controls.Add(this.GsUriTextValue);
            this.DeviceList.Controls.Add(this.label2);
            this.DeviceList.Controls.Add(this.ApiUriTextValue);
            this.DeviceList.Controls.Add(this.label1);
            this.DeviceList.Controls.Add(this.button1);
            this.DeviceList.Controls.Add(this.ListviewDeviceNotify);
            this.DeviceList.Name = "DeviceList";
            this.DeviceList.TabStop = false;
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // GsUriTextValue
            // 
            this.GsUriTextValue.BackColor = System.Drawing.SystemColors.Window;
            this.GsUriTextValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.GsUriTextValue, "GsUriTextValue");
            this.GsUriTextValue.Name = "GsUriTextValue";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // ApiUriTextValue
            // 
            this.ApiUriTextValue.BackColor = System.Drawing.SystemColors.Window;
            this.ApiUriTextValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.ApiUriTextValue, "ApiUriTextValue");
            this.ApiUriTextValue.Name = "ApiUriTextValue";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ListviewDeviceNotify
            // 
            this.ListviewDeviceNotify.GridLines = true;
            this.ListviewDeviceNotify.HideSelection = false;
            resources.ApplyResources(this.ListviewDeviceNotify, "ListviewDeviceNotify");
            this.ListviewDeviceNotify.Name = "ListviewDeviceNotify";
            this.ListviewDeviceNotify.UseCompatibleStateImageBehavior = false;
            this.ListviewDeviceNotify.View = System.Windows.Forms.View.Details;
            this.ListviewDeviceNotify.SelectedIndexChanged += new System.EventHandler(this.ListviewDeviceNotify_SelectedIndexChanged);
            // 
            // DeviceNotificationListForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DeviceList);
            this.Name = "DeviceNotificationListForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DeviceNotificationListForm_FormClosed);
            this.Load += new System.EventHandler(this.DeviceNotificationListForm_Load);
            this.DeviceList.ResumeLayout(false);
            this.DeviceList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox DeviceList;
        private System.Windows.Forms.ListView ListviewDeviceNotify;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ApiUriTextValue;
        private System.Windows.Forms.TextBox GsUriTextValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
    }
}