
namespace TC_Insitu_Monitor
{
    partial class EmailInputBox
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
            this.textBox_EmailInputBox = new System.Windows.Forms.TextBox();
            this.label_EmailInputBox = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_EmailInputBox
            // 
            this.textBox_EmailInputBox.Location = new System.Drawing.Point(33, 62);
            this.textBox_EmailInputBox.Name = "textBox_EmailInputBox";
            this.textBox_EmailInputBox.Size = new System.Drawing.Size(455, 29);
            this.textBox_EmailInputBox.TabIndex = 0;
            // 
            // label_EmailInputBox
            // 
            this.label_EmailInputBox.AutoSize = true;
            this.label_EmailInputBox.Location = new System.Drawing.Point(30, 31);
            this.label_EmailInputBox.Name = "label_EmailInputBox";
            this.label_EmailInputBox.Size = new System.Drawing.Size(103, 18);
            this.label_EmailInputBox.TabIndex = 1;
            this.label_EmailInputBox.Text = "請輸入Email";
            // 
            // Button_OK
            // 
            this.Button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Button_OK.Location = new System.Drawing.Point(119, 97);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 41);
            this.Button_OK.TabIndex = 2;
            this.Button_OK.Text = "確認";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancle
            // 
            this.Button_Cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Button_Cancle.Location = new System.Drawing.Point(282, 97);
            this.Button_Cancle.Name = "Button_Cancle";
            this.Button_Cancle.Size = new System.Drawing.Size(75, 41);
            this.Button_Cancle.TabIndex = 3;
            this.Button_Cancle.Text = "取消";
            this.Button_Cancle.UseVisualStyleBackColor = true;
            // 
            // EmailInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 150);
            this.Controls.Add(this.Button_Cancle);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.label_EmailInputBox);
            this.Controls.Add(this.textBox_EmailInputBox);
            this.Name = "EmailInputBox";
            this.Text = "EmailInputBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_EmailInputBox;
        private System.Windows.Forms.Label label_EmailInputBox;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancle;
    }
}