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
    public partial class ModProveedoresARP : Form
    {
        public DataGridView dgv;
        private string sucursal;
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=TOPTECH; User=root; Password=root");
        public ModProveedoresARP(string _sucursal, DataGridView _dgv)
        {
            InitializeComponent();
            this.sucursal = _sucursal;
            this.dgv = _dgv;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            
            try
            {
                string comand = $"INSERT INTO TOPTECH.proovedor (nombre, adeudo, categoria, noTelefono) " +
                $"VALUES ('{this.txtNombre.Text}','0','{this.txtCategoria.Text}','{this.txtNoTelefono.Text}')";
                MySqlCommand cmd = new MySqlCommand(comand, conn);
                if (cmd.ExecuteNonQuery() != 1)
                {
                    MessageBox.Show("Error");
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            conn.Close();
            this.dgv.DataSource = null;
            ActualizarProveedores actualizar = new ActualizarProveedores();
            actualizar.sucursal = this.sucursal;
            actualizar.dgv = this.dgv;
            Thread thread = new Thread(new ThreadStart(actualizar.actualizarData));
            thread.Start();
            thread.Join();
            this.Close();
        }
    }
}
