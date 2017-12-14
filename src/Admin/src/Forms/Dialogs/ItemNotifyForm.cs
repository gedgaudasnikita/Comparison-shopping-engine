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
using Comparison_shopping_engine_notification_protocol;

namespace Comparison_shopping_engine_admin
{
    /// <summary>
    /// This form is responsible for editing and sending the notifications to the front end
    /// </summary>
    public partial class ItemNotifyForm : Form
    {
        NotificationSender sender;

        public ItemNotifyForm(IEnumerable<Item> items)
        {
            sender = new NotificationSender();
            InitializeComponent();
            DisplayItem(items);
        }

        private void DisplayItem(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                ItemsTextBox.Text += item.Name + Environment.NewLine;
            }
        }

        private void NotifyButton_Click(object sender, EventArgs e)
        {
            var note = new NotificationData();
            foreach (var line in ItemsTextBox.Lines)
            {
                note.MapItemToText.Add(line, NotificationTextBox.Text);
            }

            NotificationSender.Send(note);

            this.Close();
        }
    }
}
