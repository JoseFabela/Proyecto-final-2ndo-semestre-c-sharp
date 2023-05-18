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
        //The Registrarse constructor is called when a new instance of the Registrarse form is created.

        public Registrarse()
        {
            InitializeComponent();//InitializeComponent() initializes the components of the form.

            btnEscaneoRostro.Visible = false; 

            FormBorderStyle = FormBorderStyle.None; //FormBorderStyle = FormBorderStyle.None; sets the form's border style to None, removing the form's border.


        }
        //The button1_Click event handler is triggered when the associated button is clicked.

        private void button1_Click(object sender, EventArgs e)
        {
            //It retrieves the values entered in the textUser, textPassword, and txtConfirmPassword text boxes.

            string username = textUser.Text;
            string password = textPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            //The if condition checks if any of the fields are null or empty. If so, it displays a message box indicating that it cannot be null and returns.

            if (username==null|| password == null || confirmPassword ==null || username == "" || password == "" || confirmPassword == " " || username == " " || password == " " || confirmPassword == " ") { MessageBox.Show("NO PUEDE SER NULO"); return; }

            try
            {
                // Verificar si se seleccionó "recordar usuario"
                // Agregar el nuevo usuario
                //If all fields have values, it attempts to add the new user by calling Usuario.AgregarUsuario(username, password, confirmPassword).

                Usuario.AgregarUsuario(username, password, confirmPassword);
                //If successful, it displays a message box indicating successful registration and clears the text boxes.

                MessageBox.Show("Usuario registrado correctamente.", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar los campos de texto
                textUser.Clear();
                textPassword.Clear();
                txtConfirmPassword.Clear();
            }
            //If an ArgumentException is thrown during the user addition process, it catches the exception and displays its message in a message box with an error icon.
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //The linkLabel1_LinkClicked event handler is triggered when the associated link label is clicked.

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Mostrar el formulario de inicio de sesión
            //It creates a new instance of the Inicio_de_sesion form.

            Inicio_de_sesion inicio_De_Sesion = new Inicio_de_sesion();
            //It displays the Inicio_de_sesion form as a modal dialog using ShowDialog(). This means that the current form will be hidden until the Inicio_de_sesion
            //form is closed.

            inicio_De_Sesion.ShowDialog();
            //After the Inicio_de_sesion form is closed, it hides the current form using this.Hide().

            this.Hide();
        }
        //The btnEscaneoRostro_Click event handler is triggered when the associated button is clicked.

        private void btnEscaneoRostro_Click(object sender, EventArgs e)
        {
            //It creates a new instance of the Form2 form.

            Form2 escaner = new Form2();
            //It displays the Form2 form as a modal dialog using ShowDialog(). This means that the current form will be hidden until the Form2 form is closed.

            escaner.ShowDialog();
            //After the Form2 form is closed, it hides the current form using this.Hide().

            this.Hide();
        }
    }
}
