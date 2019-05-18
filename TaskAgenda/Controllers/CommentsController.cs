using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskAgenda.Models;
using TaskAgenda.Services;
using TaskAgenda.ViewModels;

namespace TaskAgenda.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private ICommentService commentService;
        public CommentsController(ICommentService commentService)
        {
            this.commentService = commentService;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400)]
        public IEnumerable<CommentGetModel> GetFiltered([FromQuery] string filter)
        {
            return commentService.GetAllFiltered(filter);
        }
    }
}