namespace OpenZWave.NetStandard.Configuration
{
    /// <summary>
    /// Device metadata.
    /// </summary>
    public class DeviceMetadata
    {
        /// <summary>
        /// The device identifier.
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// The name of the manufacturer.
        /// </summary>
        public string ManufacturerName { get; set; }

        /// <summary>
        /// A list of command class metadata.
        /// </summary>
        public CommandClassMetadata[] Commands;
    }
}
