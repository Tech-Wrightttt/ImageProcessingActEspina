using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessingActEspina
{
    public partial class Form1 : Form
    {

        private Bitmap image1, image2, image3;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && image1 != null)
            {
                image2 = new Bitmap(image1.Width, image1.Height);
                for (int y = 0; y < image1.Height; y++)
                {
                    for (int x = 0; x < image1.Width; x++)
                    {
                        Color pixelColor = image1.GetPixel(x, y);
                        int InversionValue1 = 255 - pixelColor.R;
                        int InversionValue2 = 255 - pixelColor.G;
                        int InversionValue3 = 255 - pixelColor.B;
     
                        Color newColor = Color.FromArgb(InversionValue1, InversionValue2, InversionValue3);
                        //the most monkey ass inefficient code ever part 2
                        image2.SetPixel(x, y, newColor);
                    }
                }


                pictureBox2.Image = image2;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && image1 != null)
            {
                image2 = new Bitmap(image1.Width, image1.Height);
                for (int y = 0; y < image1.Height; y++) { 
                    for (int x = 0; x < image1.Width; x++) {
                        Color pixelColor = image1.GetPixel(x, y);
                        image2.SetPixel(x, y, pixelColor);
                    }
                }

                pictureBox2.Image = image2;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }
        private void newImageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox pictureBoxOne = new PictureBox();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && image1 != null)
            {
                image2 = new Bitmap(image1.Width, image1.Height);
                for (int y = 0; y < image1.Height; y++)
                {
                    for (int x = 0; x < image1.Width; x++)
                    {
                        Color pixelColor = image1.GetPixel(x, y);
                        int SepiaR = (int)((pixelColor.R * 0.393) + (pixelColor.G * 0.769) + (pixelColor.B * 0.189));
                        int SepiaG = (int)((pixelColor.R * 0.349) + (pixelColor.G * 0.686) + (pixelColor.B * 0.168));
                        int SepiaB = (int)((pixelColor.R * 0.272) + (pixelColor.G * 0.534) + (pixelColor.B * 0.131));

                        SepiaR = Math.Min(255, Math.Max(0, SepiaR));
                        SepiaG = Math.Min(255, Math.Max(0, SepiaG));
                        SepiaB = Math.Min(255, Math.Max(0, SepiaB));

                        Color newColor = Color.FromArgb(SepiaR, SepiaG, SepiaB);
                        //the most monkey ass inefficient code ever part 2
                        image2.SetPixel(x, y, newColor);
                    }
                }


                pictureBox2.Image = image2;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && image1 != null)
            {
                image2 = new Bitmap(image1.Width, image1.Height);
                for (int y = 0; y < image1.Height; y++)
                {
                    for (int x = 0; x < image1.Width; x++)
                    {
                        Color pixelColor = image1.GetPixel(x, y);
        

                        int grayvalue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        Color newColor = Color.FromArgb(grayvalue, grayvalue, grayvalue);
                        //the most monkey ass inefficient code ever
                        image2.SetPixel(x, y, newColor);
                    }
                }

                //find a way to get the pixel color in RGB so i can divide it by 3 and get the gra

                pictureBox2.Image = image2;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();


            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(fileDialog.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                image1 = new Bitmap(fileDialog.FileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
           if (pictureBox1.Image != null && image1 != null)
{
    image2 = new Bitmap(image1.Width, image1.Height);
    for (int y = 0; y < image1.Height; y++)
    {
        for (int x = 0; x < image1.Width; x++)
        {
            Color pixelColor = image1.GetPixel(x, y);
            int grayvalue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
            Color newColor = Color.FromArgb(grayvalue, grayvalue, grayvalue);
            image2.SetPixel(x, y, newColor);
        }
    }

    // FIX 1: Change from 255 to 256 to include all gray levels (0-255)
    int[] histogram = new int[256];
    
    for(int i = 0; i < histogram.Length; i++)
    {
        histogram[i] = 0;
    }

    // Count each gray level in the image
    for (int y = 0; y < image2.Height; y++)
    {
        for (int x = 0; x < image2.Width; x++)
        {
            Color pixelColor = image2.GetPixel(x, y);
            int grayLevel = pixelColor.R; // In grayscale, R=G=B

            histogram[grayLevel]++;
        }
    }


    int width = 400;
    int height = 400;
    Bitmap histogramGraph = new Bitmap(width, height);

    using (Graphics g = Graphics.FromImage(histogramGraph))
    {
        // Fill background
        g.Clear(Color.White);

        // Find maximum count for scaling
        int maxCount = 0;
        for (int i = 0; i < 256; i++)
        {
            if (histogram[i] > maxCount)
                maxCount = histogram[i];
        }

        // Draw axes
        Pen axisPen = new Pen(Color.Black, 2);
        g.DrawLine(axisPen, 40, height - 40, width - 20, height - 40); // X-axis
        g.DrawLine(axisPen, 40, height - 40, 40, 20); // Y-axis

        // Draw histogram bars
        Pen barPen = new Pen(Color.Blue, 1);
        Brush barBrush = new SolidBrush(Color.LightBlue);

        float barWidth = (width - 60) / 256f;
                        
        for (int i = 0; i < 256; i++)
        {
            if (histogram[i] > 0)
            {
                float barHeight = ((float)histogram[i] / maxCount) * (height - 80);
                float x = 40 + (i * barWidth);
                float y = height - 40 - barHeight;

                // Draw bar
                g.FillRectangle(barBrush, x, y, barWidth, barHeight);
                g.DrawRectangle(barPen, x, y, barWidth, barHeight);
            }
        }

        // Draw labels
        Font labelFont = new Font("Arial", 8);
        Brush textBrush = new SolidBrush(Color.Black);

        // X-axis labels
        g.DrawString("0", labelFont, textBrush, 40, height - 30);
        g.DrawString("128", labelFont, textBrush, 40 + (128 * barWidth), height - 30);
        g.DrawString("255", labelFont, textBrush, width - 25, height - 30);

        // Axis labels
        g.DrawString("Pixel Count", labelFont, textBrush, 10, height / 2);
        g.DrawString("Gray Level", labelFont, textBrush, width / 2 - 30, height - 20);

        // Title
        Font titleFont = new Font("Arial", 10, FontStyle.Bold);
        g.DrawString("Grayscale Histogram", titleFont, textBrush, width / 2 - 60, 5);

        // Clean up
        axisPen.Dispose();
        barPen.Dispose();
        barBrush.Dispose();
        labelFont.Dispose();
        textBrush.Dispose();
        titleFont.Dispose();
    }

    pictureBox2.Image = image2;
    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

    // FIX 4: Display histogram in pictureBox3 
    if (pictureBox3 != null)
    {
        pictureBox3.Image = histogramGraph;
        pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
    }
}
else
{
    MessageBox.Show("Please load an image first!");
}
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
          

            if(fileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(fileDialog.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    
                image1 = new Bitmap(fileDialog.FileName);
            }

        }
    }
}
    