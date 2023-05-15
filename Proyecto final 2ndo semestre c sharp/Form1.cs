using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Proyecto_final_2ndo_semestre_c_sharp
{
    public partial class Form1 : Form
    {
        private List<Producto> listaProductos = new List<Producto>(); //The list of the products

        

        public Form1()
        {
            //Add the columns To the Table
            InitializeComponent();
            dtgProduct.Columns.Add("Name", "Name");
            dtgProduct.Columns.Add("Price", "Price");
            //this.FormBorderStyle = FormBorderStyle.None;
            dtgProduct.Columns.Add("Quantity", "Quantity");
            FormBorderStyle = FormBorderStyle.None;
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Add the new product to the list of the products
            if (string.IsNullOrEmpty(txtNombreDeProducto.Text) || string.IsNullOrEmpty(txtPrecioDelProducto.Text) || string.IsNullOrEmpty(txtCantidadDelProducto.Text))
            {
                MessageBox.Show("No hay información para agregar.", "Agregar producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Verificar si el producto ya existe en la lista
            string nombre = txtNombreDeProducto.Text;
            if (listaProductos.Any(p => p.Name == nombre))
            {
                MessageBox.Show("El producto ya existe en la lista.", "Agregar producto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Convertir el precio y la cantidad
            double precio;
            int cantidad;
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
            Producto newProduct = new Producto(nombre, precio, cantidad);
            listaProductos.Add(newProduct);

            // Clear 
            txtNombreDeProducto.Clear();
            txtCantidadDelProducto.Clear();
            txtPrecioDelProducto.Clear();

            UpdateTableOfproducts();
        }

        public class Producto
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public Producto(string name, double price, int quantity )
            {
                Name = name;
                Quantity = quantity;
                Price = price;
            }
        }
        

        private void UpdateTableOfproducts()
        {
            //Clean the table of the products
            dtgProduct.Rows.Clear();

            

            foreach (Producto p in listaProductos)
            {
                dtgProduct.Rows.Add(p.Name, p.Price, p.Quantity);
            }
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
           
            int indiceSeleccionado = dtgProduct.CurrentRow.Index;

            if (dtgProduct.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete the selected row?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                listaProductos.RemoveAt(indiceSeleccionado);
            }
            else
            {
                return;
            }

            UpdateTableOfproducts();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dtgProduct.SelectedRows.Count == 1)
            {
                // Establecemos la propiedad ReadOnly del DataGridView en false
                dtgProduct.ReadOnly = true;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            // Establecemos la propiedad ReadOnly de las filas no seleccionadas en true
            foreach (DataGridViewRow row in dtgProduct.Rows)
            {
                if (row.Index != e.RowIndex)
                {
                    row.ReadOnly = true;
                }
            }
        }

        private void btnOpenArchieve_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos TXT (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            openFileDialog1.Title = "Selecciona un archivo CSV";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(openFileDialog1.FileName);
                    string line = "";
                    string[] strArray;

                    while ((line = sr.ReadLine()) != null)
                    {
                        line = RemoveAccents(line);
                        strArray = line.Split(';');

                        // Check if the name is already in the list
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

                        // Add the product to the list
                        Producto newProduct = new Producto(strArray[0], price, quantity);
                        listaProductos.Add(newProduct);
                    }

                    sr.Close();
                    UpdateTableOfproducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            



        }
            
           
            }

        private string RemoveAccents(string input)
        {
            return new string(input
                .Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

       

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Archivos TXT (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            saveFileDialog1.Title = "Guardar archivo CSV";
            saveFileDialog1.FileName = "nuevo_archivo.csv";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        foreach (Producto p in listaProductos)
                        {
                            sw.WriteLine($"{p.Name};{p.Price};{p.Quantity}");
                        }
                    }

                    MessageBox.Show("Archivo guardado correctamente.", "Guardado exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
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
        }
    }
}
