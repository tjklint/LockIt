// Team Name: LockIt
// Team Members: Dylan Savelson, Joshua Kravitz, Timothy (TJ) Klint
// Description: Loads embedded appsettings.json configuration from the assembly resources.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace LockIt.Helpers
{
    /// <summary>
    /// Utility class to load the embedded appsettings.json from the assembly's resources.
    /// </summary>
    public static class AppSettingsLoader
    {
        /// <summary>
        /// Loads the embedded appsettings.json and returns it as a <see cref="JsonElement"/>.
        /// </summary>
        /// <returns>A <see cref="JsonElement"/> representing the parsed appsettings.json file.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the appsettings.json resource cannot be found.</exception>
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
