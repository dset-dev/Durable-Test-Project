using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetworkStatusPOC
{
    public partial class NetworkStatusForm : Form
    {
        public NetworkStatusForm()
        {
            InitializeComponent();
        }

        public bool IsConnectedToInternet()
        {
            string host = "https://www.google.com";  
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch(Exception e){
                VerboseNetworkStatusLabel.Text = $"An error occured. Exception message: {e.Message}\n Inner message: {e.InnerException.Message}";
            }
            return result;
        }

        private void checkNetworkButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("The check network connection button was clicked.");
            if (IsConnectedToInternet())
            {
                Console.WriteLine("Yep, we got internet");
                NetworkStatusDisplayLabel.Text = "Yep, we got internet";
            }
            else
            {
                Console.WriteLine("Nope, no internet here");
                NetworkStatusDisplayLabel.Text = "Nope, no internet here";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void NetworkStatusForm_Load(object sender, EventArgs e)
        {

        }
    }
}
