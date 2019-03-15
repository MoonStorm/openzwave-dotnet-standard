using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using OpenZWave.NetStandard.Configuration;
using OpenZWave.NetStandard.Validations;

namespace OpenZWave.NetStandard.DeviceMetadataBuilder
{
    /// <summary>
    /// Used for parsing configuration files.
    /// </summary>
    internal class DeviceConfigurationFileParser
    {
        private readonly ILogger<DeviceConfigurationFileParser> _logger;

        /// <summary>
        /// Default constructor
        /// </summary>
        public DeviceConfigurationFileParser(ILogger<DeviceConfigurationFileParser> logger)
        {
            Requires.NotNull(logger, nameof(logger));

            _logger = logger;
        }

        /// <summary>
        /// Parses a configurtion
        /// </summary>
        public DeviceMetadata ParseConfiguration(DeviceConfigurationFile configuration)
        {
            Requires.NotNull(configuration, nameof(configuration));
            var commandClasses = this.ParseConfigurationFile(configuration.FilePath).ToArray();
            return null;
        }

        private IEnumerable<CommandClassMetadata> ParseConfigurationFile(string xmlFilePath)
        {
            var validator = new DeviceConfigurationFileValidator(xmlFilePath);

            using (var configFileStream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var xmlConfig = XDocument.Load(configFileStream);

                var xmlProduct = xmlConfig.Root;
                validator.ValidateElement(xmlProduct, "Product");

                foreach (var xmlCommandClass in xmlProduct.Elements())
                {
                    validator.ValidateElement(xmlCommandClass, "CommandClass");
                    validator.ValidateMandatoryElementAttributes(xmlCommandClass, "id");
                    validator.ValidateKnownElementAttributes(xmlCommandClass, "id", "base");

                    int? baseZeroOrOne = null;
                    if (int.TryParse(xmlCommandClass.Attribute("base")?.Value, out int rawBaseZeroOrOne))
                    {
                        baseZeroOrOne = rawBaseZeroOrOne;
                    }
                    var commandClassId = validator.ValidateCommandClassId(xmlCommandClass.Attribute("id")?.Value);

                    var commandClassMetadata = new CommandClassMetadata()
                    {
                        CommandClassId = commandClassId
                    };

                    yield return commandClassMetadata;
                }
            }
        }
    }
}
