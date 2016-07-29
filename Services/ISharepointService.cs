using E_LearningWeb.Core.Models;
using E_LearningWeb.Models;
using System.Collections.Generic;

namespace E_LearningWeb.Services
{
    public interface ISharepointService
    {
        List<Post> GetDiscussionPosts(int courseId);
        int GetUserId();
        List<User> GetSharepointUsers();
        bool AddPost(string text, string courseId);
        bool CheckIfTheUserHasPermissions();


    }
}