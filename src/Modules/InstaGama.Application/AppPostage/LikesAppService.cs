using InstaGama.Application.AppPostage.Interfaces;
using InstaGama.Domain.Core.Interfaces;
using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InstaGama.Application.AppPostage
{
    public class LikesAppService : ILikesAppService
    {
        private readonly ILikesRepository _likesRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ILogged _logged;

        public LikesAppService(ILikesRepository likesRepository, ICommentRepository commentRepository, ILogged logged)
        {
            _likesRepository = likesRepository;
            _commentRepository = commentRepository;
            _logged = logged;
        }

        public async Task<int> GetQuantityOfLikesByPostageIdAsync(int postageId)
        {
            return await _likesRepository
                            .GetQuantityOfLikesByPostageIdAsync(postageId)
                            .ConfigureAwait(false);
        }

        public async Task InsertAsync(int postageId)
        {
            var loggedUser = _logged.GetUserLoggedId();

            var userId = await _commentRepository.GetUserIdByPostage(postageId);

            var EhAmigo = await _commentRepository.CheckIfRelationshipIsTrue(userId);

            var likesExistForPostage = await _likesRepository
                                                .GetByUserIdAndPostageIdAsync(loggedUser, postageId)
                                                .ConfigureAwait(false);
            if( EhAmigo == true )
            { 
                if (likesExistForPostage != null)
                {
                    await _likesRepository.DeleteAsync(likesExistForPostage.Id)
                              .ConfigureAwait(false);
                }

                var like = new Likes(postageId, loggedUser);
                await _likesRepository.InsertAsync(like).ConfigureAwait(false);
            }
            else
            {
                //new UnauthorizedAccessException("Apenas amigos podem curtir os posts deste usuário.");
                Console.WriteLine("Apenas amigos podem curtir os posts deste usuário.");
            }
        }

       /* private void UnauthorizedAccessException(string v)
        {
            throw new UnauthorizedAccessException(v);
        }*/
    }
}
