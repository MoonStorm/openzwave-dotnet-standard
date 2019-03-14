using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenZWave.NetStandard.Validations;

namespace OpenZWave.NetStandard.DeviceMetadataBuilder
{
    /// <summary>
    /// Discovers device configuration files.
    /// </summary>
    internal class DeviceConfigFileEnumerator
    {
        private readonly DeviceMetadataBuilderConfiguration _configuration;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DeviceConfigFileEnumerator(DeviceMetadataBuilderConfiguration configuration)
        {
            Requires.NotNull(configuration, nameof(configuration));
            Requires.DirectoryExists(configuration.ConfigurationFolderPath, nameof(configuration.ConfigurationFolderPath));

            _configuration = configuration;
        }

        /// <summary>
        /// Returns the configuration files discovered in the provided path.
        /// </summary>
        public IEnumerable<DeviceConfigurationFile> GetConfigurationFiles()
        {
            return Directory.EnumerateFiles(_configuration.ConfigurationFolderPath, "*.xml", SearchOption.AllDirectories).Select(
                fullConfigFilePath =>
                {
                    var deviceId = Path.GetFileNameWithoutExtension(fullConfigFilePath);
                    var manufacturerName = Path.GetFileName(Path.GetDirectoryName(fullConfigFilePath));
                    return new DeviceConfigurationFile(manufacturerName, deviceId, fullConfigFilePath);
                }).Take(_configuration.MaximumConfigurationFiles??int.MaxValue);
        }
    }
}
