using AppFactory.BusinessLogic;
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
    public partial class frmSolution : Form
    {
        public List<D365Publisher> publisher;

        public frmSolution(List<D365Publisher> publisher)
        {
            InitializeComponent();
            this.publisher = publisher;
        }

        private void btnNewPublisher_Click(object sender, EventArgs e)
        {
            frmPublisher f_pub = new frmPublisher();
            DialogResult result = f_pub.ShowDialog();
            if (result == DialogResult.OK)
            {
                D365Publisher newpub = new D365Publisher
                {
                    Displayname = f_pub.Controls["txtDisplayname"].Text,
                    Prefix = f_pub.Controls["txtPrefix"].Text,
                    Schemaname = f_pub.Controls["txtSchemaname"].Text
                };

                publisher.Add(newpub);


                // select the new publisher in Combo Box
                loadPublisher();
                cmbPublisher.SelectedText = newpub.Displayname;

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
 
        }

        private void frmSolution_Load(object sender, EventArgs e)
        {
            // load all publishers 
            loadPublisher(); 
        }

        private void loadPublisher()
        {
            cmbPublisher.Items.Clear();
            for (int i = 0; i < publisher.Count; i++)
            {
                cmbPublisher.Items.Add(publisher[i].Displayname);
            }
        }

        private void txtSolutionDisplayName_Leave(object sender, EventArgs e)
        {
            txtSolutionName.Text = txtSolutionDisplayName.Text.Replace(" ", string.Empty).ToLower();
        }

        private void cmbPublisher_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cbx = (ComboBox)sender;

        }
    }
}
