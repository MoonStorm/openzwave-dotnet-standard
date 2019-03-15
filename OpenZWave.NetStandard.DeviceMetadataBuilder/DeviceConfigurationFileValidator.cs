using System;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;
using OpenZWave.NetStandard.Configuration;
using OpenZWave.NetStandard.Exceptions;

namespace OpenZWave.NetStandard.DeviceMetadataBuilder
{
    /// <summary>
    /// Holds validation logic for xml configuration files.
    /// </summary>
    internal class DeviceConfigurationFileValidator
    {
        private readonly string _xmlFilePath;

        /// <summary>
        /// Constructor.
        /// </summary>
        public DeviceConfigurationFileValidator(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }

        /// <summary>
        /// Validates an XML element
        /// </summary>
        [ContractAnnotation("xmlElement:null => halt")]
        public void ValidateElement(XElement xmlElement, string elementName)
        {
            if (xmlElement == null)
            {
                throw new ZWaveConfigurationException($"Found no element in file '{_xmlFilePath}' when an element named '{elementName}' was expected.");
            }

            if (xmlElement.Name.LocalName != elementName)
            {
                throw new ZWaveConfigurationException($"Expecting element '{elementName}' in file '{_xmlFilePath}' but found '{xmlElement.Name.LocalName}' instead.");
            }
        }

        /// <summary>
        /// Validates whether an XML element has the provided attributes.
        /// </summary>
        public void ValidateKnownElementAttributes(XElement xmlElement, params string[] knownAttributes)
        {
            var unknownAttributes = xmlElement.Attributes()
                .Select(attr => attr.Name.LocalName)
                .Except(knownAttributes)
                .ToArray();

            if (unknownAttributes.Length > 0)
            {
                throw new ZWaveConfigurationException($"Unknown attributes '{string.Join(',', unknownAttributes)}' were found on the element '{xmlElement.Name.LocalName}' in file '{_xmlFilePath}'");
            }
        }

        /// <summary>
        /// Validates whether an XML element has the provided attributes.
        /// </summary>
        public void ValidateMandatoryElementAttributes(XElement xmlElement, params string[] mandatoryAttributes)
        {
            var mandatoryAttrsNotPresent = mandatoryAttributes
                .Except(xmlElement.Attributes().Select(attr => attr.Name.LocalName))
                .ToArray();

            if (mandatoryAttrsNotPresent.Length > 0)
            {
                throw new ZWaveConfigurationException($"Mandatory attributes '{string.Join(',',mandatoryAttrsNotPresent)}' are not present on the element '{xmlElement.Name.LocalName}' in file '{_xmlFilePath}'");
            }
        }

        /// <summary>
        /// Validates a command class id in string format
        /// </summary>
        public CommandClassId ValidateCommandClassId(string commandClassId)
        {
            if (!int.TryParse(commandClassId, out int intCommandClassId))
            {
                throw new ZWaveConfigurationException($"Unknown command class id '{commandClassId}' found in file '{_xmlFilePath}'");
            }

            return this.ValidateCommandClassId(intCommandClassId);
        }

        /// <summary>
        /// Validates a command class id in int format
        /// </summary>
        public CommandClassId ValidateCommandClassId(int commandClassId)
        {
            if (!Enum.GetValues(typeof(CommandClassId)).Cast<int>().Contains(commandClassId))
            {
                throw new ZWaveConfigurationException($"Unknown command class id '0x{commandClassId:X}' found in file '{_xmlFilePath}'");
            }

            return (CommandClassId) commandClassId;
        }

    }
}
