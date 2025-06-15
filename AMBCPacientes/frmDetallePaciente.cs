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
            cboCalle.DataSource = lista;
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
            CargarCombo();
            if (estaEditando)
            {

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
            bool telefonoValidado = !string.IsNullOrWhiteSpace(txtTelefono.Text);
            btnAceptar.Enabled = codigoValidado && nombreValidado && apellidoValidado && dniValidado && edadValidado && calleValidado && numeracionValidado && telefonoValidado;
        }
    }
}