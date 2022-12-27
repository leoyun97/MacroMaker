using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChartHelper
{
    public partial class BpmFrm : Form
    {
        public BpmFrm()
        {
            InitializeComponent();
        }

        private double _seconds;

        private void timer1_Tick(object sender, EventArgs e)
        {
            IncreaseSeconds();
            ShowTime();
        }

        private void IncreaseSeconds()
        {
            //  if (_seconds ==59)
            //   {
            //       _seconds = 0;

            //   }
            //    else
            //   {
            _seconds++;
            //    }
        }




        private void ShowTime()
        {

            label3.Text = _seconds.ToString("00");
        }
        private void button3_Click(object sender, EventArgs e)
        {

            label1.Text = "00";
            label2.Text = "00";
            _seconds = 0;

            ShowTime();
        }


        public void BClickEvent()
        {




            double temDo;
            double bpmHeart;
            label1.Text = label3.Text.ToString();
            temDo = Convert.ToDouble(label1.Text.ToString()) / 60;

            if (temDo == 0)
            {
                bpmHeart = 0;
            }
            else
            {
                bpmHeart = 60 / temDo;
            }

            label2.Text = bpmHeart.ToString("0");

            //--------------------------------------------------------------------------

            if (Convert.ToDouble(label2.Text) != 0 && textBox1.Text == "0")
            {
                textBox1.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 && textBox2.Text == "0")
            {
                textBox2.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 && Convert.ToDouble(textBox2.Text) > 0 && textBox3.Text == "0")
            {
                textBox3.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 && Convert.ToDouble(textBox2.Text) > 0 && Convert.ToDouble(textBox3.Text) > 0 && textBox4.Text == "0")
            {
                textBox4.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 &&
                     Convert.ToDouble(textBox2.Text) > 0 &&
                     Convert.ToDouble(textBox3.Text) > 0 &&
                     Convert.ToDouble(textBox4.Text) > 0 &&
                     textBox5.Text == "0")
            {
                textBox5.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 &&
         Convert.ToDouble(textBox2.Text) > 0 &&
         Convert.ToDouble(textBox3.Text) > 0 &&
         Convert.ToDouble(textBox4.Text) > 0 &&
         Convert.ToDouble(textBox5.Text) > 0 &&
         textBox6.Text == "0")
            {
                textBox6.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 &&
Convert.ToDouble(textBox2.Text) > 0 &&
Convert.ToDouble(textBox3.Text) > 0 &&
Convert.ToDouble(textBox4.Text) > 0 &&
Convert.ToDouble(textBox5.Text) > 0 &&
Convert.ToDouble(textBox6.Text) > 0 &&
textBox7.Text == "0")
            {
                textBox7.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 &&
Convert.ToDouble(textBox2.Text) > 0 &&
Convert.ToDouble(textBox3.Text) > 0 &&
Convert.ToDouble(textBox4.Text) > 0 &&
Convert.ToDouble(textBox5.Text) > 0 &&
Convert.ToDouble(textBox6.Text) > 0 &&
Convert.ToDouble(textBox7.Text) > 0 &&
textBox8.Text == "0")
            {
                textBox8.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 &&
Convert.ToDouble(textBox2.Text) > 0 &&
Convert.ToDouble(textBox3.Text) > 0 &&
Convert.ToDouble(textBox4.Text) > 0 &&
Convert.ToDouble(textBox5.Text) > 0 &&
Convert.ToDouble(textBox6.Text) > 0 &&
Convert.ToDouble(textBox7.Text) > 0 &&
Convert.ToDouble(textBox8.Text) > 0 &&
textBox9.Text == "0")
            {
                textBox9.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 &&
Convert.ToDouble(textBox2.Text) > 0 &&
Convert.ToDouble(textBox3.Text) > 0 &&
Convert.ToDouble(textBox4.Text) > 0 &&
Convert.ToDouble(textBox5.Text) > 0 &&
Convert.ToDouble(textBox6.Text) > 0 &&
Convert.ToDouble(textBox7.Text) > 0 &&
Convert.ToDouble(textBox8.Text) > 0 &&
Convert.ToDouble(textBox9.Text) > 0 &&
textBox10.Text == "0")
            {
                textBox10.Text = label2.Text.ToString();
            }
            else if (Convert.ToDouble(textBox1.Text) > 0 &&
Convert.ToDouble(textBox2.Text) > 0 &&
Convert.ToDouble(textBox3.Text) > 0 &&
Convert.ToDouble(textBox4.Text) > 0 &&
Convert.ToDouble(textBox5.Text) > 0 &&
Convert.ToDouble(textBox6.Text) > 0 &&
Convert.ToDouble(textBox7.Text) > 0 &&
Convert.ToDouble(textBox8.Text) > 0 &&
Convert.ToDouble(textBox9.Text) > 0 &&
Convert.ToDouble(textBox10.Text) > 0)
            {
                double Asum;
                Asum = (
                       Convert.ToDouble(textBox2.Text) +
                       Convert.ToDouble(textBox3.Text) +
                       Convert.ToDouble(textBox4.Text) +
                       Convert.ToDouble(textBox5.Text) +
                       Convert.ToDouble(textBox6.Text) +
                       Convert.ToDouble(textBox7.Text) +
                       Convert.ToDouble(textBox8.Text) +
                       Convert.ToDouble(textBox9.Text) +
                       Convert.ToDouble(textBox10.Text)) / 9;
                //---------------------> 9회 평균값인 이유는 1번째는 항상 적게 나오는 경향이 있음

                //MessageBox.Show("10회 측정 평균값 :" + Environment.NewLine + Asum.ToString("0"));

                label5.Visible = true;
                label5.Text = "Average : " + Asum.ToString("0");

                textBox1.Text = "0";
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox4.Text = "0";
                textBox5.Text = "0";
                textBox6.Text = "0";
                textBox7.Text = "0";
                textBox8.Text = "0";
                textBox9.Text = "0";
                textBox10.Text = "0";

                label1.Text = "00";
                label2.Text = "00";

            }


            //--------------------------------------------------------------------------


            _seconds = 0;

            ShowTime();

            timer1.Enabled = true;






        }

        private void button1_Click(object sender, EventArgs e)
        {
            //double temDo;
            //double bpmHeart;
            //label1.Text = label3.Text.ToString();
            //temDo = Convert.ToDouble(label1.Text.ToString()) / 60;
            //bpmHeart = 60 / temDo;
            //label2.Text = bpmHeart.ToString("0");
            //_seconds = 0;

            //ShowTime();

            //timer1.Enabled = true;

            BClickEvent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            label1.Text = "00";
            label2.Text = "00";
            label5.Visible = false;
            _seconds = 0;

            ShowTime();
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BClickEvent();
            }
        }

        private void BpmFrm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            textBox5.Text = "0";
            textBox6.Text = "0";
            textBox7.Text = "0";
            textBox8.Text = "0";
            textBox9.Text = "0";
            textBox10.Text = "0";
            label6.Text = "Copyrights(c)2018 Nok-Yang Animal Clinic";
        }
    }
}
