using InstaGama.Application.AppRelationships.Input;
using InstaGama.Application.AppRelationships.Interfaces;
using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaGama.Application.AppRelationships
{
    public class RelationshipsAppService : IRelationshipsAppService
    {
        private readonly IRelationshipsRepository _relationshipsRepository;
        private readonly ILogged _logged;

        public RelationshipsAppService(IRelationshipsRepository relationshipsRepository, 
                                       ILogged logged)
        {
            _relationshipsRepository = relationshipsRepository;
            _logged = logged;
        }

        public async Task<List<Relationships>> GetRelationshipsByUserIdAsync()
        {
            var userId = _logged.GetUserLoggedId();

            var relationships = await _relationshipsRepository
                                    .GetRelationshipsByUserIdAsync(userId)
                                    .ConfigureAwait(false);
            return relationships;
        }

        public async Task<Relationships> InsertAsync(RelationshipsInput input)
        {
            var userId = _logged.GetUserLoggedId();

            var relationship = new Relationships(userId, input.FriendId);

            var id = await _relationshipsRepository
                             .InsertAsync(relationship)
                             .ConfigureAwait(false);

            relationship.SetId(id);

            return relationship;
        }
    }
}
