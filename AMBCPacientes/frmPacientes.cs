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
    public partial class FrmPaciente : Form
    {
        public FrmPaciente()
        {
            InitializeComponent();
        }
         public List<Paciente> TraerPacientes(string consulta)
        {
            AccesoDatos accesoDatos = new AccesoDatos();
            List<Paciente> pacientes = new List<Paciente>();
            accesoDatos.ConsultarBD(consulta);
            while (accesoDatos.pReader.Read())
            {
                int id = Convert.ToInt32(accesoDatos.pReader["id_paciente"]);
                string nombre = accesoDatos.pReader["nombre"].ToString();
                string apellido = accesoDatos.pReader["apellido"].ToString();
                int dni = Convert.ToInt32(accesoDatos.pReader["dni"]);
                int edad = Convert.ToInt32(accesoDatos.pReader["edad"]);
                Calle calle = new Calle(Convert.ToInt32(accesoDatos.pReader["id_calle"]), accesoDatos.pReader["c_nombre"].ToString());
                int numeracion = Convert.ToInt32(accesoDatos.pReader["numeracion"]);
                string telefono = accesoDatos.pReader["telefono"].ToString(); //por alguna extraña razon este campo es un varchar en un lugar de un int, supongo que por el uso de guiones 
                Paciente paciente = new Paciente(id, nombre, apellido, dni, edad, calle, numeracion, telefono);
                pacientes.Add(paciente);
            }
            dgvPacientes.DataSource = pacientes;
            accesoDatos.Desconectar();
            //dgvPacientes.Columns["calle"].Visible = false; 
            return pacientes;
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmDetallePaciente frmDetallePaciente = new frmDetallePaciente();
            frmDetallePaciente.ShowDialog();
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
            if (string.IsNullOrEmpty(txtPacientes.Text))
            {
                string consulta = "select p.*, c.id_calle, c.nombre as c_nombre from Pacientes as p join Calles as c on p.id_calle = c.id_calle";
                TraerPacientes(consulta);
            }
            else
            {
                string nombre = txtPacientes.Text;
                int id;
                if (int.TryParse(txtPacientes.Text, out id))
                {
                    string consulta = $"select p.*, c.id_calle, c.nombre as c_nombre from Pacientes as p join Calles as c on p.id_calle = c.id_calle where p.id_paciente like '%{id}%'";
                    TraerPacientes(consulta);
                }
                else
                {
                    string consulta = $"select p.*, c.id_calle, c.nombre as c_nombre from Pacientes as p join Calles as c on p.id_calle = c.id_calle where p.nombre like '%{nombre}%' or p.apellido like '%{nombre}%'";
                    TraerPacientes(consulta);
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgvPacientes.CurrentRow != null)
                {
                    int numero = Convert.ToInt32(dgvPacientes.CurrentRow.Cells["id"].Value);
                    DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar este paciente?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        string consulta = "delete from Pacientes where id_paciente = @id_paciente";
                        List<Parametro> parametros = new List<Parametro>();
                        Parametro id = new Parametro("@id_paciente", numero);
                        parametros.Add(id);
                        AccesoDatos accesoDatos = new AccesoDatos();
                        accesoDatos.ActualizarBD(consulta, parametros);
                        MessageBox.Show("Paciente eliminado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnConsultar.PerformClick();
                    }
                }
                else
                {
                    MessageBox.Show("Debes seleccionar un paciente para borrar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 547)
                {
                    MessageBox.Show($"Error al eliminar el paciente: {ex.Message} \nHay registros vinculados a este paciente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Error al eliminar el paciente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Erro inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvPacientes.CurrentRow != null)
            {
                Paciente pacienteSeleccionado = (Paciente)dgvPacientes.CurrentRow.DataBoundItem;
                frmDetallePaciente FrmDetallePaciente = new frmDetallePaciente(pacienteSeleccionado);
                FrmDetallePaciente.ShowDialog();
                btnConsultar.PerformClick();
            }
            else
            {
                MessageBox.Show("Debes seleccionar un paciente para editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

