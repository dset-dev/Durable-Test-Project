using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static CapabilityRequest.DemoCapabilityRequest;

namespace CapabilityRequest
{
    
    public partial class DemoUserLicense : Form
    {
        CapabilityRequest.DemoCapabilityRequest demo;
        public DemoUserLicense()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string txtlicFileName = licFileName.Text;
            if (demo.DemoLoadLicenseFile(txtlicFileName))
                txtStatus.Text = demo.DemoDisplayLicenses();

        }

        private void DemoUserLicense_Load(object sender, EventArgs e)
        {
            demo = new DemoCapabilityRequest();
        }
    }
}
