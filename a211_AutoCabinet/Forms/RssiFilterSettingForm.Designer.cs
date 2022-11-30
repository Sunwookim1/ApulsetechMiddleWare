namespace a211_AutoCabinet.Forms
{
    partial class RssiFilterSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RssiFilterSettingForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cbRssiFilter = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbFilterValue = new System.Windows.Forms.TextBox();
            this.btnFilterValue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cbRssiFilter
            // 
            resources.ApplyResources(this.cbRssiFilter, "cbRssiFilter");
            this.cbRssiFilter.Name = "cbRssiFilter";
            this.cbRssiFilter.UseVisualStyleBackColor = true;
            this.cbRssiFilter.CheckedChanged += new System.EventHandler(this.cbRssiFilter_CheckedChanged);
            this.cbRssiFilter.CheckStateChanged += new System.EventHandler(this.cbRssiFilter_CheckStateChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txbFilterValue
            // 
            resources.ApplyResources(this.txbFilterValue, "txbFilterValue");
            this.txbFilterValue.Name = "txbFilterValue";
            // 
            // btnFilterValue
            // 
            resources.ApplyResources(this.btnFilterValue, "btnFilterValue");
            this.btnFilterValue.Name = "btnFilterValue";
            this.btnFilterValue.UseVisualStyleBackColor = true;
            this.btnFilterValue.Click += new System.EventHandler(this.btnFilterValue_Click);
            // 
            // RssiFilterSettingForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnFilterValue);
            this.Controls.Add(this.txbFilterValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbRssiFilter);
            this.Controls.Add(this.label1);
            this.Name = "RssiFilterSettingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbRssiFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbFilterValue;
        private System.Windows.Forms.Button btnFilterValue;
    }
}