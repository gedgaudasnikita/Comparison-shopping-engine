namespace Comparison_shopping_engine_admin
{
    partial class ItemNotifyForm
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
            this.ItemNameLabel = new System.Windows.Forms.Label();
            this.NotificationTextLabel = new System.Windows.Forms.Label();
            this.ItemNameTextBox = new System.Windows.Forms.TextBox();
            this.NotificationTextBox = new System.Windows.Forms.TextBox();
            this.NotifyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ItemNameLabel
            // 
            this.ItemNameLabel.AutoSize = true;
            this.ItemNameLabel.Location = new System.Drawing.Point(12, 9);
            this.ItemNameLabel.Name = "ItemNameLabel";
            this.ItemNameLabel.Size = new System.Drawing.Size(61, 13);
            this.ItemNameLabel.TabIndex = 0;
            this.ItemNameLabel.Text = "Item Name:";
            // 
            // NotificationTextLabel
            // 
            this.NotificationTextLabel.AutoSize = true;
            this.NotificationTextLabel.Location = new System.Drawing.Point(12, 37);
            this.NotificationTextLabel.Name = "NotificationTextLabel";
            this.NotificationTextLabel.Size = new System.Drawing.Size(87, 13);
            this.NotificationTextLabel.TabIndex = 1;
            this.NotificationTextLabel.Text = "Notification Text:";
            // 
            // ItemNameTextBox
            // 
            this.ItemNameTextBox.Enabled = false;
            this.ItemNameTextBox.Location = new System.Drawing.Point(105, 6);
            this.ItemNameTextBox.Name = "ItemNameTextBox";
            this.ItemNameTextBox.Size = new System.Drawing.Size(167, 20);
            this.ItemNameTextBox.TabIndex = 2;
            // 
            // NotificationTextBox
            // 
            this.NotificationTextBox.Location = new System.Drawing.Point(105, 34);
            this.NotificationTextBox.Name = "NotificationTextBox";
            this.NotificationTextBox.Size = new System.Drawing.Size(167, 20);
            this.NotificationTextBox.TabIndex = 3;
            // 
            // NotifyButton
            // 
            this.NotifyButton.Location = new System.Drawing.Point(105, 64);
            this.NotifyButton.Name = "NotifyButton";
            this.NotifyButton.Size = new System.Drawing.Size(75, 23);
            this.NotifyButton.TabIndex = 4;
            this.NotifyButton.Text = "Notify";
            this.NotifyButton.UseVisualStyleBackColor = true;
            this.NotifyButton.Click += new System.EventHandler(this.NotifyButton_Click);
            // 
            // ItemNotifyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 94);
            this.Controls.Add(this.NotifyButton);
            this.Controls.Add(this.NotificationTextBox);
            this.Controls.Add(this.ItemNameTextBox);
            this.Controls.Add(this.NotificationTextLabel);
            this.Controls.Add(this.ItemNameLabel);
            this.Name = "ItemNotifyForm";
            this.Text = "ItemNotifyForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ItemNameLabel;
        private System.Windows.Forms.Label NotificationTextLabel;
        private System.Windows.Forms.TextBox ItemNameTextBox;
        private System.Windows.Forms.TextBox NotificationTextBox;
        private System.Windows.Forms.Button NotifyButton;
    }
}