using InstaGama.Application.AppRelationships.Input;
using InstaGama.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaGama.Application.AppRelationships.Interfaces
{
    public interface IRelationshipsAppService
    {
        Task<Relationships> InsertAsync(RelationshipsInput input);
        Task<List<Relationships>> GetRelationshipsByUserIdAsync();
    }
}
