using Laboratorio5.Models;
using System.Data;
using System.Data.SqlClient;
namespace Laboratorio5.Handlers
{
    public class PaisesHandler
    {
        private SqlConnection conexion;
        private string rutaConexion;
        public PaisesHandler()
        {
            var builder = WebApplication.CreateBuilder();
            rutaConexion = builder.Configuration.GetConnectionString("PaísesContext"); conexion = new SqlConnection(rutaConexion);
        }
        private DataTable CrearTablaConsulta(string consulta)
        {
            SqlCommand comandoParaConsulta = new SqlCommand(consulta, conexion); SqlDataAdapter adaptadorParaTabla = new SqlDataAdapter(comandoParaConsulta); DataTable consultaFormatoTabla = new DataTable();
            conexion.Open();
            adaptadorParaTabla.Fill(consultaFormatoTabla);
            conexion.Close();
            return consultaFormatoTabla;
        }
        public List<PaisModel> ObtenerPaises()
        {
            List<PaisModel> paises = new List<PaisModel>();
            string consulta = "SELECT  *  FROM  dbo.Pais  ";
            DataTable tablaResultado = CrearTablaConsulta(consulta);
            foreach (DataRow columna in tablaResultado.Rows)
            {
                paises.Add(
                new PaisModel
                {
                    Id = Convert.ToInt32(columna["Id"]),
                    Nombre = Convert.ToString(columna["Nombre"]),
                    Idioma = Convert.ToString(columna["Idioma"]),
                    Continente = Convert.ToString(columna["Continente"]),
                });
            }
            return paises;
        }

        public bool CrearPais(PaisModel pais)
        {
            var consulta = @"INSERT  INTO  [dbo].[Pais]  ([Nombre],[Idioma]  ,[Continente])
                             VALUES(@Nombre,  @Idioma,  @Continente)  ";

            var comandoParaConsulta = new SqlCommand(consulta, conexion);
            comandoParaConsulta.Parameters.AddWithValue("@Nombre", pais.Nombre);
            comandoParaConsulta.Parameters.AddWithValue("@Idioma", pais.Idioma);
            comandoParaConsulta.Parameters.AddWithValue("@Continente", pais.Continente);

            conexion.Open();
            bool exito = comandoParaConsulta.ExecuteNonQuery() >= 1;
            conexion.Close();
            return exito;
        }
    }
}