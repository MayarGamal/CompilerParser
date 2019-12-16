using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParserApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tokenType = new List<String>();
            tokenValue = new List<String>();
        }
        public string input;
        public int input_size;
        public List<string> tokenValue;
        public List<string> tokenType;

        private void InputText_TextChanged(object sender, EventArgs e)
        {
            input = InputText.Text;
            input_size = input.Length;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public string temp=string.Empty;

        private void genBtn_Click(object sender, EventArgs e)
        {
            //list of TokenValue & list of TokenType
            for (int i = 0; i < input_size;)
            {
                if (input[i] == ' ')
                {
                    i++;
                }
                else
                {
                    while ((input[i] != ',') && (input[i] != ' '))
                    {

                        char ip = input[i];
                        string input_s = ip.ToString();
                        int length = temp.Length;
                        temp = temp.Insert(length, input_s);
                        
                        i++;
                    }
                    tokenValue.Add(temp);
                    while (input[i] == ' ')
                    {
                        i++;
                    }
                    if (input[i] == ',')
                    {
                        i++;
                        temp = string.Empty;
                        while (input[i] == ' ')
                        {
                            i++;
                        }

                        while (i < input_size)
                        {
                            if (i < input_size + 2 && (input[i] == '\r') && (input[i + 1] == '\n'))
                            {
                                i += 2;
                                break;
                            }
                            char ip = input[i];
                            string input_s = ip.ToString();
                            int length = temp.Length;
                            temp = temp.Insert(length, input_s);
                            
                            i++;
                        }
                        tokenType.Add(temp);
                        temp = string.Empty;

                    }


                }

            }
            
            for (int i = 0; i < tokenType.Count; i++)
            {
                string type = tokenType[i];
                string value = tokenValue[i];

                Output.Text += i +"   " +value+"   " + type + "\r\n";
            }

            //loop on tokenType
            for (int i=0;i<tokenType.Count;i++)
            {
               //is statment

               //is expression 
                
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Output_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
