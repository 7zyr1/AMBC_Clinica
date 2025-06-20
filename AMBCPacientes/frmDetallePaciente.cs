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
    public partial class frmDetallePaciente : Form
    {
        bool estaEditando;
        public frmDetallePaciente()
        {
            InitializeComponent();
        }
        public frmDetallePaciente(Paciente paciente)
        {
            InitializeComponent();
            CargarDatos(paciente);
            estaEditando = true;
            btnAceptar.Enabled= true;
        }
        public void CargarDatos(Paciente paciente)
        {
            if (paciente != null)
            {
                txtCodigo.Text = paciente.id.ToString();
                txtNombre.Text = paciente.nombre;
                txtApellido.Text = paciente.apellido;
                txtDni.Text = paciente.dni.ToString();
                txtEdad.Text = paciente.edad.ToString();
                cboCalle.SelectedValue = paciente.calle.id_calle;
                txtNumeracion.Text = paciente.numeracion.ToString();
                txtTelefono.Text = paciente.telefono.ToString();
                validarCampos(this, EventArgs.Empty);
            }
        }
        public void clear() 
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtDni.Clear();
            txtEdad.Clear();
            cboCalle.SelectedIndex = -1;
            txtNumeracion.Clear();
            txtTelefono.Clear();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void CargarCombo()
        {
            string consulta = "select * from Calles";
            frmCalles frmCalles = new frmCalles();
            List<Calle> lista = frmCalles.TraerCalles(consulta);
            cboCalle.DropDownStyle = ComboBoxStyle.DropDownList;
            var listaCombo = lista.ToList();
            listaCombo.Add(new Calle(lista.Count, "Otro")); 
            cboCalle.DataSource = listaCombo;
            cboCalle.DisplayMember = "nombre";
            cboCalle.ValueMember = "id_calle";
        }
        private void frmDetallePaciente_Load(object sender, EventArgs e)
        {
            txtCodigo.Enabled = false;
            txtNombre.TextChanged += validarCampos;
            txtApellido.TextChanged += validarCampos;
            txtDni.TextChanged += validarCampos;
            txtNombre.TextChanged += validarCampos;
            txtEdad.TextChanged += validarCampos;
            txtNumeracion.TextChanged += validarCampos;
            txtTelefono.TextChanged += validarCampos;
            txtDni.KeyPress += SoloNumeros_KeyPress;
            txtEdad.KeyPress += SoloNumeros_KeyPress;
            txtNumeracion.KeyPress += SoloNumeros_KeyPress;
            txtTelefono.KeyPress += SoloNumeros_KeyPress;
            CargarCombo();
            if (!estaEditando)
            {
                btnAceptar.Enabled = false;
                cboCalle.SelectedIndex = -1;
                AccesoDatos accesoDatos = new AccesoDatos();
                txtCodigo.Text = accesoDatos.GetMaxId("id_paciente", "pacientes").ToString();
            }
        }
        public void validarCampos(object sender, EventArgs e)
        {
            bool codigoValidado = !string.IsNullOrWhiteSpace(txtCodigo.Text);
            bool nombreValidado = !string.IsNullOrWhiteSpace(txtNombre.Text);
            bool apellidoValidado = !string.IsNullOrWhiteSpace(txtApellido.Text);
            bool dniValidado = !string.IsNullOrWhiteSpace(txtDni.Text);
            bool edadValidado = !string.IsNullOrWhiteSpace(txtEdad.Text);
            bool calleValidado = !string.IsNullOrWhiteSpace(cboCalle.Text);
            bool numeracionValidado = !string.IsNullOrWhiteSpace(txtNumeracion.Text);
            //bool telefonoValidado = !string.IsNullOrWhiteSpace(txtTelefono.Text);
            btnAceptar.Enabled = codigoValidado && nombreValidado && apellidoValidado && dniValidado && edadValidado && calleValidado && numeracionValidado; //&& telefonoValidado;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (estaEditando)
            {
                string consulta = "update pacientes set nombre = @nombre, apellido = @apellido, dni = @dni, edad = @edad, id_calle = @id_calle, numeracion = @numeracion, telefono = @telefono where id_paciente = @id_paciente";
                AccesoDatos accesoDatos = new AccesoDatos();
                List<Parametro> parametros = new List<Parametro>();
                Parametro nombre = new Parametro("@nombre", txtNombre.Text);
                parametros.Add(nombre);
                Parametro apellido = new Parametro("@apellido", txtApellido.Text);
                parametros.Add(apellido);
                Parametro dni = new Parametro("@dni", txtDni.Text);
                parametros.Add(dni);
                Parametro edad = new Parametro("@edad", txtEdad.Text);
                parametros.Add(edad);
                Parametro id_calle = new Parametro("@id_calle", cboCalle.SelectedValue);
                parametros.Add(id_calle);
                Parametro numeracion = new Parametro("@numeracion", txtNumeracion.Text);
                parametros.Add(numeracion);
                Parametro telefono = new Parametro("@telefono", txtTelefono.Text);
                parametros.Add(telefono);
                Parametro id = new Parametro("@id_paciente", txtCodigo.Text);
                parametros.Add(id);
                accesoDatos.ActualizarBD(consulta, parametros);
                MessageBox.Show("Paciente actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                string consulta = "insert into Pacientes(nombre, apellido, dni, edad, id_calle, numeracion, telefono) values (@nombre, @apellido, @dni, @edad, @id_calle, @numeracion, @telefono); select scope_identity()";
                AccesoDatos accesoDatos = new AccesoDatos();
                List<Parametro> parametros = new List<Parametro>();
                Parametro nombre = new Parametro("@nombre", txtNombre.Text);
                parametros.Add(nombre);
                Parametro apellido = new Parametro("@apellido", txtApellido.Text);
                parametros.Add(apellido);
                Parametro dni = new Parametro("@dni", txtDni.Text);
                parametros.Add(dni);
                Parametro edad = new Parametro("@edad", txtEdad.Text);
                parametros.Add(edad);
                Parametro id_calle = new Parametro("@id_calle", cboCalle.SelectedValue);
                parametros.Add(id_calle);
                Parametro numeracion = new Parametro("@numeracion", txtNumeracion.Text);
                parametros.Add(numeracion);
                Parametro telefono = new Parametro("@telefono", txtTelefono.Text);
                parametros.Add(telefono);
                try
                {
                    txtCodigo.Text = accesoDatos.ActualizarBD(consulta, parametros).ToString();
                    MessageBox.Show("Paciente agregado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show($"Error inesperado: {ex}");
                }
            }
        }   
        private void SoloNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
            e.Handled = true;
            }
        }

        private void cboCalle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCalle.Text != null && cboCalle.SelectedIndex != -1)
            {
                if (cboCalle.Text == "Otro")
                {
                    DialogResult result = MessageBox.Show($"Usted Selecciono {cboCalle.Text}\n¿Desea ingresar una calle?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        frmDetalleCalle frmDetalleCalle = new frmDetalleCalle();
                        frmDetalleCalle.ShowDialog();
                        CargarCombo();
                        cboCalle.SelectedIndex = -1;
                    }
                    else
                    {
                        cboCalle.SelectedIndex = -1;
                    }
                }
            }
        }
    }
}