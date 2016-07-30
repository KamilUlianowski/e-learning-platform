using E_LearningWeb.Services;
using Microsoft.AspNet.SignalR;

namespace E_LearningWeb.Hubs
{
    public class DiscussionHub : Hub
    {

        public void Send(string message, string courseId)
        {
            ISharepointService sharepointService = new SharepointService();
            sharepointService.AddPost(message, courseId);
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage();
        }
    }
}