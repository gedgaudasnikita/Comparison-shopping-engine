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
    /// <summary>
    /// This form is responsible for editing the item entries
    /// </summary>
    public partial class ItemCorrectForm : Form
    {
        private Item item;
        private DbClient client;

        public ItemCorrectForm(Item _item, DbClient cli)
        {
            InitializeComponent();
            item = _item;
            DisplayItem();
            client = cli;
        }

        private void DisplayItem()
        {
            NameTextBox.Text = item.Name;
            PriceTextBox.Text = item.Price.ToString();
            StoreTextBox.Text = item.Store;
            DateTextBox.Text = item.Date.ToString("yyyy-MM-dd");
        }

        private Item GetItem()
        {
            return new Item()
            {
                Name = NameTextBox.Text,
                Price = Int32.Parse(PriceTextBox.Text),
                Store = StoreTextBox.Text,
                Date = DateTime.Parse(DateTextBox.Text)
            };
        }

        private void PriceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse(PriceTextBox.Text, out float value) && value != 0f)
            {
                PriceAllowedLabel.Text = "Allowed";
                UpdateButton.Enabled = true;
            } else
            {
                PriceAllowedLabel.Text = "Not allowed";
                UpdateButton.Enabled = false;
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
            client.UpdateItem(GetItem());
            Close();
        }
    }
}
