using System;
using System.Linq;
using LitleW.Models;

namespace LitleW.Helpers
{
    public static class UriHelper
    {
        public static string CombineUri(params string[] uriParts)
        {
            string uri = string.Empty;
            if (uriParts != null && uriParts.Count() > 0)
            {
                char[] trims = { '\\', '/' };
                uri = (uriParts[0] ?? string.Empty).TrimEnd(trims);
                for (int i = 1; i < uriParts.Count(); i++)
                {
                    uri = string.Format("{0}/{1}", uri.TrimEnd(trims), (uriParts[i] ?? string.Empty).TrimStart(trims));
                }
            }
            return uri;
        }

        public static string GetUrlName(Type type)
        {
            if (type == Type.GetType("LitleW.Models.Order"))
                return "Orders";
            else if (type == Type.GetType("LitleW.Models.Inventory"))
                return "Inventories";
            else if (type == Type.GetType("LitleW.Models.StorageCells"))
                return "StorageCells";

            return string.Empty;
        }
    }
}
