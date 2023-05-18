using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Keras;
using Newtonsoft.Json.Linq;
using OpenTK;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static Proyecto_final_2ndo_semestre_c_sharp.Usuario;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Form1 : Form, ICerrarSesion
    {
        private List<Producto> listaProductos = new List<Producto>(); //The list of the products


        public Form1( )
        {
            //Add the columns To the Table
            InitializeComponent();
            dtgProduct.Columns.Add("Name", "Name");
            dtgProduct.Columns.Add("Price", "Price");
            dtgProduct.Columns.Add("Quantity", "Quantity");
            FormBorderStyle = FormBorderStyle.None; //This line sets the form's border style to None, which means that the form will have no border.
            this.pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage; //This line sets the PictureBox control named pictureBox1 to display the image in the center.
                                                                        //If the image is smaller than the PictureBox size, it will be displayed in the center of the PictureBox.


            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; //This line sets the PictureBox control named pictureBox1 to display the image by zooming it to fit
                                                                 //within the PictureBox bounds.The aspect ratio of the image will be preserved, and the image will be resized to completely fit within the PictureBox.





            dtgProduct.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Automatically adjust the size of the first column based on the content
            dtgProduct.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; //Automatically adjusts the size of the second column to fill the remaining space
            dtgProduct.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Automatically adjust the size of the third column to fill the remaining space

            dtgProduct.DefaultCellStyle.BackColor = Color.Blue; // Change the background color of the cells
            dtgProduct.DefaultCellStyle.ForeColor = Color.White; // Change the text color of the cells
            dtgProduct.AlternatingRowsDefaultCellStyle.BackColor = Color.Black; // Change the background color of alternate rows

            dtgProduct.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;// Change the background color of the column headers
            dtgProduct.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; // Change the text color of the column headers
            dtgProduct.ColumnHeadersDefaultCellStyle.Font = new Font(dtgProduct.Font, FontStyle.Bold);// Change the font style of the column headers

            btnAdd.BackColor = Color.ForestGreen; // Cambia el color de fondo a azul
            btnDelete.BackColor = Color.ForestGreen;
            btnEdit.BackColor = Color.ForestGreen;
            btnSave.BackColor = Color.DarkRed; // Cambia el color de fondo a azul
            btnOpenArchieve.BackColor = Color.DarkRed;
            btnSignOff.BackColor = Color.DarkMagenta;
            btnExit.BackColor = Color.DarkCyan; // Cambia el color de fondo a azul
            btnModificarUsuario.BackColor = Color.DarkKhaki;
            button1.BackColor = Color.DarkKhaki;


            btnAdd.FlatStyle = FlatStyle.Flat; // Set a flat border style
            btnAdd.FlatAppearance.BorderSize = 2; // Set the border width
            btnAdd.FlatAppearance.BorderColor = Color.Black; // Set the border color
            btnDelete.FlatStyle = FlatStyle.Flat; // Set a flat border style
            btnDelete.FlatAppearance.BorderSize = 2; // Set the border width
            btnDelete.FlatAppearance.BorderColor = Color.Black; // Set the border color
            btnEdit.FlatStyle = FlatStyle.Flat; // Set a flat border style
            btnEdit.FlatAppearance.BorderSize = 2; // Set the border width
            btnEdit.FlatAppearance.BorderColor = Color.Black; // Set the border color
            btnSave.FlatStyle = FlatStyle.Flat; // Set a flat border style
            btnSave.FlatAppearance.BorderSize = 2; // Set the border width
            btnSave.FlatAppearance.BorderColor = Color.Black; // Set the border color
            btnOpenArchieve.FlatStyle = FlatStyle.Flat; // Set a flat border style
            btnOpenArchieve.FlatAppearance.BorderSize = 2; // Set the border width
            btnOpenArchieve.FlatAppearance.BorderColor = Color.Black; // Set the border color
            btnSignOff.FlatStyle = FlatStyle.Flat; // Set a flat border style
            btnSignOff.FlatAppearance.BorderSize = 2; // Set the border width
            btnSignOff.FlatAppearance.BorderColor = Color.Black; // Set the border color
            btnExit.FlatStyle = FlatStyle.Flat; // Set a flat border style
            btnExit.FlatAppearance.BorderSize = 2; // Set the border width
            btnExit.FlatAppearance.BorderColor = Color.Black; // Set the border color
            btnModificarUsuario.FlatStyle = FlatStyle.Flat; // Set a flat border style
            btnModificarUsuario.FlatAppearance.BorderSize = 2; // Set the border width
            btnModificarUsuario.FlatAppearance.BorderColor = Color.Black; // Set the border color
            button1.FlatStyle = FlatStyle.Flat; // Set a flat border style
            button1.FlatAppearance.BorderSize = 2; // Set the border width
            button1.FlatAppearance.BorderColor = Color.Black; // Set the border color





        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            graphicsPath.AddEllipse(0, 0, pictureBox1.Width - 1, pictureBox1.Height - 1);
            pictureBox1.Region = new Region(graphicsPath);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Paint += new PaintEventHandler(pictureBox1_Paint);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Add the new product to the list of the products
            //We check if the txtNombreDeProducto, txtPrecioDelProducto, and txtCantidadDelProducto fields are empty using string.IsNullOrEmpty.
            //If any of them is empty, we show a warning message and return, preventing adding a product without complete information.
            if (string.IsNullOrEmpty(txtNombreDeProducto.Text) || string.IsNullOrEmpty(txtPrecioDelProducto.Text) || string.IsNullOrEmpty(txtCantidadDelProducto.Text))
            {
                MessageBox.Show("No hay información para agregar.", "Agregar producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el producto ya existe en la lista
            string nombre = txtNombreDeProducto.Text;
            //We check if the product already exists in the list using listaProductos.Any(p => p.Name == name). If it already exists,
            //we show a warning message and return.
            if (listaProductos.Any(p => p.Name == nombre))
            {
                MessageBox.Show("El producto ya existe en la lista.", "Agregar producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Convertir el precio y la cantidad
            double precio;
            int cantidad;
           // We convert the price and quantity entered in the text fields to the double and int data types, respectively.
           // We use double.TryParse and int.TryParse to check if the values are numeric and perform the conversion.
           // If the format is invalid, we show an error message and return.
            if (!double.TryParse(txtPrecioDelProducto.Text, out precio))
            {
                MessageBox.Show("El formato del precio es incorrecto");
                return;
            }

            if (!int.TryParse(txtCantidadDelProducto.Text, out cantidad))
            {
                MessageBox.Show("El formato de la cantidad es incorrecto");
                return;
            }

            // Crear el nuevo producto y agregarlo a la lista
           // We create a new Producto object with the entered name, price, and quantity, and add it to the listaProductos list.
            Producto newProduct = new Producto(nombre, precio, cantidad);
            listaProductos.Add(newProduct);

            // We clear the input fields.

            txtNombreDeProducto.Clear();
            txtCantidadDelProducto.Clear();
            txtPrecioDelProducto.Clear();
            //We call the UpdateTableOfproducts method to update the product table and display the newly added product.
            UpdateTableOfproducts();
        }
        //public class Producto: This line declares a public class named Producto.

        public class Producto
        {
            //public string Name { get; set; }: This line declares a public property Name of type string in the Producto class.
            //It has a getter and a setter, allowing the name of the product to be accessed and modified.

            public string Name { get; set; }

            //public int Quantity { get; set; }: This line declares a public property Quantity of type int in the Producto class.
            //It has a getter and a setter, allowing the quantity of the product to be accessed and modified.
            public int Quantity { get; set; }
          
            
            // public double Price { get; set; }: This line declares a public property Price of type double in the Producto class.
           // It has a getter and a setter, allowing the price of the product to be accessed and modified.
            public double Price { get; set; }
           
            
            //public Producto(string name, double price, int quantity): This line declares a constructor for the Producto class
            //that takes three parameters: name (string), price (double), and quantity (int). The constructor is used to create a new
            //instance of the Producto class and initializes its Name, Price, and Quantity properties with the values passed as arguments.

            public Producto(string name, double price, int quantity )
            {
                Name = name;
                Quantity = quantity;
                Price = price;
            }
        }
        //private void UpdateTableOfproducts(): This line declares a private method named UpdateTableOfproducts without any parameters.
        //This method is responsible for updating the table of products.

        private void UpdateTableOfproducts()
        {
            //Clean the table of the products
           // dtgProduct.Rows.Clear(): This line clears all the rows in the dtgProduct DataGridView control.
           // It removes all the existing product entries from the table.

            dtgProduct.Rows.Clear();


            //foreach (Producto p in listaProductos): This line starts a loop that iterates through each Producto object (p) in 
            //the listaProductos list.

            foreach (Producto p in listaProductos)
            {

                //dtgProduct.Rows.Add(p.Name, p.Price, p.Quantity): This line adds a new row to the dtgProduct DataGridView control
                //with the values of p.Name, p.Price, and p.Quantity. It populates the table with the name, price, and quantity of
                //each product in the listaProductos list.

                dtgProduct.Rows.Add(p.Name, p.Price, p.Quantity);
            }
        }

        //private void btnDelete_Click(object sender, EventArgs e): This line declares a private method named btnDelete_Click
        //that handles the click event of a button (presumably a delete button).

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //int indiceSeleccionado = dtgProduct.CurrentRow.Index;: This line retrieves the index of the currently selected row
            //in the dtgProduct DataGridView control and assigns it to the indiceSeleccionado variable.

            int indiceSeleccionado = dtgProduct.CurrentRow.Index;


            //if (dtgProduct.SelectedRows.Count == 0): This line checks if there are no selected rows in the dtgProduct
            //DataGridView control. If there are no selected rows, it displays an error message and returns from the method.

            if (dtgProduct.SelectedRows.Count == 0)
            {
                //MessageBox.Show("Please select a row to delete.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error):
                //This line displays an error message box indicating that the user should select a row before deleting.

                MessageBox.Show("Please select a row to delete.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //if (MessageBox.Show("Are you sure you want to delete the selected row?", "Confirmation", MessageBoxButtons.YesNo) ==
            //DialogResult.Yes): This line shows a confirmation message box asking the user if they want to delete the selected row.
            //If the user clicks "Yes," the condition is true and the following block of code executes.

            if (MessageBox.Show("Are you sure you want to delete the selected row?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //listaProductos.RemoveAt(indiceSeleccionado): This line removes the Producto object at the index specified by
                //indiceSeleccionado from the listaProductos list. It effectively deletes the selected product from the list.

                listaProductos.RemoveAt(indiceSeleccionado);
            }
            //else: If the user clicks "No" in the confirmation message box, this line is executed.
            //It simply returns from the method without performing any further actions.

            else
            {
                return;
            }
            //UpdateTableOfproducts(): This line calls the UpdateTableOfproducts method to refresh the table after deleting the row.
            //It ensures that the table is updated with the latest product information after a deletion.

            UpdateTableOfproducts();
        }
        //private void btnEdit_Click(object sender, EventArgs e): This line declares a private method named btnEdit_Click that
        //handles the click event of a button (presumably an edit button).

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //if (dtgProduct.SelectedRows.Count == 1): This line checks if there is exactly one selected row in the dtgProduct
            //DataGridView control.

            if (dtgProduct.SelectedRows.Count == 1)
            {
                
                // Establecemos la propiedad ReadOnly del DataGridView en false: This comment suggests that the intention might be to set the ReadOnly property of the DataGridView to false. However, the actual code sets it to true, which seems contradictory. It's possible that this code snippet is incomplete or needs to be modified.

                dtgProduct.ReadOnly = true;
            }
        }
        //private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e): This line declares a private method
        //named dataGridView1_CellEnter that handles the CellEnter event of the dataGridView1 DataGridView control
        //(or dtgProduct as mentioned in the comments).

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Establecemos la propiedad ReadOnly de las filas no seleccionadas en true: This comment suggests that the intention is
            // to set the ReadOnly property of the non-selected rows to true.


            //foreach (DataGridViewRow row in dtgProduct.Rows): This line initiates a loop that iterates over each row in the dtgProduct
            //DataGridView control.

            foreach (DataGridViewRow row in dtgProduct.Rows)
            {

                //if (row.Index != e.RowIndex): This line checks if the current row index is not equal to the row index that triggered
                //the CellEnter event. It ensures that the property is only modified for non-selected rows.

                if (row.Index != e.RowIndex)
                {
                    //row.ReadOnly = true;: This line sets the ReadOnly property of the current row to true, making it read-only.

                    row.ReadOnly = true;
                }
            }
        }
        //OpenFileDialog openFileDialog1 = new OpenFileDialog();: This line creates a new instance of the OpenFileDialog class
        //to open a file dialog box for selecting a file.

        private void btnOpenArchieve_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.Filter = "Archivos TXT (*.txt)|*.txt|Todos los archivos (*.*)|*.*";:
            //This line sets the file filter for the dialog to display only TXT files or all files.

            openFileDialog1.Filter = "Archivos TXT (*.txt)|*.txt|Todos los archivos (*.*)|*.*";

            //openFileDialog1.Title = "Selecciona un archivo CSV";: This line sets the title of the dialog box to "Selecciona un archivo CSV".

            openFileDialog1.Title = "Selecciona un archivo CSV";

            //if (openFileDialog1.ShowDialog() == DialogResult.OK): This line displays the file dialog and checks if the
            //user clicked the OK button to select a file.

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //StreamReader sr = new StreamReader(openFileDialog1.FileName);: This line creates a StreamReader
                    //to read the contents of the selected file.

                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    //string line = "";: This line declares a variable to store each line read from the file.

                    string line = "";
                    //string[] strArray;: This line declares an array to store the split values of each line.

                    string[] strArray;
                    //while ((line = sr.ReadLine()) != null): This line reads each line from the file until there are no more
                    //lines (end of file).

                    while ((line = sr.ReadLine()) != null)
                    {
                        //line = RemoveAccents(line);: This line calls a method RemoveAccents to remove any accents from the line.
                        //This method is not shown in the provided code.

                        line = RemoveAccents(line);
                        //strArray = line.Split(';');: This line splits the line into an array of strings using a semicolon (;)
                        //as the delimiter.

                        strArray = line.Split(';');

                        // Check if the name is already in the list
                        //if (listaProductos.Any(p => p.Name == strArray[0])): This line checks if the name in strArray[0] already
                        //exists in the listaProductos list by using the LINQ Any method.

                        if (listaProductos.Any(p => p.Name == strArray[0]))
                        {
                            MessageBox.Show($"El producto con el nombre {strArray[0]} ya ha sido agregado anteriormente.", "Error al agregar producto", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }

                        // Convert the price and quantity to the correct data types
                        double price;
                        int quantity;

                        if (!double.TryParse(strArray[1], NumberStyles.Float, CultureInfo.InvariantCulture, out price))
                        {
                            MessageBox.Show("El formato del precio es incorrecto.");
                            continue;
                        }

                        if (!int.TryParse(strArray[2], out quantity))
                        {
                            MessageBox.Show("El formato de la cantidad es incorrecto.");
                            continue;
                        }
                        //  This code block attempts to convert the price(strArray[1]) and quantity(strArray[2]) from strings to the appropriate
                        //  data types(double and int, respectively).If the conversion fails, a message box is displayed with an error message,
                        //  and the continue statement skips the rest of the loop iteration.




                        //This code block creates a new Producto object with the extracted values (strArray[0] for name, price for price,
                        //and quantity for quantity), and adds it to the listaProductos list.

                        // Add the product to the list
                        Producto newProduct = new Producto(strArray[0], price, quantity);
                        listaProductos.Add(newProduct);
                    }
                    //This code block closes the StreamReader to release the file resources and calls the UpdateTableOfproducts method to
                    //update the table display with the newly added products.

                    sr.Close();
                    UpdateTableOfproducts();
                }
                //This code block catches any exception that occurs during the file reading or data conversion process and displays an error
                //message box with the exception message.


                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            



        }
            
           
            }
        //This method takes an input string and removes any accents from it using Unicode normalization and filtering out non-spacing marks.
        //It returns the modified string without accents.

        private string RemoveAccents(string input)
        {
            return new string(input
                .Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }


        //This code block handles the event when the "Save" button is clicked. It opens a SaveFileDialog to choose the location and name for
        //the CSV file to be saved.

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archivos TXT (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            saveFileDialog1.Title = "Guardar archivo CSV";
            saveFileDialog1.FileName = "nuevo_archivo.csv";
            //If the user selects a file and clicks "OK" in the dialog, the code continues to execute. Otherwise, nothing happens.

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Inside the try block, a StreamWriter is created to write to the selected file.

                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        //For each Producto object in the listaProductos list, a line is written to the file in the format:
                        //"Name;Price;Quantity", using string interpolation.

                        foreach (Producto p in listaProductos)
                        {
                            sw.WriteLine($"{p.Name};{p.Price};{p.Quantity}");
                        }
                        //After writing all the product lines, the file is closed.

                    }

                    MessageBox.Show("Archivo guardado correctamente.", "Guardado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //If the process completes without any exceptions, a message box is displayed indicating the successful file saving.

                catch (Exception ex)
                {
                    //If an exception occurs during the file saving process, an error message box is displayed with the exception message.
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message, "Error al guardar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtgProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }
        //This code block handles the event when the "Modificar Usuario" (Modify User) button is clicked.
        //It opens an OpenFileDialog to choose an image file.

        private void btnModificarUsuario_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //The filter property is set to restrict file selection to image formats such as jpg, jpeg, png, gif, and bmp.

            openFileDialog.Filter= "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            //If the user selects a file and clicks "OK" in the dialog, the code continues to execute. Otherwise, nothing happens.

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Inside the try block, the selected file's path is obtained from the FileName property of the OpenFileDialog.

                try
                {
                    // Obtener la ruta del archivo seleccionado
                    string rutaImagen = openFileDialog.FileName;
                  //  The image from the selected file is loaded and assigned to the Image property of the pictureBox1 control, which displays the image.

                    // Mostrar la imagen en el PictureBox
                    pictureBox1.Image = Image.FromFile(rutaImagen);
                }
                //If an exception occurs during the image opening process, an error message box is displayed with the exception message.
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir la imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //This code block handles the event when the "button1" is clicked. It creates an instance of the "Camara" form and shows
        //it as a modal dialog. The user interaction with the "Camara" form blocks interaction with other forms until it is closed.

        private void button1_Click(object sender, EventArgs e)
        {
            Camara camara = new Camara();
            camara.ShowDialog();
            
        }
        //This is a method named "CerrarSesion" (Logout) that performs the logic to log out the user.

        public void CerrarSesion()
        {
            // Código para cerrar sesión
            // ...
            //The code for logging out is not shown and should be implemented as per the specific requirements of the application.

            // Redirigir al formulario de inicio de sesión
            //After the user is logged out, a new instance of the "Inicio_de_sesion" (Login) form is created.

            Inicio_de_sesion formInicioSesion = new Inicio_de_sesion();
            //The "Inicio_de_sesion" form is displayed using the Show method, and the current form is hidden using the Hide method.

            formInicioSesion.Show();
            this.Hide();
        }
        //This code block handles the event when the "btnSignOff" (Sign Off) button is clicked.

        private void btnSignOff_Click(object sender, EventArgs e)
        {
           // It calls the "CerrarSesion" method on the current form by accessing it through the ICerrarSesion interface.

            // Llamar al método de la interfaz para cerrar sesión
            ICerrarSesion cerrarSesion = this;
            //The ICerrarSesion interface is assumed to be implemented by the current form or one of its parent forms, allowing access
            //to the "CerrarSesion" method.
            cerrarSesion.CerrarSesion();
        }
    }
}