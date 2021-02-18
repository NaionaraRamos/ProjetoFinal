using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Application.AppPostage.Interfaces
{
    public interface IRelationshipAppService
    {
        Task<int> RequestConnection(int idSolicitado);
        Task<int> AcceptConnection(int idSolicitante);
        Task<int> DeclineConnection(int idSolicitante);
        Task<List<int>> GetAllRelationshipRequests();
        Task<List<int>> GetAllDeclinedRequests();
        Task<List<int>> GetAllAcceptedRequests();
    }
}
