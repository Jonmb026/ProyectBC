using System.Data;
using Microsoft.Data.SqlClient;
using Models.ACME;

namespace DataAccess.ACME
{
    public class EmpresaDA
    {
        private Conexion _conexion = new Conexion();

        public void Insertar(EmpresaEntidad empresaEntidad)
        {
            //obtener una instancia de la conexion//
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "InsertarEmpresa";

                // Agregar parámetros
                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", SqlDbType.Int) { Direction = ParameterDirection.Output });
                sqlComm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", empresaEntidad.IDTipoEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@Empresa", empresaEntidad.Empresa));
                sqlComm.Parameters.Add(new SqlParameter("@Direccion", empresaEntidad.Direccion));
                sqlComm.Parameters.Add(new SqlParameter("@RUC", empresaEntidad.RUC));
                sqlComm.Parameters.Add(new SqlParameter("@FechaCreacion", empresaEntidad.FechaCreacion));
                sqlComm.Parameters.Add(new SqlParameter("@Presupuesto", empresaEntidad.Presupuesto));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", empresaEntidad.Activo));

                sqlComm.ExecuteNonQuery();

                empresaEntidad.IDEmpresa = (int)sqlComm.Parameters[sqlComm.Parameters.IndexOf("@IDEmpresa")].Value;

                sqlComm.Clone();

            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Insertar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }
        ////////// fin insertar /////

        public void Modificar(EmpresaEntidad empresaEntidad)
        {
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "ModificarEmpresa";

                // Agregar parámetros//
                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", empresaEntidad.IDEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@IDTipoEmpresa", empresaEntidad.IDTipoEmpresa));
                sqlComm.Parameters.Add(new SqlParameter("@Empresa", empresaEntidad.Empresa));
                sqlComm.Parameters.Add(new SqlParameter("@Direccion", empresaEntidad.Direccion));
                sqlComm.Parameters.Add(new SqlParameter("@RUC", empresaEntidad.RUC));
                sqlComm.Parameters.Add(new SqlParameter("@FechaCreacion", empresaEntidad.FechaCreacion));
                sqlComm.Parameters.Add(new SqlParameter("@Presupuesto", empresaEntidad.Presupuesto));
                sqlComm.Parameters.Add(new SqlParameter("@Activo", empresaEntidad.Activo));

                if (sqlComm.ExecuteNonQuery() != 1)
                {
                    throw new Exception("EmpresaDA.Modificar: Problema al actualizar.");
                }
                sqlComm.Clone();
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Modificar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }
        /////////////fin modificar /////////
        public void Anular(EmpresaEntidad empresaEntidad)
        {
            SqlConnection sqlConn = _conexion.Conectar();
            SqlCommand sqlComm = new SqlCommand();

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "AnularEmpresa";

                // Agregar parámetros
                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", empresaEntidad.IDEmpresa));

                sqlComm.ExecuteNonQuery();

                sqlConn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Anular: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
            /////////////////// fin anular ////////////////



        }

        public List<EmpresaEntidad> Listar()
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataReader;
            SqlCommand sqlComm = new SqlCommand();
            List<EmpresaEntidad>? listaEmpresas = new List<EmpresaEntidad>();
            EmpresaEntidad? empresaEntidad;
            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "ListarEmpresa";

                sqlDataReader = sqlComm.ExecuteReader();
           
                while (sqlDataReader.Read())
                {
                    empresaEntidad = new(); // dos ?? significa en caso de q sea nulo va a asignar la cadena vacia 
                    empresaEntidad.IDEmpresa = (int)sqlDataReader["IDEmpresa"];
                    empresaEntidad.IDTipoEmpresa = (int)sqlDataReader["IDTipoEmpresa"];
                    empresaEntidad.Empresa = sqlDataReader["Empresa"].ToString() ?? string.Empty;
                    empresaEntidad.Direccion = sqlDataReader["Direccion"].ToString() ?? string.Empty;
                    empresaEntidad.RUC = sqlDataReader["RUC"].ToString() ?? string.Empty;
                    if (sqlDataReader["FechaCreacion"] != DBNull.Value)
                    {
                        empresaEntidad.FechaCreacion = (DateTime)sqlDataReader["FechaCreacion"];
                    }
                    if (sqlDataReader["Presupuesto"] != DBNull.Value)
                    {
                        empresaEntidad.Presupuesto = (decimal)sqlDataReader["Presupuesto"];
                    }
                    empresaEntidad.Activo = (bool)sqlDataReader["Activo"];

                    listaEmpresas.Add(empresaEntidad);
                }

                sqlConn.Close();
                return listaEmpresas;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.Listar: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }
        }
        /////// fin Listar /////////

        public EmpresaEntidad BuscarID(int IDEmpresa)
        {
            // Obtener una instancia de la conexión
            SqlConnection sqlConn = _conexion.Conectar();
            SqlDataReader sqlDataRead;
            SqlCommand sqlComm = new SqlCommand();
            EmpresaEntidad? empresaEntidad = null;

            try
            {
                sqlConn.Open();
                sqlComm.Connection = sqlConn;
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandText = "BuscarEmpresa";

                sqlComm.Parameters.Add(new SqlParameter("@IDEmpresa", IDEmpresa));

                sqlDataRead = sqlComm.ExecuteReader();

                while (sqlDataRead.Read())
                {
                    empresaEntidad = new EmpresaEntidad();
                    empresaEntidad.IDEmpresa = (int)sqlDataRead["IDEmpresa"];
                    empresaEntidad.IDTipoEmpresa = (int)sqlDataRead["IDTipoEmpresa"];
                    empresaEntidad.Empresa = sqlDataRead["Empresa"].ToString() ?? string.Empty;
                    empresaEntidad.Direccion = sqlDataRead["Direccion"].ToString() ?? string.Empty;
                    empresaEntidad.RUC = sqlDataRead["RUC"].ToString() ?? string.Empty;
                    if (sqlDataRead["FechaCreacion"] != DBNull.Value)
                    {
                        empresaEntidad.FechaCreacion = (DateTime)sqlDataRead["FechaCreacion"];
                    }
                    if (sqlDataRead["Presupuesto"] != DBNull.Value)
                    {
                        empresaEntidad.Presupuesto = (decimal)sqlDataRead["Presupuesto"];
                    }
                    empresaEntidad.Activo = (bool)sqlDataRead["Activo"];
                }

                sqlConn.Close();

                if (empresaEntidad == null)
                {
                    throw new Exception("EmpresaDA.BuscarID: El ID de empresa no existe.");
                }
                return empresaEntidad;
            }
            catch (Exception ex)
            {
                throw new Exception("EmpresaDA.BuscarID: " + ex.Message);
            }
            finally
            {
                sqlConn.Dispose();
            }

        }
        //// fin buscar ///
    }


}



