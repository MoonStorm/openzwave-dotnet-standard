using System;
using System.Collections.Generic;
using System.Text;

namespace OpenZWave.NetStandard.Exceptions
{
    /// <summary>
    /// Exception denoting a configuration issue.
    /// </summary>
    public class ZWaveConfigurationException:ZWaveException
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ZWaveConfigurationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public ZWaveConfigurationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
