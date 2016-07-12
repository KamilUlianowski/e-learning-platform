using E_LearningWeb.Models;
using E_LearningWeb.ViewModels;
using Microsoft.SharePoint.Client;
using System;
using System.Web.Mvc;

namespace E_LearningWeb.Controllers
{
    public class HomeController : Controller
    {
        private static bool _sessionInitialized = false;
        private static bool _permissionSet;

        [HttpGet]
        public ActionResult Course()
        {
            var homeViewModel = new HomeViewModel();
            var spContext = (SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"];

            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    if (_permissionSet == false)
                    {
                        _permissionSet = true;
                        System.Web.HttpContext.Current.Session.Add("logged", SetPermissions(clientContext));
                    }
                    var web = clientContext.Web;
                    clientContext.Load(web.Lists);
                    clientContext.ExecuteQuery();
                    var contextList = clientContext.Web.Lists.GetByTitle("Courses");

                    CamlQuery query = CamlQuery.CreateAllItemsQuery();
                    ListItemCollection items = contextList.GetItems(query);
                    clientContext.Load(items);
                    clientContext.ExecuteQuery();

                    foreach (ListItem listItem in items)
                    {
                        homeViewModel.ListOfCourses.Add(new Course()
                        {
                            Title = listItem["Title"].ToString(),
                            Id = Convert.ToInt32(listItem["qgjk"]), // Nazwy kolumn w sharepoint
                            Description = listItem["b6yw"].ToString(),
                            ImageUrl = listItem["_x0076_pe2"].ToString()
                        });

                    }

                    contextList = clientContext.Web.Lists.GetByTitle("Discussion");
                    items = contextList.GetItems(CamlQuery.CreateAllItemsQuery());

                    clientContext.Load(items);
                    clientContext.ExecuteQuery();

                    foreach (var item in items)
                    {
                        FieldUserValue help = (FieldUserValue)item["Author"];
                        homeViewModel.ListOfPosts.Add(new Post
                        {
                            Body = item["Body"].ToString(),
                            Created = item["Created"].ToString(),
                            Author = help.LookupValue

                        });
                    }
                }
            }
            return View(homeViewModel);
        }

        [HttpPost]
        public ActionResult Course(string text)
        {
            var spContext = (SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"];
            using (var clientContext = spContext.CreateUserClientContextForSPHost())
            {
                if (clientContext != null)
                {
                    var web = clientContext.Web;
                    clientContext.Load(web.Lists);
                    clientContext.ExecuteQuery();
                    var contextList = clientContext.Web.Lists.GetByTitle("Discussion");

                    ListItemCreationInformation itemCreateInfo = new ListItemCreationInformation();
                    ListItem listItem = contextList.AddItem(itemCreateInfo);
                    listItem["Body"] = text;

                    listItem.Update();
                    clientContext.ExecuteQuery();
                }
            }
            return RedirectToAction("Course", "Home");
        }

        [SharePointContextFilter]
        public ActionResult Index()
        {
            if (_sessionInitialized == false)
            {
                InitializeSession();
            }
            return View();
        }


        private void InitializeSession()
        {
            System.Web.HttpContext.Current.Session.Add("HttpContext", HttpContext);

            System.Web.HttpContext.Current.Session.Add("SharepointContext",
                SharePointContextProvider.Current.GetSharePointContext(HttpContext));

            System.Web.HttpContext.Current.Session.Add("spHostUrl",
                SharePointContext.GetSPHostUrl(System.Web.HttpContext.Current.Request).AbsoluteUri);

            _sessionInitialized = true;

        }

        private bool SetPermissions(ClientContext clientContext)
        {
            BasePermissions bp = new BasePermissions();

            bp.Set(PermissionKind.ManageWeb);
            ClientResult<bool> manageWeb = clientContext.Web.DoesUserHavePermissions(bp);
            clientContext.ExecuteQuery();

            return manageWeb.Value;
        }

    }
}
