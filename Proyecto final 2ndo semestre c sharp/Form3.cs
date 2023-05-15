using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using Keras;
using Keras.Models;
using System.Windows.Forms;
using Accord.Imaging;
using Accord.MachineLearning;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;

using Accord.Statistics.Kernels;

using AForge.Video;
using Emgu.CV.Structure;
using Emgu.CV;

namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Form3 : Form
    {


        // ...

        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice videoCaptureDevice;
        private readonly CascadeClassifier cascadeClassifier;
        private SupportVectorMachine<Gaussian> svm;
        public Form3()
        {
            InitializeComponent();
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (filterInfoCollection.Count == 0)
            {
                MessageBox.Show("No se encontraron cámaras en su dispositivo");
                return;
            }

            foreach (FilterInfo filterInfo in filterInfoCollection)
                cboDevice.Items.Add(filterInfo.Name);

            cboDevice.SelectedIndex = 0;

            cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt_tree.xml");

        }

        private void FormRegistro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop();
            }
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }
              
    private void Form3_Load(object sender, EventArgs e)
    {
            svm = new SupportVectorMachine<Gaussian>();

            // Entrenar el modelo SVM con los datos adecuados
            double[][] inputs = GetTrainingData(); // Obtener los datos de entrenamiento
            int[] outputs = GetLabels(); // Obtener las etiquetas correspondientes

            // Definir el kernel a utilizar (por ejemplo, kernel Gaussiano)
            Gaussian kernel = new Gaussian();

            // Crear un objeto de tipo SequentialMinimalOptimization para entrenar el SVM
            var teacher = new SequentialMinimalOptimization<Gaussian>()
            {
                Complexity = 100, // Ajustar la complejidad del modelo (hiperparámetro)
                Kernel = kernel
            };

            svm = teacher.Learn(inputs, outputs); // Entrenar el modelo SVM

            // Inicializar el dispositivo de captura de video
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();

        }










    private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop();
            }
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            Rectangle[] rectangles = cascadeDetector.ProcessFrame(bitmap);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (Pen pen = new Pen(Color.Red, 1))
                {
                    foreach (Rectangle rectangle in rectangles)
                    {
                        graphics.DrawRectangle(pen, rectangle);
                    }
                }
            }

            pbScan.Image = bitmap;

            if (rectangles.Length > 0)
            {
                // Obtener las características de la cara escaneada
                double[] features = cascadeDetector.GetFeatures(rectangles[0]);

                // Comparar la cara escaneada con las caras guardadas en la carpeta
                double decision = svm.Decide(bagOfVisualWords.BagOfFeatures.Transform(features));

                if (decision == 1)
                {
                    MessageBox.Show("Inicio de sesión exitoso.", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el formulario de inventario
                    var formInventory = new Form1();
                    formInventory.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("No existe registro de la cara escaneada.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        

       

        private void btnScanne_Click_1(object sender, EventArgs e)
        {
            if (pbScan.Image != null)
            {
                Bitmap scannedImage = (Bitmap)pbScan.Image;
                double[] features = cascadeDetector.GetFeatures(scannedImage);

                // Predecir si la cara escaneada es una coincidencia usando el modelo SVM
                int prediction = svm.Decide(features);

                if (prediction == 1) // Si la predicción es 1, es una coincidencia
                {
                    MessageBox.Show("Inicio de sesión exitoso.", "Inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Abrir el formulario de inventario
                    Form1 formInventory = new Form1();
                    formInventory.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("No existe registro de la cara escaneada.", "Error de inicio de sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No hay imagen para escanear.", "Error de escaneo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.SignalToStop();
                videoCaptureDevice.WaitForStop();
            }
            this.Close();
        }
    }
}
