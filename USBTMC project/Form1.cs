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
//using Ivi.Visa;
//using System.IO.Ports;
using System.IO;


namespace USBTMC_project
{
    public partial class Form1 : Form
    {
        Ivi.Visa.Interop.ResourceManager rMgr = new ResourceManagerClass();//Create a resource manager
        FormattedIO488 src = new FormattedIO488Class(); //Create a new IEEE 488.2-like message based session 

        public Form1()
        {
            InitializeComponent();
            getAvailableResources(); // calls for the function GetAvailable resources 
            
        }
        
        public string sendstring; // instanciating strings
        public string sendlogstring; 


        public void getAvailableResources()//function that find the available USBTMC resources and populates combobox1
        {
        //   Ivi.Visa.Interop.ResourceManager rMgr = new ResourceManagerClass();
         //   FormattedIO488 src = new FormattedIO488Class();

            try
            {
                string[] resources = rMgr.FindRsrc("USB?*");
                comboBox1.Items.AddRange(resources);
            }
            catch (Exception)
            {
                textBox2.Text = "No Resources Available";

            }
            
           
        }
        
        public void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            
        }

       public void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox1.Text;
            button3.Enabled = true;
            button4.Enabled = true;


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
         }

       


        public void button1_Click(object sender, EventArgs e)
        {
            string srcAddress = label1.Text;
            
            src.IO = (IMessage)rMgr.Open(srcAddress, AccessMode.NO_LOCK, 2000, "");
            src.IO.Timeout = 12000;
            sendstring = textBox1.Text;
            src.WriteString(sendstring + "\n");//("*IDN?\n");
            textBox2.AppendText(textBox1.Text + "\r\n");
            textBox1.Text = "";
            //  textBox2.Text = src.ReadString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


            

        }

        public void button3_Click(object sender, EventArgs e)
        {
            
            comboBox1.Enabled = false;
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
        }

        public void textBox2_TextChanged(object sender, EventArgs e)
        {
            
                  

            
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void button2_Click_1(object sender, EventArgs e)
        {
            string srcAddress = label1.Text;

           src.IO = (IMessage)rMgr.Open(srcAddress, AccessMode.NO_LOCK, 2000, "");

            try
            {
                textBox2.Text += src.ReadString() + "\r\n";
            }
            catch (TimeoutException)
            {
                textBox2.Text = "timeout exception";

            }
                        
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, new EventArgs());
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label3.Text = textBox3.Text;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label4.Text = textBox4.Text;
        }

        private async void button5_Click(object sender, EventArgs e)
        {

            label8.Text = "";
       //     Ivi.Visa.Interop.ResourceManager rMgr = new ResourceManagerClass();
        //    FormattedIO488 src = new FormattedIO488Class();
           string srcAddress = label1.Text;
            src.IO = (IMessage)rMgr.Open(srcAddress, AccessMode.NO_LOCK, 2000, "");

           
            for (int a = 0; a < Convert.ToInt32(textBox5.Text); a = a + 1)
            {
                
                
                    

                    try
                    {
                        sendlogstring = label3.Text + ";" + label4.Text;

                        src.WriteString("*IDN?\n");
                        string IDN = src.ReadString();


                        src.WriteString(sendlogstring + "\n");//("*IDN?\n");
                                                              //  textBox2.AppendText(textBox1.Text + "\r\n");
                                                              // textBox1.Text = ""; 

                        

                        string fullstring = src.ReadString(); //textBox2.Text;
                        var onestring = fullstring.Split(';');
                        label6.Text = onestring[0];
                        label7.Text = onestring[1];
                        textBox2.Text += fullstring + "\r\n";
                    
                        // CSV LOGGING
                        StringBuilder csvcontent = new StringBuilder();
                    
                        csvcontent.AppendLine(DateTime.Now.ToString("HH:mm:ss:fff") + "," + label3.Text + "," + label6.Text + "," + label4.Text + "," + label7.Text);
                    // string csvpath = "C:\\Users\\eamezquita\\Documents\\Current Projects\\trial_juerves.csv";
                    string csvpath = textBox7.Text;

                    File.AppendAllText(csvpath, csvcontent.ToString());
                    // csvcontent.AppendLine("einar, 895");
                        int b = Convert.ToInt32(textBox6.Text);
                        await Task.Delay(b*1000);
                    
                }

                 
                    catch (TimeoutException)
                    {
                        textBox2.Text = "timeout exception";

                    }
                

            }
            label8.Text = "Logging Completed";

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

