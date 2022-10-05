namespace TopTech
{
    partial class PedidoAV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PedidoAV));
            this.label3 = new System.Windows.Forms.Label();
            this.Modificación = new System.Windows.Forms.Label();
            this.cmbAgente = new System.Windows.Forms.ComboBox();
            this.btnPedido = new System.Windows.Forms.Button();
            this.cmbPago = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(105, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Método de pago";
            // 
            // Modificación
            // 
            this.Modificación.AutoSize = true;
            this.Modificación.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Modificación.Location = new System.Drawing.Point(105, 53);
            this.Modificación.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Modificación.Name = "Modificación";
            this.Modificación.Size = new System.Drawing.Size(138, 20);
            this.Modificación.TabIndex = 11;
            this.Modificación.Text = "Agente de ventas";
            // 
            // cmbAgente
            // 
            this.cmbAgente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAgente.FormattingEnabled = true;
            this.cmbAgente.Location = new System.Drawing.Point(109, 78);
            this.cmbAgente.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbAgente.Name = "cmbAgente";
            this.cmbAgente.Size = new System.Drawing.Size(252, 28);
            this.cmbAgente.TabIndex = 9;
            // 
            // btnPedido
            // 
            this.btnPedido.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPedido.Location = new System.Drawing.Point(109, 171);
            this.btnPedido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPedido.Name = "btnPedido";
            this.btnPedido.Size = new System.Drawing.Size(155, 37);
            this.btnPedido.TabIndex = 8;
            this.btnPedido.Text = "Realizar pedido";
            this.btnPedido.UseVisualStyleBackColor = true;
            this.btnPedido.Click += new System.EventHandler(this.btnPedido_Click);
            // 
            // cmbPago
            // 
            this.cmbPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPago.FormattingEnabled = true;
            this.cmbPago.Items.AddRange(new object[] {
            "Efectivo",
            "VISA/MASTERCARD",
            "AMEX"});
            this.cmbPago.Location = new System.Drawing.Point(109, 134);
            this.cmbPago.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbPago.Name = "cmbPago";
            this.cmbPago.Size = new System.Drawing.Size(252, 28);
            this.cmbPago.TabIndex = 14;
            this.cmbPago.SelectedIndexChanged += new System.EventHandler(this.cmbPago_SelectedIndexChanged);
            // 
            // PedidoAV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(467, 318);
            this.Controls.Add(this.cmbPago);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Modificación);
            this.Controls.Add(this.cmbAgente);
            this.Controls.Add(this.btnPedido);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "PedidoAV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PedidoAV";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label Modificación;
        private System.Windows.Forms.ComboBox cmbAgente;
        private System.Windows.Forms.Button btnPedido;
        private System.Windows.Forms.ComboBox cmbPago;
    }
}