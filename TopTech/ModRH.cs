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
    public partial class ModRH : Form
    {
        public DataGridView dgv;
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=TOPTECH; User=root; Password=root");
        private string sucursal;
        public ModRH(string _sucursal, DataGridView _dgv)
        {
            InitializeComponent();
            this.dgv = _dgv;
            this.sucursal = _sucursal;
            this.txtRFC.Enabled = false;
            this.txtDatos.Enabled = false;
        }

        private void btnActulizar_Click(object sender, EventArgs e)
        {
            conn.Open();
            string comand = $"UPDATE `TOPTECH`.`recursosHumanos`" +
                $" SET `TOPTECH`.`recursosHumanos`.`{this.cmbOperacion.SelectedItem.ToString().ToLower()}`" +
                $" = '{this.txtDatos.Text}' WHERE `TOPTECH`.`recursosHumanos`.`rfc` = '{this.txtRFC.Text}'";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            if (cmd.ExecuteNonQuery() == 1)
            {
                this.dgv.DataSource = null;
                ActualizarEmpleados actualizar = new ActualizarEmpleados();
                actualizar.sucursal = this.sucursal;
                actualizar.dgv = this.dgv;
                Thread thread = new Thread(new ThreadStart(actualizar.actualizarData));
                thread.Start();
                thread.Join();
                this.Close();
            }
            else
                MessageBox.Show("Error");
            conn.Close();
            this.Close();
        }

        private void cmbOperacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtRFC.Enabled = true;
            this.txtDatos.Enabled = true;
            this.txtRFC.Text = "";
            this.txtDatos.Text = "";
        }
    }
}
