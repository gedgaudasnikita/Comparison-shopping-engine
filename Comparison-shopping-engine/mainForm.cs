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
        /// Reacts to button <see cref="btn_NewReceipt"></see> click by opening a <see cref="OpenFileDialog"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_NewReceipt_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "c:\\",
                Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            // code to read the stream
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
