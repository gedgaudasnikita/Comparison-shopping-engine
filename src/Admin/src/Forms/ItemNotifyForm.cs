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
    public partial class ItemNotifyForm : Form
    {
        private Item item;

        public ItemNotifyForm(Item _item)
        {
            InitializeComponent();
            item = _item;
            DisplayItem();
        }

        private void DisplayItem()
        {
            ItemNameTextBox.Text = item.Name;
        }

        private void NotifyButton_Click(object sender, EventArgs e)
        {
            NotificationSender.Send(ItemNameTextBox.Text, NotificationTextBox.Text);
        }
    }
}
