using InstaGama.Application.AppPostage.Interfaces;
using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Application.AppPostage
{
    public class RelationshipAppService : IRelationshipAppService
    {
        private readonly IRelationshipRepository _relationshipRepository;
        private readonly ILogged _logged;

        public RelationshipAppService(IRelationshipRepository relationshipRepository, ILogged logged)
        {
            _relationshipRepository = relationshipRepository;
            _logged = logged;
        }

        public async Task<int> AcceptConnection(int idSolicitante)
        {
            var id = await _relationshipRepository.AcceptConnection(idSolicitante).ConfigureAwait(false);
            return id;
        }

        public async Task<int> DeclineConnection(int idSolicitante)
        {
            var id = await _relationshipRepository.DeclineConnection(idSolicitante).ConfigureAwait(false);
            return id;
        }

        public async Task<int> DeleteConnection(int idSolicitado)
        {
            var id = await _relationshipRepository.DeleteConnection(idSolicitado).ConfigureAwait(false);
            return id;
        }

        public async Task<int> RequestConnection(int idSolicitado)
        {
            var id = await _relationshipRepository.RequestConnection(idSolicitado).ConfigureAwait(false);
            return id;
        }

      /*  public async Task<List<int>> GetAllDeclinedRequests()
        {
            var declinedRequests = await _relationshipRepository.GetAllDeclinedRequests().ConfigureAwait(false);
            return declinedRequests;
        }

        public async Task<List<int>> GetAllAcceptedRequests()
        {
            var acceptedRequests = await _relationshipRepository.GetAllAcceptedRequests().ConfigureAwait(false);
            return acceptedRequests;
        }

        public async Task<List<int>> GetAllRelationshipRequests()
        {
            var allRequests = await _relationshipRepository.GetAllRelationshipRequests().ConfigureAwait(false);
            return allRequests;
        }*/
    }
}
