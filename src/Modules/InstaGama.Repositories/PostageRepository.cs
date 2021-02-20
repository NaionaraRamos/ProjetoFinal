using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InstaGama.Repositories
{
    public class PostageRepository : IPostageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogged _logged;

        public PostageRepository(IConfiguration configuration, ILogged logged)
        {
            _configuration = configuration;
            _logged = logged;
        }

        public async Task<List<Postage>> GetPostageByUserIdAsync(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
	                                   UsuarioId,
                                       Texto,
                                       Criacao
                                FROM 
	                                Postagem
                                WHERE 
	                                UsuarioId= '{userId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var postagesForUser = new List<Postage>();

                    while (reader.Read())
                    {
                        var postage = new Postage(int.Parse(reader["Id"].ToString()),
                                                    reader["Texto"].ToString(),
                                                    int.Parse(reader["UsuarioId"].ToString()),
                                                    DateTime.Parse(reader["Criacao"].ToString()));

                        postagesForUser.Add(postage);
                    }

                    return postagesForUser;
                }
            }
        }

        public async Task<int> InsertAsync(Postage postage)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                Postagem (UsuarioId,
                                           Texto,
                                           Criacao)
                                VALUES (@usuarioId,
                                        @texto,
                                        @criacao); SELECT scope_identity();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("usuarioId", postage.UserId);
                    cmd.Parameters.AddWithValue("texto", postage.Text);
                    cmd.Parameters.AddWithValue("criacao", postage.Created);

                    con.Open();
                    var id = await cmd
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    return int.Parse(id.ToString());
                }
            }
        }

        public async Task<List<string>> GetGalleryByUserIdAsync(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                 var loggedUser = _logged.GetUserLoggedId();

                 var sqlCmd = @$"IF(EXISTS(SELECT * FROM TesteExtra
                                           WHERE IdSolicitante = '{loggedUser}'
                                           AND IdSolicitado = '{userId}' AND Status = 1))
                                 BEGIN
                                       SELECT Foto FROM Postagem WHERE UsuarioId= '{userId}' AND Foto <> ''
                                 END";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var GalleryForUser = new List<string>();

                    while (reader.Read())
                    {
                        var image = reader["Foto"].ToString();

                        GalleryForUser.Add(image);
                    }

                    return GalleryForUser;
                }
            }
        }

        public async Task<List<Postage>> FeedUsuario()
        {
            var loggedUSer = _logged.GetUserLoggedId();

            using ( var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"SELECT P.Texto, P.Foto, P.Video, P.Criacao from Postagem P
                                INNER JOIN TesteExtra T on T.idSolicitado = P.UsuarioId and T.Status = 1
                                WHERE t.IdSolicitante = '{loggedUSer}' ORDER BY P.Criacao;";

               // var sqlCmd = @$"SELECT EXISTS(SELECT * FROM TesteExtra WHERE IdSolicitado='{loggedUSer}' AND Status=1);";
                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                    var feed = new List<Postage>();

                    while (reader.Read())
                    {
                        //var post = reader["Texto"].ToString();
                        var post = new Postage(int.Parse(reader["Id"].ToString()),
                                               reader["Texto"].ToString(),
                                               int.Parse(reader["UsuarioId"].ToString()),
                                               DateTime.Parse(reader["Criacao"].ToString()));

                        feed.Add(post);
                    }

                    return feed;
                }
            }
        }
    }
}
