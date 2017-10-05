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
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void btn_LogIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mf = new mainForm();
            mf.FormClosed += (s, args) => this.Close();
            mf.Show();
        }

        private void btn_SignIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            var naf = new newAccountForm();
            naf.FormClosed += (s, args) => this.Close();
            naf.Show();
        }
    }
}
