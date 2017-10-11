namespace Comparison_shopping_engine
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

        /// <summary>
        /// Initializes all components in a form
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_NewReceipt = new System.Windows.Forms.Button();
            this.lbl_ReceptInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_NewReceipt
            // 
            this.btn_NewReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_NewReceipt.Location = new System.Drawing.Point(204, 343);
            this.btn_NewReceipt.Name = "btn_NewReceipt";
            this.btn_NewReceipt.Size = new System.Drawing.Size(187, 28);
            this.btn_NewReceipt.TabIndex = 0;
            this.btn_NewReceipt.Text = "Upload new receipt";
            this.btn_NewReceipt.UseVisualStyleBackColor = true;
            this.btn_NewReceipt.Click += new System.EventHandler(this.Btn_NewReceipt_Click);
            // 
            // lbl_ReceptInfo
            // 
            this.lbl_ReceptInfo.AutoSize = true;
            this.lbl_ReceptInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_ReceptInfo.Location = new System.Drawing.Point(0, 0);
            this.lbl_ReceptInfo.Name = "lbl_ReceptInfo";
            this.lbl_ReceptInfo.Padding = new System.Windows.Forms.Padding(10);
            this.lbl_ReceptInfo.Size = new System.Drawing.Size(174, 33);
            this.lbl_ReceptInfo.TabIndex = 1;
            this.lbl_ReceptInfo.Text = "Receipt information will be here";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 383);
            this.Controls.Add(this.lbl_ReceptInfo);
            this.Controls.Add(this.btn_NewReceipt);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comparison Shopping Engine";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btn_NewReceipt;
        private System.Windows.Forms.Label lbl_ReceptInfo;
    }
}