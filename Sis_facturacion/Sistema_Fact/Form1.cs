﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sistema_Fact
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string[,] ListaVenta = new string[200, 6];
        int Fila = 0;

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCargarlista_Click(object sender, EventArgs e)
        {
            try 
            {
                if (txtIdArticulo.Text != "" && txtNombre.Text != "" && txtPrecio.Text != "" && txtCantidad.Text != "") ;
                {
                    ListaVenta[Fila, 0] = txtIdArticulo.Text;
                    ListaVenta[Fila, 1] = txtNombre.Text;
                    ListaVenta[Fila, 2] = txtPrecio.Text;
                    ListaVenta[Fila, 3] = txtCantidad.Text;
                    ListaVenta[Fila, 4] = (float.Parse(txtPrecio.Text) * float.Parse(txtCantidad.Text)).ToString();


                    dgvLista.Rows.Add(ListaVenta[Fila, 0], ListaVenta[Fila, 1], ListaVenta[Fila, 2], ListaVenta[Fila, 3], ListaVenta[Fila, 4]);
                    Fila++;
                    txtIdArticulo.Text = txtNombre.Text = txtPrecio.Text = txtCantidad.Text = "";
                }
            }
            catch 
            {
            
            }
            CostoaPagar();
        }
        public void CostoaPagar()
        {
            float CostoTotal = 0;
            int Conteo = 0;

            Conteo = dgvLista.RowCount;
            for (int i=0; i< Conteo; i++)
            {
                CostoTotal += float.Parse(dgvLista.Rows[i].Cells[4].Value.ToString());
            }
            lblCostoaPagar.Text = CostoTotal.ToString();
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            try 
            {
                lblDevolucion.Text=(float.Parse(txtEfectivo.Text)-float.Parse(lblCostoaPagar.Text)).ToString();
            }
            catch 
            {
                lblDevolucion.Text = "0.00";
            }
        }

        private void txtVender_Click(object sender, EventArgs e)
        {
            clsFunciones.CreaTicket Ticket1 = new clsFunciones.CreaTicket();

            Ticket1.TextoCentro("CG Technology Security"); //imprime una linea de descripcion
            Ticket1.TextoCentro("**********************************");

            Ticket1.TextoIzquierda("Dirc: xxxx");
            Ticket1.TextoIzquierda("Tel:xxxx ");
            Ticket1.TextoIzquierda("Rnc: xxxx");
            Ticket1.TextoIzquierda("");
            Ticket1.TextoCentro("Factura de Venta"); //imprime una linea de descripcion
            Ticket1.TextoIzquierda("No Fac: 000");
            Ticket1.TextoIzquierda("Fecha:" + DateTime.Now.ToShortDateString() + " Hora:" + DateTime.Now.ToShortTimeString());
            Ticket1.TextoIzquierda("Le Atendio: xxxx");///////////////////
            Ticket1.TextoIzquierda("");
            clsFunciones.CreaTicket.LineasGuion();

            clsFunciones.CreaTicket.EncabezadoVenta();
            clsFunciones.CreaTicket.LineasGuion();
            foreach (DataGridViewRow r in dgvLista.Rows)
            {
                // Articulo                     //Precio                                    cantidad                            Subtotal
                Ticket1.AgregaArticulo(r.Cells[1].Value.ToString(), double.Parse(r.Cells[2].Value.ToString()), int.Parse(r.Cells[3].Value.ToString()), double.Parse(r.Cells[4].Value.ToString())); //imprime una linea de descripcion
            }


            clsFunciones.CreaTicket.LineasGuion();
            Ticket1.AgregaTotales("Sub-Total", double.Parse("000")); // imprime linea con Subtotal
            Ticket1.AgregaTotales("Menos Descuento", double.Parse("000")); // imprime linea con decuento total
            Ticket1.AgregaTotales("Mas ITBIS", double.Parse("000")); // imprime linea con ITBis total
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Total", double.Parse(lblCostoaPagar.Text)); // imprime linea con total
            Ticket1.TextoIzquierda(" ");
            Ticket1.AgregaTotales("Efectivo Entregado:", double.Parse(txtEfectivo.Text));
            Ticket1.AgregaTotales("Efectivo Devuelto:", double.Parse(lblDevolucion.Text));


            // Ticket1.LineasTotales(); // imprime linea 

            Ticket1.TextoIzquierda(" ");
            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoCentro("*     Gracias por preferirnos    *");

            Ticket1.TextoCentro("**********************************");
            Ticket1.TextoIzquierda(" ");
            string impresora = "Microsoft XPS Document Writer";
            string nombreArchivo = $"Factura_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            Ticket1.ImprimirTiket( impresora);
            //Hasta aqui es el codigo de imprimir 



            Fila = 0;
            while (dgvLista.RowCount > 0)//limpia el dgv
            { dgvLista.Rows.Remove(dgvLista.CurrentRow); }
            //LBLIDnuevaFACTURA.Text = ClaseFunciones.ClsFunciones.IDNUEVAFACTURA().ToString();

            txtIdArticulo.Text = txtNombre.Text = txtPrecio.Text = txtCantidad.Text =txtEfectivo.Text= "";
            lblCostoaPagar.Text = lblDevolucion.Text = "0.00";
        }
    }
}
