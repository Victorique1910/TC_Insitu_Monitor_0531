using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC_Insitu_Monitor
{
    public partial class EmailInputBox : Form
    {
        public EmailInputBox()
        {
            InitializeComponent();
        }
        public string Msg { get; private set; }
        private void Button_OK_Click(object sender, EventArgs e)
        {
            Msg = textBox_EmailInputBox.Text;
        }
    }
}
