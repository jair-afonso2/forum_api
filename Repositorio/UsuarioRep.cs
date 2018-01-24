
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApiForum.Models;

namespace WebApiForum.Repositorio
{
    public class UsuarioRep
    {
        string connectionString = @"Data source=localhost,11433;Initial Catalog=WebApiForum;uid=sa;pwd=DockerSql2017";

        public List<UsuarioModel> Listar(){
            List<UsuarioModel> lstUsuarios = new List<UsuarioModel>();
            
            SqlConnection con = new SqlConnection(connectionString);

            string SqlQuery = "Select * from tbUsuarios";

            SqlCommand cmd = new SqlCommand(SqlQuery,con);

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read()){
                UsuarioModel cidade = new UsuarioModel();

                cidade.Id = Convert.ToInt16(sdr["Id"]);
                cidade.Nome = sdr["Nome"].ToString();
                cidade.Login = sdr["Login"].ToString();
                cidade.Senha = sdr["Senha"].ToString();
                cidade.DataCadastro = Convert.ToDateTime(sdr["DataCadastro"]);

                lstUsuarios.Add(cidade);
            }

            con.Close();

            return lstUsuarios;
        }

        public void Cadastrar(UsuarioModel usuario){                            
            
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                string SqlQuery = "insert into tbUsuarios(Nome, Login, Senha) values (@n, @l, @s)";        
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SqlQuery;
                cmd.Parameters.AddWithValue("@n", usuario.Nome);
                cmd.Parameters.AddWithValue("@l", usuario.Login);
                cmd.Parameters.AddWithValue("@s", usuario.Senha);
                
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

        public void Editar(UsuarioModel usuario){
            SqlConnection con = new SqlConnection(connectionString);

            try
            {                       
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "Update tbUsuarios set nome = @n, login =@l, senha = @s where id =@id";
                cmd.Parameters.AddWithValue("@n", usuario.Nome);
                cmd.Parameters.AddWithValue("@l", usuario.Login );
                cmd.Parameters.AddWithValue("@s", usuario.Senha);
                cmd.Parameters.AddWithValue("@id", usuario.Id);
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
                cmd.CommandText = "delete from tbUsuarios  where id =@id";
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