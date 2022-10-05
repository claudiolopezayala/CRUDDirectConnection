using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopTech
{
    public partial class AgenteDeVentas : Form
    {
        private List<Producto> productos = new List<Producto>();
        private Dictionary<string, int> compra = new Dictionary<string, int>();
        private int img = 0;
        private int idVendedor;
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=TOPTECH; User=root; Password=root");

        public AgenteDeVentas(int _idVEndedor)
        {
            InitializeComponent();
            this.idVendedor = _idVEndedor;
            this.productos.Add(new Producto("Teclados", 0));
            this.productos.Add(new Producto("Audifonos", 1));
            this.productos.Add(new Producto("Procesadores", 2));
            foreach (Producto x in productos)
            {
                compra.Add(x.nombre, 0);
                this.cmbProductos.Items.Add(x.nombre);
            }
        }

        //botones imagenes
        private void btnIzq_Click(object sender, EventArgs e)
        {
            img--;
            if (img < 0)
            {
                img = 2;
            }
            lblProductos.ImageIndex = img;
            int index = img;
            this.lblNombreP.Text = productos[index].nombre;
            this.cmbProductos.SelectedIndex = productos[index].nImg;
            this.txtCantidad.Text = "";
        }
        private void btnDer_Click(object sender, EventArgs e)
        {
            img++;
            if (img > 2)
            {
                img = 0;
            }
            lblProductos.ImageIndex = img;
            int index = img;
            this.lblNombreP.Text = productos[index].nombre;
            this.cmbProductos.SelectedIndex = productos[index].nImg;
            this.txtCantidad.Text = "";
        }

        private void btnAgregarP_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                this.compra[lblNombreP.Text] += 1;
            }
            else
            {
                this.compra[lblNombreP.Text] += int.Parse(txtCantidad.Text);
            }
            this.txtCantidad.Text = "";
            this.lstCarrito.Items.Clear();
            foreach (var x in compra)
            {
                if(x.Value != 0)
                    this.lstCarrito.Items.Add(x.Key + " x" + x.Value);
            }
        }

        private void pbCerrarSesion_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void cmbProductos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            int index = cmbProductos.SelectedIndex;
            this.lblProductos.ImageIndex = productos[index].nImg;
            this.lblNombreP.Text = productos[index].nombre;
            this.txtCantidad.Text = "";
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            foreach (var x in compra)
            {
                if (x.Value!= 0)
                {
                    conn.Open();
                    string comand = $"SELECT id_inventario FROM `TOPTECH`.`inventario` WHERE `nombre`='{x.Key}'";
                    MySqlCommand cmd = new MySqlCommand(comand, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int idProducto = int.Parse(dr.GetValue(dr.GetOrdinal("id_inventario")).ToString());
                        dr.Close();
                        conn.Close();
                        conn.Open();
                        comand = $"SELECT `TOPTECH`.`inventario`.`cantidadStock` FROM `TOPTECH`.`inventario`" +
                            $"WHERE nombre = '{x.Key}'";
                        cmd = new MySqlCommand(comand, conn);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            int stock = int.Parse(dr.GetValue(dr.GetOrdinal("cantidadStock")).ToString()) - x.Value;
                            if (stock < 0)
                            {
                                MessageBox.Show("No hay stock suficiente");
                                conn.Close();
                                return;
                            }

                            conn.Close();
                            conn.Open();
                            comand = $"INSERT INTO TOPTECH.ventas (fechaVenta, id_agenteVentas, id_producto, cantidad) " +
                                $"VALUES ('{DateTime.Now.ToString("yyyy-MM-dd")}','{this.idVendedor}','{idProducto}','{x.Value}')";
                            cmd = new MySqlCommand(comand, conn);
                            if (cmd.ExecuteNonQuery() != 1)
                            {
                                MessageBox.Show("Error");
                            }
                            else
                            {

                                dr.Close();
                                conn.Close();
                                conn.Open();
                                comand = $"UPDATE `TOPTECH`.`inventario` SET `TOPTECH`.`inventario`.`cantidadStock` = {stock} " +
                                    $"WHERE `TOPTECH`.`inventario`.`nombre` = '{x.Key}'";
                                cmd = new MySqlCommand(comand, conn);
                                if (cmd.ExecuteNonQuery() != 1)
                                {
                                    MessageBox.Show("Error");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                        
                        
                    }
                }
                conn.Close();

            }   
        }

        private void AgenteDeVentas_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            lstCarrito.Items.Clear();
            foreach (var x in compra.ToList())
            {
                compra[x.Key] = 0;
            }
        }
    }
}
