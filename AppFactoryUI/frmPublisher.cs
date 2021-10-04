using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFactory.UI
{
    public partial class frmPublisher : Form
    {

        public frmPublisher()
        {
            InitializeComponent();
 
        }

        private void btnAbbrechen_Click(object sender, EventArgs e)
        {
            
        }

        private void frmPublisher_Load(object sender, EventArgs e)
        {

        }

        private void btnSpeichern_Click(object sender, EventArgs e)
        {
            

        }

        private void txtDisplayname_Leave(object sender, EventArgs e)
        {
            txtSchemaname.Text = txtDisplayname.Text.Replace(" ", string.Empty).ToLower(); 
            txtPrefix.Text = txtDisplayname.Text.Replace(" ", string.Empty).ToLower();
        }
    }
}
