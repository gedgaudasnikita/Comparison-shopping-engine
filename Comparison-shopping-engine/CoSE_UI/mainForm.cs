using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoSE_UI
{
    public partial class mainForm : Form
    {
        List<Panel> listPanel = new List<Panel>();

        public mainForm()
        {
            InitializeComponent();
        }

        private void btn_LogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            var lf = new loginForm();
            lf.FormClosed += (s, args) => this.Close();
            lf.Show();
        }
        
        private void mainForm_Load(object sender, EventArgs e)
        {
            listPanel.Add(pnl_NewCheck);
            listPanel.Add(pnl_History);
            listPanel.Add(pnl_PersonalInfo);
            listPanel[0].BringToFront();
            tableLayoutPanel1.CellPaint += tableLayoutPanel1_CellPaint;
        }

        private void btn_History_Click(object sender, EventArgs e)
        {
            listPanel[1].BringToFront();
            listPanel[0].SendToBack();
            listPanel[2].SendToBack();
        }

        private void btn_PersonalInfo_Click(object sender, EventArgs e)
        {
            listPanel[2].BringToFront();
            listPanel[0].SendToBack();
            listPanel[1].SendToBack();
        }

        private void btn_NewCheck_Click(object sender, EventArgs e)
        {
            listPanel[0].BringToFront();
            listPanel[2].SendToBack();
            listPanel[1].SendToBack();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnl_PersonalInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        void tableLayoutPanel1_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            if (e.Column == 0 && e.Row == 0)
                e.Graphics.DrawRectangle(new Pen(Color.Gray), e.CellBounds);
            if (e.Column == 0 && e.Row == 1)
                e.Graphics.DrawRectangle(new Pen(Color.Gray), e.CellBounds);
            if (e.Column == 0 && e.Row == 2)
                e.Graphics.DrawRectangle(new Pen(Color.Gray), e.CellBounds);
            if (e.Column == 0 && e.Row == 3)
                e.Graphics.DrawRectangle(new Pen(Color.Gray), e.CellBounds);
            if (e.Column == 0 && e.Row == 4)
                e.Graphics.DrawRectangle(new Pen(Color.Gray), e.CellBounds);
            if (e.Column == 0 && e.Row == 5)
                e.Graphics.DrawRectangle(new Pen(Color.Gray), e.CellBounds);
            if (e.Column == 0 && e.Row == 6)
                e.Graphics.DrawRectangle(new Pen(Color.Gray), e.CellBounds);
            if (e.Column == 0 && e.Row == 7)
                e.Graphics.DrawRectangle(new Pen(Color.Gray), e.CellBounds);
        }
    }
}
