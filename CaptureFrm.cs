using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.VisualBasic;

namespace ChartHelper
{
    public partial class CaptureFrm : Form
    {

      // OleDbConnection DBconn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = DbPath.dat");

        public string savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        public string DbPath = "DbPath.txt";
        


        public CaptureFrm()
        {
            InitializeComponent();
        }


        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public void SavePic()
        {

            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;

            EncoderParameters myEncoderParameters = new EncoderParameters(1);



            try
            {
                string DAT1 = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string Fname = DAT1 + @".jpg";

                //   string LS = comboBox1.Text.ToString() + "L";

                int Lscore = Convert.ToInt16(comboBox1.Text.ToString());

                //MessageBox.Show(Lscore.ToString());

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, Lscore);
                myEncoderParameters.Param[0] = myEncoderParameter;


                pictureBox1.Image.Save(label1.Text + @"\" + Fname, jpgEncoder, myEncoderParameters);



            }
            catch (Exception)
            {

                MessageBox.Show("Upload image first! or Error");
            }


        }




        public void PutPic()
        {

            pictureBox1.Image = Clipboard.GetImage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SavePic();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Clipboard.GetImage();
        }

        public void openSaveDir()
        {
            string[] OptList = { savePath, "100", "50" };

            string FROM_DIRECTORY;

            FolderBrowserDialog FBD = new FolderBrowserDialog();

            FBD.ShowDialog();
            FROM_DIRECTORY = FBD.SelectedPath;
            label1.Text = FROM_DIRECTORY.ToString();

            if (label1.Text.Equals(null) || label1.Text.Equals(""))
            {
                MessageBox.Show("취소");
                DBpath_Load();
            }
            else
            {

                if (File.Exists(DbPath))
                {
                    var lineCount = File.ReadLines(DbPath).Count();
                    if (lineCount == 3)
                    {
                        string[] Optionlist = File.ReadAllLines(DbPath);

                        TxtlineChanger(FROM_DIRECTORY.ToString(), DbPath, 1);

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






                //DBconn.Open();

                ////string picPaths = openDB.FileName.ToString();

                //OleDbCommand DBCmd = new OleDbCommand(@"UPDATE db SET P_path = '" + FROM_DIRECTORY.ToString() + "'", DBconn);

                //DBCmd.ExecuteNonQuery();
                //DBconn.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("이미지를 저장할 폴더를 변경하시겠습니까?", "Directory Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                openSaveDir();

            }

            
        }

        static void TxtlineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
        }


        private void DBpath_Load()
        {
            string[] OptList = { savePath, "100", "50" };

            if (!File.Exists(DbPath))
            {
                System.IO.File.WriteAllLines(DbPath, OptList);
            }
            else
            {
                String[] lines = File.ReadAllLines(DbPath);
                if (lines[1] == "" || Convert.ToInt32(lines[1]) > 100)
                {
                    label1.Text = savePath;
                    comboBox1.Text = "100";
                }
                else
                {

                    label1.Text = lines[0];
                    comboBox1.Text = lines[1];

                }
            }




            //DBconn.Open();
            //OleDbDataAdapter txtpath = new OleDbDataAdapter("SELECT * FROM db", DBconn);
            //DataTable pathTB = new DataTable();
            //txtpath.Fill(pathTB);
            //DBconn.Close();
            //label1.Text = pathTB.Rows[0]["P_path"].ToString();
            //comboBox1.Text = pathTB.Rows[0]["P_size"].ToString();
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)     // 단축키
        {
            Keys key = keyData & ~(Keys.Shift | Keys.Control);

            switch (key)
            {
                case Keys.V:
                    if ((keyData & Keys.Control) != 0)
                    {
                        PutPic();
                        return true;
                    }
                    break;

            }

            switch (key)
            {
                case Keys.S:
                    if ((keyData & Keys.Control) != 0)
                    {

                        SavePic();
                        return true;
                    }
                    break;

            }

            switch (key)
            {
                case Keys.D:
                    if ((keyData & Keys.Control) != 0)
                    {

                        DragFrmOpen();
                        return true;
                    }
                    break;

            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            // OleDbConnection DBconn = new OleDbConnection(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = PDb.dat");

            //string realPath;

            //DBconn.Open();
            //OleDbDataAdapter txtpath = new OleDbDataAdapter("SELECT * FROM db", DBconn);
            //DataTable pathTB = new DataTable();
            //txtpath.Fill(pathTB);
            //DBconn.Close();
            //realPath = pathTB.Rows[0]["P_path"].ToString();


            //if (!Directory.Exists(realPath))
            //{
            //    MessageBox.Show("폴더가 없습니다. 링크가 변경되었거나 DB경로가 잘못 설정되있을 수 있습니다.");

            //}
            //else if (Directory.Exists(realPath))
            //{

            //    System.Diagnostics.Process program = System.Diagnostics.Process.Start(realPath);

            //}
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] OptList = { savePath, "100", "50" };

            if (File.Exists(DbPath))
            {
                var lineCount = File.ReadLines(DbPath).Count();
                if (lineCount == 3)
                {
                    string[] Optionlist = File.ReadAllLines(DbPath);

                    TxtlineChanger(comboBox1.Text.ToString(), DbPath, 2);

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





            //DBconn.Open();

            //OleDbCommand DBCmd = new OleDbCommand(@"UPDATE db SET P_size = '" + comboBox1.Text.ToString() + "'", DBconn);

            //DBCmd.ExecuteNonQuery();
            //DBconn.Close();
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openMPic = new OpenFileDialog();
            openMPic.InitialDirectory = "";
            openMPic.Filter = "Image Files (*.jpg)|*.jpg|All Files(*.*)|*.*";
            openMPic.FilterIndex = 1;

            if (openMPic.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openMPic.CheckFileExists)
                {
                    string picPaths = Environment.CurrentDirectory;

                    pictureBox1.Image = Image.FromFile(openMPic.FileName);

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string realPath = label1.Text;

            if (!Directory.Exists(realPath))
            {
                MessageBox.Show("폴더가 없습니다. 링크가 변경되었거나 DB경로가 잘못 설정되있을 수 있습니다.");

            }
            else if (Directory.Exists(realPath))
            {

                System.Diagnostics.Process program = System.Diagnostics.Process.Start(realPath);

            }

            //label1_DoubleClick(sender, e);
        }

        private void dragCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DragFrmOpen();
        }

        public void DragFrmOpen()
        {
            this.Hide();
            CapWin CWF = new CapWin();
            CWF.Opacity = 0.5;
            CWF.ShowDialog();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            //프로그램 종료
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePic();
        }

        private void putImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PutPic();
        }

        private void CaptureFrm_Load(object sender, EventArgs e)
        {
            DBpath_Load();
        }
    }
}
