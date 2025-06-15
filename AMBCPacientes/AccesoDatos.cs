using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AMBCPacientes
{
    internal class AccesoDatos
    {
        //aca lo conecte localmente, conectenlo con el suyo
        private string CadenaConexion = "Data Source=LAPTOP-7KUNN01M\\SQLEXPRESS;Initial Catalog=CLINICA_db_CORRECCION;Integrated Security=True;";
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader reader;
        public SqlDataReader pReader
        {
            get { return reader; }
            set { reader = value; }
        }
        public AccesoDatos()
        {
            conexion = new SqlConnection(CadenaConexion);
        }
        public void Conectar()
        {
            conexion.Open();
            comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
        }
        public void Desconectar()
        {
            conexion.Close();
        }
        public void EjecutarConsulta(string consulta)
        {
            this.Conectar();
            comando.CommandText = consulta;
            comando.ExecuteNonQuery();
            this.Desconectar();
        }
        public DataTable ConsultarTabla(string consulta)
        {
            this.Conectar();
            comando.CommandText = consulta;
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            this.Desconectar();
            return tabla;
        }
        public void ConsultarBD(string consulta)
        {
            this.Conectar();
            comando.CommandText = consulta;
            reader = comando.ExecuteReader();
        }
        public int ActualizarBD(string consulta, List<Parametro> lista) 
        {
            this.Conectar();
            comando.CommandText = consulta;
            foreach (Parametro p in lista)
            {
                comando.Parameters.AddWithValue(p.Nombre, p.Valor);
            }
            object lastId = comando.ExecuteScalar();
            this.Desconectar();
            return Convert.ToInt32(lastId) + 1;
        }
        public int GetMaxId(string campo, string tabla) 
        {
            this.Conectar();
            comando.CommandText = $"select isnull(max({campo}),0) from {tabla}";
            object lastId = comando.ExecuteScalar();
            this.Desconectar();
            return Convert.ToInt32(lastId) + 1;
        }
    }
}