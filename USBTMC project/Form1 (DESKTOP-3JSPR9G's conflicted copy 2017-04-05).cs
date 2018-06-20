using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ivi.Visa.Interop;
using NationalInstruments.Visa;
using Ivi.Visa;
using System.IO.Ports;

namespace USBTMC_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            getAvailableResources();
        }

        void getAvailableResources()
        {


            Ivi.Visa.Interop.ResourceManager rMgr = new ResourceManagerClass();
            FormattedIO488 src = new FormattedIO488Class();


           string [] resources = rMgr.FindRsrc("USB?*");
            comboBox1.Items.AddRange(resources);
            string port = comboBox1.ToString();
            

            // string srcAddress = "USB0::0xF4ED::0xEE3A::SDG2XBA3150073::INSTR";

           // src.IO = (Ivi.Visa.Interop.IMessage)rMgr.Open(port, Ivi.Visa.Interop.AccessMode.NO_LOCK, 0, "");




        /*src.IO = (IMessage)rMgr.Open(, AccessMode.NO_LOCK, 2000, "");
              src.IO.Timeout = 12000;
              src.WriteString("*IDN?\n");
              string IDN = src.ReadString();

              Console.WriteLine(IDN);
            */


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox1.Text;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                    }
    }
}
