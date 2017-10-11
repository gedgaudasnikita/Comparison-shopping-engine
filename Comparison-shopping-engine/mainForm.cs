using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comparison_shopping_engine
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Reacts to a button (<see cref="btn_NewReceipt"></see>) click by opening an <see cref="OpenFileDialog"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_NewReceipt_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ProcessReceipt(new Bitmap(openFileDialog.FileName));
            }
        }
        /// <summary>
        /// Changes <see cref="lbl_ReceiptInfo"></see> text to a given <see langword="string"/>.
        /// </summary>
        private void UpdateResultLabel(string resultInfo)
        {
            this.lbl_ReceptInfo.Text = resultInfo;
        }
    }
}
