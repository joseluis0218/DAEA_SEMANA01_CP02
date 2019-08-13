using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace Semana01_CP02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
        public void ListaClientes()
        {
            using(SqlDataAdapter Df = new SqlDataAdapter("Usp_ListadoCompañias_Neptuno",cn))
            {
                Df.SelectCommand.CommandType = CommandType.StoredProcedure;
                using (DataSet Da = new DataSet())
                {
                    Df.Fill(Da, "Clientes");
                    comboClientes.DataSource = Da.Tables["Clientes"];
                    comboClientes.DisplayMember = "Cliente";
                }
            }
        }
        public void ListaPedidos()
        {
            string selected = comboClientes.GetItemText(comboClientes.SelectedItem);
            using (SqlDataAdapter Df = new SqlDataAdapter("Usp_ListadoPedidosPorNombre_Neptuno", cn))
            {
                Df.SelectCommand.CommandType = CommandType.StoredProcedure;
                Df.SelectCommand.Parameters.AddWithValue("@Nombre", selected);
                using (DataSet Da = new DataSet())
                {
                    Df.Fill(Da, "Pedidos");
                    dgPedidos.DataSource = Da.Tables["Pedidos"];
                    lblCantidad.Text = Da.Tables["Pedidos"].Rows.Count.ToString();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ListaClientes();
        }
        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            ListaPedidos();
        }
    }
}
