using ProyectoBase.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBase.AccesoADatos
{
    class AD_Cursos
    {
        
        
        
        
        public static int ObtenerUltimoIdCurso()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "SELECT MAX(Id) FROM cursos";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;
                int resultado = (int)cmd.ExecuteScalar();
                return resultado;


            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                cn.Close();
            }

        }

        public static bool AltaNuevoCurso (int idCurso, string NombreCurso, string Descripcion, int IdCarreras, List<int> ListaIdAlumnos)
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlTransaction ObjTransaccion = null;
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                SqlCommand cmd = new SqlCommand();
                string consulta = "INSERT INTO cursos values(@Nombre, @Descripcion, @IdCarrera)";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Nombre", NombreCurso);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
                cmd.Parameters.AddWithValue("@IdCarrera", IdCarreras);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = consulta;

                cn.Open();

                ObjTransaccion = cn.BeginTransaction("AltaDeCurso");

                cmd.Transaction = ObjTransaccion;

                cmd.Connection = cn;

                cmd.ExecuteNonQuery();

                foreach (var IdAlumno in ListaIdAlumnos)
                {
                   
                    string consultaCursoXAlumno = "INSERT INTO personas_por_cursos values (@IdPersona, @IdCurso,@FechaAsociacion)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdPersona", IdAlumno);
                    cmd.Parameters.AddWithValue("@IdCurso", idCurso);
                    cmd.Parameters.AddWithValue("@FechaAsociacion", DateTime.Now);

                    cmd.CommandText = consultaCursoXAlumno;
                    cmd.ExecuteNonQuery();
                }
                ObjTransaccion.Commit();
                return true;

            }
            catch (Exception ex)
            {
                ObjTransaccion.Rollback();
                return false;
            }
            finally
            {
                cn.Close();
            }

        }



        public static DataTable ObtenerTiposDocumentos()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                bool resultado = false;
                SqlCommand cmd = new SqlCommand();
                string consulta = "GetTipoDocumentos";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                return tabla;
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                cn.Close();
            }
        }





        public static DataTable ObtenerCarreras()
        {
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"];
            SqlConnection cn = new SqlConnection(cadenaConexion);
            try
            {
                bool resultado = false;
                SqlCommand cmd = new SqlCommand();
                string consulta = "GetCarreras";

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = consulta;

                cn.Open();
                cmd.Connection = cn;

                DataTable tabla = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tabla);
                return tabla;
            }

            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                cn.Close();
            }
        }



       


        
    }
}
