using System.IO;
using System.Reflection;

namespace BertScout2019XmlData
{
    public static class EmbeddedData
    {
        private const string baseResourcePath = "BertScout2019XmlData.EmbeddedResources";

        public static Stream XmlDataStoreFRCEvents()
        {
            var assembly = Assembly.GetAssembly(typeof(EmbeddedData));
            string resourcePath = $"{baseResourcePath}.FRCEvents.xml";
            Stream stream = assembly.GetManifestResourceStream(resourcePath);
            return stream;
        }

        public static Stream XmlDataStoreTeams()
        {
            var assembly = Assembly.GetAssembly(typeof(EmbeddedData));
            string resourcePath = $"{baseResourcePath}.Teams.xml";
            Stream stream = assembly.GetManifestResourceStream(resourcePath);
            return stream;
        }

        public static Stream XmlDataStoreEventTeams()
        {
            var assembly = Assembly.GetAssembly(typeof(EmbeddedData));
            string resourcePath = $"{baseResourcePath}.EventTeams.xml";
            Stream stream = assembly.GetManifestResourceStream(resourcePath);
            return stream;
        }
    }
}
