﻿using System;
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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ItemDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemDataGridView.Sort(ItemDataGridView.Columns[e.ColumnIndex], ListSortDirection.Ascending);
        }

        private void ItemDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            bool restore, merge, delete, correct, notify;

            switch (ItemDataGridView.SelectedRows.Count)
            {
                case 0:
                    restore = merge = delete = correct = notify = false;
                    break;
                case 1:
                    restore = delete = correct = notify = true;
                    merge = false;
                    break;
                case 2:
                    restore = delete = correct = false;
                    merge = notify = true;
                    break;
                default:
                    restore = merge = delete = correct = false;
                    notify = true;
                    break;
            }

            RestoreButton.Enabled = restore;
            MergeButton.Enabled = merge;
            DeleteButton.Enabled = delete;
            CorrectButton.Enabled = correct;
            NotifyButton.Enabled = notify;
        }

        private void CorrectButton_Click(object sender, EventArgs e)
        {
            var correctionForm = new ItemCorrectForm(GetItemFromCells(ItemDataGridView.SelectedRows[0].Cells));

            correctionForm.Show();
        }

        private Item GetItemFromCells(DataGridViewCellCollection cells)
        {
            return new Item((string)cells[0].Value, (string)cells[2].Value, (int)cells[1].Value, DateTime.Parse(cells[3].Value.ToString()));
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var deletetionForm = new ItemDeleteForm(GetItemFromCells(ItemDataGridView.SelectedRows[0].Cells));

            deletetionForm.Show();
        }

        private void NotifyButton_Click(object sender, EventArgs e)
        {
            var itemsToNotify = new List<Item>();
            foreach (DataGridViewRow row in ItemDataGridView.SelectedRows)
            {
                itemsToNotify.Add(GetItemFromCells(row.Cells));
            }

            var notifyForm = new ItemNotifyForm(itemsToNotify);
            notifyForm.Show();
        }
    }
}
