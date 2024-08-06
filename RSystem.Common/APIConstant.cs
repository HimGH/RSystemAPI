using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSystem.Common
{
    public static class APIConstant
    {
       public  const string TOPSTORIES_IDS_URL = "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
       public  const string TOPSTORY_BYID_URL = "https://hacker-news.firebaseio.com/v0/item/{0}.json?print=pretty";
    }
}
