using FaceRecognition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.Threading;
using Timer = System.Windows.Forms.Timer;

namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Camara : Form
    {
        //This code defines a delegate named FotoTomadaEventHandler, which represents the signature of an event handler method that takes an object sender and an
        //Image foto as parameters and does not return a value.


        public delegate void FotoTomadaEventHandler(object sender, Image foto);
       



        public event FotoTomadaEventHandler FotoTomada; //The FotoTomada event is declared using the FotoTomadaEventHandler delegate. This event can be raised when a photo is taken,
        //and it allows other code to subscribe to the event and be notified when it occurs.

        private FilterInfoCollection videoDevices;//videoDevices is a variable that holds a collection of available video devices. It is of type FilterInfoCollection,
                                                  //which is typically used to store information about video capture devices.


        private VideoCaptureDevice videoSource;//videoSource is a variable that represents the selected video capture device. It is of type VideoCaptureDevice.



        // private ManualResetEvent captureCompleteEvent;
        private Bitmap fotogramaActual;//fotogramaActual is a variable that holds the current frame or snapshot captured from the video source. It is of type Bitmap.


        private Timer temporizador;//temporizador is a timer object that can be used to schedule recurring or one-time events. It is of type Timer.


        private int tiempoRestante = 5;//tiempoRestante is an integer variable that represents the remaining time in seconds. It is initialized with a value of 5.




        //This is the constructor method for the Camara class.


        public Camara()
        {
            //InitializeComponent() is called to initialize the components of the form associated with the Camara class.


            InitializeComponent();
            //The pictureBox1.SizeMode property is set to PictureBoxSizeMode.CenterImage and then immediately set to PictureBoxSizeMode.Zoom.
            //This may be redundant or unintended.


            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            //FormBorderStyle is set to FormBorderStyle.None, which removes the form's title bar and borders.


            FormBorderStyle = FormBorderStyle.None;
            //videoDevices is initialized with a collection of available video input devices using FilterInfoCollection and FilterCategory.VideoInputDevice.


            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            //videoSource is initialized with the selected video input device using VideoCaptureDevice and the first device in the videoDevices collection.


            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            //An event handler, CapturarFrame, is assigned to the NewFrame event of the videoSource to capture new frames from the video source.


            videoSource.NewFrame += new NewFrameEventHandler(CapturarFrame);
            //temporizador is initialized as a Timer object with an interval of 5000 milliseconds (5 seconds).


            temporizador = new Timer();
            temporizador.Interval = 5000; // 5 segundos
            //An event handler, TemporizadorTick, is assigned to the Tick event of the temporizador. This event handler will be executed when the timer interval elapses.


            temporizador.Tick += new EventHandler(TemporizadorTick);
        }
        //The Camara_Load event handler is triggered when the form is loaded.

        private void Camara_Load(object sender, EventArgs e)
        {
            //videoSource.Start() is called to start capturing video from the video source.

            videoSource.Start();
            ActualizarTiempo(); //ActualizarTiempo() is called to update the displayed time.



        }
        private void ActualizarTiempo()
        {
            //The ActualizarTiempo method updates the text of the labelTimer control with the value of the tiempoRestante variable converted to a string.

            labelTimer.Text = tiempoRestante.ToString();
        }

        //The btnCapture_Click event handler is triggered when the "Capture" button is clicked.

        private void btnCapture_Click(object sender, EventArgs e)
        {
            //temporizador.Start() is called to start the timer.

            temporizador.Start();

        }
        //temporizador.Start() is called to start the timer.

        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //The new frame is cloned and assigned to the pictureBox1.Image property to display it in the PictureBox control.

            // Muestra el nuevo fotograma en el control PictureBox
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }
        //The btnClose_Click event handler is triggered when the "Close" button is clicked.

        private void btnClose_Click(object sender, EventArgs e)
        {
            //this.Close() is called to close the form and terminate the camera capture.

            this.Close();

        }
        //The TemporizadorTick method is triggered when the timer interval elapses.

        private void TemporizadorTick(object sender, EventArgs e)
        {
            //videoSource.SignalToStop() is called to signal the video source to stop capturing frames.

            // Detener la cámara
            videoSource.SignalToStop();
            //videoSource.WaitForStop() is called to wait until the video source stops capturing frames.

            videoSource.WaitForStop();

            //TomarFoto() is called to capture the photo.

            // Tomar la foto
            TomarFoto();

            //this.Close() is called to close the form.

            // Cerrar el formulario
            this.Close();
        }
        //The TomarFoto method is responsible for capturing the photo.

        private void TomarFoto()
        {
            //If fotogramaActual is not null (a frame has been captured), the frame is saved as a JPEG image at the specified rutaImagen.

            if (fotogramaActual != null)
            {
                string rutaImagen = "C:\\Users\\junio\\OneDrive\\Imágenes\\camara\\Imagen.Png";
                fotogramaActual.Save(rutaImagen, System.Drawing.Imaging.ImageFormat.Png);

            }

        }
        //The CapturarFrame event handler is triggered when a new frame is captured from the video source.

        private void CapturarFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //The new frame is cloned and assigned to the fotogramaActual variable.

            // Capturar el fotograma actual
            fotogramaActual = (Bitmap)eventArgs.Frame.Clone();
            // Mostrar el fotograma en el PictureBox
            //The pictureBox1.Image property is set to the new frame to display it in the PictureBox control.

            pictureBox1.Image = fotogramaActual;
        }
       

    }
}
