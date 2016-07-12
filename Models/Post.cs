namespace E_LearningWeb.Models
{
    public class Post
    {
        public string Author { get; set; }

        private string _body;

        public string Body
        {
            get
            {
                int start = _body.IndexOf("<p>") + 3;
                if (start == 2)
                {
                    start = _body.IndexOf(">") + 1;
                }
                string help = _body.Substring(start, _body.Length - start);

                int end = help.IndexOf("<");
                return help.Substring(0, end);
            }
            set { _body = value; }
        }
        public string Created { get; set; }
    }
}