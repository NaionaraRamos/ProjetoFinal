using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogged _logged;

        public CommentRepository(IConfiguration configuration, ILogged logged)
        {
            _configuration = configuration;
            _logged = logged;
        }

        public async Task<List<Comments>> GetByPostageIdAsync(int postageId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
	                                   UsuarioId,
                                       PostagemId,
                                       Texto,
                                       Criacao,
                                       Foto,
                                       Video
                                FROM 
	                                Comentario
                                WHERE 
	                                PostagemId= '{postageId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var commentsForPostage = new List<Comments>();

                    while (reader.Read())
                    {
                        var comment = new Comments(int.Parse(reader["Id"].ToString()),
                                                    int.Parse(reader["PostagemId"].ToString()),
                                                    int.Parse(reader["UsuarioId"].ToString()),
                                                    reader["Texto"].ToString(),
                                                    DateTime.Parse(reader["Criacao"].ToString()));
                                                    reader["Foto"].ToString();
                                                    reader["Video"].ToString();

                        commentsForPostage.Add(comment);
                    }

                    return commentsForPostage;
                }
            }
        }

        public async Task<bool> CheckIfRelationshipIsTrue(int userId)
        {
            var loggedUser = _logged.GetUserLoggedId();

            bool EhAmigo = false;

            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"SELECT COUNT(*) AS Contagem FROM TesteExtra WHERE IdSolicitado = {userId}, IdSolicitante = {loggedUser} AND Status = 1"; 

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var relationshipExists = int.Parse(reader["Contagem"].ToString());

                    if(relationshipExists == 1)
                    {
                        EhAmigo = true;
                    }

                    return EhAmigo;
                }
            }
        }

        public async Task<int> GetUserIdByPostageId(int postagemId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"SELECT UsuarioId FROM Postagem WHERE PostagemId = {postagemId}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        return int.Parse(reader["UsuarioId"].ToString());
                    }

                    return default;
                }
            }
        }

        public async Task<int> InsertAsync(Comments comment)
        {
            var loggedUser = _logged.GetUserLoggedId();

            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"IF(EXISTS(SELECT * FROM TesteExtra
                                           WHERE IdSolicitante = '{loggedUser}'
                                           AND IdSolicitado = @usuarioId AND Status = 1))
                                 BEGIN
                                       INSERT INTO
                                Comentario (UsuarioId,
                                             PostagemId,
                                             Texto,
                                             Criacao)
                                VALUES (@usuarioId,
                                        @postagemId,
                                        @texto,
                                        @criacao)
                                 END";
                /*var sqlCmd = @"INSERT INTO
                                Comentario (UsuarioId,
                                             PostagemId,
                                             Texto,
                                             Criacao)
                                VALUES (@usuarioId,
                                        @postagemId,
                                        @texto,
                                        @criacao); SELECT scope_identity();";*/

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("usuarioId", comment.UserId);
                    cmd.Parameters.AddWithValue("postagemId", comment.PostageId);
                    cmd.Parameters.AddWithValue("texto", comment.Text);
                    cmd.Parameters.AddWithValue("criacao", comment.Created);

                    con.Open();
                    var id = await cmd
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    id = comment.UserId;

                    return int.Parse(id.ToString());
                }
            }
        }
    }
}

