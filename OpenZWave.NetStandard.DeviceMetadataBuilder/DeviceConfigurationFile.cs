using OpenZWave.NetStandard.Validations;

namespace OpenZWave.NetStandard.DeviceMetadataBuilder
{
    /// <summary>
    /// Represents a device manufacturer
    /// </summary>
    internal class DeviceConfigurationFile
    {
        public DeviceConfigurationFile(string manufacturerName, string deviceId, string filePath)
        {
            Requires.NotNullOrWhiteSpace(deviceId, nameof(deviceId));
            Requires.FileExists(filePath, nameof(filePath));
            Requires.NotNullOrWhiteSpace(manufacturerName, nameof(manufacturerName));

            DeviceId = deviceId;
            FilePath = filePath;
            ManufacturerName = manufacturerName;
        }

        /// <summary>
        /// The device identifier.
        /// </summary>
        public string DeviceId { get; private set; }

        /// <summary>
        /// The full path to the configuration file.
        /// </summary>
        public string FilePath { get; private set; }

        /// <summary>
        /// The name of the manufacturer.
        /// </summary>
        public string ManufacturerName  { get; private set; }
    }
}
