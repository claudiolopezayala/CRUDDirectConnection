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
    public partial class PedidoAV : Form
    {
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=TOPTECH; User=root; Password=root");
        private Dictionary<string, int> compra;
        public PedidoAV(Dictionary<string, int> _compra)
        {
            InitializeComponent();
            this.compra = _compra;
            cmbPago.Enabled = false;

            conn.Open();
            string comand = $"SELECT `TOPTECH`.`recursosHumanos`.`nombre` FROM `TOPTECH`.`recursosHumanos`" +
                $" WHERE `TOPTECH`.`recursosHumanos`.`puesto` = 'agenteVentas'";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla");
            conn.Close();


            foreach (DataRow row in ds.Tables["tabla"].Rows)
                this.cmbAgente.Items.Add(row["nombre"].ToString());
        }

        private void cmbPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbPago.Enabled = true;
        }

        private void btnPedido_Click(object sender, EventArgs e)
        {
            //sql
            
            this.Hide();
        }
    }
}
