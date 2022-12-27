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
    public partial class CapWin : Form
    {
        Point startPos;

        Point currentPos;

        double RtcornerX;
        double RtcornerY;
        double LtcornerX;
        double LtcornerY;

        bool drawing;

        List<Rectangle> rectangles = new List<Rectangle>();

        BufferedGraphicsContext graphicsContext;

        BufferedGraphics graphics;

        Rectangle rectangle;


        public CapWin()
        {
            InitializeComponent();
            graphicsContext = BufferedGraphicsManager.Current;
            graphicsContext.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
        }

        private Rectangle getRectangle()

        {


            rectangle = new Rectangle(

            Math.Min(startPos.X, currentPos.X),

            Math.Min(startPos.Y, currentPos.Y),

            Math.Abs(startPos.X - currentPos.X),

            Math.Abs(startPos.Y - currentPos.Y));

            return rectangle;

        }

        public void LoadForms()
        {
            this.Opacity = 0.5;
            //this.BackColor = Color.Fuchsia;
            // TransparencyKey = Color.Fuchsia;
        }





        public void OutForm()
        {
            //MirrorFrm MF = new MirrorFrm();
            //MF.Xlocation = LtcornerX;
            //MF.Ylocation = LtcornerY;
            //MF.WidthVal = RtcornerX;
            //MF.HeightVal = RtcornerY;
            //MF.Width = (int)Math.Abs(RtcornerX - LtcornerX);
            //MF.Height = (int)Math.Abs(RtcornerY - LtcornerY);

            //MF.Show();
            //MF.startMirror();

            CopytoClip();

            CaptureFrm CF = new CaptureFrm();
            CF.PutPic();
            CF.Show();

            this.Close();

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawing)

            {

                drawing = false;

                var rc = getRectangle();

                if (rc.Width > 0 && rc.Height > 0) rectangles.Add(rc);


            }

            OutForm();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            currentPos = e.Location;

            if (drawing)

            {
                SolidBrush blueBrush = new SolidBrush(Color.Blue);
                graphics.Graphics.Clear(SystemColors.Control);

                graphics.Graphics.DrawRectangle(Pens.Green, getRectangle()); // 사각형 테두리

                graphics.Graphics.FillRectangle(blueBrush, getRectangle()); // 사각형 채우기

                if (rectangles.Count > 0)  // if 절을 활성화하면 여러개의 사각형을 그림

                {

                    //graphics.Graphics.DrawRectangles(Pens.Green, rectangles.ToArray());

                }

                graphics.Render(pictureBox1.CreateGraphics());

                LtcornerX = startPos.X;
                LtcornerY = startPos.Y;
                RtcornerX = currentPos.X;
                RtcornerY = currentPos.Y;

            }

            TransparencyKey = Color.Blue;
        }

        public void CopytoClip()
        {
            Bitmap BTM = new Bitmap(Convert.ToInt32(rectangle.Width), Convert.ToInt32(rectangle.Height));
            Graphics Graf = Graphics.FromImage(BTM);
            Graf.CopyFromScreen(Convert.ToInt32(startPos.X), Convert.ToInt32(startPos.Y), 0, 0, BTM.Size, CopyPixelOperation.SourceCopy);
            Clipboard.SetImage(BTM);
            //GC.Collect();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            currentPos = startPos = e.Location;

            drawing = true;

            graphics = graphicsContext.Allocate(pictureBox1.CreateGraphics(), new Rectangle(new Point(0, 0), pictureBox1.Size));
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OutForm();
        }
    }
}
