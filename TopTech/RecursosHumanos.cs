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
    public partial class RecursosHumanos : Form
    {
        
        private string sucursal;
        public RecursosHumanos(string _sucursal)
        {
            InitializeComponent();
            this.sucursal = _sucursal;
            Control.CheckForIllegalCrossThreadCalls = false;

            ActualizarEmpleados actualizar = new ActualizarEmpleados();
            actualizar.sucursal = this.sucursal;
            actualizar.dgv = this.dgvEmpleados;
            Thread thread = new Thread(new ThreadStart(actualizar.actualizarData));
            thread.Start();
            thread.Join();
        }

        private void btnModEmpleados_Click(object sender, EventArgs e)
        {
            Form formPopup = new ModEmpleadosRH(this.sucursal, this.dgvEmpleados);
            formPopup.ShowDialog(this);
        }
        
        private void RecursosHumanos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pbCerrarSesion_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void btnCambiar_Click(object sender, EventArgs e)
        {
            Form formPopup = new ModRH(this.sucursal, this.dgvEmpleados);
            formPopup.ShowDialog(this);
        }
    }
}
