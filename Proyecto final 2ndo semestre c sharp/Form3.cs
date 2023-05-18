using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Threading;
using System.Diagnostics;
using FaceRecognition;
using Emgu.CV.Structure;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.BgSegm;
using static System.Net.WebRequestMethods;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Form3 : Form
    {

        #region Variables
        int testid = 0;
       // private Capture videoCapture = null;
        private Image<Bgr, Byte> currentFrame = null;
        Mat frame = new Mat();
        private bool facesDetectionEnabled = false;
        CascadeClassifier faceCascadeClassifier = new CascadeClassifier(@"C:\Users\junio\OneDrive\Documentos\haarcascade_frontalface_alt_tree.xml");
        Image<Bgr, Byte> faceResult = null;
        List<Image<Gray, Byte>> TrainedFaces = new List<Image<Gray, byte>>();
        List<int> personsLabels = new List<int>();

        bool enableSaveImage = false;
        private bool isTrained = false;
        EigenFaceRecognizer recognizer;
        List<string> personsNames = new List<string>();

        #endregion


        public Form3()
        {
            InitializeComponent();
            

        }

        FaceRec faceRec = new FaceRec();

        private void btnCapture_Click(object sender, EventArgs e)
        {
            faceRec.openCamera(pictureBox1, pictureBox2);
        }

        private void btnDetect_Click(object sender, EventArgs e)
        {

            //string[] files = Directory.GetFiles(folderPath, "*.jpg", SearchOption.AllDirectories);

            Image imagen = pictureBox1.Image;
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePathh = Path.Combine(desktopPath, "imagen.jpg");
            imagen.Save(filePathh, System.Drawing.Imaging.ImageFormat.Jpeg);

            // Cargar la imagen original
            Image<Bgr, byte> originalImage = new Image<Bgr, byte>(filePathh);

            // Convertir la imagen a escala de grises
            Image<Gray, byte> grayImage = originalImage.Convert<Gray, byte>();

            // Utiliza grayImage para lo que necesites
            //pictureBox2.Image = grayImage.ToBitmap();
            // Utiliza el arreglo de bytes para lo que necesites

            string folderPath = @"C:\\Users\\junio\\OneDrive\\Documentos\\UsuariosScaner";

            foreach (var filePath in Directory.GetFiles(folderPath, "*.jpg"))
            {
                        Image<Bgr, Byte> registeredFace = new Image<Bgr, Byte>(filePath);
             
            //    Image<Gray, Byte> grayFaceResult = registeredFace.Convert<Gray, Byte>().Resize(200, 200, Inter.Cubic);
                    CvInvoke.EqualizeHist(registeredFace, registeredFace);
                

                var result = recognizer.Predict(registeredFace);
                
                //  pictureBox1.Image = grayFaceResult.Bitmap;
                //  pictureBox2.Image = TrainedFaces[result.Label].Bitmap;
                //   var result = recognizer.Predict(registeredFace);

                Debug.WriteLine(result.Label + ". " + result.Distance);

                        // Aquí realizas las comparaciones y acciones en función de los resultados



                        if (result.Label != -1 && result.Distance < 1000)
                        {
                         //   Form1 form1 = new Form1();
                           // form1.ShowDialog();
                        }
                        else
                        {
                        MessageBox.Show("El modelo de reconocimiento facial no ha sido entrenado. Por favor, registre al menos una cara antes de continuar.");
                                    
                    
                        }
            }

               
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();
        }



       

        // Otros métodos y miembros de la clase FaceRec...
    }




























    /*
            private void ProcessFrame(object sender, EventArgs e)
            {
                // Step 1: Video Capture
                if (videoCapture != null && videoCapture.Ptr != IntPtr.Zero)
                {
                    Mat frame = videoCapture.QueryFrame();

                    // Check if the frame is valid
                    if (frame != null)
                    {
                        // Check the image type of frame
                        if (frame.NumberOfChannels == 3)
                        {
                            currentFrame = frame.ToImage<Bgr, byte>();
                        }
                        else if (frame.NumberOfChannels == 1)
                        {
                            currentFrame = frame.ToImage<Gray, byte>().Convert<Bgr, byte>();
                        }
                        else
                        {
                            // Handle unsupported image type
                            // Display an error message or perform appropriate actions
                            return;
                        }

                        // Resize the image to fit the PictureBox
                        currentFrame = currentFrame.Resize(picCapture.Width, picCapture.Height, Inter.Cubic);

                        // Update the PictureBox image on the UI thread
                        picCapture.Invoke(new Action(() =>
                        {
                            picCapture.Image = currentFrame.Bitmap;
                        }));


                        // Resize the image to fit the PictureBox
                        currentFrame = currentFrame.Resize(picCapture.Width, picCapture.Height, Inter.Cubic);



                    // Step 2: Face Detection
                    if (facesDetectionEnabled)
                    {
                        // Convert from Bgr to Gray Image
                        Mat grayImage = new Mat();
                        CvInvoke.CvtColor(currentFrame, grayImage, ColorConversion.Bgr2Gray);
                        // Enhance the image to get better result
                        CvInvoke.EqualizeHist(grayImage, grayImage);

                        Rectangle[] faces = faceCascadeClassifier.DetectMultiScale(grayImage, 1.1, 3, Size.Empty, Size.Empty);
                        // If faces detected
                        if (faces.Length > 0)
                        {
                            foreach (var face in faces)
                            {
                                // Draw square around each face 
                                CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Red).MCvScalar, 2);

                                // Step 3: Add Person
                                // Rest of the code...

                                // Step 5: Recognize the face
                                if (isTrained)
                                {
                                    Image<Gray, Byte> grayFaceResult = currentFrame.Convert<Gray, Byte>().Resize(200, 200, Inter.Cubic);
                                    CvInvoke.EqualizeHist(grayFaceResult, grayFaceResult);
                                    var result = recognizer.Predict(grayFaceResult);

                                    if (result.Label != -1 && result.Distance < 2000)
                                    {
                                        // Access the "Inventario" form when a recognized face is found
                                        Form1 inventarioForm = new Form1();
                                        inventarioForm.Show();

                                        // Hide the current form
                                        this.Hide();
                                    }
                                }
                            }
                        }
                    }
                }

                // Step 6: Render the video capture into the Picture Box picCapture
                picCapture.Image = currentFrame.Bitmap;


                // Dispose the Current Frame after processing it to reduce memory consumption
                if (currentFrame != null)
                    currentFrame.Dispose();
                }
            }


            private void btnCapture_Click(object sender, EventArgs e)
            {
                // Liberar la captura de video si se creó anteriormente
                if (videoCapture != null)
                    videoCapture.Dispose();

                // Crear una nueva captura de video
                videoCapture = new Capture();

                // Asociar el método ProcessFrame al evento ImageGrabbed de la captura de video
                videoCapture.ImageGrabbed += ProcessFrame;
                videoCapture.Start();
            }



            private void btnDetect_Click(object sender, EventArgs e)
            {
                facesDetectionEnabled = true;

            }

            private void btnClose_Click(object sender, EventArgs e)
            {

            }*/
}

