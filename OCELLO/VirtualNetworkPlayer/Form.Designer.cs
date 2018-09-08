namespace VirtualNetworkPlayer
{
    partial class Form
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtRow = new System.Windows.Forms.TextBox();
            this.txtCol = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUDPSocketCreate = new System.Windows.Forms.Button();
            this.txtSendIP = new System.Windows.Forms.TextBox();
            this.txtSendPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtRandNum = new System.Windows.Forms.TextBox();
            this.btnOrder = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(172, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(100, 19);
            this.txtIp.TabIndex = 0;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(172, 37);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 19);
            this.txtPort.TabIndex = 0;
            // 
            // txtRow
            // 
            this.txtRow.Location = new System.Drawing.Point(172, 344);
            this.txtRow.Name = "txtRow";
            this.txtRow.Size = new System.Drawing.Size(100, 19);
            this.txtRow.TabIndex = 0;
            // 
            // txtCol
            // 
            this.txtCol.Location = new System.Drawing.Point(172, 369);
            this.txtCol.Name = "txtCol";
            this.txtCol.Size = new System.Drawing.Size(100, 19);
            this.txtCol.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "IPAddress";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 347);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "RowIndex";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 369);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "ColIndex";
            // 
            // btnUDPSocketCreate
            // 
            this.btnUDPSocketCreate.Location = new System.Drawing.Point(172, 118);
            this.btnUDPSocketCreate.Name = "btnUDPSocketCreate";
            this.btnUDPSocketCreate.Size = new System.Drawing.Size(100, 49);
            this.btnUDPSocketCreate.TabIndex = 2;
            this.btnUDPSocketCreate.Text = "ソケット作成";
            this.btnUDPSocketCreate.UseVisualStyleBackColor = true;
            this.btnUDPSocketCreate.Click += new System.EventHandler(this.btnUDPSocketCreate_Click);
            // 
            // txtSendIP
            // 
            this.txtSendIP.Location = new System.Drawing.Point(172, 68);
            this.txtSendIP.Name = "txtSendIP";
            this.txtSendIP.Size = new System.Drawing.Size(100, 19);
            this.txtSendIP.TabIndex = 0;
            // 
            // txtSendPort
            // 
            this.txtSendPort.Location = new System.Drawing.Point(172, 93);
            this.txtSendPort.Name = "txtSendPort";
            this.txtSendPort.Size = new System.Drawing.Size(100, 19);
            this.txtSendPort.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "SendIPAddress";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 93);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "SendPort";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(172, 394);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(100, 51);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "送信";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtRandNum
            // 
            this.txtRandNum.Location = new System.Drawing.Point(172, 229);
            this.txtRandNum.Name = "txtRandNum";
            this.txtRandNum.Size = new System.Drawing.Size(100, 19);
            this.txtRandNum.TabIndex = 4;
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(172, 279);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(100, 59);
            this.btnOrder.TabIndex = 5;
            this.btnOrder.Text = "順序決め";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(172, 173);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 50);
            this.button1.TabIndex = 6;
            this.button1.Text = "接続";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 452);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.txtRandNum);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnUDPSocketCreate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCol);
            this.Controls.Add(this.txtSendPort);
            this.Controls.Add(this.txtRow);
            this.Controls.Add(this.txtSendIP);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIp);
            this.Name = "Form";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtRow;
        private System.Windows.Forms.TextBox txtCol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUDPSocketCreate;
        private System.Windows.Forms.TextBox txtSendIP;
        private System.Windows.Forms.TextBox txtSendPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtRandNum;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button button1;
    }
}

