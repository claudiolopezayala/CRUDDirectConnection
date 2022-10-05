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
    public partial class ModARP : Form
    {
        public DataGridView dgv;
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=TOPTECH; User=root; Password=root");
        private string sucursal;

        public ModARP(string _sucursal, DataGridView _dgv)
        {
            InitializeComponent();
            this.dgv = _dgv;
            this.sucursal = _sucursal;
        }

        private void btnActulizar_Click(object sender, EventArgs e)
        {
            conn.Open();
            string comand = $"UPDATE `TOPTECH`.`proovedor`" +
                $" SET `TOPTECH`.`proovedor`.`adeudo`" +
                $" = '{this.txtAdeudo.Text}' WHERE `TOPTECH`.`proovedor`.`noTelefono` = '{this.txtNum.Text}'";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            if (cmd.ExecuteNonQuery() == 1)
            {
                this.dgv.DataSource = null;
                ActualizarProveedores actualizar = new ActualizarProveedores();
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
    }
}
