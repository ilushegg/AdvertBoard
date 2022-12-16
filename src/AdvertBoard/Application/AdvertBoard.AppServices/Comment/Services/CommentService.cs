
using AdvertBoard.Infrastructure.FileService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvertBoard.DataAccess.EntityConfigurations.Comment;
using AdvertBoard.Domain;
using AdvertBoard.Infrastructure.Enum;
using AdvertBoard.Contracts;
using static System.Net.Mime.MediaTypeNames;

namespace AdvertBoard.AppServices.Comment.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;



        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<GetPagedResultDto<CommentDto>> GetAllByAdvertisement(int skip, int take, Guid advertisementId, CancellationToken cancellationToken)
        {
            try
            {
                var total = await _commentRepository.GetAllCount(c => c.AdvertisementId == advertisementId, cancellationToken);
                var comments = await _commentRepository.GetAllPaged(skip, take, c => c.AdvertisementId == advertisementId, cancellationToken);


                return new GetPagedResultDto<CommentDto>
                {
                    Offset = skip,
                    Limit = take,
                    Total = total,
                    Items = comments
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetPagedResultDto<CommentDto>> GetAllByUser(int skip, int take, Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var total = await _commentRepository.GetAllCount(c => c.UserId == userId, cancellationToken);
                var comments = await _commentRepository.GetAllPaged(skip, take, c => c.UserId == userId, cancellationToken);


                return new GetPagedResultDto<CommentDto>
                {
                    Offset = skip,
                    Limit = take,
                    Total = total,
                    Items = comments
                };

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Guid> AddAsync(Guid userId, Guid advertisementId, string text, CancellationToken cancellationToken)
        {
            try
            {
                var exComment = await _commentRepository.GetWhere(c => c.AdvertisementId == advertisementId && c.UserId == userId, cancellationToken);
                if (exComment == null)
                {
                    var comment = new Domain.Comment
                    {
                        Id = new Guid(),
                        UserId = userId,
                        AdvertisementId = advertisementId,
                        Text = text,
                        DateTimeCreated = DateTime.UtcNow,
                        Status = CommentStatus.Moderating
                    };

                    await _commentRepository.AddAsync(comment, cancellationToken);
                    return comment.Id;
                }
                else
                {
                    throw new InvalidOperationException($"Комментарий к этому объявлению у пользователя уже существует");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<Guid> EditAsync(Guid id, string text, CommentStatus commentStatus, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetById(id, cancellationToken);
            if (comment == null)
            {
                throw new InvalidOperationException($"Комментарий с идентификатором {id} не найден.");
            }

            comment.Text = text;
            comment.Status = CommentStatus.Moderating;

            await _commentRepository.EditAsync(comment, cancellationToken);
            return id;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetById(id, cancellationToken);
            if (comment == null)
            {
                throw new InvalidOperationException($"Комментарий с идентификатором {id} не найден.");
            }
            await _commentRepository.Delete(comment, cancellationToken);
        }

    }
}
