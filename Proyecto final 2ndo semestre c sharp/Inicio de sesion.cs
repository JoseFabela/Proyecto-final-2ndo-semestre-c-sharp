using Accord.Statistics.Kernels;
using AForge.Video;
using AForge.Video.DirectShow;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Proyecto_final_2ndo_semestre_c_sharp.Usuario;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Inicio_de_sesion : Form
    {
        //These lines declare private variables used within the class.

        private int intentosFallidos = 0; //intentosFallidos is an integer variable to track the number of failed attempts.

        private string usuario; //usuario is a string variable to store the user.

        private FilterInfoCollection dispositivos;//dispositivos is a collection that holds the available video input devices.

        private VideoCaptureDevice fuenteVideo;//fuenteVideo is a VideoCaptureDevice object that represents the selected video capture device.

        private Bitmap fotogramaActual;//fotogramaActual is a Bitmap object to store the current frame from the video capture device.


        private const int MaximoIntentosFallidos = 3; //This line declares a constant integer variable MaximoIntentosFallidos and assigns it the value 3.
                                                      //It represents the maximum number of allowed failed attempts.
            //This is the constructor of the Inicio_de_sesion class.

        public Inicio_de_sesion()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;      
       
            //this.pictureBox1.ImageLocation = Path.Combine(Application.StartupPath, "Downloads", "flat-design-login-screen-template-vector.jpg");
          
            //It initializes the form components and sets the properties of pictureBox1 to display the image in either CenterImage or Zoom mode.

            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            btnLogWithFace.Visible = false;
            //It creates a new instance of FilterInfoCollection by passing FilterCategory.VideoInputDevice to retrieve the available video input devices.


            dispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            //It initializes the fuenteVideo object with the selected video capture device from dispositivos.

            fuenteVideo = new VideoCaptureDevice(dispositivos[0].MonikerString);
            //fuenteVideo.NewFrame += new NewFrameEventHandler(CapturarFrame);
            //It sets the form's border style to None, making it appear without a border.
            FormBorderStyle = FormBorderStyle.None;
        }
        //This method is an event handler for the Load event of the Inicio_de_sesion form.
        //It is triggered when the form is being loaded.

        private void Inicio_de_sesion_Load(object sender, EventArgs e)
        {
            // Obtener los dispositivos de captura de video disponibles
            FilterInfoCollection dispositivosVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);  //It retrieves the available video capture devices using FilterInfoCollection with the filter category FilterCategory.VideoInputDevice.


            if (dispositivosVideo.Count > 0)  //If at least one video capture device is found (dispositivosVideo.Count > 0), the first device in the collection is selected and assigned to the fuenteVideo variable.

            {
                fuenteVideo = new VideoCaptureDevice(dispositivosVideo[0].MonikerString);
                //It subscribes to the NewFrame event of fuenteVideo by adding the CapturarFrame event handler.

                fuenteVideo.NewFrame += CapturarFrame;
                //fuenteVideo.Start();
            }
            else
            {
                MessageBox.Show("No se encontraron dispositivos de captura de video.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//If no video capture devices are found, it displays an error message box.

            }
        }
        //This method is an event handler for the LinkClicked event of the LblRegistrarse link label.

        private void LblRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) //It is triggered when the link label is clicked.


        {
            //It creates an instance of the Registrarse form.


            Registrarse registrarse = new Registrarse();
            //It displays the registrarse form using ShowDialog(), which opens it as a modal dialog.

            registrarse.ShowDialog();
           // It hides the current form using this.Hide(), effectively hiding the login form.
           this.Hide();
            
        }

        private void CapturarFrame(object sender, NewFrameEventArgs eventArgs)  //This method is an event handler for the NewFrame event of the video capture device.
                                                                                //It is triggered when a new frame is captured by the device.
                                                                                //The method receives the captured frame through the eventArgs parameter.
                                                                                //It clones the captured frame by casting it to a Bitmap object and calling the Clone() method.
                                                                                //The cloned frame is stored in the fotogramaActual variable, which is of type Bitmap.
                                                                                //This allows you to have access to the current frame for further processing or display purposes.

        {
            // Capturar el fotograma actual
            fotogramaActual = (Bitmap)eventArgs.Frame.Clone();
        }

        //This method is an event handler for the "Log In" button click event.

        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            //It retrieves the entered username and password from the respective text boxes.

            string username = txtUser.Text;
            string password = txtPassword.Text;

            //The Usuario.BuscarUsuario(username, password) method is called to find the user with the given username and password.

            // Find the user with the given username
            Usuario usuario = Usuario.BuscarUsuario(username, password);

            if (usuario == null)
            {
                MessageBox.Show("El usuario no existe o la contraseña es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //If no user is found or the password is incorrect, an error message is displayed, and the intentosFallidos variable is incremented.

                intentosFallidos++;

                if (intentosFallidos == MaximoIntentosFallidos)
                {
                    //If the number of failed attempts reaches the maximum allowed(MaximoIntentosFallidos), the TomarFoto(username) method is called to take a photo.

                    TomarFoto(username);
                }
                return;
            }
            //If a user is found and the password is correct, the code inside the else block is executed.

            else
            {
                
            }
            // Mostrar el formulario de inventario
            //The inventory form (Form1) is displayed, and a message box is shown with the text "Que haremos el dia de hoy?" followed by the username.

            Form1 inventoryForm = new Form1();
            inventoryForm.Show();
            MessageBox.Show("Que haremos el dia de hoy?  "+ username);
            //The current form (Inicio_de_sesion) is hidden.
            this.Hide();
            }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        // Declara videoCaptureDevice en el nivel de la clase
        private VideoCaptureDevice videoCaptureDevice;
        private bool isScanned = false;

        //This method is an event handler for the "Log In with Face" button click event.

        private void btnLogWithFace_Click(object sender, EventArgs e)
        {
            //It creates an instance of Form3 (presumably a form for scanning and recognizing faces) and shows it as a dialog.

            Form3 scanear = new Form3();
            scanear.ShowDialog();

        }
        //This method is called when the number of failed login attempts reaches the maximum allowed (MaximoIntentosFallidos).

        private void TomarFoto(string username)
        {
            //It starts the video capture from the camera (fuenteVideo.Start()).

            // Iniciar la cámara
            fuenteVideo.Start();

            // Capturar el fotograma actual
            //It captures the current frame from the camera after a delay of 4 seconds (Thread.Sleep(4000)).

            Thread.Sleep(5000); // Esperar un segundo antes de tomar la foto (opcional)
            //The captured frame is stored in a Bitmap object named fotograma.

            Bitmap fotograma = (Bitmap)fotogramaActual.Clone();
            //The camera is stopped (fuenteVideo.Stop()).

            // Detener la cámara
            fuenteVideo.Stop();


            // Capturar el último fotograma
            if (fotogramaActual != null) //If a frame was successfully captured (fotogramaActual != null), the current timestamp is used to generate a unique filename for the photo.

            {
                string fecha = DateTime.Now.ToString("yyyyMMddHHmmss");
                string nombreArchivo = $"Foto_{username}_{fecha}.jpg";
                //The desktop folder path is obtained (rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).

                string rutaEscritorio = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                //The complete path for saving the photo is created by combining the desktop folder path and the generated filename (rutaCompleta = Path.Combine(rutaEscritorio, nombreArchivo)).

                string rutaCompleta = Path.Combine(rutaEscritorio, nombreArchivo);

                // Guardar la imagen en el escritorio
                //The photo is saved in JPEG format at the specified path (fotogramaActual.Save(rutaCompleta, System.Drawing.Imaging.ImageFormat.Jpeg)).

                fotogramaActual.Save(rutaCompleta, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }


        //This method is an event handler for the form's FormClosing event.

        private void FormInicioSesion_FormClosing(object sender, FormClosingEventArgs e)
        {
            //It is triggered when the form is being closed.
            //It checks if the video source (fuenteVideo) is not null and if it is currently running (fuenteVideo.IsRunning).
            if (fuenteVideo != null && fuenteVideo.IsRunning)
            {
                //If both conditions are met, it signals the video source to stop capturing frames (fuenteVideo.SignalToStop()).

                fuenteVideo.SignalToStop();
                //It waits for the video source to completely stop capturing frames (fuenteVideo.WaitForStop()).

                fuenteVideo.WaitForStop();
                //Finally, it sets the video source to null.

                fuenteVideo = null;
            }
        }
        //This method is an event handler for the "Sign Off" button click event.

        private void btnSignOff_Click(object sender, EventArgs e)
        {
            //It terminates the application by calling Application.Exit(), which closes all windows and ends the message loop.
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
