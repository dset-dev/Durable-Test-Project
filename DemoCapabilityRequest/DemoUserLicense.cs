﻿using FlxDotNetClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;
//using static CapabilityRequest.DemoCapabilityRequest;

namespace CapabilityRequest
{
    
    public partial class DemoUserLicense : Form
    {
        //CapabilityRequest.DemoCapabilityRequest demo;
        CapabilityRequest.Demo demo;
        public DemoUserLicense()
        {
            //comm
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
        //To-do: add more host id types, including two ethernet hosts
        //to-do: need to display a listbox when selecting ethernet
        private void InitHostIdTypes()
        {
            int selectedIndex = 0;
            lstHostType.Items.Clear();
            demo = new Demo();
            //foreach (HostIdEnum hostIdType in Enum.GetValues(typeof(HostIdEnum)))
            var hostIdTypes = demo.AvailableHostIDs();

            foreach (HostIdEnum hostIdType in hostIdTypes.Keys)
            {
                if (hostIdType == HostIdEnum.FLX_HOSTID_TYPE_ETHERNET ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_STRING ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_USER ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_DISPLAY ||
                     hostIdType == HostIdEnum.FLX_HOSTID_TYPE_HOSTDOMAIN ||
                       hostIdType == HostIdEnum.FLX_HOSTID_TYPE_HOSTNAME ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_LONGHOSTID ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_VSN||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_COMPOSITE ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_INTERNET ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_INTERNET6 ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_FLEXID9 ||
                    hostIdType == HostIdEnum.FLX_HOSTID_TYPE_VM_UUID)
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
            //demo = new DemoCapabilityRequest();
            demo = new Demo();
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

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
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
                    trialFileName.Text = dialog.FileName;
                    //strLastLicenseFilePath = Path.GetDirectoryName(dialog.FileName);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtTrials.Text = demo.DisplayTrialsFeatures();
        }
       
        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void EntitlementIdSubmitButton_Click(object sender, EventArgs e)
        {
            TSFNOOnline.Text = HttpUtility.HtmlDecode(demo.DisplayTSFeatures());
        }

        private void button15_Click(object sender, EventArgs e)
        {
            TSFNOOffline.Text = demo.DisplayTSFeatures();
        }

        private void btnOnlineActivate_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (actID1.Text.Length > 0)
            {
                var actId = actID1.Text;
                var actId2 = actID2.Text;
                var serverURL = "https://eaton-fno-uat.flexnetoperations.com//flexnet//operations//deviceservices";
                if (demo.DemoUnregister(actId, actId2, serverURL))
                {
                    LoggingOutput.Text = $"License Removed";
                }
            }
        }

        private void AcquireLLS_Click(object sender, EventArgs e)
        {
            if (feature1.Text.Length > 0)
            {

                string feature = feature1.Text;
                string version = feature1Version.Text;
                int cnt = Int32.Parse(feature1Cnt.Text);

                //var serverURL = "https://flex1369-uat.compliance.flexnetoperations.com:443/instances/H4H1HKBLWK8G/request";
                var serverURL = "http://localhost:7070/fne/bin/capability";

                if (demo.DemoSendCapabilityFeatureTimedRequest(feature, version, cnt, serverURL))
                {
                    llsTSStatus.Text = $"Registration succeeded";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txtTrials.Text= demo.DemoProcessTrialFile(trialFileName.Text);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            llsTSStatus.Text= demo.DisplayTSFeatures();
        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            TSFNOOffline.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            TSFNOOnline.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            llsTSStatus.Text = "";
        }

        private void btnCloudAcquire_Click(object sender, EventArgs e)
        {
            if (feature1.Text.Length > 0)
            {

                string feature = cloudFeature1.Text;
                string version = cloudVersion1.Text;
                int cnt = Int32.Parse(cloudCount1.Text);
                string device = cloudDevice.Text;

                var serverURL="https://eaton-uat.compliance.flexnetoperations.com/instances/" + device + "/request";

                //Sankar
                //var serverURL = "https://eaton-uat.compliance.flexnetoperations.com/instances/" + device + "/request"
                //var serverURL = "https://eaton-fno-uat.flexnetoperations.com/instances/LJKDF7J35DAJ/request";
                //var serverURL = "https://flex1369-uat.compliance.flexnetoperations.com:443/instances/"+device+ "/request";


                //if (demo.DemoSendCapabilityFeatureTimedRequest(feature, version, cnt, serverURL))
                if (demo.DemoSendCapabilityFeatureRequest(feature, version, cnt, serverURL))
                {
                    cloudStatus.Text = $"Registration succeeded";
                    CloudTS.Text = demo.DisplayTSFeatures();
                }
            }
        }

        private void btnOnlineActivate_Click_1(object sender, EventArgs e)
        {
            if (actID1.Text.Length > 0)
            {
                var actId = actID1.Text;
                int cnt1 = Int32.Parse(onlineCnt1.Text);
                var actId2="";
                var actId3="";
                int cnt2 = 0;
                int cnt3 = 0;
                if (actID2.Text.Length > 0)
                {
                    actId2 = actID2.Text;
                    actId3 = actID3.Text;
                    cnt2 = Int32.Parse(onlineCnt2.Text);
                    cnt3 = Int32.Parse(onlineCnt3.Text);
                }
                var serverURL = "https://eaton-fno-uat.flexnetoperations.com//flexnet//operations//deviceservices";
                //var serverURL="https://eaton-fno-uat.flexnetoperations.com//flexnet//deviceservices";
                if (demo.DemoSendCapabilityRequest(actId, actId2, actId3, cnt1,cnt2,cnt3, serverURL))
                // if (demo.DemoSendCapabilityFeatureRequest(feature, cnt, serverURL))
                {
                    TSFNOOnline.Text = $"Registration succeeded";
                }
            }
        }
        string strLastDirPath = "";
        private void button21_Click_1(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.AddExtension = false;
                dialog.Filter = "Binary Files|*.bin|All Files|*.*";
                dialog.Title = "Open License Data File";
                if (!string.IsNullOrEmpty(strLastLicenseFilePath))
                {
                    dialog.InitialDirectory = strLastDirPath;
                }
                //else if (cbxLicenseFile.Items.Count >= 1)
                //{
                //    dialog.InitialDirectory = Path.GetDirectoryName(cbxLicenseFile.Items[0].ToString());
                //}
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtReqRespFile.Text = dialog.FileName;
                    strLastDirPath = Path.GetDirectoryName(dialog.FileName);
                }
            }
        }

        private void button19_Click_1(object sender, EventArgs e)
        {
            if (actOffline1.Text.Length > 0)
            {
                var actId = actOffline1.Text;
                var actId2 = actOffline2.Text;
                var fileName = txtReqRespFile.Text;
                int cnt1 = Int32.Parse(offlineCnt1.Text);
                int cnt2 = Int32.Parse(offlineCnt2.Text);
                if (demo.DemoGenerateCapabilityRequest(actId, actId2, cnt1, cnt2, fileName))
                //if (demo.DemoCapabilityRequestTest(fileName))
                {
                    TSFNOOffline.Text = $"capabilityRequest.bin generated";
                }
            }
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            if (actOffline1.Text.Length > 0)
            {
                var actId = actOffline1.Text;
                var actId2 = actOffline2.Text;
                var fileName = txtReqRespFile.Text;
                if (demo.DemoProcessCapabilityResponse(actId, actId2, fileName))
                {
                    TSFNOOffline.Text = $"capabilityResponse.bin processed";
                }
            }
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            CloudTS.Text = demo.DisplayTSFeatures();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bool res = demo.ReturnTrials();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            CloudTS.Text = "";
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void cmdLLSPreview_Click(object sender, EventArgs e)
        {
            var serverURL = "http://localhost:7070/fne/bin/capability";

            llsTSStatus.Text = demo.Preview(serverURL);


        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (feature1.Text.Length > 0)
            {

                string feature = LLSFeature1.Text;
                string version = LLSVersion1.Text;
                int cnt = Int32.Parse(LLSCount1.Text);

                //var serverURL = "https://flex1369-uat.compliance.flexnetoperations.com:443/instances/H4H1HKBLWK8G/request";
                var serverURL = "http://localhost:7070/fne/bin/capability";

                if (demo.DemoSendCapabilityFeatureTimedRequest(feature, version, cnt, serverURL))
                {
                    LLSStatus.Text = $"Registration succeeded";
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            demo.BufferLicensingHost(LLSHostID.Text);
            LLSStatus.Text = "Reloaded host";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            demo.ResetTS();
        }
    }
}
