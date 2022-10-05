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
    public partial class ModEmpleadosRH : Form
    {
        public DataGridView dgv;
        private string sucursal;
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=TOPTECH; User=root; Password=root");
        public ModEmpleadosRH(string _sucursal, DataGridView _dgv)
        {
            InitializeComponent();
            this.sucursal = _sucursal;
            this.dgv = _dgv;
        }

        private void btnActulizar_Click(object sender, EventArgs e)
        {
            conn.Open();
            string comand = $"SELECT id_recursosHumanos FROM `TOPTECH`.`recursosHumanos` WHERE `rfc`='{this.txtJefe.Text}'";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                int jefe = int.Parse(dr.GetValue(dr.GetOrdinal("id_recursosHumanos")).ToString());
                dr.Close();
                try
                {
                    comand = $"INSERT INTO TOPTECH.recursosHumanos(nombre, rfc, sueldo, puesto, sucursal, jefe) " +
                    $"VALUES ('{this.txtNombre.Text}','{this.txtRfc.Text}','{int.Parse(this.txtSueldo.Text)}','{this.txtPuesto.Text}','{this.sucursal}','{jefe}')";
                    cmd = new MySqlCommand(comand, conn);
                    if( cmd.ExecuteNonQuery() != 1)
                    {
                        MessageBox.Show("Error");
                    }
                }
                catch
                {
                    MessageBox.Show("Error");
                }
                
            }
            else
                MessageBox.Show("Error");
            conn.Close();
            this.dgv.DataSource = null;
            ActualizarEmpleados actualizar = new ActualizarEmpleados();
            actualizar.sucursal = this.sucursal;
            actualizar.dgv = this.dgv;
            Thread thread = new Thread(new ThreadStart(actualizar.actualizarData));
            thread.Start();
            thread.Join();
            this.Close();
        }

        private void cmbOperacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtRfc.Enabled = true;
            this.txtRfc.Text = "";
        }
    }
}
