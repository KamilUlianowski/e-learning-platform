using E_LearningWeb.Models;
using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;

namespace E_LearningWeb.Services
{
    public class SharepointService : ISharepointService
    {
        private SharePointContext spContext;
        private ClientContext clientContext;
        public SharepointService()
        {
            spContext = (SharePointContext)System.Web.HttpContext.Current.Session["SharepointContext"];
            clientContext = spContext.CreateUserClientContextForSPHost();
        }

        private List GetSharepointListByTitle(string nameOfList)
        {
            var web = clientContext.Web;
            clientContext.Load(web.Lists);
            clientContext.ExecuteQuery();
            return clientContext.Web.Lists.GetByTitle(nameOfList);
        }

        private ListItemCollection GetAllItems(List contextList)
        {
            CamlQuery query = CamlQuery.CreateAllItemsQuery();
            ListItemCollection items = contextList.GetItems(query);
            clientContext.Load(items);
            clientContext.ExecuteQuery();
            return items;
        }

        public List<Course> GetCourses()
        {
            var listOfCourses = new List<Course>();
            if (clientContext == null) return listOfCourses;

            var contextList = GetSharepointListByTitle("Courses");
            var items = GetAllItems(contextList);

            foreach (ListItem listItem in items)
            {
                listOfCourses.Add(new Course()
                {
                    Title = listItem["Title"].ToString(),
                    Id = Convert.ToInt32(listItem["qgjk"]),
                    Description = listItem["b6yw"].ToString(),
                    ImageUrl = listItem["_x0076_pe2"].ToString()
                });

            }
            return listOfCourses;
        }
    }
}