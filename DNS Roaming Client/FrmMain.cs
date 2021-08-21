using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNS_Roaming_Client
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnForceClose_Click(object sender, EventArgs e)
        {
            ForceClose();
        }

        private void ForceClose()
        {
            this.Close();
        }

        private void menuPause_Click(object sender, EventArgs e)
        {

        }

        private void menuSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.ShowDialog();
        }

        private void menuStopAndClose_Click(object sender, EventArgs e)
        {
            ForceClose();
        }

        private void contextMenuStrip_DoubleClick(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.ShowDialog();
        }


    }
}
