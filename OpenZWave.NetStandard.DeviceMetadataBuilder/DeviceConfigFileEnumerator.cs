using System;
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
            return Directory.EnumerateDirectories(_configuration.ConfigurationFolderPath, "*.*",
                    SearchOption.TopDirectoryOnly)
                .OrderBy(path => path)
                .Select(manufacturerPath =>
                {
                    var manufacturerName = Path.GetFileName(manufacturerPath);
                    return new
                    {
                        ManufacturerName = manufacturerName,
                        ManufacturerDeviceConfigFilePaths =
                            Directory.EnumerateFiles(manufacturerPath, "*.xml", SearchOption.AllDirectories)
                    };
                })
                .SelectMany(manufacturerInfo =>
                {
                    return manufacturerInfo.ManufacturerDeviceConfigFilePaths.Select(manufacturerDeviceConfigFilePath =>
                    {
                        var deviceId = Path.GetFileNameWithoutExtension(manufacturerDeviceConfigFilePath);
                        return new DeviceConfigurationFile(manufacturerInfo.ManufacturerName, deviceId,
                            manufacturerDeviceConfigFilePath);
                    });
                }).Take(_configuration.MaximumConfigurationFiles ?? int.MaxValue);
        }
    }
}
