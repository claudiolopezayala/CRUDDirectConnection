using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace TopTech
{
    class ActualizarEmpleados
    {
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=; User=root; Password=root");
        public string sucursal { get; set; }
        public DataGridView dgv { get; set; }

        public void actualizarData()
        {
            conn.Open();
            string comand = "SELECT `TOPTECH`.`recursosHumanos`.`nombre`, `TOPTECH`.`recursosHumanos`.`rfc`," +
                " `TOPTECH`.`recursosHumanos`.`sueldo`, `TOPTECH`.`recursosHumanos`.`puesto`," +
                " `seg`.`nombre` as jefe" +
                " FROM `TOPTECH`.`recursosHumanos` INNER JOIN `TOPTECH`.`recursosHumanos` AS seg" +
                " ON `TOPTECH`.`recursosHumanos`.`jefe` = `seg`.`id_recursosHumanos`" +
                $" WHERE `TOPTECH`.`recursosHumanos`.`sucursal` = '{sucursal}'";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, "tabla");
            this.dgv.DataSource = ds.Tables["tabla"];            
            conn.Close();
        }
    }
}
