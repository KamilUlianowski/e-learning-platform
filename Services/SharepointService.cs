using E_LearningWeb.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using User = E_LearningWeb.Core.Models.User;

namespace E_LearningWeb.Services
{
    public class SharepointService : ISharepointService
    {
        private readonly ClientContext _clientContext;

        public SharepointService()
        {
            if ((ClientContext)System.Web.HttpContext.Current.Session["ClientContext"] != null)
                _clientContext = (ClientContext)System.Web.HttpContext.Current.Session["ClientContext"];
        }

        public List<User> GetSharepointUsers()
        {
            var listOfUsers = new List<User>();
            if (_clientContext == null) return listOfUsers;
            var web = _clientContext.Web;
            _clientContext.Load(web.SiteUsers);
            _clientContext.ExecuteQuery();
            var items = web.SiteUsers;
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.Email))
                {
                    listOfUsers.Add(new User()
                    {
                        Id = item.Id,
                        Email = item.Email,
                        Name = item.Title
                    });
                }
            }
            return listOfUsers;
        }

        private List GetSharepointListByTitle(string nameOfList)
        {
            var web = _clientContext.Web;
            _clientContext.Load(web.Lists);
            _clientContext.ExecuteQuery();

            return _clientContext.Web.Lists.GetByTitle(nameOfList);
        }

        private ListItemCollection GetAllItems(List contextList)
        {
            if (_clientContext == null) return null;
            var query = CamlQuery.CreateAllItemsQuery();
            var items = contextList.GetItems(query);
            _clientContext.Load(items);
            _clientContext.ExecuteQuery();

            return items;
        }

        public int GetUserId()
        {
            var spContext = ((SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"]);
            if (spContext == null) return 0;
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    clientContext.Load(clientContext.Web.CurrentUser);
                    clientContext.ExecuteQuery();
                    return clientContext.Web.CurrentUser.Id;
                }
            }
            return 0;
        }

        public List<Post> GetDiscussionPosts(int courseId)
        {
            var listOfPosts = new List<Post>();
            if (_clientContext == null) return listOfPosts;
            var contextList = GetSharepointListByTitle("Discussion");
            var items = GetAllItems(contextList);
            int id = 0;
            foreach (var item in items)
            {
                if (item["CourseId"] != null)
                {
                    var course = item["CourseId"].ToString();
                    if (string.Equals(courseId.ToString(), course, StringComparison.CurrentCultureIgnoreCase))
                    {
                        id = int.Parse(item["ID"].ToString());
                    }
                }
                if (item["ParentItemID"] != null && Int32.Parse(item["ParentItemID"].ToString()) == id)
                {
                    var help = (FieldUserValue)item["Author"];
                    listOfPosts.Add(new Post
                    {
                        Body = item["Body"].ToString(),
                        Created = item["Created"].ToString(),
                        Author = help.LookupValue

                    });
                }
            }
            return listOfPosts;

        }

        public bool AddPost(string text, string courseId)
        {
            var spContext = ((SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"]);
            if (spContext == null) return false;
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    var web = clientContext.Web;
                    clientContext.Load(web.Lists);
                    clientContext.ExecuteQuery();

                    var contextList = clientContext.Web.Lists.GetByTitle("Discussion");
                    if (contextList == null) return true;

                    CamlQuery query = CamlQuery.CreateAllItemsQuery();
                    ListItemCollection items = contextList.GetItems(query);
                    clientContext.Load(items);
                    clientContext.ExecuteQuery();

                    int id = 0;
                    foreach (var item in items)
                    {
                        if (item["CourseId"] != null)
                        {
                            var course = item["CourseId"].ToString();
                            if (string.Equals(course, courseId))
                            {
                                id = Int32.Parse(item["ID"].ToString());
                            }
                        }
                    }

                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem listItem = contextList.AddItem(itemCreateInfo);
                    listItem["Body"] = text;
                    listItem["ParentItemID"] = id;
                    listItem.Update();
                    clientContext.ExecuteQuery();

                    return true;
                }
            }
            return false;
        }

        public bool CheckIfTheUserHasPermissions()
        {
            if (_clientContext == null) return false;

            BasePermissions bp = new BasePermissions();
            bp.Set(PermissionKind.ManageWeb);
            ClientResult<bool> manageWeb = _clientContext.Web.DoesUserHavePermissions(bp);
            _clientContext.ExecuteQuery();

            return manageWeb.Value;
        }
    }
}