
namespace a211_AutoCabinet.Controls
{
    partial class TagDataView
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelTagCount = new System.Windows.Forms.Label();
            this.labelStateOUT = new System.Windows.Forms.Label();
            this.labelAntNum = new System.Windows.Forms.Label();
            this.labelStateIN = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTagCount
            // 
            this.labelTagCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTagCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTagCount.Font = new System.Drawing.Font("맑은 고딕", 32F, System.Drawing.FontStyle.Bold);
            this.labelTagCount.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelTagCount.Location = new System.Drawing.Point(0, 0);
            this.labelTagCount.Name = "labelTagCount";
            this.labelTagCount.Size = new System.Drawing.Size(160, 140);
            this.labelTagCount.TabIndex = 2;
            this.labelTagCount.Text = "0";
            this.labelTagCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelStateOUT
            // 
            this.labelStateOUT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStateOUT.BackColor = System.Drawing.Color.White;
            this.labelStateOUT.Location = new System.Drawing.Point(143, 121);
            this.labelStateOUT.Name = "labelStateOUT";
            this.labelStateOUT.Size = new System.Drawing.Size(14, 14);
            this.labelStateOUT.TabIndex = 4;
            // 
            // labelAntNum
            // 
            this.labelAntNum.AutoSize = true;
            this.labelAntNum.Font = new System.Drawing.Font("맑은 고딕", 16F, System.Drawing.FontStyle.Bold);
            this.labelAntNum.ForeColor = System.Drawing.Color.MediumBlue;
            this.labelAntNum.Location = new System.Drawing.Point(3, 3);
            this.labelAntNum.Name = "labelAntNum";
            this.labelAntNum.Size = new System.Drawing.Size(65, 37);
            this.labelAntNum.TabIndex = 3;
            this.labelAntNum.Text = "128";
            // 
            // labelStateIN
            // 
            this.labelStateIN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStateIN.BackColor = System.Drawing.Color.White;
            this.labelStateIN.Location = new System.Drawing.Point(3, 121);
            this.labelStateIN.Name = "labelStateIN";
            this.labelStateIN.Size = new System.Drawing.Size(14, 14);
            this.labelStateIN.TabIndex = 4;
            // 
            // TagDataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.labelStateIN);
            this.Controls.Add(this.labelStateOUT);
            this.Controls.Add(this.labelAntNum);
            this.Controls.Add(this.labelTagCount);
            this.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TagDataView";
            this.Size = new System.Drawing.Size(160, 140);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelTagCount;
        public System.Windows.Forms.Label labelStateOUT;
        public System.Windows.Forms.Label labelAntNum;
        public System.Windows.Forms.Label labelStateIN;
    }
}
