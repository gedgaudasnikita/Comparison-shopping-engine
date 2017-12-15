namespace Comparison_shopping_engine_admin
{
    partial class MainForm
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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.ItemDataGridView = new System.Windows.Forms.DataGridView();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.MergeButton = new System.Windows.Forms.Button();
            this.RestoreButton = new System.Windows.Forms.Button();
            this.CorrectButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.NotifyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ItemDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(22, 16);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // ItemDataGridView
            // 
            this.ItemDataGridView.AllowUserToAddRows = false;
            this.ItemDataGridView.AllowUserToDeleteRows = false;
            this.ItemDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemDataGridView.Enabled = false;
            this.ItemDataGridView.Location = new System.Drawing.Point(22, 55);
            this.ItemDataGridView.Name = "ItemDataGridView";
            this.ItemDataGridView.ReadOnly = true;
            this.ItemDataGridView.ShowEditingIcon = false;
            this.ItemDataGridView.Size = new System.Drawing.Size(449, 150);
            this.ItemDataGridView.TabIndex = 1;
            this.ItemDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemDataGridView_CellContentClick);
            this.ItemDataGridView.SelectionChanged += new System.EventHandler(this.ItemDataGridView_SelectionChanged);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Location = new System.Drawing.Point(103, 214);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteButton.TabIndex = 2;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // MergeButton
            // 
            this.MergeButton.Enabled = false;
            this.MergeButton.Location = new System.Drawing.Point(184, 214);
            this.MergeButton.Name = "MergeButton";
            this.MergeButton.Size = new System.Drawing.Size(75, 23);
            this.MergeButton.TabIndex = 3;
            this.MergeButton.Text = "Merge";
            this.MergeButton.UseVisualStyleBackColor = true;
            // 
            // RestoreButton
            // 
            this.RestoreButton.Enabled = false;
            this.RestoreButton.Location = new System.Drawing.Point(265, 214);
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.Size = new System.Drawing.Size(98, 23);
            this.RestoreButton.TabIndex = 4;
            this.RestoreButton.Text = "Restore version";
            this.RestoreButton.UseVisualStyleBackColor = true;
            // 
            // CorrectButton
            // 
            this.CorrectButton.Enabled = false;
            this.CorrectButton.Location = new System.Drawing.Point(22, 214);
            this.CorrectButton.Name = "CorrectButton";
            this.CorrectButton.Size = new System.Drawing.Size(75, 23);
            this.CorrectButton.TabIndex = 5;
            this.CorrectButton.Text = "Correct";
            this.CorrectButton.UseVisualStyleBackColor = true;
            this.CorrectButton.Click += new System.EventHandler(this.CorrectButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Enabled = false;
            this.RefreshButton.Location = new System.Drawing.Point(392, 16);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 6;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // NotifyButton
            // 
            this.NotifyButton.Location = new System.Drawing.Point(369, 214);
            this.NotifyButton.Name = "NotifyButton";
            this.NotifyButton.Size = new System.Drawing.Size(98, 23);
            this.NotifyButton.TabIndex = 9;
            this.NotifyButton.Text = "Notify";
            this.NotifyButton.UseVisualStyleBackColor = true;
            this.NotifyButton.Click += new System.EventHandler(this.NotifyButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 259);
            this.Controls.Add(this.NotifyButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.CorrectButton);
            this.Controls.Add(this.RestoreButton);
            this.Controls.Add(this.MergeButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.ItemDataGridView);
            this.Controls.Add(this.ConnectButton);
            this.Name = "MainForm";
            this.Text = "Item Management";
            ((System.ComponentModel.ISupportInitialize)(this.ItemDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.DataGridView ItemDataGridView;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button MergeButton;
        private System.Windows.Forms.Button RestoreButton;
        private System.Windows.Forms.Button CorrectButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button NotifyButton;
    }
}

