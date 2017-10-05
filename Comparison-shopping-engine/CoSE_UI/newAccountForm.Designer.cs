namespace CoSE_UI
{
    partial class newAccountForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbox_NewEmail = new System.Windows.Forms.TextBox();
            this.tbox_NewSurname = new System.Windows.Forms.TextBox();
            this.tbox_NewName = new System.Windows.Forms.TextBox();
            this.tbox_NewNickname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbox_NewPhoneNumber = new System.Windows.Forms.TextBox();
            this.dtp_NewBirth = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_DoneRegistration = new System.Windows.Forms.Button();
            this.btn_CancelRegistration = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbox_NewEmail);
            this.groupBox1.Controls.Add(this.tbox_NewSurname);
            this.groupBox1.Controls.Add(this.tbox_NewName);
            this.groupBox1.Controls.Add(this.tbox_NewNickname);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 138);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enter information";
            // 
            // tbox_NewEmail
            // 
            this.tbox_NewEmail.Location = new System.Drawing.Point(77, 102);
            this.tbox_NewEmail.Name = "tbox_NewEmail";
            this.tbox_NewEmail.Size = new System.Drawing.Size(167, 20);
            this.tbox_NewEmail.TabIndex = 2;
            // 
            // tbox_NewSurname
            // 
            this.tbox_NewSurname.Location = new System.Drawing.Point(77, 76);
            this.tbox_NewSurname.Name = "tbox_NewSurname";
            this.tbox_NewSurname.Size = new System.Drawing.Size(167, 20);
            this.tbox_NewSurname.TabIndex = 2;
            // 
            // tbox_NewName
            // 
            this.tbox_NewName.Location = new System.Drawing.Point(77, 50);
            this.tbox_NewName.Name = "tbox_NewName";
            this.tbox_NewName.Size = new System.Drawing.Size(167, 20);
            this.tbox_NewName.TabIndex = 2;
            // 
            // tbox_NewNickname
            // 
            this.tbox_NewNickname.Location = new System.Drawing.Point(77, 24);
            this.tbox_NewNickname.Name = "tbox_NewNickname";
            this.tbox_NewNickname.Size = new System.Drawing.Size(167, 20);
            this.tbox_NewNickname.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "E-mail:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nickname:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Surname:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Name:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbox_NewPhoneNumber);
            this.groupBox2.Controls.Add(this.dtp_NewBirth);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 86);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enter additional information (optional)";
            // 
            // tbox_NewPhoneNumber
            // 
            this.tbox_NewPhoneNumber.Location = new System.Drawing.Point(98, 23);
            this.tbox_NewPhoneNumber.Name = "tbox_NewPhoneNumber";
            this.tbox_NewPhoneNumber.Size = new System.Drawing.Size(146, 20);
            this.tbox_NewPhoneNumber.TabIndex = 4;
            // 
            // dtp_NewBirth
            // 
            this.dtp_NewBirth.Checked = false;
            this.dtp_NewBirth.Location = new System.Drawing.Point(98, 49);
            this.dtp_NewBirth.MaxDate = new System.DateTime(2017, 12, 31, 0, 0, 0, 0);
            this.dtp_NewBirth.MinDate = new System.DateTime(1901, 1, 1, 0, 0, 0, 0);
            this.dtp_NewBirth.Name = "dtp_NewBirth";
            this.dtp_NewBirth.Size = new System.Drawing.Size(146, 20);
            this.dtp_NewBirth.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(68, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Date of birth:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Phone number:";
            // 
            // btn_DoneRegistration
            // 
            this.btn_DoneRegistration.Location = new System.Drawing.Point(186, 257);
            this.btn_DoneRegistration.Name = "btn_DoneRegistration";
            this.btn_DoneRegistration.Size = new System.Drawing.Size(101, 23);
            this.btn_DoneRegistration.TabIndex = 5;
            this.btn_DoneRegistration.Text = "Done";
            this.btn_DoneRegistration.UseVisualStyleBackColor = true;
            this.btn_DoneRegistration.Click += new System.EventHandler(this.btn_DoneRegistration_Click);
            // 
            // btn_CancelRegistration
            // 
            this.btn_CancelRegistration.Location = new System.Drawing.Point(79, 257);
            this.btn_CancelRegistration.Name = "btn_CancelRegistration";
            this.btn_CancelRegistration.Size = new System.Drawing.Size(101, 23);
            this.btn_CancelRegistration.TabIndex = 5;
            this.btn_CancelRegistration.Text = "Cancel";
            this.btn_CancelRegistration.UseVisualStyleBackColor = true;
            this.btn_CancelRegistration.Click += new System.EventHandler(this.btn_CancelRegistration_Click);
            // 
            // newAccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 292);
            this.Controls.Add(this.btn_CancelRegistration);
            this.Controls.Add(this.btn_DoneRegistration);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "newAccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CoSE Registration";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbox_NewEmail;
        private System.Windows.Forms.TextBox tbox_NewSurname;
        private System.Windows.Forms.TextBox tbox_NewName;
        private System.Windows.Forms.TextBox tbox_NewNickname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbox_NewPhoneNumber;
        private System.Windows.Forms.DateTimePicker dtp_NewBirth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_DoneRegistration;
        private System.Windows.Forms.Button btn_CancelRegistration;
    }
}