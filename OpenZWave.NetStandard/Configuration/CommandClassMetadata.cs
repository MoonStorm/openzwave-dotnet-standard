namespace OpenZWave.NetStandard.Configuration
{
    /// <summary>
    /// Metadata information for a command class.
    /// </summary>
    public class CommandClassMetadata
    {
        /// <summary>
        /// The command class identifier.
        /// </summary>
        public CommandClassId CommandClassId { get; set; }

        /// <summary>
        /// A list of parameter metadata.
        /// </summary>
        public CommandClassParameterMetadata[] Parameters { get; set; }
    }
}
