using System;

namespace E_LearningWeb.Models
{
    public class Post
    {
        public string Id { get; set; }

        public string Author { get; set; }

        private string _body;

        public string Body
        {
            get
            {
                int start = _body.IndexOf("<p>", StringComparison.Ordinal) + 3;
                if (start == 2)
                {
                    start = _body.IndexOf(">", StringComparison.Ordinal) + 1;
                }
                string help = _body.Substring(start, _body.Length - start);

                int end = help.IndexOf("<", StringComparison.Ordinal);
                if (end < 0) return string.Empty;
                return help.Substring(0, end);
            }
            set { _body = value; }
        }
        public string Created { get; set; }
    }
}