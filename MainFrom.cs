﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace MacroMaker
{
    public partial class MainFrom : Form
    {

        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;      // The left button is down.
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;        // The left button is up.


        public MainFrom()
        {
            InitializeComponent();

        }

        public string XYpannelXYVal;
        


        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = Cursor.Position.X.ToString();
            label4.Text = Cursor.Position.Y.ToString();
        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            string[] xypointArr = XYpannelXYVal.Split(',');
            label3.Text = xypointArr[0];
            label4.Text = xypointArr[1];
        }
    }
}