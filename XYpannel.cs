using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MacroMaker
{
    public partial class XYpannel : Form
    {
        public XYpannel()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            this.Hide();
            MainFrom MF = new MainFrom();
            MF.XYpannelXYVal = e.X.ToString() +","+ e.Y.ToString();
            MF.ShowDialog();
            this.Close();
        }
    }
}
