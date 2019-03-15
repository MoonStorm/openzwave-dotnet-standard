using OpenZWave.NetStandard.Validations;

namespace OpenZWave.NetStandard.DeviceMetadataBuilder
{
    /// <summary>
    /// The processor that stores the device configuration files into the database.
    /// </summary>
    internal class DeviceMetadataBuilderProcessor
    {
        private readonly DeviceConfigurationFileParser _configFileParser;
        private readonly DeviceConfigFileEnumerator _configFileEnumerator;

        /// <summary>
        /// Constructor
        /// </summary>
        public DeviceMetadataBuilderProcessor(
            DeviceConfigFileEnumerator configFileEnumerator,
            DeviceConfigurationFileParser configFileParser)
        {
            Requires.NotNull(configFileEnumerator, nameof(configFileEnumerator));
            Requires.NotNull(configFileParser, nameof(configFileParser));

            _configFileParser = configFileParser;
            _configFileEnumerator = configFileEnumerator;
        }

        /// <summary>
        /// Processes the configuration files.
        /// </summary>
        public void Process()
        {
            foreach (var deviceConfigFile in _configFileEnumerator.GetConfigurationFiles())
            {
                var deviceMetadata = _configFileParser.ParseConfiguration(deviceConfigFile);
            }
        }
    }
}
