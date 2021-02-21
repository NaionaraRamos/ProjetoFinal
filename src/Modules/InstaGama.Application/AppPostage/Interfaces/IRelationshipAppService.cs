using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using InstaGama.Domain.Entities;

namespace InstaGama.Application.AppPostage.Interfaces
{
    public interface IRelationshipAppService
    {
        Task<int> RequestConnection(int idSolicitado);
        Task<int> AcceptConnection(int idSolicitante);
        Task<int> DeclineConnection(int idSolicitante);
        Task<int> DeleteConnection(int idSolicitado);
        Task<List<User>> GetAllRelationshipRequests();
        Task<List<User>> GetAllAcceptedRequests();
    }
}
