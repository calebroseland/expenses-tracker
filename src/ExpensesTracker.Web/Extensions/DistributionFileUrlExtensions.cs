using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpensesTracker.Web
{
    public static class DistributionFileUrlExtensions
    {
        private const string DISTFOLDER = "~/assets/dist/";

        private static Dictionary<string, List<string>> _stats;

        public static string DistributionFile(this UrlHelper helper, string filename)
        {
            var stats = GetStats();
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
            var realFilename = stats[nameWithoutExtension].First(x => x.EndsWith(Path.GetExtension(filename)));
            return helper.Content(DISTFOLDER + realFilename);
        }

        private static Dictionary<string, List<string>> GetStats()
        {
            if (_stats == null || HttpContext.Current.IsDebuggingEnabled)
            {
                var statsPath = HttpContext.Current.Server.MapPath(DISTFOLDER + "stats.json");
                _stats = JsonConvert.DeserializeObject<StatsFile>(File.ReadAllText(statsPath)).AssetsByChunkName;
            }

            return _stats;
        }

        private class StatsFile
        {
            public Dictionary<string, List<string>> AssetsByChunkName { get; set; }
        }
    }
}