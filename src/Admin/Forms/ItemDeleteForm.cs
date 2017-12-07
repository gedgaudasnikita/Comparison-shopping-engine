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
    public partial class ItemDeleteForm : Form
    {
        private Item item;

        public ItemDeleteForm(Item _item)
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

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            //Do the database stuff
            Close();
        }
    }
}
