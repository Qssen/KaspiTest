using System.Collections.Generic;

namespace PublicationWebApi.TokenManagement
{
    public static class DumbApiTokenManager
    {
        public static Dictionary<string, IList<string>> TokensWithPermissions;

        static DumbApiTokenManager()
        {
            TokensWithPermissions = new Dictionary<string, IList<string>>()
            {
                { "admin-token", new List<string>() { "/api/topWords", "/api/posts", "/api/search" } },
                { "topWords-token", new List<string>() { "/api/topWords" } },
                { "search-token", new List<string>() { "/api/search" } },
            };
        }
    }
}
