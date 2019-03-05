using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_работа_ASCII_art_
{
    public partial class Form1 : Form
    {
        private string thePathToTheFile;
        public Form1()
        {
            InitializeComponent();
        }

        private Bitmap convertingToBlackAndWhite(Bitmap btm)
        {
            ColorMatrix matrix = new ColorMatrix();

            for (int i = 0; i <= 2; i++)
                for (int j = 0; j <= 2; j++)
                    matrix[i, j] = 1 / 3f;

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix);

            Graphics gphGrey = Graphics.FromImage(btm);
            Rectangle rect = new Rectangle(0, 0, btm.Width, btm.Height);
            gphGrey.DrawImage(btm, rect, 0, 0, btm.Width, btm.Height, GraphicsUnit.Pixel, attributes);

            return btm;
        }

        private void convertingToAsciiArt(Bitmap bmp)
        {
            using (StreamWriter sw = new StreamWriter("ascii.txt", false, System.Text.Encoding.Default))
            {
                for (int h = 0; h < bmp.Height / 10; h++)
                {
                    int _startY = (h * 10);

                    for (int w = 0; w < bmp.Width / 5; w++)
                    {
                        int _startX = (w * 5);

                        int _allBrightness = 0;

                        for (int y = 0; y < 10; y++)
                        {
                            for (int x = 0; x < 10; x++)
                            {
                                int _cY = y + _startY;
                                int _cX = x + _startX;
                                try
                                {
                                    Color _c = bmp.GetPixel(_cX, _cY);
                                    int _b = (int)(_c.GetBrightness() * 10);
                                    _allBrightness = (_allBrightness + _b);
                                }
                                catch
                                {
                                    _allBrightness = (_allBrightness + 10);
                                }
                            }
                        }

                        int _sb = (_allBrightness / 10);
                        if (_sb < 25)
                        {
                            sw.Write("#");
                        }
                        else if (_sb < 30)
                        {
                            sw.Write("@");
                        }
                        else if (_sb < 40)
                        {
                            sw.Write("W");
                        }
                        else if (_sb < 45)
                        {
                            sw.Write("@");
                        }
                        else if (_sb < 50)
                        {
                            sw.Write("&");
                        }
                        else if (_sb < 55)
                        {
                            sw.Write("+");
                        }
                        else if (_sb < 60)
                        {
                            sw.Write("~");
                        }
                        else if (_sb < 60)
                        {
                            sw.Write(".");
                        }
                        else if (_sb < 70)
                        {
                            sw.Write("E");
                        }
                        else if (_sb < 80)
                        {
                            sw.Write("r");
                        }
                        else
                        {
                            sw.Write(" ");
                        }
                    }
                    sw.WriteLine();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            thePathToTheFile = openFileDialog1.FileName;

            pictureBox1.Visible = true;
            pictureBox1.Image = new Bitmap(thePathToTheFile);
            richTextBox1.Visible = false;

            button3.Enabled = true;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
            pictureBox1.Visible = false;

            using (StreamReader sr = new StreamReader(@"ascii.txt"))
            {
                richTextBox1.Text = sr.ReadToEnd();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Идет преобразование, пожалуйста дождитесь окончания.", "Внимание");
            Bitmap image = new Bitmap(thePathToTheFile);
            convertingToAsciiArt(convertingToBlackAndWhite(image));
            button2.Enabled = true;
        }
    }
}