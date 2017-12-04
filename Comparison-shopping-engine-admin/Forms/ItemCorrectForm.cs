using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_admin
{
    public partial class ItemCorrectForm : Form
    {
        private Item item;

        public ItemCorrectForm(Item _item)
        {
            InitializeComponent();
            item = _item;
            DisplayItem();
        }

        private void DisplayItem()
        {
            NameLabel.Text = item.Name;
            PriceLabel.Text = item.Price.ToString();
            StoreLabel.Text = item.Store;
            DateLabel.Text = item.Date.ToString("yyyy-MM-dd");
        }

        private void PriceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(PriceTextBox.Text, out float value) && value != 0f)
            {
                PriceAllowedLabel.Text = "Not allowed";
                UpdateButton.Enabled = false;
            } else
            {
                PriceAllowedLabel.Text = "Allowed";
                UpdateButton.Enabled = true;
            }
        }

        private void DateTextBox_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(PriceTextBox.Text, out DateTime value))
            {
                PriceAllowedLabel.Text = "Not allowed";
                UpdateButton.Enabled = false;
            }
            else
            {
                PriceAllowedLabel.Text = "Allowed";
                UpdateButton.Enabled = true;
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            //Do the database stuff
            Close();
        }
    }
}
