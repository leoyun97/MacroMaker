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
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Messaging;
using Microsoft.VisualBasic;
using ChartHelper;

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
        public string[] btnNameList = { "Empty1", "Empty2", "Empty3", "Empty4", "Empty5", "Empty6", "Empty7", "Empty8", "Empty9", "Empty10" };
        public string SelectCom = "SelectCom.txt";
        public string comNo;
        public string savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        public string DbPath = "DbPath.txt";


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
            if (FulTxt !="")
            {
                Clipboard.SetText(FulTxt, TextDataFormat.Rtf); //서식있는 텍스트 가져올때
            }
            


            //Clipboard.SetText(FulTxt);     //메모장에서 글씨만 가져올때
            // var CoBoTxt = Clipboard.GetText();     // 클립보드에서 text 가져오기

        }

        private void MainFrom_Load(object sender, EventArgs e)
        {
            comboxNo(); // 콤보박스 selectindex 파일 없다면 만들고

            //FrmSize();
            Size = new Size(154, 1);

            if (XYpannelXYVal!=null)
            {
                string[] xypointArr = XYpannelXYVal.Split(',');
                XaxisVal = xypointArr[0];
                YaxisVal = xypointArr[1];
                XYpannelXYVal = "";
            }

            GetBtnName5(); //라벨이름이 있는 텍스트파일이 없다면 만들고
            IfLostRtfFile(); //rtf파일이 폴더에 없다면 만들고
            comboxNo(); // 콤보박스 selectindex 파일 없다면 만들고
            capOption(); // 드래그 캡춰 경로파일 없으면 만들고 경로가 존재하는지도 확인
        }

        public void capOption() //드래그 캡춰에 필요한 경로 및 이미지 배율과 번역기 투명도
        {
            string[] OptList = { savePath, "100", "50" };

            if (File.Exists(DbPath))
            {
                var lineCount = File.ReadLines(DbPath).Count();
                if (lineCount == 3)
                {
                    String[] lines = File.ReadAllLines(DbPath);
                    string capPath = lines[0];
                    DirectoryInfo dirInfo = new DirectoryInfo(capPath);
                    if (!dirInfo.Exists )
                    {
                        System.IO.File.WriteAllLines(DbPath, OptList);
                    }

                }
                else
                {
                    System.IO.File.WriteAllLines(DbPath, OptList);
                }


            }
            else if (!File.Exists(DbPath))
            {
                System.IO.File.WriteAllLines(DbPath, OptList);
            }
        }

        public void comboxNo()
        {
            if (!File.Exists(SelectCom))
            {
                StreamWriter sw = new StreamWriter(SelectCom);
                sw.Write(10);
                sw.Close();
            }
            else
            {
                String[] lines = File.ReadAllLines(SelectCom);
                //var lineCount = File.ReadLines(SelectCom).Count();
                if (lines[0] == "" || Convert.ToInt32(lines[0])>10)
                {
                    comboBox1.Text = "10";
                }
                else
                {

                    comboBox1.Text = lines[0].ToString();

                }
            }
        }

   

        public void FrmSize()
        {
            string howmanyBt = comboBox1.SelectedItem as string;
            if (howmanyBt =="1")
            {
                Size = new Size(154, 111);
            }
            else if (howmanyBt == "2")
            {
                Size = new Size(154, 150);
            }
            else if (howmanyBt == "3")
            {
                Size = new Size(154, 185);
            }
            else if (howmanyBt == "4")
            {
                Size = new Size(154, 222);
            }
            else if (howmanyBt == "5")
            {
                Size = new Size(154, 260);
            }
            else if (howmanyBt == "6")
            {
                Size = new Size(154, 295);
            }
            else if (howmanyBt == "7")
            {
                Size = new Size(154, 330);
            }
            else if (howmanyBt == "8")
            {
                Size = new Size(154, 365);
            }
            else if (howmanyBt == "9")
            {
                Size = new Size(154, 400);
            }
            else if (howmanyBt == "10")
            {
                Size = new Size(154, 435);
            }
        }

        public void ChangeBtnTxt()
        {
            string labelName = "label" + 1 + ".Text";
            string btnTxtFileTexts = File.ReadAllText(btnTxtFile);
            btnTxtFileTexts = btnTxtFileTexts.Replace(labelName, "접종");
            File.WriteAllText(btnTxtFile, btnTxtFileTexts);
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
            if (!File.Exists(btnTxtFile))
            {
               // string[] btnNameList = { "Empty1","Empty2", "Empty3", "Empty4", "Empty5", "Empty6", "Empty7", "Empty8", "Empty9", "Empty10" };
                System.IO.File.WriteAllLines(btnTxtFile, btnNameList);
            }
            else
            {
                var lineCount = File.ReadLines(btnTxtFile).Count();
                if (lineCount == 10)
                {
                    String[] lines = File.ReadAllLines(btnTxtFile);

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
                else
                {
                    //string[] btnNameList = { "Empty1", "Empty2", "Empty3", "Empty4", "Empty5", "Empty6", "Empty7", "Empty8", "Empty9", "Empty10" };
                   // System.IO.File.WriteAllLines(btnTxtFile, btnNameList);

                    StreamWriter sw = new StreamWriter(btnTxtFile,false);

                    for (int i = 0; i < 10; i++)
                    {
                        sw.WriteLine(btnNameList[i]);
                    }
                    
                    
                    sw.Close();

                }
              
            }

        }


        private void button2_Click(object sender, EventArgs e) // 클릭실행
        {
         
           
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



        private void label1_Click(object sender, EventArgs e)
        {
            BtnNum = 1;
            label1.BackColor = Color.Black;
            getTxtfromClip();
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);

        }

        private void label2_Click(object sender, EventArgs e)
        {
            BtnNum = 2;
            getTxtfromClip();
            label2.BackColor = Color.Black;
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            BtnNum = 3;
            getTxtfromClip();
            label3.BackColor = Color.Black;
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            BtnNum = 4;
            getTxtfromClip();
            label4.BackColor = Color.Black;
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            BtnNum = 5;
            getTxtfromClip();
            label5.BackColor = Color.Black;
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            BtnNum = 6;
            getTxtfromClip();
            label6.BackColor = Color.Black;
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            BtnNum = 7;
            getTxtfromClip();
            label7.BackColor = Color.Black;
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            BtnNum = 8;
            getTxtfromClip();
            label8.BackColor = Color.Black;
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            BtnNum = 9;
            getTxtfromClip();
            label9.BackColor = Color.Black;
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            BtnNum = 10;
            getTxtfromClip();
            label10.BackColor = Color.Black;
            AutoclosingMbox.Show("Copied", "원하는곳에 Ctrl+V", 500);
            Size = new Size(154, 1);
        }

        public void FrmReload()
        {
            this.Hide();
            MainFrom MF = new MainFrom();
            MF.ShowDialog();
            this.Close();   
        }

        private void 버튼명입력ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(btnTxtFile))
            {
                var lineCount = File.ReadLines(btnTxtFile).Count();
                if (lineCount== 10)
                {
                    string[] btnNamelist = File.ReadAllLines(btnTxtFile);

                    string bxinput;
                    bxinput = Interaction.InputBox("교체할 버튼명을 입력하세요.", "현재 버튼명 =  " + btnNamelist[BtnNum - 1], "", -1, -1);

                    if (bxinput != "")
                    {
                        TxtlineChanger(bxinput, btnTxtFile, BtnNum);
                        FrmReload();
                    }
                }
                else
                {
                    System.IO.File.WriteAllLines(btnTxtFile, btnNameList);
                }
                
                
            }
            else if (!File.Exists(btnTxtFile))
            {
                System.IO.File.WriteAllLines(btnTxtFile, btnNameList);
            }
            // Process.Start(Application.StartupPath + "\\"+btnTxtFile);
        }

        static void TxtlineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }

        private void contentsWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BtnNum==1)
            {
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }

            }
            else if (BtnNum==2)
            {
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }
            }
            else if (BtnNum == 3)
            {
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }

            }
            else if (BtnNum == 4)
            {
                
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }

            }
            else if (BtnNum == 5)
            {
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }

            }
            else if (BtnNum == 6)
            {
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }

            }
            else if (BtnNum ==7)
            {
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }

            }
            else if (BtnNum == 8)
            {
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }

            }
            else if (BtnNum == 9)
            {
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }

            }
            else if (BtnNum == 10)
            {
                string ReadNwWord = "btntxt" + BtnNum.ToString() + ".rtf";
                if (File.Exists(ReadNwWord))
                {
                    Process.Start(Application.StartupPath + "\\" + ReadNwWord);
                }

            }



        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.ForeColor = Color.Red;
            label1.BackColor = Color.White;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.ForeColor = Color.White;
            label1.BackColor = Color.SlateGray;
        }

        private void label2_MouseHover(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Red;
            label2.BackColor = Color.White;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.White;
            label2.BackColor = Color.SlateGray;
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Red;
            label3.BackColor = Color.White;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.White;
            label3.BackColor = Color.SlateGray;
        }

        private void label4_MouseHover(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
            label4.BackColor = Color.White;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
            label4.BackColor = Color.SlateGray;
        }

        private void label5_MouseHover(object sender, EventArgs e)
        {
            label5.ForeColor = Color.Red;
            label5.BackColor = Color.White;
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.White;
            label5.BackColor = Color.SlateGray;
        }

        private void label6_MouseHover(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Red;
            label6.BackColor = Color.White;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
            label6.BackColor = Color.SlateGray;
        }

        private void label7_MouseHover(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Red;
            label7.BackColor = Color.White;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.White;
            label7.BackColor = Color.SlateGray;
        }

        private void label8_MouseHover(object sender, EventArgs e)
        {
            label8.ForeColor = Color.Red;
            label8.BackColor = Color.White;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            label8.ForeColor = Color.White;
            label8.BackColor = Color.SlateGray;
        }

        private void label9_MouseHover(object sender, EventArgs e)
        {
            label9.ForeColor = Color.Red;
            label9.BackColor = Color.White;
        }

        private void label9_MouseLeave(object sender, EventArgs e)
        {
            label9.ForeColor = Color.White;
            label9.BackColor = Color.SlateGray;
        }

        private void label10_MouseHover(object sender, EventArgs e)
        {
            label10.ForeColor = Color.Red;
            label10.BackColor = Color.White;
        }

        private void label10_MouseLeave(object sender, EventArgs e)
        {
            label10.ForeColor = Color.White;
            label10.BackColor = Color.SlateGray;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmSize();

            StreamWriter sw = new StreamWriter(SelectCom, false);

                sw.WriteLine(comboBox1.Text.ToString());
                sw.Close();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
       

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 1;
            }
        }

        private void label2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 2;
            }
        }

        private void label3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 3;
            }
        }

        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 4;
            }
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 5;
            }
        }

        private void label6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 6;
            }
        }

        private void label7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 7;
            }
        }

        private void label8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 8;
            }
        }

        private void label9_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 9;
            }
        }

        private void label10_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                BtnNum = 10;
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & ~(Keys.Shift | Keys.Control);

            switch (key)
            {
                case Keys.Q:
                    if ((keyData & Keys.Control) != 0)
                    {
                        this.WindowState = FormWindowState.Minimized;
                        return true;
                    }
                    break;

                case Keys.F1:
                    if ((keyData & Keys.Control) != 0)
                    {
                        label12_Click(this, null);
                        return true;
                    }
                    break;

                case Keys.E:
                    if ((keyData & Keys.Control) != 0)
                    {
                        TranslatorFrm TransFrm = new TranslatorFrm();
                        TransFrm.ShowDialog();
                        return true;
                    }
                    break;

                case Keys.B:
                    if ((keyData & Keys.Control) != 0)
                    {
                        BpmFrm BFrm = new BpmFrm();
                        BFrm.ShowDialog();
                        return true;
                    }
                    break;

                case Keys.D:
                    if ((keyData & Keys.Control) != 0)
                    {
                        CaptureFrm CF = new CaptureFrm();
                        CF.Show();
                        return true;
                    }
                    break;

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void label12_Click(object sender, EventArgs e)
        {
            MessageBox.Show("타이틀바 클릭시 버튼이 보이고 한번더 클릭하면 버튼 사라집니다."
                              + Environment.NewLine+Environment.NewLine + "  1.  마우스 우클릭으로 버튼명입력(수정) 하기"
                             + Environment.NewLine + "  2.  마우스 우클릭 후 내용변경 눌러 원하는 내용 입력 후 저장"
                             + Environment.NewLine + "  3.  버튼클릭 후 원하는 곳에 Ctrl+V 하세요"
                             + Environment.NewLine + "  4.  콤보박스 눌러서 버튼갯수 정하기(최대10개)"
                             + Environment.NewLine+ Environment.NewLine + "Ctrl+A : Drag & Save"
                             + Environment.NewLine + "Ctrl+T : 한타/영타 번역기"
                             + Environment.NewLine + "Ctrl+H : BPM 카운터"
                             + Environment.NewLine + "Ctrl+Q : 최소화"
                             + Environment.NewLine + "Ctrl+F1 : 도움말"
                             , "Inform");
        }


        const int WM_NCLBUTTONDOWN = 0x00A1;

        string nansu;

        protected override void WndProc(ref Message m)
        {

            base.WndProc(ref m);
            // no client area
            if (m.Msg == WM_NCLBUTTONDOWN)
            {
                //select tittle area only If (){
                this.Cursor = new Cursor(Cursor.Current.Handle);
                if (nansu == null || nansu =="")
                {
                    FrmSize();
                    nansu = "1";
                }
                else if (nansu == "1")
                {
                    Size = new Size(154, 1);
                    nansu = "";
                }
                
                //}
            }
        }

        private void FoldeOpenTab_Click(object sender, EventArgs e)
        {
            string pathName = Application.StartupPath;
            if (!Directory.Exists(pathName))
            {
                MessageBox.Show("Root folder가 없거나 삭제되었음");
            }
            else
            {
                Process.Start(pathName);
                
                //FolderBrowserDialog FD = new FolderBrowserDialog();
                //FD.SelectedPath= pathName;  
                //FD.ShowDialog();
            }

        }

        private void EncryptorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TranslatorFrm TransFrm = new TranslatorFrm();
            TransFrm.ShowDialog();
        }

        private void bPMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BpmFrm BFrm = new BpmFrm();
            BFrm.ShowDialog();
        }

        private void captureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureFrm CF = new CaptureFrm();
            CF.Show();
        }
    }
}
