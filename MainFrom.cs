using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Timers;

namespace MacroMaker
{
    public partial class MainFrom : Form
    {

        [DllImport("user32.dll")] //-- 마우스 클릭 관련
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;      // The left button is down.
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;        // The left button is up.





        public MainFrom()
        {
            InitializeComponent();

        }





        public string XYpannelXYVal;
        public string XaxisVal;
        public string YaxisVal;
        public string btnTxtFile = "BtnName.txt";
        public int BtnNum;  //라벨넘버 




        private void button1_Click(object sender, EventArgs e)
        {
            openXYpannel();
        }

        public void openXYpannel() //좌표측정
        {
            this.Hide();
            XYpannel XYP = new XYpannel();
            XYP.ShowDialog();
            this.Close();
        }

        public void copyTointoVet() //지정 좌표에 붙여넣기
        {
            Cursor.Position = new Point(Convert.ToInt32(XaxisVal), Convert.ToInt32(YaxisVal));
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            //Thread.Sleep(300);
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait("^v");
            
           
        }


        public void getTxtfromClip() // 메모장 내용 클립보드로 복사
        {
            string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf"; 
            string FulTxt = System.IO.File.ReadAllText(ReadNwWord);
            Clipboard.SetText(FulTxt, TextDataFormat.Rtf); //서식있는 텍스트 가져올때


            //Clipboard.SetText(FulTxt);     //메모장에서 글씨만 가져올때
            // var CoBoTxt = Clipboard.GetText();     // 클립보드에서 text 가져오기

        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            if (XYpannelXYVal!=null)
            {
                string[] xypointArr = XYpannelXYVal.Split(',');
                XaxisVal = xypointArr[0];
                YaxisVal = xypointArr[1];
                XYpannelXYVal = "";
            }

            GetBtnName5(); //라벨이름이 있는 텍스트파일이 없다면 만들고
            IfLostRtfFile(); //rtf파일이 폴더에 없다면 만들고

        }

        public void IfLostRtfFile()
        {
            for (int i = 1; i < 11; i++)
            {
                string ReadNwWord = "btntxt" + i.ToString() + ".rtf";

                if (!File.Exists(ReadNwWord))
                {
                   // File.Create(ReadNwWord);
                    File.WriteAllText(ReadNwWord,"");
                }
            }
            

        }

        public void GetBtnName5()
        {
           // string btnTxtFile = "ButtonName.txt";
            if (!File.Exists(btnTxtFile))
            {
                string[] btnNameList = { "Empty1","Empty2", "Empty3", "Empty4", "Empty5", "Empty6", "Empty7", "Empty8", "Empty9", "Empty10" };
                System.IO.File.WriteAllLines(btnTxtFile, btnNameList);
            }
            else
            {
                String[] lines = File.ReadAllLines(btnTxtFile);
               // var lineCount = File.ReadLines(btnTxtFile).Count();

                label1.Text = lines[0].ToString();
                label2.Text = lines[1].ToString();
                label3.Text = lines[2].ToString();
                label4.Text = lines[3].ToString();
                label5.Text = lines[4].ToString();
                label6.Text = lines[5].ToString();
                label7.Text = lines[6].ToString();
                label8.Text = lines[7].ToString();
                label9.Text = lines[8].ToString();
                label10.Text = lines[9].ToString();
            }

        }


        private void button2_Click(object sender, EventArgs e) // 클릭실행
        {
            //copyTointoVet(); //지정 좌표에 붙여넣기  
                    
           
        }

    


        private void button3_Click(object sender, EventArgs e) //Txt파일에서 클립보드로 내용 복사
        {
            getTxtfromClip();

            
        }




        private void button4_Click(object sender, EventArgs e) //word파일 만들기
        {

            string MakeNwWord = "wordEx2.txt";

            string[] textVal = {XaxisVal.ToString(),YaxisVal.ToString()};
            System.IO.File.WriteAllLines(MakeNwWord,textVal);


        }

        private void 좌표선택ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openXYpannel();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
            BtnNum = 1;
            getTxtfromClip();

        }

        private void label2_Click(object sender, EventArgs e)
        {
            BtnNum = 2;
            getTxtfromClip();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            BtnNum = 3;
            getTxtfromClip();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            BtnNum = 4;
            getTxtfromClip();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            BtnNum = 5;
            getTxtfromClip();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            BtnNum = 6;
            getTxtfromClip();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            BtnNum = 7;
            getTxtfromClip();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            BtnNum = 8;
            getTxtfromClip();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            BtnNum = 9;
            getTxtfromClip();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            BtnNum = 10;
            getTxtfromClip();
        }
    }
}
