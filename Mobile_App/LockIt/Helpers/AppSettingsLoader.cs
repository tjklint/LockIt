using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace LockIt.Helpers
{
    public static class AppSettingsLoader
    {
        public static JsonElement Load()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = assembly.GetManifestResourceNames()
                                       .FirstOrDefault(name => name.EndsWith("appsettings.json"));

            if (resourceName is null)
                throw new FileNotFoundException("Embedded appsettings.json not found in assembly resources.");

            using var stream = assembly.GetManifestResourceStream(resourceName);
            using var doc = JsonDocument.Parse(stream);
            return doc.RootElement.Clone(); 
        }
    }

}
