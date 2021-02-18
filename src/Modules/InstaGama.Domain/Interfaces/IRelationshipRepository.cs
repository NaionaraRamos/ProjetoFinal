using InstaGama.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Domain.Interfaces
{
    public interface IRelationshipRepository
    {
       // Task<int> InsertAsync(Relationship relationship);
        //Task<List<int>> GetRelationshipsByUserIdAsync(int userId);
          Task<int> RequestConnection(int idSolicitado);
        //  Task<int> ReverseConnection(int idSolicitante);
          Task<int> AcceptConnection(int idSolicitante);
          Task<int> DeclineConnection(int idSolicitante);
          Task<List<int>> GetAllRelationshipRequests();
          Task<List<int>> GetAllDeclinedRequests();
          Task<List<int>> GetAllAcceptedRequests();//friends
    }
}
