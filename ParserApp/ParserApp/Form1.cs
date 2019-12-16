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
                
            }

            //loop on tokenType
            for (int i=0;i<tokenType.Count;i++)
            {
                //is statment

               //is expression 
                
            }


        }

       





    }
}
