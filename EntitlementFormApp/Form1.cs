using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntitlementFormApp
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

        public void AddLoggingOutput(string newLog) {
            DateTime current = DateTime.Now;
            var locale = new CultureInfo(locales[0]);
            string newOutput = $"\n{current.ToString(locale)} - ";
            newOutput += newLog;
            LoggingOutput.Text += newOutput;
        }

        private void EntitlementIdField_TextChanged(object sender, EventArgs e)
        {

        }

        private void EntitlementIdFieldDescriptor_Click(object sender, EventArgs e)
        {

        }

        private void ButtonResponse1_Click(object sender, EventArgs e)
        {

        }

        private void EntitlementIdSubmitButton_Click(object sender, EventArgs e)
        {
            var newEntitlementId = EntitlementIdField.Text;
            var msgToLog = $"The Entitlement ID was set to {newEntitlementId}";
            AddLoggingOutput(msgToLog);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonResponse1.Text = "button 1 press";
            AddLoggingOutput("Button 1 was pressed");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonResponse2.Text = "button 2 press";

            AddLoggingOutput("Button 2 was pressed");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ButtonResponse3.Text = "button 3 press";
            AddLoggingOutput("Button 3 was pressed");
        }
    }
}
