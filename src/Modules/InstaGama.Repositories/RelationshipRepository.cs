using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using InstaGama.Domain.Core.Interfaces;

namespace InstaGama.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogged _logged;

        public RelationshipRepository(IConfiguration configuration, ILogged logged)
        {
            _configuration = configuration;
            _logged = logged;
        }
 
        public async Task<List<int>> GetAllDeclinedRequests() 
        {
            var loggedUser = _logged.GetUserLoggedId();

            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {

                var sqlCmd = $@"SELECT IdSolicitante FROM TesteExtra
                                  WHERE IdSolicitado = {loggedUser} AND Status = 2";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                    var requests = new List<int>();

                    while (reader.Read())
                    {
                        var requester = int.Parse(reader["IdSolicitante"].ToString());

                        requests.Add(requester);
                    }
                    return requests;
                }
            }
        }
 
        public async Task<List<int>> GetAllAcceptedRequests()
        {
            var loggedUser = _logged.GetUserLoggedId();

            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {

                var sqlCmd = $@"SELECT IdSolicitante FROM TesteExtra
                                  WHERE IdSolicitado = {loggedUser} AND Status = 1";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                    var requests = new List<int>();

                    while (reader.Read())
                    {
                        var requester = int.Parse(reader["IdSolicitante"].ToString());

                        requests.Add(requester);
                    }
                    return requests;
                }
            }
        }

        public async Task<List<int>> GetAllRelationshipRequests()
        {
           var loggedUser = _logged.GetUserLoggedId();

           using (var con = new SqlConnection(_configuration["ConnectionString"]))
           {

               var sqlCmd = $@"SELECT IdSolicitante FROM TesteExtra
                                  WHERE IdSolicitado = {loggedUser} AND Status = 0";

               using (var cmd = new SqlCommand(sqlCmd, con))
               {
                   cmd.CommandType = CommandType.Text;
                   con.Open();

                    var reader = await cmd.ExecuteReaderAsync().ConfigureAwait(false);

                    var requests = new List<int>();
                    
                    while (reader.Read())
                    {
                        var requester = int.Parse(reader["IdSolicitante"].ToString());

                        requests.Add(requester);
                    }

                    return requests;
                }
           }
        }

        public async Task<int> RequestConnection(int idSolicitado)
        {
             var loggedUser = _logged.GetUserLoggedId();

             using (var con = new SqlConnection(_configuration["ConnectionString"]))
             {
                var sqlCmd = $@"INSERT INTO TesteExtra (IdSolicitante, IdSolicitado, Status) VALUES
                                 ('{loggedUser}', '{idSolicitado}', 0);";

                 using (var cmd = new SqlCommand(sqlCmd, con))
                 {
                     cmd.CommandType = CommandType.Text;

                     con.Open();
                     var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                     id = idSolicitado;

                     return int.Parse(id.ToString());
                 }
             }
        }
         
        public async Task<int> AcceptConnection(int idSolicitante)
        {
            var loggedUser = _logged.GetUserLoggedId();

            using (var con = new SqlConnection(_configuration["ConnectionString"]))
             {
                 var sqlCmd = $@"UPDATE TesteExtra SET Status = 1 WHERE IdSolicitado = '{loggedUser}' AND IdSolicitante = '{idSolicitante}'";

                 using (var cmd = new SqlCommand(sqlCmd, con))
                 {
                     con.Open();

                     var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                     id = idSolicitante;
                     await ReverseConnection(idSolicitante);

                     return int.Parse(id.ToString());
                 }
             }
        }

        private async Task<int> ReverseConnection(int idSolicitante)
        {
            var loggedUser = _logged.GetUserLoggedId();

            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"INSERT INTO TesteExtra (IdSolicitante, IdSolicitado, Status) VALUES
                                 ('{idSolicitante}', '{loggedUser}', 1);";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                    id = idSolicitante;

                    return int.Parse(id.ToString());
                }
            }
        }

        public async Task<int> DeclineConnection(int idSolicitante)
        {
            var loggedUser = _logged.GetUserLoggedId();

            using (var con = new SqlConnection(_configuration["ConnectionString"]))
            {
                var sqlCmd = $@"UPDATE TesteExtra SET Status = 2 WHERE IdSolicitado = '{loggedUser}' AND IdSolicitante = '{idSolicitante}'";

                using (var cmd = new SqlCommand(sqlCmd, con))
                {
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

                    id = idSolicitante;

                    return int.Parse(id.ToString());
                }
            }
        }
    }
}




/*  public RelationshipRepository(IConfiguration configuration)
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

        public async Task<int> InsertAsync(Relationship relationship)
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
        }*/


