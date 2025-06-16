using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMBCPacientes
{
    public partial class frmCallesPacientes : Form
    {
        public frmCallesPacientes()
        {
            InitializeComponent();
        }
        private void btnPacientes_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmPaciente frm = new FrmPaciente();
            frm.ShowDialog();
            this.Show();
        }
        private void btnCalles_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCalles frmCalles = new frmCalles();
            frmCalles.ShowDialog();
            this.Show();
        }
    }
}