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
using ConVMatrix;


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
            this.Text = "Image Processing Lab";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isNormal == false && isWebcam ==false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }

            if (pictureBox1.Image != null && image1 != null && isNormal == true)
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

            }else if(isWebcam == true)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30; // ~30 FPS

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return;

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image bmap = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap b = new Bitmap(bmap);

                        // Inversion per pixel
                        image2 = new Bitmap(b.Width, b.Height);
                        for (int y = 0; y < b.Height; y++)
                        {
                            for (int x = 0; x < b.Width; x++)
                            {
                                Color pixelColor = b.GetPixel(x, y);
                                int InversionValue1 = 255 - pixelColor.R;
                                int InversionValue2 = 255 - pixelColor.G;
                                int InversionValue3 = 255 - pixelColor.B;
                                Color newColor = Color.FromArgb(InversionValue1, InversionValue2, InversionValue3);
                                image2.SetPixel(x, y, newColor);
                            }
                        }
                        pictureBox2.Image = image2;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage; // Fill the box edge-to-edge
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time invert started. Close the form or stop webcam to end.");
            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (isNormal == false && isWebcam == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }
            else if (pictureBox1.Image != null && image1 != null && isNormal == true)
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
            else if (isWebcam == true)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30; // ~30 FPS

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return; // Safeguard: device must exist

                    Device d = webcamDevice;
                    try
                    {
                        d.Sendmessage();

                        IDataObject data = Clipboard.GetDataObject();
                        if (data == null)
                            return; // Clipboard API failed

                        if (data.GetDataPresent("System.Drawing.Bitmap", true))
                        {
                            object imgObj = data.GetData("System.Drawing.Bitmap", true);
                            if (imgObj != null)
                            {
                                Image bmap = (Image)imgObj;
                                Bitmap b = new Bitmap(bmap);

                                image2 = b;
                                pictureBox2.Image = image2;
                                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Optionally log or handle errors (avoid crash)
                        Console.WriteLine("Webcam copy error: " + ex.Message);
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time copy started. Close the form or stop webcam to end.");
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
            if (isNormal == false && isWebcam == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }

            if (pictureBox1.Image != null && image1 != null && isNormal == true)
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

            }else if(isWebcam == true)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30; // ~30 FPS

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return;

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image bmap = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap b = new Bitmap(bmap);

                        // Sepia conversion
                        image2 = new Bitmap(b.Width, b.Height);
                        for (int y = 0; y < b.Height; y++)
                        {
                            for (int x = 0; x < b.Width; x++)
                            {
                                Color pixelColor = b.GetPixel(x, y);
                                int SepiaR = (int)((pixelColor.R * 0.393) + (pixelColor.G * 0.769) + (pixelColor.B * 0.189));
                                int SepiaG = (int)((pixelColor.R * 0.349) + (pixelColor.G * 0.686) + (pixelColor.B * 0.168));
                                int SepiaB = (int)((pixelColor.R * 0.272) + (pixelColor.G * 0.534) + (pixelColor.B * 0.131));

                                SepiaR = Math.Min(255, Math.Max(0, SepiaR));
                                SepiaG = Math.Min(255, Math.Max(0, SepiaG));
                                SepiaB = Math.Min(255, Math.Max(0, SepiaB));

                                Color newColor = Color.FromArgb(SepiaR, SepiaG, SepiaB);
                                image2.SetPixel(x, y, newColor);
                            }
                        }
                        pictureBox2.Image = image2;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage; // Fill the box edge-to-edge
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time sepia started. Close the form or stop webcam to end.");
            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (isNormal == false && isWebcam == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }

            if (pictureBox1.Image != null && image1 != null && isNormal == true)
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

            }else if(isWebcam == true)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30; // ~30 FPS

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return;

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image bmap = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap b = new Bitmap(bmap);

                        // Grayscale conversion
                        image2 = new Bitmap(b.Width, b.Height);
                        for (int y = 0; y < b.Height; y++)
                        {
                            for (int x = 0; x < b.Width; x++)
                            {
                                Color pixelColor = b.GetPixel(x, y);
                                int grayvalue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                                Color newColor = Color.FromArgb(grayvalue, grayvalue, grayvalue);
                                image2.SetPixel(x, y, newColor);
                            }
                        }
                        pictureBox2.Image = image2;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage; // Fill the box
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time grayscale started. Close the form or stop webcam to end.");




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

            if (isNormal == false && isWebcam == false)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;

            }

            if (image1 != null && image2 != null && isNormal == true)
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




            }else if(isWebcam == true && image2 !=null)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30; // ~30 FPS

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null || image2 == null)
                        return; // image2 must hold your background image!

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image webcamImage = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap webcamFrame = new Bitmap(webcamImage);

                        int width = Math.Min(webcamFrame.Width, image2.Width);
                        int height = Math.Min(webcamFrame.Height, image2.Height);

                        image3 = new Bitmap(width, height);

                        // --- CHROMA KEY TUNING PARAMETERS ---
                        int greenMin = 80;        // Minimum green to be considered screen
                        int redMax = 100;        // Max red value to allow as green
                        int blueMax = 100;        // Max blue value to allow as green
                        int fuzz = 40;         // Green must exceed R/B by at least this much
                                               // ------------------------------------

                        for (int x = 0; x < width; x++)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                Color fgPixel = webcamFrame.GetPixel(x, y);
                                Color bgPixel = image2.GetPixel(x, y);

                                int r = fgPixel.R;
                                int g = fgPixel.G;
                                int b = fgPixel.B;

                                bool isGreenScreenPixel =
                                    g > greenMin &&
                                    r < redMax &&
                                    b < blueMax &&
                                    (g - Math.Max(r, b)) > fuzz;

                                if (isGreenScreenPixel)
                                    image3.SetPixel(x, y, bgPixel); // Replace green with background
                                else
                                    image3.SetPixel(x, y, fgPixel); // Keep original pixel
                            }
                        }

                        pictureBox3.Image = image3;
                        pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage; // Fill the output box
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time green screen started. Adjust tuning values for best results! Close form or stop webcam to end.");
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
                saveFileDialog.FileName = ""; // default name


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
                saveFileDialog.FileName = ""; // default name

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

        private void button14_Click(object sender, EventArgs e)
        {

        }
        private void button17_Click(object sender, EventArgs e)
        {
            if (!isNormal && !isWebcam)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;
            }

            if (pictureBox1.Image != null && image1 != null && isNormal)
            {
                // Sharpen kernel setup
                ConvMatrix m = new ConvMatrix();
                m.TopLeft = 0; m.TopMid = -2; m.TopRight = 0;
                m.MidLeft = -2; m.Pixel = 11; m.MidRight = -2;
                m.BottomLeft = 0; m.BottomMid = -2; m.BottomRight = 0;
                m.Factor = 3;
                m.Offset = 0;

                image2 = (Bitmap)image1.Clone();
                for (int i = 0; i < 3; i++)
                    ConVMatrix.Convolution.Conv3x3(image2, m);


                pictureBox2.Image = image2;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (isWebcam)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30;

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return;

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image bmap = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap b = new Bitmap(bmap);

                        ConvMatrix m = new ConvMatrix();
                        m.TopLeft = 0; m.TopMid = -5; m.TopRight = 0;
                        m.MidLeft = -5; m.Pixel = 25; m.MidRight = -5;
                        m.BottomLeft = 0; m.BottomMid = -5; m.BottomRight = 0;
                        m.Factor = 5;   // made it stronger but di gyapon klaro lol
                        m.Offset = 0;

                        image2 = (Bitmap)b.Clone();
                        for (int i = 0; i < 2; i++)
                            ConVMatrix.Convolution.Conv3x3(image2, m);


                        pictureBox2.Image = image2;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time sharpen (intensified) started. Close the form or stop webcam to end.");

            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!isNormal && !isWebcam)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;
            }

            if (pictureBox1.Image != null && image1 != null && isNormal)
            {
                // Smoothing (mean blur) kernel
                ConVMatrix.ConvMatrix m = new ConVMatrix.ConvMatrix();
                m.SetAll(1);
                m.Pixel = 1;      // Classic box blur (center = 1)
                m.Factor = 9;     // Sum of all entries for classic box blur
                m.Offset = 0;

                image2 = (Bitmap)image1.Clone();
                for (int i = 0; i < 10; i++)
                    ConVMatrix.Convolution.Conv3x3(image2, m);


                pictureBox2.Image = image2;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (isWebcam)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30;

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return;

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image bmap = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap b = new Bitmap(bmap);

                        ConVMatrix.ConvMatrix m = new ConVMatrix.ConvMatrix();
                        m.SetAll(1);
                        m.Pixel = 1;
                        m.Factor = 9;
                        m.Offset = 0;

                        image2 = (Bitmap)b.Clone();
                        for (int i = 0; i < 10; i++)
                            ConVMatrix.Convolution.Conv3x3(image2, m);


                        pictureBox2.Image = image2;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time smoothing started. Close the form or stop webcam to end.");
            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (!isNormal && !isWebcam)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;
            }

            if (pictureBox1.Image != null && image1 != null && isNormal)
            {
                // Classic 3x3 Gaussian blur kernel
                ConVMatrix.ConvMatrix m = new ConVMatrix.ConvMatrix();
                m.TopLeft = 1;
                m.TopMid = 2;
                m.TopRight = 1;
                m.MidLeft = 2;
                m.Pixel = 4;
                m.MidRight = 2;
                m.BottomLeft = 1;
                m.BottomMid = 2;
                m.BottomRight = 1;
                m.Factor = 16;
                m.Offset = 0;

                image2 = (Bitmap)image1.Clone();
                for (int i = 0; i < 10; i++)
                    ConVMatrix.Convolution.Conv3x3(image2, m);


                pictureBox2.Image = image2;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (isWebcam)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30;

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return;

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image bmap = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap b = new Bitmap(bmap);

                        ConVMatrix.ConvMatrix m = new ConVMatrix.ConvMatrix();
                        m.TopLeft = 1;
                        m.TopMid = 2;
                        m.TopRight = 1;
                        m.MidLeft = 2;
                        m.Pixel = 4;
                        m.MidRight = 2;
                        m.BottomLeft = 1;
                        m.BottomMid = 2;
                        m.BottomRight = 1;
                        m.Factor = 16;
                        m.Offset = 0;

                        image2 = (Bitmap)b.Clone();
                        for (int i = 0; i < 10; i++)
                            ConVMatrix.Convolution.Conv3x3(image2, m);

                        pictureBox2.Image = image2;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time Gaussian blur started. Close the form or stop webcam to end.");
            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (!isNormal && !isWebcam)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;
            }

            if (pictureBox1.Image != null && image1 != null && isNormal)
            {
                ConVMatrix.ConvMatrix m = new ConVMatrix.ConvMatrix();
                m.SetAll(-1);
                m.Pixel = 9;
                m.Factor = 1;
                m.Offset = 0;

                image2 = (Bitmap)image1.Clone();
                    ConVMatrix.Convolution.Conv3x3(image2, m);

                pictureBox2.Image = image2;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (isWebcam)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30;

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return;

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image bmap = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap b = new Bitmap(bmap);

                        ConVMatrix.ConvMatrix m = new ConVMatrix.ConvMatrix();
                        m.SetAll(-1);
                        m.Pixel = 9;
                        m.Factor = 1;
                        m.Offset = 0;

                        image2 = (Bitmap)b.Clone();
                            ConVMatrix.Convolution.Conv3x3(image2, m);

                        pictureBox2.Image = image2;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time mean removal (5x) started. Close the form or stop webcam to end.");
            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (!isNormal && !isWebcam)
            {
                MessageBox.Show("PLEASE SELECT A MODE FIRST!");
                return;
            }

            if (pictureBox1.Image != null && image1 != null && isNormal)
            {
                // Laplacian emboss kernel
                ConVMatrix.ConvMatrix m = new ConVMatrix.ConvMatrix();
                m.TopLeft = -1; m.TopMid = 0; m.TopRight = -1;
                m.MidLeft = 0; m.Pixel = 4; m.MidRight = 0;
                m.BottomLeft = -1; m.BottomMid = 0; m.BottomRight = -1;
                m.Factor = 1;
                m.Offset = 127; // To shift mid-greys to visible range

                image2 = (Bitmap)image1.Clone();
                ConVMatrix.Convolution.Conv3x3(image2, m);

                pictureBox2.Image = image2;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else if (isWebcam)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30;

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return;

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image bmap = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap b = new Bitmap(bmap);

                        ConVMatrix.ConvMatrix m = new ConVMatrix.ConvMatrix();
                        m.TopLeft = -1; m.TopMid = 0; m.TopRight = -1;
                        m.MidLeft = 0; m.Pixel = 4; m.MidRight = 0;
                        m.BottomLeft = -1; m.BottomMid = 0; m.BottomRight = -1;
                        m.Factor = 1;
                        m.Offset = 127;

                        image2 = (Bitmap)b.Clone();
                        ConVMatrix.Convolution.Conv3x3(image2, m);

                        pictureBox2.Image = image2;
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time emboss started. Close the form or stop webcam to end.");
            }
            else
            {
                MessageBox.Show("Please load an image first!");
            }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            if (isNormal == false && isWebcam == false)
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
}else if(isWebcam == true)
            {
                System.Windows.Forms.Timer webcamTimer = new System.Windows.Forms.Timer();
                webcamTimer.Interval = 30; // ~30 FPS

                webcamTimer.Tick += (s, ev) =>
                {
                    if (webcamDevice == null)
                        return;

                    Device d = webcamDevice;
                    d.Sendmessage();
                    IDataObject data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent("System.Drawing.Bitmap", true))
                    {
                        Image bmap = (Image)data.GetData("System.Drawing.Bitmap", true);
                        Bitmap b = new Bitmap(bmap);

                        // Grayscale conversion
                        image2 = new Bitmap(b.Width, b.Height);
                        for (int y = 0; y < b.Height; y++)
                        {
                            for (int x = 0; x < b.Width; x++)
                            {
                                Color pixelColor = b.GetPixel(x, y);
                                int grayvalue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                                Color newColor = Color.FromArgb(grayvalue, grayvalue, grayvalue);
                                image2.SetPixel(x, y, newColor);
                            }
                        }

                        // Build histogram from grayscale
                        int[] histogram = new int[256];
                        for (int i = 0; i < histogram.Length; i++) histogram[i] = 0;
                        for (int y = 0; y < image2.Height; y++)
                        {
                            for (int x = 0; x < image2.Width; x++)
                            {
                                Color pixelColor = image2.GetPixel(x, y);
                                int grayLevel = pixelColor.R;
                                histogram[grayLevel]++;
                            }
                        }

                        // Draw histogram
                        int width = 400, height = 400;
                        Bitmap histogramGraph = new Bitmap(width, height);

                        using (Graphics g = Graphics.FromImage(histogramGraph))
                        {
                            g.Clear(Color.White);
                            int maxCount = histogram.Max();
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
                        pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                        if (pictureBox3 != null)
                        {
                            pictureBox3.Image = histogramGraph;
                            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                };

                webcamTimer.Start();
                MessageBox.Show("Webcam real-time grayscale+histogram started. Close the form or stop webcam to end.");
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
    