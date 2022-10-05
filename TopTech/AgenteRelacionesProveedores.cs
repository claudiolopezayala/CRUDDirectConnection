using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopTech
{
    public partial class AgenteRelacionesProveedores : Form
    {
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=TOPTECH; User=root; Password=root");
        private string sucursal;
        public AgenteRelacionesProveedores(string _sucursal)
        {
            InitializeComponent();
            this.sucursal = _sucursal;
            Control.CheckForIllegalCrossThreadCalls = false;

            ActualizarProveedores actualizar = new ActualizarProveedores();
            actualizar.sucursal = this.sucursal;
            actualizar.dgv = this.dgvProveedores;
            Thread thread = new Thread(new ThreadStart(actualizar.actualizarData));
            thread.Start();

            conn.Open();
            string comand = $"SELECT nombre, cantidadStock FROM `TOPTECH`.`inventario` WHERE `cantidadStock` < 3";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla");

            foreach (DataRow row in ds.Tables["tabla"].Rows)
            {
                this.lstProductos.Items.Add($"Quedan {row["cantidadStock"]} {row["nombre"]}");
            }

                thread.Join();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Form formPopup = new ModARP(this.sucursal, this.dgvProveedores);
            formPopup.ShowDialog(this);
        }
        
        private void btnModProveed_Click(object sender, EventArgs e)
        {
            Form formPopup = new ModProveedoresARP(this.sucursal, this.dgvProveedores);
            formPopup.ShowDialog(this);
        }

        private void pbCerrarSesion_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void AgenteRelacionesProveedores_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
