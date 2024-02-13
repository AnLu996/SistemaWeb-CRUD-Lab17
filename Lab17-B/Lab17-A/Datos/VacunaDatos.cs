using Lab17_A.Models;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace Lab17_A.Datos
{
    public class VacunaDatos
    {

        public bool Registrar(string usuario, string contraseña)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("RegistrarUser", conexion);
                    cmd.Parameters.AddWithValue("n_perfil", usuario);
                    cmd.Parameters.AddWithValue("n_contraseña", contraseña);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    cmd.ExecuteReader();

                    rpta = Convert.ToBoolean(cmd.Parameters["mensaje"].Value);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public int Login(string usuario, string contraseña)
        {
            int rpta;

            try
            {
                var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("LoginUsuario", conexion);
                    cmd.Parameters.AddWithValue("n_perfil", usuario);
                    cmd.Parameters.AddWithValue("n_contraseña", contraseña);
                    cmd.Parameters.Add(new MySqlParameter("n_mensaje", MySqlDbType.Int32));
                    cmd.Parameters["n_mensaje"].Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    cmd.ExecuteReader();

                    rpta = Convert.ToInt32(cmd.Parameters["n_mensaje"].Value);
                }
            }

            catch (Exception e)
            {
                string error = e.Message;
                rpta = 0;
            }

            return rpta;
        }

        public List<VacunaModel> Listar() 
        {
            var oLista =  new List<VacunaModel>();

            var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("listarVacunas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new VacunaModel()
                        {
                            IdVacuna = Convert.ToInt32(dr["IdVacuna"]),
                            Nombre = dr["Nombre"].ToString(),
                            FechaFabricacion = ((DateTime)dr["FechaFabricacion"]).Date,
                            FechaVencimiento = ((DateTime)dr["FechaVencimiento"]).Date,
                            Laboratorio = dr["Laboratorio"].ToString()
                        }) ;
                    }
                }
            }
            return oLista;
        }

        public VacunaModel Obtener(int IdVacuna)
        {
            var oVacuna = new VacunaModel();

            var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("obtenerVacuna", conexion);
                cmd.Parameters.AddWithValue("n_idvacuna", IdVacuna);                
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oVacuna.Nombre = dr["Nombre"].ToString();
                        oVacuna.FechaFabricacion = ((DateTime)dr["FechaFabricacion"]).Date;
                        oVacuna.FechaVencimiento = ((DateTime)dr["FechaVencimiento"]).Date;
                        oVacuna.Laboratorio = dr["Laboratorio"].ToString();
                    }
                }
            }
            return oVacuna;
        }

        public bool Guardar(VacunaModel ovacuna) 
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("registrarVacuna", conexion);
                    cmd.Parameters.AddWithValue("n_nombre", ovacuna.Nombre);
                    cmd.Parameters.AddWithValue("n_fechaVencimiento", ovacuna.FechaVencimiento);
                    cmd.Parameters.AddWithValue("n_laboratorio", ovacuna.Laboratorio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }

            catch (Exception e) 
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool Editar(VacunaModel ovacuna)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("editarVacuna", conexion);
                    cmd.Parameters.AddWithValue("n_idvacuna", ovacuna.IdVacuna);
                    cmd.Parameters.AddWithValue("n_nombre", ovacuna.Nombre);
                    cmd.Parameters.AddWithValue("n_fechaVencimiento", ovacuna.FechaVencimiento);
                    cmd.Parameters.AddWithValue("n_laboratorio", ovacuna.Laboratorio);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }

            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }

        public bool Eliminar(int IdVacuna)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("eliminarVacuna", conexion);
                    cmd.Parameters.AddWithValue("n_idVacuna", IdVacuna);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }

            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }

            return rpta;
        }
    }
}
