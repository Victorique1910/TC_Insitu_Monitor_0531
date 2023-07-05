
namespace USBscan
{
    partial class TC_PreCheck_COMport
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Label_ShowInformation = new System.Windows.Forms.Label();
            this.Button_End = new System.Windows.Forms.Button();
            this.Label_ShowStatus = new System.Windows.Forms.Label();
            this.Button_Jump = new System.Windows.Forms.Button();
            this.TextBox_SN = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label_ShowInformation
            // 
            this.Label_ShowInformation.AutoSize = true;
            this.Label_ShowInformation.Font = new System.Drawing.Font("新細明體", 12F);
            this.Label_ShowInformation.Location = new System.Drawing.Point(32, 18);
            this.Label_ShowInformation.Name = "Label_ShowInformation";
            this.Label_ShowInformation.Size = new System.Drawing.Size(64, 24);
            this.Label_ShowInformation.TabIndex = 0;
            this.Label_ShowInformation.Text = "label1";
            // 
            // Button_End
            // 
            this.Button_End.Location = new System.Drawing.Point(221, 111);
            this.Button_End.Name = "Button_End";
            this.Button_End.Size = new System.Drawing.Size(105, 49);
            this.Button_End.TabIndex = 1;
            this.Button_End.Text = "進入TC";
            this.Button_End.UseVisualStyleBackColor = true;
            this.Button_End.Click += new System.EventHandler(this.Button_End_Click);
            // 
            // Label_ShowStatus
            // 
            this.Label_ShowStatus.AutoSize = true;
            this.Label_ShowStatus.Font = new System.Drawing.Font("新細明體", 12F);
            this.Label_ShowStatus.Location = new System.Drawing.Point(32, 71);
            this.Label_ShowStatus.Name = "Label_ShowStatus";
            this.Label_ShowStatus.Size = new System.Drawing.Size(64, 24);
            this.Label_ShowStatus.TabIndex = 3;
            this.Label_ShowStatus.Text = "label1";
            // 
            // Button_Jump
            // 
            this.Button_Jump.Location = new System.Drawing.Point(36, 111);
            this.Button_Jump.Name = "Button_Jump";
            this.Button_Jump.Size = new System.Drawing.Size(105, 49);
            this.Button_Jump.TabIndex = 4;
            this.Button_Jump.Text = "跳過";
            this.Button_Jump.UseVisualStyleBackColor = true;
            this.Button_Jump.Click += new System.EventHandler(this.Button_Jump_Click);
            // 
            // TextBox_SN
            // 
            this.TextBox_SN.Location = new System.Drawing.Point(171, 66);
            this.TextBox_SN.Name = "TextBox_SN";
            this.TextBox_SN.Size = new System.Drawing.Size(195, 29);
            this.TextBox_SN.TabIndex = 5;
            this.TextBox_SN.TextChanged += new System.EventHandler(this.TextBox_SN_TextChanged);
            // 
            // TC_PreCheck_COMport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 185);
            this.Controls.Add(this.TextBox_SN);
            this.Controls.Add(this.Button_Jump);
            this.Controls.Add(this.Label_ShowStatus);
            this.Controls.Add(this.Button_End);
            this.Controls.Add(this.Label_ShowInformation);
            this.Name = "TC_PreCheck_COMport";
            this.Text = "TC_PreCheck_COMport";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_ShowInformation;
        private System.Windows.Forms.Button Button_End;
        private System.Windows.Forms.Label Label_ShowStatus;
        private System.Windows.Forms.Button Button_Jump;
        private System.Windows.Forms.TextBox TextBox_SN;
    }
}

