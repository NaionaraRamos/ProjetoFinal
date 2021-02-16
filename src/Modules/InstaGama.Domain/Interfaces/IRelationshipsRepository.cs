using InstaGama.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaGama.Domain.Interfaces
{
    public interface IRelationshipsRepository
    {
        Task<int> InsertAsync(Relationships relationship);
        Task<List<int>> GetRelationshipsByUserIdAsync(int userId);
    }
}
