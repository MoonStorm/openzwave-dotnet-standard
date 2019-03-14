using CommandLine;

namespace OpenZWave.NetStandard.DeviceMetadataBuilder
{
    /// <summary>
    /// Holds the configuration parsed from the command line.
    /// </summary>
    internal class DeviceMetadataBuilderConfiguration
    {
        /// <summary>
        /// Gets or sets the base configuration folder.
        /// This folder contains subfolders with the name of manufacturers and holding the configuration files inside of them.
        /// </summary>
        [Option(
            'i', 
            "configFolder",
            Required = true,
            HelpText = "The path to the folder containing the configuration files grouped in manufacturer folders.")]
        public string ConfigurationFolderPath { get; set; }

        /// <summary>
        /// Gets or sets the database file path.
        /// </summary>
        [Option(
            'o', 
            "database",
            Required = true,
            HelpText = "The full path to the output database.")]
        public string DatabaseFilePath { get; set; }

        [Option(
            'c', 
            "configCount",
            Required = false,
            HelpText = "Maximum number of configuration files to process.")]
        public int? MaximumConfigurationFiles { get; set; }
    }
}
