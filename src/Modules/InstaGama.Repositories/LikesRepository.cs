using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InstaGama.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogged _logged;

        public LikesRepository(IConfiguration configuration, ILogged logged)
        {
            _configuration = configuration;
            _logged = logged;

        }

        public async Task DeleteAsync(int id)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"DELETE 
                                FROM
                                Curtidas
                               WHERE 
                                Id={id}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                     await cmd
                            .ExecuteScalarAsync()
                            .ConfigureAwait(false);
                }
            }
        }
        
        public async Task<Likes> GetByUserIdAndPostageIdAsync(int userId, int postageId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
	                                   UsuarioId
                                       PostagemId
                                FROM 
	                                Curtidas
                                WHERE 
	                                UsuarioId= '{userId}'
                                AND 
                                    PostagemId= '{postageId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        var like = new Likes(int.Parse(reader["Id"].ToString()),
                                                int.Parse(reader["PostagemId"].ToString()),
                                                int.Parse(reader["UsuarioId"].ToString()));

                        return like;
                    }

                    return default;
                }
            }
        }

        public async Task<int> GetQuantityOfLikesByPostageIdAsync(int postageId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT
                                    COUNT(*) AS Quantidade
                                FROM 
	                                Curtidas
                                WHERE 
	                                PostagemId={postageId}";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    while (reader.Read())
                    {
                        return int.Parse(reader["Quantidade"].ToString());
                    }

                    return default;
                }
            }
        }

        public async Task<int> InsertAsync(Likes likes, int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                /* var sqlCmd = @"INSERT INTO
                                 Curtidas (UsuarioId,
                                            PostagemId)
                                 VALUES (@usuarioId,
                                         @postagemId); SELECT scope_identity();";*/
                var loggedUser = _logged.GetUserLoggedId();

                var sqlCmd = @$"IF(EXISTS(SELECT * FROM TesteExtra
                                           WHERE IdSolicitante = '{loggedUser}'
                                           AND IdSolicitado = @usuarioId AND Status = 1))
                                 BEGIN
                                       INSERT INTO
                                           Curtidas (UsuarioId,
                                           PostagemId)
                                       VALUES (@usuarioId,
                                           @postagemId); SELECT scope_identity()
                                 END";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("usuarioId", likes.UserId);
                    cmd.Parameters.AddWithValue("postagemId", likes.PostageId);

                    con.Open();
                    var id = await cmd
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    id = userId;

                    return int.Parse(id.ToString());
                }
            }
        }
    }
}
