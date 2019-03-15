namespace OpenZWave.NetStandard.Exceptions
{
    /// <summary>
    /// The base exception for all the library exceptions.
    /// </summary>
    public class ZWaveException:System.Exception
    {
        /// <summary>
        /// Constructor taking a message.
        /// </summary>
        public ZWaveException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor taking a message and an inner exception.
        /// </summary>
        public ZWaveException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
