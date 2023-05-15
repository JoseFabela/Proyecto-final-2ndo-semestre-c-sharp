using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Inicio_de_sesion : Form
    {

        public Inicio_de_sesion()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;      
            //this.pictureBox1.ImageLocation = Path.Combine(Application.StartupPath, "Downloads", "flat-design-login-screen-template-vector.jpg");
            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void Inicio_de_sesion_Load(object sender, EventArgs e)
        {

        }
             
        private void LblRegistrarse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           

            Registrarse registrarse = new Registrarse();
            
            registrarse.ShowDialog();
            this.Hide();
            
        }

        private void BtnLogIn_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPassword.Text;
            // Find the user with the given username
            Usuario usuario = Usuario.BuscarUsuario(username, password);

            if (usuario == null)
            {
                MessageBox.Show("El usuario no existe o la contraseña es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Bienvenido "+ username);

            }
            // Mostrar el formulario de inventario
            Form1 inventoryForm = new Form1();
            inventoryForm.Show();
            MessageBox.Show("Que haremos el dia de hoy?  "+ username);
            this.Hide();


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        // Declara videoCaptureDevice en el nivel de la clase
        private VideoCaptureDevice videoCaptureDevice;
        private bool isScanned = false;


        private void btnLogWithFace_Click(object sender, EventArgs e)
        {
            Form3 scanear = new Form3();
            scanear.ShowDialog();

        }
    }
}
