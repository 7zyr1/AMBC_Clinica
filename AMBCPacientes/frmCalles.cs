using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMBCPacientes
{
    public partial class frmCalles : Form
    {
        public frmCalles()
        {
            InitializeComponent();
        }
        public List<Calle> TraerCalles(string consulta)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<Calle> calles = new List<Calle>();
            accesoDatos.ConsultarBD(consulta);
            while (accesoDatos.pReader.Read())
            {
                int id = Convert.ToInt32(accesoDatos.pReader["id_calle"]);
                string nombre = accesoDatos.pReader["nombre"].ToString();
                Calle calle = new Calle(id, nombre);
                calles.Add(calle);
            }
            dgvCalles.DataSource = calles;
            accesoDatos.Desconectar();
            return calles;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmDetalleCalle frmDetalleCalle = new frmDetalleCalle();
            frmDetalleCalle.ShowDialog();
            btnConsultar.PerformClick(); 
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea salir?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCalle.Text))
            {
                string consulta = "select * from Calles";
                TraerCalles(consulta);
            }
            else
            {
                string nombre = txtCalle.Text;
                int id;
                if (int.TryParse(txtCalle.Text, out id))
                {
                    string consulta = $"select * from Calles where id_calle like '%{id}%'";
                    TraerCalles(consulta);
                }
                else
                {
                    string consulta = $"select * from Calles where nombre like '%{nombre}%'";
                    TraerCalles(consulta);
                }
            }
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCalles.CurrentRow != null)
                {
                    int numero = Convert.ToInt32(dgvCalles.CurrentRow.Cells["id_calle"].Value);
                    DialogResult result = MessageBox.Show($"¿Está seguro de eliminar la Calle número {numero}?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        AccesoDatos accesoDatos = new AccesoDatos();
                        string query = $"delete from calles where id_calle = @id_calle";
                        List<Parametro> parametros = new List<Parametro>();
                        Parametro id = new Parametro("@id_calle", numero);
                        parametros.Add(id);
                        accesoDatos.ActualizarBD(query, parametros);
                        MessageBox.Show("Calle eliminada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnConsultar.PerformClick();
                    }  
                }
                else
                {
                    MessageBox.Show("Debes seleccionar una calle para Borrar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show($"Error al eliminar la Calle: {ex.Message} \nHay Pacientes y Medicos vinculados a esta calle", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Error al eliminar la Calle: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvCalles.CurrentRow != null)
            {
                Calle calleSeleccionada = (Calle)dgvCalles.CurrentRow.DataBoundItem;
                frmDetalleCalle FrmDetalleCalle = new frmDetalleCalle(calleSeleccionada);
                FrmDetalleCalle.ShowDialog();
                btnConsultar.PerformClick();
            }
            else
            {
                MessageBox.Show("Debes seleccionar una calle para Editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
