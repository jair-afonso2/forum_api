using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApiForum.Models;

namespace WebApiForum.Repositorio
{
    public class PostagemRep
    {
        string connectionString = @"Data source=localhost,11433;Initial Catalog=WebApiForum;uid=sa;pwd=DockerSql2017";

        public List<PostagemModel> Listar(){
            List<PostagemModel> lstPostagens = new List<PostagemModel>();
            
            SqlConnection con = new SqlConnection(connectionString);

            string SqlQuery = "Select * from tbPostagens";

            SqlCommand cmd = new SqlCommand(SqlQuery,con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read()){
                PostagemModel postagem = new PostagemModel();

                postagem.Id = Convert.ToInt16(sdr["Id"]);
                postagem.IdTopico = Convert.ToInt16(sdr["IdTopico"]);
                postagem.Topico.Titulo = sdr["topicotitulo"].ToString();
                postagem.Topico.Descricao = sdr["topicodescricao"].ToString();
                postagem.IdUsuario = Convert.ToInt16(sdr["IdUsuario"]);
                postagem.Usuario.Nome = sdr["usuarionome"].ToString();
                postagem.Usuario.Login = sdr["usuariologin"].ToString();
                postagem.Mensagem = sdr["postagemmensagem"].ToString();
                postagem.DataPublicacao = Convert.ToDateTime(sdr["postagemDataPublicacao"]);

                lstPostagens.Add(postagem);
            }

            con.Close();

            return lstPostagens;
        }

        public void Cadastrar(PostagemModel postagem){                            
            
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                string SqlQuery = "insert into tbPostagens(Nome, Login, Senha) values (@n, @l, @s)";        
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlQuery;
                // cmd.Parameters.AddWithValue("@n", postagem.Nome);
                // cmd.Parameters.AddWithValue("@l", postagem.Login);
                // cmd.Parameters.AddWithValue("@s", postagem.Senha);
                
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

        public void Editar(PostagemModel postagem){
            SqlConnection con = new SqlConnection(connectionString);

            try
            {                       
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Update tbPostagens set nome = @n, login =@l, senha = @s where id =@id";
                // cmd.Parameters.AddWithValue("@n", usuario.Nome);
                // cmd.Parameters.AddWithValue("@l", usuario.Login );
                // cmd.Parameters.AddWithValue("@s", usuario.Senha);
                // cmd.Parameters.AddWithValue("@id", usuario.Id);
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
                cmd.CommandText = "delete from tbPostagens  where id =@id";
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