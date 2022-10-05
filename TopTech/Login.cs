using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace TopTech
{
    public partial class Login : Form
    {
        private MySqlConnection conn = new MySqlConnection("Server=localhost; Port=3306; Database=TOPTECH; User=root; Password=root");
        public Login()
        {
            InitializeComponent();
            this.txtContra.UseSystemPasswordChar = true;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            conn.Open();
            string comand = $"SELECT contraseña FROM `TOPTECH`.`logIn` WHERE `correo`='{txtCorreo.Text}'";
            MySqlCommand cmd = new MySqlCommand(comand, conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr.GetValue(dr.GetOrdinal("contraseña")).ToString().Equals(txtContra.Text))
                {
                    dr.Close();
                    comand = $"SELECT `TOPTECH`.`recursosHumanos`.`puesto` FROM `TOPTECH`.`logIn`" +
                        $" INNER JOIN `TOPTECH`.`recursosHumanos`" +
                        $" ON `TOPTECH`.`logIn`.`id_recursosHumanos` = `TOPTECH`.`recursosHumanos`.`id_recursosHumanos`" +
                        $" WHERE `correo`='{txtCorreo.Text}'";
                    cmd = new MySqlCommand(comand, conn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        string puesto = dr.GetValue(dr.GetOrdinal("puesto")).ToString();

                        dr.Close();
                        comand = $"SELECT `TOPTECH`.`recursosHumanos`.`sucursal` FROM `TOPTECH`.`logIn`" +
                        $" INNER JOIN `TOPTECH`.`recursosHumanos`" +
                        $" ON `TOPTECH`.`logIn`.`id_recursosHumanos` = `TOPTECH`.`recursosHumanos`.`id_recursosHumanos`" +
                        $" WHERE `correo`='{txtCorreo.Text}'";
                        cmd = new MySqlCommand(comand, conn);
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            if (puesto.Equals("repreRecursosHumanos"))
                            {
                                RecursosHumanos RH = new RecursosHumanos(dr.GetValue(dr.GetOrdinal("sucursal")).ToString());
                                RH.Show();
                                this.Hide();
                            }
                            else if (puesto.Equals("agenteVentas"))
                            {
                                dr.Close();
                                comand = $"SELECT `TOPTECH`.`recursosHumanos`.`id_recursosHumanos` FROM `TOPTECH`.`logIn`" +
                                    $" INNER JOIN `TOPTECH`.`recursosHumanos`" +
                                    $" ON `TOPTECH`.`logIn`.`id_recursosHumanos` = `TOPTECH`.`recursosHumanos`.`id_recursosHumanos`" +
                                    $" WHERE `correo`='{txtCorreo.Text}'";
                                cmd = new MySqlCommand(comand, conn);
                                dr = cmd.ExecuteReader();
                                if (dr.Read())
                                {
                                    AgenteDeVentas AV = new AgenteDeVentas(int.Parse(dr.GetValue(dr.GetOrdinal("id_recursosHumanos")).ToString()));
                                    AV.Show();
                                    this.Hide();
                                }
                                    
                            }
                            else if (puesto.Equals("agenteProveedor"))
                            {
                                AgenteRelacionesProveedores AP = new AgenteRelacionesProveedores(dr.GetValue(dr.GetOrdinal("sucursal")).ToString());
                                AP.Show();
                                this.Hide();
                            }
                            else if (puesto.Equals("gerente"))
                            {
                                Gerente GE = new Gerente(dr.GetValue(dr.GetOrdinal("sucursal")).ToString());
                                GE.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
                else
                {
                    MessageBox.Show("Contraseña Incorrecta");
                }
            }
            else
            {
                MessageBox.Show("Error");
            }
            conn.Close();
        }

        //checkbox para contraseña oculta
        private void cboxContra_CheckedChanged(object sender, EventArgs e)
        {
            this.txtContra.UseSystemPasswordChar = !cboxContra.Checked;
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
