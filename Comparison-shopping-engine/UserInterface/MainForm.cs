using System;
using System.Collections.Generic;
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
        private void Btn_NewReceipt_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string previousText = btn_NewReceipt.Text;
                lbl_ReceptInfo.Text = "Loading...";
                btn_NewReceipt.Enabled = false;
                Refresh();
                Controller.ProcessReceipt(new Bitmap(openFileDialog.FileName), Lbl_ReceiptInfo_Update);
            }
        }
        /// <summary>
        /// Changes <see cref="lbl_ReceiptInfo"></see> text to a given <see langword="string"/>.
        /// </summary>
        /// <param name="parsed">
        /// A <see cref="Receipt"> with all the information parsed
        /// </param>
        /// <param name="cheaper">
        /// A <see langword="List"> of <see cref="Item">, each one being
        /// the same as in <paramref name="parsed"/> but the cheapest found
        /// </param>
        public void Lbl_ReceiptInfo_Update(Receipt parsed, List<Item> cheaper)
        {
            var resultInfo = "Parsed receipt: \n";
            foreach (var item in parsed.Items)
            {
                resultInfo += $"{ item.ToString() }\n";
            }

            resultInfo += "\nCheapest items found: \n";
            foreach (var item in cheaper)
            {
                resultInfo += $"{ item.ToString() }\n";
            }

            btn_NewReceipt.Enabled = true;
            lbl_ReceptInfo.Text = resultInfo;
            Refresh();
        }
    }
}
