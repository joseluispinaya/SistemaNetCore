using Sistema.Entity;
using System.Data;
using System.Data.SqlClient;
//using System.Data;

namespace Sistema.DAL
{
    public class CategoriaData(string connectionString)
    {
        private readonly string _connectionString = connectionString;

        public List<Categoria> GetAllCategorias()
        {
            List<Categoria> lista = [];

            using (SqlConnection connection = new(_connectionString))
            {
                SqlCommand cmd = new("usp_listaCategoria", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                using var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Categoria
                    {
                        IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                        Nombre = dr["Nombre"].ToString()!,
                        Activo = Convert.ToBoolean(dr["Activo"])
                    });
                }
            }
            return lista;
        }

        public Categoria GetCategoriaById(int IdCategoria)
        {

            Categoria categoria = new();

            using (SqlConnection connection = new(_connectionString))
            {
                SqlCommand cmd = new("usp_obtenerCategoria", connection);
                cmd.Parameters.AddWithValue("@IdCategoria", IdCategoria);
                cmd.CommandType = CommandType.StoredProcedure;
                connection.Open();
                using var dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    categoria =
                        new Categoria
                        {
                            IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                            Nombre = dr["Nombre"].ToString()!,
                            Activo = Convert.ToBoolean(dr["Activo"])
                        };
                }

            }

            return categoria;
        }

        public bool AddCategoria(Categoria categoria)
        {
            bool rpta = false;

            try
            {
                using SqlConnection connection = new(_connectionString);
                using SqlCommand cmd = new("usp_RegistrarCategoria", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                //cmd.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = categoria.Nombre;

                SqlParameter outputParam = cmd.Parameters.Add("@Resultado", SqlDbType.Bit);
                outputParam.Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();

                rpta = Convert.ToBoolean(outputParam.Value);
            }
            catch (Exception)
            {
                // Aquí puedes hacer logging si lo necesitas
                rpta = false;
            }

            return rpta;
        }

        public bool UpdateCategoria(Categoria categoria)
        {
            bool rpta = false;

            try
            {
                using SqlConnection connection = new(_connectionString);
                using SqlCommand cmd = new("usp_ModificarCategoria", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = categoria.IdCategoria;
                cmd.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                cmd.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                cmd.Parameters.AddWithValue("@Activo", categoria.Activo);
                //cmd.Parameters.Add("@Activo", SqlDbType.Bit).Value = categoria.Activo;

                SqlParameter outputParam = cmd.Parameters.Add("@Resultado", SqlDbType.Bit);
                outputParam.Direction = ParameterDirection.Output;

                connection.Open();
                cmd.ExecuteNonQuery();

                rpta = Convert.ToBoolean(outputParam.Value);
            }
            catch (Exception)
            {
                // logging si lo necesitas
                rpta = false;
            }

            return rpta;
        }

        //public List<Categoria> GetAllCategorias()
        //{
        //    List<Categoria> lista = [];

        //    using (SqlConnection connection = new(_connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("usp_listaCategoria", connection);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        connection.Open();

        //        using (var dr = cmd.ExecuteReader())
        //        {
        //            while (dr.Read())
        //            {
        //                lista.Add(new Categoria
        //                {
        //                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
        //                    Nombre = dr["Nombre"].ToString(),
        //                    Activo = Convert.ToBoolean(dr["Activo"])
        //                });
        //            }
        //        }
        //    }
        //    return lista;
        //}

    }
}
