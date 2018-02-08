using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task6
{
    class Validate
    {
        public static bool Price(string url)
        {
            string pattern = @"\d{1,}(\$|\sRUB|\sBYN)";
            return Regex.IsMatch(url, pattern);
        }
        public static bool URL(string url)
        {
            string pattern = @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            return Regex.IsMatch(url, pattern);
        }
        public static bool Paths(string paths)
        {
            string pattern = @"^([a-z]:|~)\\\[^:/*?<>|]{0,}$";
            return Regex.IsMatch(paths, pattern, RegexOptions.IgnoreCase);
        }
        public static bool Email(string email)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}
