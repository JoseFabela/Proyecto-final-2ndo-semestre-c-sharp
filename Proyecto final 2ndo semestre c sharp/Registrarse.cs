using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
using System;
using System.Windows.Forms;
using System.Drawing;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;






namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Registrarse : Form
    {

        public Registrarse()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textUser.Text;
            string password = textPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            try
            {
                // Verificar si se seleccionó "recordar usuario"
                // Agregar el nuevo usuario
                Usuario.AgregarUsuario(username, password, confirmPassword);

                MessageBox.Show("Usuario registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos de texto
                textUser.Clear();
                textPassword.Clear();
                txtConfirmPassword.Clear();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
           

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Mostrar el formulario de inicio de sesión
           
            Inicio_de_sesion inicio_De_Sesion = new Inicio_de_sesion();
            inicio_De_Sesion.ShowDialog();
            this.Hide();
        }

        private void btnEscaneoRostro_Click(object sender, EventArgs e)
        {
            Form2 escaner = new Form2();
            escaner.ShowDialog();
        }
    }
}
