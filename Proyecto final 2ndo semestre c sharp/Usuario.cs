using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public class Usuario
    {
        //The get and set keywords indicate that this property has both a getter and a setter, allowing read and write access to the value.
                public string Username { get; set; } //This line defines a public property named Username of type string.
        
        //Like the previous property, it has both a getter and a setter.
                public string Password { get; set; }//This line defines a public property named Password of type string.
        
        //It also has both a getter and a setter.
                public string ConfirmPassword { get; set; }//This line defines a public property named ConfirmPassword of type string.


        // Lista estática para almacenar los usuarios registrados
        private static List<Usuario> usuariosRegistrados = new List<Usuario>(); //List<Usuario> indicates that it is a generic list that can store objects of the Usuario class or any derived class.
        //usuariosRegistrados is the name of the list variable.
        //new List<Usuario>() creates a new instance of the List<Usuario> class and initializes it as an empty list.



        //This method takes three parameters: username, password, and confirmPassword. It checks if the provided username is already registered in the usuariosRegistrados
        //list. If it is, it throws an ArgumentException with an error message.

        public static void AgregarUsuario(string username, string password, string confirmPassword)
        {
            //This code block checks if the password and confirmPassword parameters match. If they don't, it throws an ArgumentException with an error message.

            // Verificar si el usuario ya está registrado
            if (usuariosRegistrados.Any(u => u.Username == username))
            {
                throw new ArgumentException("El nombre de usuario ya está en uso.", nameof(username)); 
            }
            // Verificar si la contraseña y la confirmación de contraseña coinciden
            if (password != confirmPassword)
            {
                throw new ArgumentException("La contraseña y la confirmación de contraseña no coinciden.", nameof(confirmPassword));
            }
            // Crear un nuevo usuario y agregarlo a la lista
            //This section creates a new Usuario object using the provided username and password values, and then adds it to the usuariosRegistrados list.

            Usuario nuevoUsuario = new Usuario { Username = username, Password = password };
            usuariosRegistrados.Add(nuevoUsuario);
            // Si se seleccionó "recordar usuario", guardar la información en el archivo
            //This part saves the user information to a file. It specifies the file path as C:\Users\junio\Downloads\usuarios.txt and uses a StreamWriter
            //to write the username and password values to the file.

            string path = @"C:\Users\junio\Downloads\usuarios.txt";

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine($"{username},{password}");
                }
            
        }
        //This method takes two parameters: username and password. It searches for a user in the usuariosRegistrados list whose Username and Password properties
        //match the provided values using the FirstOrDefault LINQ method. The found Usuario object is assigned to the usuario variable.

        public static Usuario BuscarUsuario(string username, string password)
        {
            // Buscar el usuario con el nombre de usuario y contraseña correspondientes
            Usuario usuario = usuariosRegistrados.FirstOrDefault(u => u.Username == username && u.Password == password);
            // Si no se encuentra en la lista, buscar en el archivo "usuarios.txt"


            //If the usuario variable is still null after searching in the usuariosRegistrados list, this code block is executed.
            //It reads the contents of the file at the path "C:\Users\junio\Downloads\usuarios.txt" and iterates over each line.
            //It splits each line by a comma and checks if the first field (campos[0]) matches the provided username and the second field (campos[1]) matches the
            //provided password. If a match is found, a new Usuario object is created and assigned to the usuario variable, and the loop is exited using break.

            if (usuario == null)
            {
                string path = @"C:\Users\junio\Downloads\usuarios.txt";
                if (File.Exists(path))
                {
                    string[] usuarios = File.ReadAllLines(path);

                    foreach (string usuarioString in usuarios)
                    {
                        string[] campos = usuarioString.Split(',');
                        if (campos[0] == username && campos[1] == password)
                        {
                            usuario = new Usuario { Username = username, Password = password };
                            break;
                        }
                    }
                }
            }

            return usuario;
        }

        public interface ICerrarSesion
        {
            void CerrarSesion();
        }
        //This code defines the ICerrarSesion interface with a single method CerrarSesion().
        //Interfaces in C# are used to define contracts that specify the behavior that implementing classes must adhere to.
        //In this case, the interface specifies that any class implementing ICerrarSesion must provide an implementation for the CerrarSesion() method.







    }

}

