using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace E_LearningWeb.Validators
{
    public class YoutubeUrl : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var url = value.ToString();
            var youtubeMatch =
                new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)")
                    .Match(url);
            return youtubeMatch.Success;
        }
    }
}