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
    public partial class frmDetalleCalle : Form
    {
        bool estaEditando;
        public frmDetalleCalle()
        {
            InitializeComponent();
        }
        public frmDetalleCalle(Calle Calle)
        {
            InitializeComponent();
            CargarDatos(Calle);
            estaEditando = true;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void CargarDatos(Calle calle)
        {
            if (calle != null)
            {
                txtCodigo.Text = calle.id_calle.ToString();
                txtNombreCalle.Text = calle.nombre.ToString();
                validarCampos(this, EventArgs.Empty);
            }
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (estaEditando) 
            {
                string consulta = "update Calles set nombre = @nombre where id_calle = @id_calle";
                List<Parametro> parametros = new List<Parametro>();
                Parametro nombre = new Parametro("@nombre", txtNombreCalle.Text);
                parametros.Add(nombre);
                Parametro id = new Parametro("@id_calle", txtCodigo.Text);
                parametros.Add(id);
                AccesoDatos accesoDatos = new AccesoDatos();
                accesoDatos.ActualizarBD(consulta, parametros);
                MessageBox.Show("Calle actualizada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                string consulta = "insert into Calles (nombre) values (@nombre); select scope_identity()";
                List<Parametro> parametros = new List<Parametro>();
                Parametro nombre = new Parametro("@nombre", txtNombreCalle.Text);
                parametros.Add(nombre);
                AccesoDatos accesoDatos = new AccesoDatos();
                txtCodigo.Text = accesoDatos.ActualizarBD(consulta, parametros).ToString();
                MessageBox.Show("Calle agregada correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clear();
            }
        }
        public void clear() 
        {
            txtNombreCalle.Clear();
        }
        private void frmDetalleCalle_Load(object sender, EventArgs e)
        {
            txtCodigo.Enabled = false;
            btnAceptar.Enabled = false;
            txtCodigo.TextChanged += validarCampos;
            txtNombreCalle.TextChanged += validarCampos;
            if (!estaEditando)
            {
                AccesoDatos accesoDatos = new AccesoDatos();
                txtCodigo.Text = accesoDatos.GetMaxId("id_calle", "calles").ToString();
            }
        }
        public void validarCampos(object sender, EventArgs e) 
        {
            bool codigoValidado = !string.IsNullOrEmpty(txtCodigo.Text);
            bool nombreValidado = !string.IsNullOrEmpty(txtNombreCalle.Text);
            btnAceptar.Enabled = codigoValidado && nombreValidado;
        }
    }
}
