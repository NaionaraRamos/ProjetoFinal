using InstaGama.Application.AppPostage.Input;
using InstaGama.Application.AppPostage.Interfaces;
using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InstaGama.Application.AppPostage
{
    public class PostageAppService : IPostageAppService
    {
        private readonly IPostageRepository _postageRepository;
        private readonly ILogged _logged;
        public PostageAppService(IPostageRepository postageRepository,
                                  ILogged logged)
        {
            _postageRepository = postageRepository;
            _logged = logged;
        }

        public async Task<List<Postage>> GetPostageByUserIdAsync()
        {
            var userId = _logged.GetUserLoggedId();

            var postages = await _postageRepository
                                    .GetPostageByUserIdAsync(userId)
                                    .ConfigureAwait(false);
            return postages;
        }

        public async Task<Postage> InsertAsync(PostageInput input)
        {
            var userId = _logged.GetUserLoggedId();

            var postage = new Postage(input.Text, input.Photo, input.Video, userId);

            if (!postage.IsValid())
            {
                throw new ArgumentException("Existem dados que são obrigatórios e não foram preenchidos");
            }

            var id = await _postageRepository
                             .InsertAsync(postage)
                             .ConfigureAwait(false);

            postage.SetId(id);

            return postage;
        }

        public async Task<List<string>> GetGalleryByUserIdAsync(int id)
        {
            var userId = _logged.GetUserLoggedId();

            var gallery = await _postageRepository
                                    .GetGalleryByUserIdAsync(userId)
                                    .ConfigureAwait(false);

            return gallery;
        }
    }
}
