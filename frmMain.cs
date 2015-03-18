using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IcerImageProcessing;
using System.Drawing.Drawing2D;

namespace ConcreteDetect
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        class ONEPICTUREWITHANCHORPOINT
        {
            public Image img;
            public Point[] anchor;// = new Point[3];
            private int _width;
            private int _height;

            public int Width
            {
                get { return _width; }
            }

            public int Height
            {
                get { return _height; }
            }

            public ONEPICTUREWITHANCHORPOINT()
            {
            }

            public ONEPICTUREWITHANCHORPOINT(string filename)
            {
                Init(filename, new Point((int)(_width * 0.2), (int)(_height * 0.2)),
                    new Point((int)(_width * 0.8), (int)(_height * 0.2)), new Point((int)(_width * 0.5), (int)(_height * 0.8)));
            }

            public ONEPICTUREWITHANCHORPOINT(string filename, Point p1, Point p2, Point p3)
            {
                Init(filename, p1, p2, p3);
            }

            private void Init(string filename, Point p1, Point p2, Point p3)
            {
                img = Image.FromFile(filename);
                _width = img.Width;
                _height = img.Height;
                anchor = new Point[3];
                anchor[0] = p1;
                anchor[1] = p2;
                anchor[2] = p3;
            }

            public static Image operator -(ONEPICTUREWITHANCHORPOINT opic1, ONEPICTUREWITHANCHORPOINT opic2)
            {
                Image timg = Basic.getImageFromGray(
                    Morphologic.Minus(Basic.getGrayFromImage(opic1.img), Basic.getGrayFromImage(opic2.img),
                    opic1.Width, opic1.Height), opic1.Width, opic1.Height);
                return timg;
            }
        }

        ONEPICTUREWITHANCHORPOINT[] opic = new ONEPICTUREWITHANCHORPOINT[3];

        private void btnMinus_Click(object sender, EventArgs e)
        {
            //Image[] img = new Image[3];
            //img[0] = Image.FromFile("pic1.jpg");
            //img[1] = Image.FromFile("pic2.jpg");
            //img[2] = Image.FromFile("pic3.jpg");
            opic[0] = new ONEPICTUREWITHANCHORPOINT("pic4.jpg", new Point(619, 135), new Point(1777, 271), new Point(1228, 1558));
            opic[1] = new ONEPICTUREWITHANCHORPOINT("pic5.jpg", new Point(630, 128), new Point(1786, 271), new Point(1234, 1562));
            opic[2] = new ONEPICTUREWITHANCHORPOINT("pic3.jpg");

            //double theta = 90;
            //picMain.Image = Transformation.ResizeImage(opic[0].img, 0.2);
            //picMain.Image = Transformation.RotateImage(picMain.Image, (float)theta, Color.White);

            //double angle = Math.PI * theta / 180.0;
            //int rx = (int)(opic[0].anchor[0].X * 0.2);
            //int ry = (int)(opic[0].anchor[0].Y * 0.2);
            //int nx = (int)(rx * Math.Cos(angle) - ry * Math.Sin(angle));
            //int ny = (int)(rx * Math.Sin(angle) + ry * Math.Cos(angle));

            //MessageBox.Show(string.Format("origin coor;({0},{1})\r\nnew coor:({2},{3})", rx, ry, nx, ny));

            //picMain.CreateGraphics().DrawRectangle(new Pen(new SolidBrush(Color.Red)), new Rectangle(nx, ny, 2, 2));


            //for (int i = 0; i < 2; ++i)
            //{
            //    for (int j = i + 1; j < 3; ++j)
            //    {
            //        Image timg = Transformation.ResizeImage(Basic.getImageFromGray(
            //            Morphologic.Minus(Basic.getGrayFromImage(opic[i].img), Basic.getGrayFromImage(opic[j].img),
            //            opic[i].Width, opic[i].Height), opic[i].Width, opic[i].Height),1);
            //        timg.Save(string.Format("pic{0}-{1}.jpg", i, j));
            //    }
            //}
            //Image oimg = Transformation.ResizeImage(Basic.getImageFromGray(
            //    Morphologic.Minus( 
            //    Morphologic.Minus(Basic.getGrayFromImage(img[0]), Basic.getGrayFromImage(img[2]),
            //    img[1].Width, img[1].Height),
            //    Morphologic.Minus(Basic.getGrayFromImage(img[1]), Basic.getGrayFromImage(img[2]),
            //    img[1].Width, img[1].Height), img[1].Width, img[1].Height), img[1].Width, img[1].Height), 0.2);
            //oimg.Save(string.Format("pic0-1-2.jpg"));



            Image timg = Basic.getImageFromGray(Morphologic.Minus(Basic.getGrayFromImage(opic[0].img), Basic.getGrayFromImage(opic[1].img),
                opic[1].Width, opic[1].Height), opic[1].Width, opic[1].Height);
            picMain.Image = Transformation.ResizeImage(timg, 0.2);
        }

        private void btnGradient_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap("pic1.jpg");
            picMain.Image = Basic.getImageFromBinary(Monochrome.GradientBytes(bmp, 80, 90), bmp.Width, bmp.Height);
            byte[] buff = Basic.getBinaryFromImage(picMain.Image);
            int w = picMain.Image.Width;
            int h = picMain.Image.Height;
            picMain.Image = Basic.getImageFromBinary(Dilation(Morphologic.Erosion(buff, w, h, 3), w, h), w, h);
            buff = Basic.getBinaryFromImage(picMain.Image);
            picMain.Image = Basic.getImageFromBinary(Morphologic.Erosion(Dilation(buff, w, h), w, h, 3), w, h);
            picMain.Image.Save("graout.png");
        }

        private byte[] Dilation(byte[] datain, int w, int h)
        {
            return Dilation(datain, 0, 0, w, h, w, h);
        }

        private byte[] Dilation(byte[] datain, int x, int y, int width, int height, int w, int h)
        {
            byte[] dataout = new byte[datain.Length];
            for (int j = x + 1; j < x + width - 1; j++)
            {
                for (int i = y + 1; i < y + height - 1; i++)
                {
                    int k = (h - i - 1) * w + j;
                    if (datain[k] == (byte)Morphologic.MapData.Black)
                    {
                        dataout[k - 1] = (byte)Morphologic.MapData.Black;
                        //dataout[k + 1] = (byte)Morphologic.MapData.Black;
                        dataout[k + 0] = (byte)Morphologic.MapData.Black;
                        //dataout[k + w - 1] = (byte)Morphologic.MapData.Black;
                        //dataout[k + w + 1] = (byte)Morphologic.MapData.Black;
                        //dataout[k + w + 0] = (byte)Morphologic.MapData.Black;
                        dataout[k - w - 1] = (byte)Morphologic.MapData.Black;
                        //dataout[k - w + 1] = (byte)Morphologic.MapData.Black;
                        dataout[k - w + 0] = (byte)Morphologic.MapData.Black;
                    }
                }
            }
            return dataout;
        }

        private void btnMarkArea_Click(object sender, EventArgs e)
        {
            //Bitmap bmp  = new Bitmap("test.png");
            //int w = bmp.Width;
            //int h = bmp.Height;
            //byte[] buff = Basic.getBinaryFromImage(bmp); //Monochrome.GradientBytes(bmp, 80, 90);
            //byte[] outbuff = MarkAreas(w, h, buff);
            //picMain.Image = Transformation.ResizeImage(Basic.getImageFromGray(outbuff, w, h), 2);
            //Basic.getImageFromGray(outbuff, w, h).Save("aa.png");
            int w = picMain.Image.Width;
            int h = picMain.Image.Height;
            int maxarea = 0;
            int[] buff = MarkAreas(w, h, Basic.getBinaryFromImage(picMain.Image), out maxarea);
            int[] areas = new int[maxarea];
            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    ++areas[buff[x + w * y]];
                }
            }
            int maxacreage = 0;
            List<Point> lines = new List<Point>();
            for (int i = 0; i < areas.Length; ++i)
            {
                bool p = false;
                for (int j = 0; j < lines.Count; ++j)
                {
                    if (lines[j].X == areas[i])
                    {
                        lines[j] = new Point(lines[j].X, lines[j].Y + 1);
                        p = true;
                        break;
                    }
                }
                if (!p)
                {
                    if (areas[i] < 10000)
                    {
                        if (maxacreage < areas[i]) maxacreage = areas[i];
                        lines.Add(new Point(areas[i], 1));
                    }
                    else
                    {
                        --maxarea;
                    }
                }
            }
            Point[] nlines = lines.ToArray();
            for (int i = 0; i < nlines.Length - 1; ++i)
            {
                for (int j = i + 1; j < nlines.Length; ++j)
                {
                    if (nlines[i].X > nlines[j].X)
                    {
                        Point tmp = nlines[i];
                        nlines[i] = nlines[j];
                        nlines[j] = tmp;
                    }
                }
            }
            lstLines.Items.Clear();
            txtLines.Text = "";
            for (int i = 0; i < nlines.Length; ++i)
            {
                lstLines.Items.Add(nlines[i].ToString());
                txtLines.Text += string.Format("{0}\t{1}\r\n", nlines[i].X, nlines[i].Y);
            }

            txtRegions.Text = "";
            double rr = 3.8101;
            List<double> regions = new List<double>();
            for (int i = 0; i < 42; ++i)
            {
                regions.Add(rr);
                txtRegions.Text += string.Format("{0:F}", rr);
                rr *= 1.112994;
                txtRegions.Text += string.Format(" - {0:F}\r\n", rr);
            }
            int[] rstat = new int[regions.Count];
            //regions.Add(rr);

            double minupp = 4.0 / 3; //min um per pixel
            //double maxupp = regions[regions.Count - 1] / nlines[nlines.Length - 1].X; //max um per pixel
            //double uppminum = 4.0;
            //double uppminpixel = 3;
            //double uppmaxum = regions[regions.Count - 1];
            //double uppmaxpixel = nlines[nlines.Length - 1].X;
            for (int i = 0; i < nlines.Length; ++i)
            {
                double tmpum = Math.Sqrt(nlines[i].X) * minupp;
                //double tmpum = Math.Sqrt(nlines[i].X);
                //tmpum = (tmpum - uppminpixel) * (uppmaxum - uppminum) / (uppmaxpixel - uppminpixel) + uppminum;
                int j;
                if (tmpum > regions[regions.Count - 1])
                {
                    j = regions.Count - 1;
                }
                else
                {
                    for (j = regions.Count - 1; j >= 0; --j)
                    {
                        if (tmpum > regions[j])
                        {
                            break;
                        }
                    }
                }
                rstat[j] += nlines[i].Y;
            }


            int cumu = 0;
            txtRegions.Text = "";
            for (int i = 0; i < 42; ++i)
            {
                cumu += rstat[i];
                txtRegions.Text += string.Format("{0:F}-{1:F}:{2:F}%,{3:F}%\r\n", regions[i], regions[i] * 1.112994, rstat[i] * 100.0 / maxarea, cumu * 100.0 / maxarea);
                rr *= 1.112994;
            }

            // 计算长度平均径、总表面积、总体积
            double avglength = 0;
            double surfaceacreage = 0;
            double volume = 0;
            for (int i = 0; i < nlines.Length; ++i)
            {
                double r = Math.Sqrt(nlines[i].X / Math.PI) * minupp;
                avglength += r * 2 * nlines[i].Y;
                double acreage = 4 * Math.PI * r * r;
                surfaceacreage += acreage * nlines[i].Y;
                //volume += (4 / 3) * Math.PI * r * r * r;
                volume += acreage * r / 3 * nlines[i].Y;
            }
            avglength /= maxarea;

            MessageBox.Show(string.Format("Average Length:{0}\r\nSurface Acreage:{1}\r\nTotal Volume:{2}", avglength, surfaceacreage, volume));

            //int th = 300;
            //Bitmap bmp = new Bitmap(nlines.Length, th);
            //Graphics g = Graphics.FromImage(bmp);
            //int cumulation = 0;
            //double percent = (double)th / Math.Sqrt(maxarea);
            //int lastx = 0;
            //int lasty = 0;
            //for (int i = 0; i < nlines.Length; ++i)
            //{
            //    cumulation += nlines[i].Y;
            //    int tmpx = (int)((double)nlines[i].X * nlines.Length / maxacreage);
            //    g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), tmpx, th - nlines[i].Y, tmpx - lastx + 1, nlines[i].Y);
            //    int tmpy = (int)(Math.Sqrt(cumulation) * percent);
            //    g.DrawLine(new Pen(new SolidBrush(Color.Blue)), new Point(tmpx, th - tmpy), new Point(lastx, th - lasty));
            //    lastx = tmpx;
            //    lasty = tmpy;
            //} 
            int th = 300;
            int tw = 300;
            Bitmap bmp = new Bitmap(tw, th);
            Graphics g = Graphics.FromImage(bmp);
            int cumulation = 0;
            double percent = (double)th / maxarea;
            int lastx = 0;
            int lasty = 0;
            double maxregionlength = regions[regions.Count - 1];
            for (int i = 0; i < rstat.Length; ++i)
            {
                cumulation += rstat[i];
                int tmpx = (int)(regions[i] * tw / maxregionlength);
                // 画粒径区间
                g.DrawRectangle(new Pen(new SolidBrush(Color.Black)), lastx, th - rstat[i], tmpx - lastx, rstat[i]);
                int tmpy = (int)(cumulation * percent);
                // 画累积线
                g.DrawLine(new Pen(new SolidBrush(Color.Blue)), new Point(tmpx, th - tmpy), new Point(lastx, th - lasty));
                lastx = tmpx;
                lasty = tmpy;
            }
            g.Flush();
            g.Dispose();
            picMain.Image = bmp;
        }

        private int[] MarkAreas(int w, int h, byte[] buff, out int areanum)
        {
            int wh = w * h;
            int[] outbuff = new int[wh];
            int[] area = new int[wh];
            List<int> indexer = new List<int>();
            for (int i = 0; i < wh; ++i)
            {
                area[i] = -1;
            }

            for (int y = 0; y < h; ++y)
            {
                for (int x = 0; x < w; ++x)
                {
                    //if (x == 45 && y == 40) { }
                    int[] k = new int[5];
                    k[0] = x + y * w;
                    k[1] = k[0] - w - 1; //左上
                    k[2] = k[0] - w; //上
                    k[3] = k[0] - w + 1; //右上
                    k[4] = k[0] - 1; //左
                    if (buff[k[0]] == 1)
                    {
                        bool p = false;
                        for (int l = 1; l <= 4; ++l)
                        {
                            if (k[l] >= 0 && k[l] < wh && buff[k[l]] == 1)
                            {
                                area[k[0]] = area[k[l]];
                                p = true;
                                break;
                            }
                        }
                        if (!p)
                        {
                            area[k[0]] = indexer.Count;
                            indexer.Add(indexer.Count);
                        }
                        int t1 = area[k[0]];
                        for (int l = 1; l <= 4; ++l)
                        {
                            //int tr = 0;
                            if (k[l] >= 0 && k[l] < wh && buff[k[l]] == 1)
                            {
                                int t2 = area[k[l]];
                                if (t1 != t2)
                                {
                                    int tmin = Math.Min(t1, t2);
                                    int tmax = Math.Max(t1, t2);
                                    int to = tmax;
                                    while (indexer[to] != to)
                                    {
                                        to = indexer[to];
                                    }
                                    tmin = Math.Min(to, tmin);
                                    tmax = Math.Max(to, tmin);
                                    //if (indexer[tmax] == tmax)
                                    //{
                                    indexer[tmax] = tmin;
                                    //}
                                    //else
                                    //{
                                    //}
                                }
                            }
                        }
                    }
                }
            }
            for (int i = indexer.Count - 1; i >= 0; --i)
            {
                while (indexer[indexer[i]] != indexer[i])
                {
                    indexer[i] = indexer[indexer[i]];
                }
            }
            List<int> newindexer = new List<int>();
            for (int i = 0; i < indexer.Count; ++i)
            {
                bool p = false;
                for (int j = 0; j < newindexer.Count; ++j)
                {
                    if (newindexer[j] == indexer[i])
                    {
                        indexer[i] = j;
                        p = true;
                        break;
                    }
                }
                if (!p)
                {
                    newindexer.Add(indexer[i]);
                    indexer[i] = newindexer.Count - 1;
                }
            }
            //MessageBox.Show(newindexer.Count.ToString());
            for (int y = 0; y < h; ++y)
            {
                for (int x = 0; x < w; ++x)
                {
                    int k = x + y * w;
                    if (area[k] > -1)
                    {
                        outbuff[k] = indexer[area[k]]; //(byte)((indexer[area[k]] + 1) * 28);
                    }
                    else
                    {
                        outbuff[k] = 0;
                    }
                }
            }
            areanum = newindexer.Count;
            return outbuff;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //Stream myStream = null;
            OpenFileDialog diaOpen = new OpenFileDialog();

            //diaOpen.InitialDirectory = "c:\\";
            diaOpen.Filter = "image files (*.png;*.gif;*.jpg;*.bmp)|*.png;*.gif;*.jpg;*.bmp|All files (*.*)|*.*";
            diaOpen.FilterIndex = 2;
            //diaOpen.RestoreDirectory = true;

            if (diaOpen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //if ((myStream = diaOpen.OpenFile()) != null)
                    //{
                    //    using (myStream)
                    //    {
                    //        // Insert code to read the stream here.
                    //    }
                    //}
                    picMain.Image = new Bitmap(diaOpen.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void txtLines_Click(object sender, EventArgs e)
        {
            txtLines.SelectAll();
            Clipboard.SetText(txtLines.Text);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtRegions.Text = "";
            double rr = 3.8101;
            for (int i = 0; i < 42; ++i)
            {
                txtRegions.Text += string.Format("{0:F}", rr);
                rr *= 1.112994;
                txtRegions.Text += string.Format(" - {0:F}\r\n", rr);
            }

        }
    }
}
