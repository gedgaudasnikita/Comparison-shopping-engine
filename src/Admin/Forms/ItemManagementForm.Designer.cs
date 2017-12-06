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
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PriceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StoreColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.MergeButton = new System.Windows.Forms.Button();
            this.RestoreButton = new System.Windows.Forms.Button();
            this.CorrectButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.NameSearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
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
            // 
            // ItemDataGridView
            // 
            this.ItemDataGridView.AllowUserToAddRows = false;
            this.ItemDataGridView.AllowUserToDeleteRows = false;
            this.ItemDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn,
            this.PriceColumn,
            this.StoreColumn,
            this.DateColumn});
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
            // NameColumn
            // 
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            // 
            // PriceColumn
            // 
            this.PriceColumn.HeaderText = "Price";
            this.PriceColumn.Name = "PriceColumn";
            this.PriceColumn.ReadOnly = true;
            // 
            // StoreColumn
            // 
            this.StoreColumn.HeaderText = "Store";
            this.StoreColumn.Name = "StoreColumn";
            this.StoreColumn.ReadOnly = true;
            // 
            // DateColumn
            // 
            this.DateColumn.HeaderText = "Date";
            this.DateColumn.Name = "DateColumn";
            this.DateColumn.ReadOnly = true;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Location = new System.Drawing.Point(145, 214);
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
            this.MergeButton.Location = new System.Drawing.Point(260, 214);
            this.MergeButton.Name = "MergeButton";
            this.MergeButton.Size = new System.Drawing.Size(75, 23);
            this.MergeButton.TabIndex = 3;
            this.MergeButton.Text = "Merge";
            this.MergeButton.UseVisualStyleBackColor = true;
            // 
            // RestoreButton
            // 
            this.RestoreButton.Enabled = false;
            this.RestoreButton.Location = new System.Drawing.Point(373, 214);
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
            this.RefreshButton.Location = new System.Drawing.Point(103, 16);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(75, 23);
            this.RefreshButton.TabIndex = 6;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            // 
            // NameSearchTextBox
            // 
            this.NameSearchTextBox.Enabled = false;
            this.NameSearchTextBox.Location = new System.Drawing.Point(290, 18);
            this.NameSearchTextBox.Name = "NameSearchTextBox";
            this.NameSearchTextBox.Size = new System.Drawing.Size(100, 20);
            this.NameSearchTextBox.TabIndex = 7;
            // 
            // SearchButton
            // 
            this.SearchButton.Enabled = false;
            this.SearchButton.Location = new System.Drawing.Point(396, 16);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 8;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 259);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.NameSearchTextBox);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.DataGridView ItemDataGridView;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button MergeButton;
        private System.Windows.Forms.Button RestoreButton;
        private System.Windows.Forms.Button CorrectButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.TextBox NameSearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PriceColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StoreColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateColumn;
    }
}

