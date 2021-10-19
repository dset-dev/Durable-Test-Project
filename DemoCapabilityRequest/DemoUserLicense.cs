using FlxDotNetClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            InitHostIdTypes();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            txtException.Text = "";
            txtStatus.Text = "";

            string txtlicFileName = licFileName.Text;
            string ret = demo.DemoLoadLicenseFile(txtlicFileName);
            if (ret == "")
                txtStatus.Text = demo.DemoDisplayLicenses();
            else
                txtException.Text = ret;
        }
        private Dictionary<string, HostIdEnum> hostIdTypesByText = new Dictionary<string, HostIdEnum>();
        
        private void InitHostIdTypes()
        {
            int selectedIndex = 0;
            lstHostType.Items.Clear();
            foreach (HostIdEnum hostIdType in Enum.GetValues(typeof(HostIdEnum)))
            {
                if (hostIdType == HostIdEnum.FLX_HOSTID_TYPE_ETHERNET ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_STRING ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_USER ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_INTERNET ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_INTERNET6)
                {
                    if (hostIdType == HostIdEnum.FLX_HOSTID_TYPE_STRING)
                    {
                        selectedIndex = lstHostType.Items.Count;
                    }
                    lstHostType.Items.Add(hostIdType);
                  hostIdTypesByText.Add(hostIdType.ToString(), hostIdType);
                }
            }
            lstHostType.SelectedIndex = selectedIndex;
        }
        private void DemoUserLicense_Load(object sender, EventArgs e)
        {
            demo = new DemoCapabilityRequest();
        }
        string strLastLicenseFilePath= "C:\\\\revenera\\BuildSample\\flexnet_client-xt-dotnet-x64_windows-2021.09.0\\bin\tools\\";
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.AddExtension = false;
                dialog.Filter = "Binary Files|*.bin|All Files|*.*";
                dialog.Title = "Open License Data File";
                if (!string.IsNullOrEmpty(strLastLicenseFilePath))
                {
                    dialog.InitialDirectory = strLastLicenseFilePath;
                }
                //else if (cbxLicenseFile.Items.Count >= 1)
                //{
                //    dialog.InitialDirectory = Path.GetDirectoryName(cbxLicenseFile.Items[0].ToString());
                //}
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    licFileName.Text = dialog.FileName;
                    strLastLicenseFilePath = Path.GetDirectoryName(dialog.FileName);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            demo.DemoRefreshLicensingHost(txtHostID.Text);
            txtStatus.Text = "Reloaded host";
        }

        private void lstHostType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstHostType.SelectedIndex >=0)
            {
                HostIdEnum selItem = hostIdTypesByText[lstHostType.Text];
                if (selItem!= HostIdEnum.FLX_HOSTID_TYPE_STRING)
                    txtHostID.Text = demo.DemoSelectHost(selItem);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
