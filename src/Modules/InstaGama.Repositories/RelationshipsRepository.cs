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
    public class RelationshipsRepository : IRelationshipsRepository
    {
        private readonly IConfiguration _configuration;

        public RelationshipsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<int>> GetRelationshipsByUserIdAsync(int userId)
        {
            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = @$"SELECT AmigoId
                                FROM 
	                                Relacionamentos
                                WHERE 
	                                UsuarioId= '{userId}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd
                                        .ExecuteReaderAsync()
                                        .ConfigureAwait(false);

                    var relationshipsForUser = new List<int>();

                    while (reader.Read())
                    {
                        var relationship = int.Parse(reader["FriendId"].ToString());       

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
                                Relationships (UsuarioId,
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
