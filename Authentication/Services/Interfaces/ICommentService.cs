using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.RequestModels;

namespace Services.Interfaces
{
    public interface ICommentService
    {
        Task<bool> PostCommentAsync(CommentModel model, string userId);
    }
}
