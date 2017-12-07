namespace Comparison_shopping_engine_admin
{
    partial class ItemCorrectForm
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
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.PriceTextBox = new System.Windows.Forms.TextBox();
            this.StoreTextBox = new System.Windows.Forms.TextBox();
            this.DateTextBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.StoreLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            this.NameAllowedLabel = new System.Windows.Forms.Label();
            this.PriceAllowedLabel = new System.Windows.Forms.Label();
            this.StoreAllowedLabel = new System.Windows.Forms.Label();
            this.DateAllowedLabel = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(52, 13);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(100, 20);
            this.NameTextBox.TabIndex = 0;
            // 
            // PriceTextBox
            // 
            this.PriceTextBox.Location = new System.Drawing.Point(52, 39);
            this.PriceTextBox.Name = "PriceTextBox";
            this.PriceTextBox.Size = new System.Drawing.Size(100, 20);
            this.PriceTextBox.TabIndex = 1;
            this.PriceTextBox.TextChanged += new System.EventHandler(this.PriceTextBox_TextChanged);
            // 
            // StoreTextBox
            // 
            this.StoreTextBox.Location = new System.Drawing.Point(52, 65);
            this.StoreTextBox.Name = "StoreTextBox";
            this.StoreTextBox.Size = new System.Drawing.Size(100, 20);
            this.StoreTextBox.TabIndex = 2;
            // 
            // DateTextBox
            // 
            this.DateTextBox.Location = new System.Drawing.Point(52, 91);
            this.DateTextBox.Name = "DateTextBox";
            this.DateTextBox.Size = new System.Drawing.Size(100, 20);
            this.DateTextBox.TabIndex = 3;
            this.DateTextBox.TextChanged += new System.EventHandler(this.DateTextBox_TextChanged);
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 16);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(38, 13);
            this.NameLabel.TabIndex = 4;
            this.NameLabel.Text = "Name:";
            // 
            // PriceLabel
            // 
            this.PriceLabel.AutoSize = true;
            this.PriceLabel.Location = new System.Drawing.Point(12, 42);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.Size = new System.Drawing.Size(34, 13);
            this.PriceLabel.TabIndex = 5;
            this.PriceLabel.Text = "Price:";
            // 
            // StoreLabel
            // 
            this.StoreLabel.AutoSize = true;
            this.StoreLabel.Location = new System.Drawing.Point(12, 68);
            this.StoreLabel.Name = "StoreLabel";
            this.StoreLabel.Size = new System.Drawing.Size(35, 13);
            this.StoreLabel.TabIndex = 6;
            this.StoreLabel.Text = "Store:";
            // 
            // DateLabel
            // 
            this.DateLabel.AutoSize = true;
            this.DateLabel.Location = new System.Drawing.Point(13, 94);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(33, 13);
            this.DateLabel.TabIndex = 7;
            this.DateLabel.Text = "Date:";
            // 
            // NameAllowedLabel
            // 
            this.NameAllowedLabel.AutoSize = true;
            this.NameAllowedLabel.Location = new System.Drawing.Point(158, 16);
            this.NameAllowedLabel.Name = "NameAllowedLabel";
            this.NameAllowedLabel.Size = new System.Drawing.Size(44, 13);
            this.NameAllowedLabel.TabIndex = 8;
            this.NameAllowedLabel.Text = "Allowed";
            // 
            // PriceAllowedLabel
            // 
            this.PriceAllowedLabel.AutoSize = true;
            this.PriceAllowedLabel.Location = new System.Drawing.Point(158, 42);
            this.PriceAllowedLabel.Name = "PriceAllowedLabel";
            this.PriceAllowedLabel.Size = new System.Drawing.Size(44, 13);
            this.PriceAllowedLabel.TabIndex = 9;
            this.PriceAllowedLabel.Text = "Allowed";
            // 
            // StoreAllowedLabel
            // 
            this.StoreAllowedLabel.AutoSize = true;
            this.StoreAllowedLabel.Location = new System.Drawing.Point(158, 68);
            this.StoreAllowedLabel.Name = "StoreAllowedLabel";
            this.StoreAllowedLabel.Size = new System.Drawing.Size(44, 13);
            this.StoreAllowedLabel.TabIndex = 10;
            this.StoreAllowedLabel.Text = "Allowed";
            // 
            // DateAllowedLabel
            // 
            this.DateAllowedLabel.AutoSize = true;
            this.DateAllowedLabel.Location = new System.Drawing.Point(158, 94);
            this.DateAllowedLabel.Name = "DateAllowedLabel";
            this.DateAllowedLabel.Size = new System.Drawing.Size(44, 13);
            this.DateAllowedLabel.TabIndex = 11;
            this.DateAllowedLabel.Text = "Allowed";
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(52, 117);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(100, 23);
            this.UpdateButton.TabIndex = 12;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // ItemCorrectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 152);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.DateAllowedLabel);
            this.Controls.Add(this.StoreAllowedLabel);
            this.Controls.Add(this.PriceAllowedLabel);
            this.Controls.Add(this.NameAllowedLabel);
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.StoreLabel);
            this.Controls.Add(this.PriceLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.DateTextBox);
            this.Controls.Add(this.StoreTextBox);
            this.Controls.Add(this.PriceTextBox);
            this.Controls.Add(this.NameTextBox);
            this.Name = "ItemCorrectForm";
            this.Text = "ItemCorrectForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox PriceTextBox;
        private System.Windows.Forms.TextBox StoreTextBox;
        private System.Windows.Forms.TextBox DateTextBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label PriceLabel;
        private System.Windows.Forms.Label StoreLabel;
        private System.Windows.Forms.Label DateLabel;
        private System.Windows.Forms.Label NameAllowedLabel;
        private System.Windows.Forms.Label PriceAllowedLabel;
        private System.Windows.Forms.Label StoreAllowedLabel;
        private System.Windows.Forms.Label DateAllowedLabel;
        private System.Windows.Forms.Button UpdateButton;
    }
}