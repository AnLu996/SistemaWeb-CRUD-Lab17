using Lab17_A.Models;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics.Eventing.Reader;

namespace Lab17_A.Datos
{
    public class PeliculaDatos
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

        public List<PeliculaModel> Listar() 
        {
            var oLista =  new List<PeliculaModel>();

            var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("listarPeliculas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new PeliculaModel()
                        {
                            IdPelicula = Convert.ToInt32(dr["IdPelicula"]),
                            Titulo = dr["Titulo"].ToString(),
                            Sipnosis = dr["Sipnosis"].ToString(),
                            Clasificacion = dr["Clasificacion"].ToString(),
                            EstudioFilmacion = dr["EstudioFilmacion"].ToString(),
                            Estado = dr["Estado"].ToString(),
                            FechaEmision = ((DateTime)dr["FechaEmision"]).Date
                        }) ;
                    }
                }
            }
            return oLista;
        }

        public PeliculaModel Obtener(int IdPelicula)
        {
            var oPelicula = new PeliculaModel();

            var cn = new Conexion();

            using (var conexion = cn.ObtenerConexion())
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand("obtenerPelicula", conexion);
                cmd.Parameters.AddWithValue("n_idpelicula", IdPelicula);                
                cmd.CommandType = CommandType.StoredProcedure;


                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oPelicula.Titulo = dr["Titulo"].ToString();
                        oPelicula.Sipnosis = dr["Sipnosis"].ToString();
                        oPelicula.Clasificacion = dr["Clasificacion"].ToString();
                        oPelicula.EstudioFilmacion = dr["EstudioFilmacion"].ToString();
                        oPelicula.Estado = dr["Estado"].ToString();
                        oPelicula.FechaEmision = ((DateTime)dr["FechaEmision"]).Date;
                        oPelicula.Idioma = dr["Idioma"].ToString();
                        oPelicula.Genero = dr["Genero"].ToString();
                    }
                }
            }
            return oPelicula;
        }

        public bool Guardar(PeliculaModel opelicula) 
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("registrarPelicula", conexion);
                    cmd.Parameters.AddWithValue("n_titulo", opelicula.Titulo);
                    cmd.Parameters.AddWithValue("n_sipnosis", opelicula.Sipnosis);
                    cmd.Parameters.AddWithValue("n_clasificacion", opelicula.Clasificacion);
                    cmd.Parameters.AddWithValue("n_estudiofilmacion", opelicula.EstudioFilmacion);
                    cmd.Parameters.AddWithValue("n_estado", opelicula.Estado);
                    cmd.Parameters.AddWithValue("n_fechaEmision", opelicula.FechaEmision);
                    cmd.Parameters.AddWithValue("n_idioma", opelicula.Idioma);
                    cmd.Parameters.AddWithValue("n_genero", opelicula.Genero);
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

        public bool Editar(PeliculaModel opelicula)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("editarPelicula", conexion);
                    cmd.Parameters.AddWithValue("n_idpelicula", opelicula.IdPelicula);
                    cmd.Parameters.AddWithValue("n_titulo", opelicula.Titulo);
                    cmd.Parameters.AddWithValue("n_sipnosis", opelicula.Sipnosis);
                    cmd.Parameters.AddWithValue("n_clasificacion", opelicula.Clasificacion);
                    cmd.Parameters.AddWithValue("n_estudiofilmacion", opelicula.EstudioFilmacion);
                    cmd.Parameters.AddWithValue("n_estado", opelicula.Estado);
                    cmd.Parameters.AddWithValue("n_fechaEmision", opelicula.FechaEmision);
                    cmd.Parameters.AddWithValue("n_idioma", opelicula.Idioma);
                    cmd.Parameters.AddWithValue("n_genero", opelicula.Genero);
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

        public bool Eliminar(int IdPelicula)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = cn.ObtenerConexion())
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand("eliminarPelicula", conexion);
                    cmd.Parameters.AddWithValue("n_idpelicula", IdPelicula);
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
