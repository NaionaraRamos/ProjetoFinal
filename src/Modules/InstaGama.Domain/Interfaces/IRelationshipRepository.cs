using InstaGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Domain.Interfaces
{
    public interface IRelationshipRepository
    {
        Task<int> RequestConnection(int idSolicitado);
        Task<int> AcceptConnection(int idSolicitante);
        Task<int> DeclineConnection(int idSolicitante);
        Task<int> DeleteConnection(int idSolicitante);
        Task<List<User>> GetAllRelationshipRequests();
        Task<List<User>> GetAllAcceptedRequests();
    }
}
