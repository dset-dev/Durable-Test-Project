using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using static CapabilityRequest.DemoCapabilityRequest;

namespace CapabilityRequest
{
    public partial class Form1 : Form
    {
        String[] locales = { "en-US", "en-GB", "fr-FR",
                                "de-DE", "ru-RU" };
        CapabilityRequest.DemoCapabilityRequest demo;
        public Form1()
        {
            InitializeComponent();
            LoggingOutput.Text = "Windows Form App Proof of Concept for Entitlement Management. Created by the Digital Solution Enablement Team at Eaton.";

            
        }

        public void AddLoggingOutput(string newLog)
        {
            DateTime current = DateTime.Now;
            var locale = new CultureInfo(locales[0]);
            string newOutput = $"\n{current.ToString(locale)} - ";
            newOutput += newLog;
            LoggingOutput.Text += newOutput;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                demo = new DemoCapabilityRequest();
                LoggingOutput.Text = demo.CheckLicensing();
            }
            catch(Exception ex)
            {
                LoggingOutput.Text = ex.ToString();
            }

        }

        private void ButtonResponse1_Click(object sender, EventArgs e)
        {

        }

        private void EntitlementIdSubmitButton_Click(object sender, EventArgs e)
        {
            /*if(EntitlementIdField.Text.Length > 0)
            {
                var newEntitlementId = EntitlementIdField.Text;
                var msgToLog = $"The Entitlement ID was set to {newEntitlementId}";
                AddLoggingOutput(msgToLog);
            }*/
            LoggingOutput.Text = demo.DisplayTSFeatures();
        }

        private void EntitlementIdFieldDescriptor_Click(object sender, EventArgs e)
        {
            
        }

        private void LoggingOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EntitlementIdField.Text.Length > 0)
            {
                var entId = EntitlementIdField.Text;
                var entId2 = entitlementID2Field.Text;

                var feature = txtFeature.Text;
                int cnt = Int32.Parse(txtCnt.Text);
               // var serverURL = "https://eaton-fno-uat.flexnetoperations.com//flexnet//operations//deviceservices";
                // var serverURL = "http://localhost:7070/api/1.0/instances/~";
               var serverURL = "http://localhost:7070/fne/bin/capability";
               // if (demo.DemoSendCapabilityRequest(entId, entId2, cnt, serverURL))
              if (demo.DemoSendCapabilityFeatureRequest(feature, "1.0", cnt, serverURL))
                {
                    LoggingOutput.Text = $"Registration succeeded";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EntitlementIdField.Text.Length > 0)
            {
                var entId = EntitlementIdField.Text;
                var entId2 = entitlementID2Field.Text;
                var fileName = FileDirField.Text + "/capabilityRequest.bin";
                int cnt = Int32.Parse(txtCnt.Text);
                if (demo.DemoGenerateCapabilityRequest(entId, entId2, cnt, cnt,fileName))
                //if (demo.DemoCapabilityRequestTest(fileName))
                {
                    LoggingOutput.Text = $"capabilityRequest.bin generated";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (EntitlementIdField.Text.Length > 0)
            {
                var entId = EntitlementIdField.Text;
                var entId2 = entitlementID2Field.Text;
                var fileName = FileDirField.Text + "/capabilityResponse.bin";
                if (demo.DemoProcessCapabilityResponse(entId, entId2, fileName))
                {
                    LoggingOutput.Text = $"capabilityResponse.bin processed";
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoggingOutput.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (EntitlementIdField.Text.Length > 0)
            {
                var entId = EntitlementIdField.Text;
                var entId2 = entitlementID2Field.Text;
                var serverURL = "https://eaton-fno-uat.flexnetoperations.com//flexnet//operations//deviceservices";
                if (demo.DemoUnregister(entId, entId2, serverURL))
                {
                    LoggingOutput.Text = $"License Removed";
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoggingOutput.Text = demo.DisplayTrialsFeatures();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bool ret = demo.DemoAcquire(txtFeature.Text, txtVersion.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bool ret = demo.DemoReturn(txtFeature.Text, txtVersion.Text);
        }

        private void flowLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
