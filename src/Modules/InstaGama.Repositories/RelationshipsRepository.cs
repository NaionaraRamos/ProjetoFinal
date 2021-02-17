using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InstaGama.Repositories
{
    public class RelationshipsRepository : IRelationshipsRepository
    {
        private readonly IConfiguration _configuration;

        public RelationshipsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<Relationships>> GetRelationshipsByUserIdAsync(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT Id,
                                       UsuarioId,
                                       AmigoId
                                FROM 
	                                Relacionamento
                                WHERE 
	                                UsuarioId= '{userId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var relationshipsForUser = new List<Relationships>();

                    while (reader.Read())
                    {
                        var relationship = new Relationships(int.Parse(reader["Id"].ToString()),
                                                        int.Parse(reader["UsuarioId"].ToString()),
                                                        int.Parse(reader["AmigoId"].ToString()));

                        relationshipsForUser.Add(relationship);

                    }

                    return relationshipsForUser;
                }
            }
        }

        public async Task<int> InsertAsync(Relationships relationship)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @"INSERT INTO
                                Relacionamento (UsuarioId,
                                                 AmigoID)
                                VALUES (@usuarioId,
                                        @amigoId); SELECT scope_identity();";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("usuarioId", relationship.UserId);
                    cmd.Parameters.AddWithValue("amigoId", relationship.FriendId);

                    con.Open();
                    var id = await cmd
                                    .ExecuteScalarAsync()
                                    .ConfigureAwait(false);

                    return int.Parse(id.ToString());
                }
            }
        }
    }
}
