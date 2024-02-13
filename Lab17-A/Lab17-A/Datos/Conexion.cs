using Lab17_A.Models;
using MySql.Data.MySqlClient;

namespace Lab17_A.Datos
{
    public class Conexion
    {
        private string cadenaInicial;
        private string cadenaActual;

        public Conexion()
        {
            this.cadenaInicial = "Server = localhost; Port = 3306; Database = CineDB; Uid = anlu996; Pwd = anlu996;";
            this.cadenaActual = cadenaInicial;
        }

        public MySqlConnection ObtenerConexion()
        {
            try
            {
                var conexion = new MySqlConnection(cadenaActual);
                return conexion;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Usuario o contraseña incorrectos.", ex);
            }
        }

        public void CambiarCadenaConexion(string usuario, string contraseña)
        {
            cadenaActual = $"Server=localhost;Port=3306;Database=CineDB;Uid={usuario};Pwd={contraseña};";
        }

        public void RestaurarCadenaConexion()
        {
            cadenaActual = cadenaInicial;
        }
    }
}
