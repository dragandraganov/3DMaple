using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using _3DMapleSystem.Web.Infrastructure.Popularizers;
using _3DMapleSystem.Data;
using _3DMapleSystem.Web.ViewModels;
using _3DMapleSystem.Data.Models;

namespace _3DMapleSystem.Web.Controllers
{
    [Authorize]
    public class CommentsController : BaseController
    {
        //private readonly ISanitizer sanitizer;

        //public CommentsController(ManagementSystemData data, ISanitizer sanitizer)
        //    : base(data)
        //{
        //    this.sanitizer = sanitizer;
        //}

        public CommentsController(_3DMapleSystemData data, IListPopulizer populizer)
            : base(data, populizer)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Add(Guid polyModelId, CommentViewModel comment)
        {
            if (comment != null && ModelState.IsValid)
            {
                var newComment = new Comment();

                newComment.CreatedOn = DateTime.Now;
                newComment.Author = this.UserProfile;
                newComment.PolyModelId = polyModelId;
                newComment.Content = comment.Content;

                this.Data.Comments.Add(newComment);
                this.Data.SaveChanges();

                comment.AuthorName = this.UserProfile.UserName;
                comment.CreatedOn = DateTime.Now;
                comment.Id = newComment.Id;

                return PartialView("_CommentPartialView", comment);
            }

            return this.JsonError("Unexpexted error");
        }

        //GET Edit comment
        public ActionResult Edit(int commentId)
        {
            var existingCommentAsModel = this.Data.Comments
                .All()
                .Where(c => c.Id == commentId)
                .Project()
                .To<CommentViewModel>()
                .FirstOrDefault();

            return PartialView("_EditComment", existingCommentAsModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int taskId, CommentViewModel comment)
        {

            if (comment != null && ModelState.IsValid)
            {
                var existingComment = this.Data.Comments
                    .All()
                    .FirstOrDefault(c => c.Id == comment.Id);

                existingComment.Content = comment.Content;

                this.Data.Comments.Update(existingComment);
                this.Data.SaveChanges();

                return PartialView("_CommentPartialView", comment);
            }

            return this.JsonError("Unexpexted error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int commentId)
        {
            var existingComment = this.Data.Comments
                   .All()
                   .FirstOrDefault(c => c.Id == commentId);

            if (existingComment != null && ModelState.IsValid)
            {
                this.Data.Comments.Delete(existingComment);
                this.Data.SaveChanges();

                return new EmptyResult();
            }

            return this.JsonError("Unexpexted error");
        }

        public bool ValidateReminderDate(DateTime reminderDate, DateTime requiredByDate)
        {
            if (reminderDate > requiredByDate)
            {
                return false;
            }
            return true;
        }
    }
}