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
        public int Token = 0 ;
        public bool ERROR = false ;

        public void Match(string s )
        {
          // MessageBox.Show(s);
            if (is_valid())
            {
                if (tokenType[Token] == s)
                {
                    Token++;
                }
                else
                {
                    ERROR = true ;
                }
            }
            else
            {
                ERROR = true ;
            }
        }

        public bool is_valid()
        {
            if ( tokenType.Count() == Token)
            {
                return false;
            }
            return true;
        }


        public void ERROR_show()
        {
            MessageBox.Show("ERROR!!!");
            ERROR = false;
        }


        public void stmt_seq()
        {
            stmt();
            if ( is_valid() )
            {
                if(tokenType[Token] == "SEMICOLON")
                {
                    Match("SEMICOLON");
                    stmt_seq();
                }
            }
               
        }

        public void stmt()
        {
            if (is_valid())
            {
                if (tokenType[Token] == "READ")
                {
                    READ();
                }
                else if (tokenType[Token] == "IDENTIFIER")
                {
                    ASSIGN();
                }
                else if (tokenType[Token] == "REPEAT")
                {
                    REPEAT();
                }
                else if (tokenType[Token] == "IF")
                {
                    IF();
                }
                else if (tokenType[Token] == "WRITE")
                {
                    WRITE();
                }
                else
                {
                    ERROR  = true;
                }
            }
            else
            {
                ERROR = true;
            }
        }

        public void READ()
        {
            Match("READ");
            Match("IDENTIFIER");
        }

        public void WRITE()
        {
            Match("WRITE");
            exp();

        }

        public void ASSIGN()
        {
            Match("IDENTIFIER");
            Match("ASSIGN");
            exp();

        }

        public void REPEAT()
        {

           Match("REPEAT");
           stmt_seq();
           Match("UNTIL");
           exp();

        }

        public void IF()
        {
            Match("IF");
            exp();
            Match("THEN");
            stmt_seq();

            if (is_valid())
            {
                if (tokenType[Token] == "ELSE")
                {
                    Match("ELSE");
                    stmt_seq();
                }
            }
            Match("END");
        }

        public void exp()
        {
            simple_exp();

            if (is_valid())
            {
                //comp_op
                if ( tokenType[Token] == "EQUAL" )
                {
                    Match("EQUAL");
                    simple_exp();
                }
                else if ( tokenType[Token] == "LESSTHAN")
                {
                    Match("LESSTHAN");
                    simple_exp();
                }
            }
        }
       

        void simple_exp()
        {
            term();

            //add_op

            if (is_valid())
            {
                if (tokenType[Token] == "PLUS")
                {
                    Match("PLUS");
                     term();
                }
                else if ( tokenType[Token] == "MINUS")
                {
                    Match("MINUS");
                    term();
                }
            }

        }

        void term()
        {
            factor();

            //mul_op
            if (is_valid())
            {
                if (tokenType[Token] == "MULT")
                {
                    Match("MULT");
                    factor();
                }
                else if ( tokenType[Token] == "DIV")
                {
                    Match("DIV");
                    factor();
                }
            }

        }

        void factor()
        {
            if (is_valid())
            {
                if (tokenType[Token] == "OPENBRACKET")
                {
                    Match("OPENBRACKET");
                    exp();
                    Match("CLOSEDBRACKET");
                }
                else if (tokenType[Token] == "NUMBER")
                {
                    Match("NUMBER");
                }
                else if (tokenType[Token] == "IDENTIFIER")
                {
                    Match("IDENTIFIER");
                }
                else
                {
                    ERROR = true;
                }
            }
            else
            {
                ERROR = true ;
            }
        }



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
            try
            {
                //genBtn.Enabled = false; 
                tokenType.Clear();
                tokenValue.Clear();
                Output.Text = String.Empty;
                Token = 0;

                //list of TokenValue & list of TokenType
                for (int i = 0; i < input_size;)
                {
                    if (input[i] == ' ')
                    {
                        i++;
                    }
                    else
                    {
                        while ((i < input_size) && (input[i] != ',') && (input[i] != ' '))
                        {

                            char ip = input[i];
                            string input_s = ip.ToString();
                            int length = temp.Length;
                            temp = temp.Insert(length, input_s);

                            i++;
                        }
                        tokenValue.Add(temp);
                        while ((i < input_size) && input[i] == ' ')
                        {
                            i++;
                        }
                        if ((i < input_size) && (input[i] == ','))
                        {
                            i++;
                            temp = string.Empty;
                            while ((i < input_size) && (input[i] == ' '))
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
                                while ((i < input_size) && (input[i] == ' '))
                                {
                                    i++;
                                }
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

                    Output.Text += i + "   " + value + "   " + type + "\r\n";
                }

                if (tokenType.Count() == 0)
                {
                    ERROR = true;
                }
                else
                {
                    stmt_seq();
                }

                if (ERROR)
                {
                    Output.Text = String.Empty;
                    ERROR_show();
                }
            }
            catch (Exception)
            {
                Output.Text = String.Empty;
                ERROR_show();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Output_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Output.Text = String.Empty;
            InputText.Text = String.Empty;
            tokenType.Clear();
            tokenValue.Clear();
            Token = 0;
           // ERROR = false;
            input_size = 0;
            input = "";
            genBtn.Enabled = true;

        }
    }
}
