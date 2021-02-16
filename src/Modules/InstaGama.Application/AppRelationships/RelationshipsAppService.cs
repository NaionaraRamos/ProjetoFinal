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

        public async Task<List<int>> GetRelationshipsByUserIdAsync()
        {
            var userId = _logged.GetUserLoggedId();

            var relationships = await _relationshipsRepository
                                    .GetRelationshipsByUserIdAsync(userId)
                                    .ConfigureAwait(false);
            return relationships;
        }

        public Task<Relationships> InsertAsync(RelationshipsInput input)
        {
            var userId = _logged.GetUserLoggedId();

            var relationship = new Relationships(userId, input.FriendId);

            if (!relationship.IsValid())
            {
                throw new ArgumentException("Existem dados que são obrigatórios e não foram preenchidos");
            }

            var id = await _postageRepository
                             .InsertAsync(postage)
                             .ConfigureAwait(false);

            postage.SetId(id);

            return postage;
        }
    }
}
