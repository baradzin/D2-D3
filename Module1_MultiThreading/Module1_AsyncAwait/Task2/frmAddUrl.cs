using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class frmAddUrl : Form
    {
        public frmAddUrl()
        {
            InitializeComponent();
        }

        public string Url { get; set; }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Url = textBox1.Text;
        }
    }
}
