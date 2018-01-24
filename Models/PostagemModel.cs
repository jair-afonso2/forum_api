using System;

namespace WebApiForum.Models
{
    public class PostagemModel
    {
        public int Id { get; set; }
        public int IdTopico { get; set; }
        public TopicoForumModel Topico { get; set; }
        
        public int IdUsuario { get; set; }
        public UsuarioModel Usuario { get; set; }
        
        public string Mensagem { get; set; }
        public DateTime DataPublicacao { get; set; }
    }
}