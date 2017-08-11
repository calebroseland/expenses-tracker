using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpensesTracker.Web
{
    public static class DistributionFileUrlExtensions
    {
        private const string DISTFOLDER = "~/assets/dist/";

        private static dynamic _stats;

        public static string DistributionFile(this UrlHelper helper, string filename)
        {
            var vendor = Path.GetFileNameWithoutExtension(filename);
            var realFilename = GetFileName(vendor, Path.GetExtension(filename));
            return helper.Content(DISTFOLDER + realFilename);
        }

        private static dynamic GetStats()
        {
            if (_stats == null || HttpContext.Current.IsDebuggingEnabled)
            {
                var statsPath = HttpContext.Current.Server.MapPath(DISTFOLDER + "stats.json");
                _stats = JsonConvert.DeserializeObject<dynamic>(File.ReadAllText(statsPath)).assetsByChunkName;
            }

            return _stats;
        }

        private static string GetFileName(string vendor, string extension)
        {
            var files = GetStats()[vendor];
            if (files.GetType() == typeof(JValue))
            {
                return files.ToString();
            }

            return ((JArray)files).First(x => x.ToString().EndsWith(extension)).ToString();
        }
    }
}