using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TopTech
{
    class ActualizarProveedores
    {
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=; User=root; Password=root");
        public string sucursal { get; set; }
        public DataGridView dgv { get; set; }

        public void actualizarData()
        {
            conn.Open();
            string comand = "SELECT `TOPTECH`.`proovedor`.`nombre`, `TOPTECH`.`proovedor`.`adeudo`," +
                " `TOPTECH`.`proovedor`.`categoria`, `TOPTECH`.`proovedor`.`noTelefono`" +
                " FROM `TOPTECH`.`proovedor`";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla");
            this.dgv.DataSource = ds.Tables["tabla"];
            conn.Close();
        }
    }
}
