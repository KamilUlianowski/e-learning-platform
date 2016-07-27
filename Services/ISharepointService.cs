using E_LearningWeb.Models;
using E_LearningWeb.ViewModels;
using System.Collections.Generic;

namespace E_LearningWeb.Services
{
    public interface ISharepointService
    {
        List<Post> GetDiscussionPosts(string courseId);
        int GetUserId();
        bool AddPost(string text, string courseId);
        bool CheckIfTheUserHasPermissions();


    }
}