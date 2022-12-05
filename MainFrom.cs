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

        [DllImport("user32.dll")]
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


        private void button1_Click(object sender, EventArgs e)
        {

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

            SendKeys.SendWait("(^v)");
            //SendKeys.SendWait("{ENTER}");
        }


        public void getTxtfromClip() // 메모장 내용 클립보드로 복사
        {
            string ReadNwWord = "wordEx1.txt";
            string FulTxt = System.IO.File.ReadAllText(ReadNwWord);

            Clipboard.SetText(FulTxt);

            // 클립보드에서 text 가져오기
            var CoBoTxt = Clipboard.GetText();

        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            string[] xypointArr = XYpannelXYVal.Split(',');
            XaxisVal = xypointArr[0];
            YaxisVal = xypointArr[1];
            XYpannelXYVal = "";

        }

        private void button2_Click(object sender, EventArgs e) // 클릭실행
        {
                      
        }

    


        private void button3_Click(object sender, EventArgs e) //실행
        {
                       
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
    }
}
