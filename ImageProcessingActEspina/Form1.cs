using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WebCamLib;


namespace ImageProcessingActEspina
{
    public partial class Form1 : Form
    {

        private Bitmap image1, image2, image3;
        private bool isNormal = false;
        private bool isWebcam = false;
        private bool webcamActive = false;
        private Device webcamDevice = null;

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
            if (isNormal == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }

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
            if(isNormal == false && isWebcam == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;
              
            }else if (pictureBox1.Image != null && image1 != null && isNormal == true)
            {
                image2 = new Bitmap(image1.Width, image1.Height);
                for (int y = 0; y < image1.Height; y++)
                {
                    for (int x = 0; x < image1.Width; x++)
                    {
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
            if (isNormal == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }

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

            if (isNormal == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }

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

                image2 = new Bitmap(fileDialog.FileName);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Color mygreen = Color.FromArgb(0, 0, 255);
            // int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            // int threshold = 5;

            // for (int x = 0; x < imageB.Width; x++)
            // {
            //     for (int y = 0; y < imageB.Height; y++)
            //     {
            //         Color pixel = imageB.GetPixel(x, y);
            //         Color backpixel = imageA.GetPixel(x, y);
            //         int grey = (pixel.R + pixel.G + pixel.B) / 3;
            //         int subtractvalue = Math.Abs(grey - greygreen);
            //         if (subtractvalue > threshold)
            //             resultImage.SetPixel(x, y, backpixel);
            //         else
            //             resultImage.SetPixel(x, y, pixel);
            //     }
            // }

            if (isNormal == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }

            if (image1 != null && image2 != null)
            {
                image3 = new Bitmap(image1.Width, image1.Height);

                int threshold = 60; // adjust for green sensitivity

                for (int x = 0; x < image1.Width; x++)
                {
                    for (int y = 0; y < image1.Height; y++)
                    {
                        Color fgPixel = image1.GetPixel(x, y); // foreground with green screen
                        Color bgPixel = image2.GetPixel(x, y); // background

                        bool isGreenScreenPixel = fgPixel.G > threshold &&
                                                  fgPixel.G > fgPixel.R * 1.5 &&
                                                  fgPixel.G > fgPixel.B * 1.5;


                        //ALTHOUGH THE LOGIC PROVIDED IN THE ONE NOTE IS CORRECT I DECIDED TO CHANGE IT BECAUSE ITS TOO SIMPLISTIC AND WONT 
                        //ACCOUNT FOR LIGHTING
                        //i know we are using greygreen and grayscale so that we can compare using the brightness but i just like this better sir i hope you dont mind

                        if (isGreenScreenPixel)
                            image3.SetPixel(x, y, bgPixel);  // replace green with background
                        else
                            image3.SetPixel(x, y, fgPixel);
                    }
                }

                pictureBox3.Image = image3;
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;




            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
                
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            image1 = null;


        }

        private void button10_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            image2 = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            image3 = null;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = @"C:\Users\Ruhmer Jairus\Documents";
                saveFileDialog.Filter = "PNG files (*.png)|*.png";
                saveFileDialog.DefaultExt = "png";
                saveFileDialog.FileName = "myimage"; // default name


                //note to self use savefiledialog if u want automatic saving

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    image3.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = @"C:\Users\Ruhmer Jairus\Documents";
                saveFileDialog.Filter = "PNG files (*.png)|*.png";
                saveFileDialog.DefaultExt = "png";
                saveFileDialog.FileName = "myimage"; // default name

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    image2.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void option2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
if (!webcamActive)
    {
        // Turn webcam ON
        isWebcam = true;
        Device[] devices = DeviceManager.GetAllDevices();
        if (devices.Length > 0)
        {
            webcamDevice = devices[0]; // Select the first webcam
            webcamDevice.ShowWindow(pictureBox1);
            webcamActive = true;
        }
        else
        {
            MessageBox.Show("No webcam devices found.");
        }
    }
    else
    {
        // Turn webcam OFF
        if (webcamDevice != null)
        {
            webcamDevice.Stop();
            webcamDevice = null;
        }
        isWebcam = false;
        webcamActive = false;
    }
    // Update menu item text to match status
    option2ToolStripMenuItem.Text = webcamActive ? "Turn Webcam OFF" : "Turn Webcam ON";


        }

        private void option1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isNormal = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (isNormal == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }

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

        g.Clear(Color.White);

   
        int maxCount = 0;
        for (int i = 0; i < 256; i++)
        {
            if (histogram[i] > maxCount)
                maxCount = histogram[i];
        }

        Pen axisPen = new Pen(Color.Black, 2);
        g.DrawLine(axisPen, 40, height - 40, width - 20, height - 40); // X-axis
        g.DrawLine(axisPen, 40, height - 40, 40, 20); // Y-axis

   
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

         
                g.FillRectangle(barBrush, x, y, barWidth, barHeight);
                g.DrawRectangle(barPen, x, y, barWidth, barHeight);
            }
        }

     
        Font labelFont = new Font("Arial", 8);
        Brush textBrush = new SolidBrush(Color.Black);

        g.DrawString("0", labelFont, textBrush, 40, height - 30);
        g.DrawString("128", labelFont, textBrush, 40 + (128 * barWidth), height - 30);
        g.DrawString("255", labelFont, textBrush, width - 25, height - 30);


        g.DrawString("Pixel Count", labelFont, textBrush, 10, height / 2);
        g.DrawString("Gray Level", labelFont, textBrush, width / 2 - 30, height - 20);

  
        Font titleFont = new Font("Arial", 10, FontStyle.Bold);
        g.DrawString("Grayscale Histogram", titleFont, textBrush, width / 2 - 60, 5);


        axisPen.Dispose();
        barPen.Dispose();
        barBrush.Dispose();
        labelFont.Dispose();
        textBrush.Dispose();
        titleFont.Dispose();
    }

    pictureBox2.Image = image2;
    pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;


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
    