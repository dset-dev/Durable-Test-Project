using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CapabilityRequest
{
    public partial class Form1 : Form
    {
        String[] locales = { "en-US", "en-GB", "fr-FR",
                                "de-DE", "ru-RU" };
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

        }

        private void ButtonResponse1_Click(object sender, EventArgs e)
        {

        }

        private void EntitlementIdSubmitButton_Click(object sender, EventArgs e)
        {
            if(EntitlementIdField.Text.Length > 0)
            {
                var newEntitlementId = EntitlementIdField.Text;
                var msgToLog = $"The Entitlement ID was set to {newEntitlementId}";
                AddLoggingOutput(msgToLog);
            }
            
        }

        private void EntitlementIdFieldDescriptor_Click(object sender, EventArgs e)
        {

        }

        private void LoggingOutput_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonResponse1.Text = "Button 1 Pressed";
            AddLoggingOutput("Button 1 Pressed");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonResponse2.Text = "Button 2 Pressed";
            AddLoggingOutput("Button 2 Pressed");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ButtonResponse3.Text = "Button 3 Pressed";
            AddLoggingOutput("Button 3 Pressed");
        }
    }
}
