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
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        // Lista estática para almacenar los usuarios registrados
        private static List<Usuario> usuariosRegistrados = new List<Usuario>();
        
        public static void AgregarUsuario(string username, string password, string confirmPassword)
        {
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
            Usuario nuevoUsuario = new Usuario { Username = username, Password = password };
            usuariosRegistrados.Add(nuevoUsuario);
            // Si se seleccionó "recordar usuario", guardar la información en el archivo
            
                string path = @"C:\Users\junio\Downloads\usuarios.txt";

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine($"{username},{password}");
                }
            
        }

        public static Usuario BuscarUsuario(string username, string password)
        {
            // Buscar el usuario con el nombre de usuario y contraseña correspondientes
            Usuario usuario = usuariosRegistrados.FirstOrDefault(u => u.Username == username && u.Password == password);
            // Si no se encuentra en la lista, buscar en el archivo "usuarios.txt"
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
    }
}

