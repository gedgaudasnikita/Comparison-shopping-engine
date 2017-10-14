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
        private void BtnNewReceiptClick(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string previousText = btn_NewReceipt.Text;
                btn_NewReceipt.Text = "Loading...";
                MainForm.ActiveForm.Refresh();
                Controller.ProcessReceipt(new Bitmap(openFileDialog.FileName), (parsed, cheaper) => {
                    btn_NewReceipt.Text = previousText;
                    UpdateResultLabel(parsed, cheaper);
                });
            }
        }
        /// <summary>
        /// Changes <see cref="lbl_ReceiptInfo"></see> text to a given <see langword="string"/>.
        /// </summary>
        /// <param name="resultInfo"></param>
        public void UpdateResultLabel(Receipt parsed, List<Item> cheaper)
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

            this.lbl_ReceptInfo.Text = resultInfo;
        }
    }
}
