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
    public partial class newAccountForm : Form
    {
        public newAccountForm()
        {
            InitializeComponent();
        }

        private void btn_CancelRegistration_Click(object sender, EventArgs e)
        {
            this.Hide();
            var lf = new loginForm();
            lf.FormClosed += (s, args) => this.Close();
            lf.Show();
        }

        private void btn_DoneRegistration_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mf = new mainForm();
            mf.FormClosed += (s, args) => this.Close();
            mf.Show();
        }
    }
}
