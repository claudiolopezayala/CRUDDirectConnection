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
    public partial class Gerente : Form
    {
        private Dictionary<string, int> articulo = new Dictionary<string, int>();
        private string sucursal;
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=TOPTECH; User=root; Password=root");
        public Gerente(string _sucursal)
        {
            InitializeComponent();
            this.sucursal = _sucursal;

            articulo.Clear();
            conn.Open();
            string comand = $"SELECT `TOPTECH`.`inventario`.`nombre` FROM `TOPTECH`.`inventario`";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla");
            conn.Close();

            foreach (DataRow row in ds.Tables["tabla"].Rows)
            {
                articulo.Add(row["nombre"].ToString(), 0);
            }
            ds.Clear();

        }

        private void pbCerrarSesion_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void cmbVentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var x in articulo.ToList())
                articulo[x.Key] = 0;

            conn.Open();
            string comand = "SELECT `TOPTECH`.`inventario`.`nombre`, `TOPTECH`.`ventas`.`fechaVenta`," +
                " `TOPTECH`.`ventas`.`cantidad`" +
                " FROM `TOPTECH`.`ventas` INNER JOIN `TOPTECH`.`inventario`" +
                " ON `TOPTECH`.`ventas`.`id_producto` = `TOPTECH`.`inventario`.`id_inventario`";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla");
            conn.Close();

            DateTime comparar = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            switch (this.cmbVentas.SelectedIndex)
            {
                case 0:
                    comparar = comparar.AddDays(-7);
                    break;
                case 1:
                    comparar = comparar.AddMonths(-1);
                    break;
                case 2:
                default:
                    comparar = comparar.AddMonths(-2);
                    break;
            }


            foreach (DataRow row in ds.Tables["tabla"].Rows)
            {
                DateTime actual = new DateTime(int.Parse(row["fechaVenta"].ToString().Substring(0,4)),int.Parse(row["fechaVenta"].ToString().Substring(5, 2)),int.Parse(row["fechaVenta"].ToString().Substring(8, 2)));
                if (DateTime.Compare(actual, comparar) >= 0)
                {
                    articulo[row["nombre"].ToString()] += int.Parse(row["cantidad"].ToString());
                }
            }
            ds.Clear();
            
            this.dgvVentas.DataSource = articulo.ToArray();
            this.dgvVentas.Columns[0].HeaderText = "artículo";
            this.dgvVentas.Columns[1].HeaderText = "cantidad";
        }

        private void Gerente_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
