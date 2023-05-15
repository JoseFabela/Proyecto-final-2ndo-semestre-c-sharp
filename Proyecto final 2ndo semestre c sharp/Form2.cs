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
using AForge.Video;
using AForge.Video.DirectShow;
using Emgu.CV;
using Emgu.CV.Structure;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Form2 : Form
    {
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice videoCaptureDevice;
        private readonly CascadeClassifier cascadeClassifier;
        public Form2()
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


        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            Image<Bgr, byte> grayImage = new Image<Bgr, byte>(bitmap);

            Rectangle[] rectangles = cascadeClassifier.DetectMultiScale(grayImage, 1.2, 1);

            foreach (Rectangle rectangle in rectangles)
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    using (Pen pen = new Pen(Color.Red, 1))
                    {
                        graphics.DrawRectangle(pen, rectangle);
                    }
                }
            }

            pictureBoxScaner.Image = bitmap;
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboDevice.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            if (videoCaptureDevice.IsRunning)
                videoCaptureDevice.Stop();
            this.Close();
            this.Hide();
            this.Visible = false;
            // Verificar si el formulario está cerrado
            if (IsDisposed)
                return;
            // aquí agregas el código para abrir el formulario de inicio de sesión
            Inicio_de_sesion inicio_De_Sesion = new Inicio_de_sesion();
            inicio_De_Sesion.ShowDialog();
            
        }
        private int imageCount = 0;

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (pictureBoxScaner.Image != null)
            {
                string fileName = "usuario" + (++imageCount).ToString() + ".jpg";
                string ruta = Path.Combine(@"C:\Users\junio\OneDrive\Documentos\UsuariosScaner", fileName);
                pictureBoxScaner.Image.Save(ruta, ImageFormat.Jpeg);
                MessageBox.Show("Imagen guardada correctamente en la ruta: " + ruta);
            }
            else
            {
                MessageBox.Show("No hay imagen para guardar");
            }
        }
    }

}

