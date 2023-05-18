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


namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Form2 : Form
    {
       /* #region Variables
        int testid = 0;
        private Capture videoCapture = null;
        private Image<Bgr, Byte> currentFrame = null;
        Mat frame = new Mat();
        private bool facesDetectionEnabled = false;
        CascadeClassifier faceCasacdeClassifier = new CascadeClassifier(@"C:\Users\junio\OneDrive\Documentos\haarcascade_frontalface_alt_tree.xml");
        Image<Bgr, Byte> faceResult = null;
        List<Image<Gray, Byte>> TrainedFaces = new List<Image<Gray, byte>>();
        List<int> PersonsLabes = new List<int>();

        bool EnableSaveImage = false;
        private bool isTrained = false;
        EigenFaceRecognizer recognizer;
        List<string> PersonsNames = new List<string>();

        #endregion*/

        public Form2()
        {
            InitializeComponent();
            btnTrain.Visible = false;
        }
        FaceRec faceRec = new FaceRec();
        private void btnCapture_Click(object sender, EventArgs e)
        {
            
            // Pasar el cuadro de imagen redimensionado a la función OpenCamera
            faceRec.openCamera(pictureBox1, pictureBox2);
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            faceRec.Save_IMAGE(txtPersonName.Text);



            // Obtener la imagen actual del PictureBox
            Image image = pictureBox2.Image;

            if (image != null)
            {
                // Crear el nombre del archivo basado en el nombre de la persona y la fecha actual
                string fileName = $"{txtPersonName.Text}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.jpg";

                // Ruta completa de la carpeta donde se guardarán las imágenes
                string folderPath = @"C:\\Users\\junio\\OneDrive\\Documentos\\UsuariosScaner"; // Reemplaza con la ruta de tu carpeta

                // Crear la carpeta si no existe
                Directory.CreateDirectory(folderPath);

                // Ruta completa del archivo de imagen
                string filePath = Path.Combine(folderPath, fileName);

                // Guardar la imagen en el archivo
                image.Save(filePath);

                // Mostrar mensaje de éxito
                MessageBox.Show("Imagen guardada con éxito.");
            }
        }

        private void btnTrain_Click(object sender, EventArgs e)
        {

        }

        private void btnDetectFaces_Click(object sender, EventArgs e)
        {
            faceRec.isTrained = true;
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            faceRec.Close();
            this.Close();
            faceRec.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }























        /*

          private void ProcessFrame(object sender, EventArgs e)
          {
              //Step 1: Video Capture
              if (videoCapture != null && videoCapture.Ptr != IntPtr.Zero)
              {
                  videoCapture.Retrieve(frame, 0);
                  currentFrame = frame.ToImage<Bgr, Byte>().Resize(picCapture.Width, picCapture.Height, Inter.Cubic);

                  //Step 2: Face Detection
                  if (facesDetectionEnabled)
                  {

                      //Convert from Bgr to Gray Image
                      Mat grayImage = new Mat();
                      CvInvoke.CvtColor(currentFrame, grayImage, ColorConversion.Bgr2Gray);
                      //Enhance the image to get better result
                      CvInvoke.EqualizeHist(grayImage, grayImage);

                      Rectangle[] faces = faceCasacdeClassifier.DetectMultiScale(grayImage, 1.1, 3, Size.Empty, Size.Empty);
                      //If faces detected
                      if (faces.Length > 0)
                      {

                          foreach (var face in faces)
                          {
                              //Draw square around each face 
                              // CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Red).MCvScalar, 2);

                              //Step 3: Add Person 
                              //Assign the face to the picture Box face picDetected
                              Image<Bgr, Byte> resultImage = currentFrame.Convert<Bgr, Byte>();
                              resultImage.ROI = face;
                              picDetected.SizeMode = PictureBoxSizeMode.StretchImage;
                              picDetected.Image = resultImage.Bitmap;

                              if (EnableSaveImage)
                              {
                                  //We will create a directory if does not exists!
                                  string path = @"C:\TrainedImages";
                                  if (!Directory.Exists(path))
                                      Directory.CreateDirectory(path);
                                  //we will save 10 images with delay a second for each image 
                                  //to avoid hang GUI we will create a new task
                                  Task.Factory.StartNew(() => {
                                      for (int i = 0; i < 10; i++)
                                      {
                                          //resize the image then saving it
                                          resultImage.Resize(200, 200, Inter.Cubic).Save(path + @"\" + txtPersonName.Text + "_" + DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss") + ".jpg");
                                          Thread.Sleep(1000);
                                      }
                                  });

                              }
                              EnableSaveImage = false;

                              if (btnAddPerson.InvokeRequired)
                              {
                                  btnAddPerson.Invoke(new ThreadStart(delegate {
                                      btnAddPerson.Enabled = true;
                                  }));
                              }

                              // Step 5: Recognize the face 
                              if (isTrained)
                              {
                                  Image<Gray, Byte> grayFaceResult = resultImage.Convert<Gray, Byte>().Resize(200, 200, Inter.Cubic);
                                  CvInvoke.EqualizeHist(grayFaceResult, grayFaceResult);
                                  var result = recognizer.Predict(grayFaceResult);
                                  pictureBox1.Image = grayFaceResult.Bitmap;
                                  pictureBox2.Image = TrainedFaces[result.Label].Bitmap;
                                  Debug.WriteLine(result.Label + ". " + result.Distance);
                                  //Here results found known faces
                                  if (result.Label != -1 && result.Distance < 2000)
                                  {
                                      CvInvoke.PutText(currentFrame, PersonsNames[result.Label], new Point(face.X - 2, face.Y - 2),
                                          FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                      CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
                                      Form1 form1 = new Form1();
                                      form1.ShowDialog();
                                  }
                                  //here results did not found any know faces
                                  else
                                  {
                                      CvInvoke.PutText(currentFrame, "Unknown", new Point(face.X - 2, face.Y - 2),
                                          FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                      CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Red).MCvScalar, 2);

                                  }
                              }
                          }
                      }
                  }

                  //Render the video capture into the Picture Box picCapture
                  picCapture.Image = currentFrame.Bitmap;
              }

              //Dispose the Current Frame after processing it to reduce the memory consumption.
              if (currentFrame != null)
                  currentFrame.Dispose();
          }





          //Step 4: train Images .. we will use the saved images from the previous example 
          private bool TrainImagesFromDir()
          {
              int ImagesCount = 0;
              double Threshold = 2000;
              TrainedFaces.Clear();
              PersonsLabes.Clear();
              PersonsNames.Clear();
              try
              {
                  string path = @"C:\TrainedImages";
                  string[] files = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories);

                  foreach (var file in files)
                  {
                      Image<Gray, byte> trainedImage = new Image<Gray, byte>(file).Resize(200, 200, Inter.Cubic);
                      CvInvoke.EqualizeHist(trainedImage, trainedImage);
                      TrainedFaces.Add(trainedImage);
                      PersonsLabes.Add(ImagesCount);
                      string name = file.Split('\\').Last().Split('_')[0];
                      PersonsNames.Add(name);
                      ImagesCount++;
                      Debug.WriteLine(ImagesCount + ". " + name);

                  }

                  if (TrainedFaces.Count() > 0)
                  {
                      // recognizer = new EigenFaceRecognizer(ImagesCount,Threshold);
                      recognizer = new EigenFaceRecognizer(ImagesCount, Threshold);
                      recognizer.Train(TrainedFaces.ToArray(), PersonsLabes.ToArray());

                      isTrained = true;
                      //Debug.WriteLine(ImagesCount);
                      //Debug.WriteLine(isTrained);
                      return true;
                  }
                  else
                  {
                      isTrained = false;
                      return false;
                  }
              }
              catch (Exception ex)
              {
                  isTrained = false;
                  MessageBox.Show("Error in Train Images: " + ex.Message);
                  return false;
              }

          }

          private void txtPersonName_TextChanged(object sender, EventArgs e)
          {

          }

          private void btnCapture_Click_1(object sender, EventArgs e)
          {
              //Dispose of Capture if it was created before
              if (videoCapture != null) videoCapture.Dispose();
              videoCapture = new Capture();
              //videoCapture.ImageGrabbed += ProcessFrame;
              Application.Idle += ProcessFrame;
              // videoCapture.Start();
          }

          private void btnAddPerson_Click_1(object sender, EventArgs e)
          {
              btnAddPerson.Enabled = false;
              EnableSaveImage = true;
          }

          private void btnTrain_Click_1(object sender, EventArgs e)
          {
              TrainImagesFromDir();
          }

          private void btnDetectFaces_Click_1(object sender, EventArgs e)
          {
              facesDetectionEnabled = true;
          }

         */
    }
}

