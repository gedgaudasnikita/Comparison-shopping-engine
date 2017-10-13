using System;
using System.Drawing;
using System.Windows.Forms;

namespace Comparison_shopping_engine
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Reacts to a button (<see cref="btn_NewReceipt"></see>) click by opening an <see cref="OpenFileDialog"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNewReceiptClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Controller.ProcessReceipt(new Bitmap(openFileDialog.FileName));
            }
        }
        /// <summary>
        /// Changes <see cref="lbl_ReceiptInfo"></see> text to a given <see langword="string"/>.
        /// </summary>
        /// <param name="resultInfo"></param>
        public void UpdateResultLabel(string resultInfo)
        {
            this.lbl_ReceptInfo.Text = resultInfo;
        }
    }
}
