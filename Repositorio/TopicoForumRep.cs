using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApiForum.Models;

namespace WebApiForum.Repositorio
{
    public class TopicoForumRep
    {
        string connectionString = @"Data source=localhost,11433;Initial Catalog=WebApiForum;uid=sa;pwd=DockerSql2017";

        public List<TopicoForumModel> Listar(){
            List<TopicoForumModel> lstTopicos = new List<TopicoForumModel>();
            
            SqlConnection con = new SqlConnection(connectionString);

            string SqlQuery = "Select * from tbTopicos";

            SqlCommand cmd = new SqlCommand(SqlQuery,con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read()){
                TopicoForumModel topico = new TopicoForumModel();

                topico.Id = Convert.ToInt16(sdr["Id"]);
                topico.Titulo = sdr["titulo"].ToString();
                topico.Descricao = sdr["descricao"].ToString();
                topico.DataCadastro = Convert.ToDateTime(sdr["DataCadastro"]);

                lstTopicos.Add(topico);
            }

            con.Close();

            return lstTopicos;
        }

        public void Cadastrar(TopicoForumModel topico){                            
            
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                string SqlQuery = "insert into tbTopicos(titulo, descricao) values (@t, @d)";        
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlQuery;
                cmd.Parameters.AddWithValue("@t", topico.Titulo);
                cmd.Parameters.AddWithValue("@d", topico.Descricao);
                
                con.Open();
                cmd.ExecuteNonQuery();
            
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally{
                con.Close();
            }
        }

        public void Editar(TopicoForumModel topico){
            SqlConnection con = new SqlConnection(connectionString);

            try
            {                       
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Update tbTopicos set titulo = @t, descricao =@d where id =@id";
                cmd.Parameters.AddWithValue("@t", topico.Titulo);
                cmd.Parameters.AddWithValue("@d", topico.Descricao );
                cmd.Parameters.AddWithValue("@id", topico.Id );
                con.Open();
                int r = cmd.ExecuteNonQuery();

                if(r == 0){
                    throw new Exception("Ocorreu um erro");
                }

                cmd.Parameters.Clear();
            }
            catch (SqlException se)
            {
                throw new Exception("Erro ao tentar atualizar dados " + se.Message);
            } 
            catch (System.Exception e)
            {
                throw new Exception("Erro inesperado " + e.Message);
                throw;
            } 
            finally{
                con.Close();
            }
        }

        public void Excluir(int Id){
            SqlConnection con = new SqlConnection(connectionString);
            try
            {                       
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "delete from tbTopicos  where id =@id";
                cmd.Parameters.AddWithValue("@id", Id);
                con.Open();
                int r = cmd.ExecuteNonQuery();

                if(r == 0){
                    throw new Exception("Ocorreu um erro");
                }

                cmd.Parameters.Clear();
            }
            catch (SqlException se)
            {
                throw new Exception("Erro ao tentar atualizar dados " + se.Message);
            } 
            catch (System.Exception e)
            {
                throw new Exception("Erro inesperado " + e.Message);
            } 
            finally{
                con.Close();
            }

        }
    }
}