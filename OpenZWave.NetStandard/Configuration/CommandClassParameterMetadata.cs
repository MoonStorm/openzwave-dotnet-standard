using System;

namespace OpenZWave.NetStandard.Configuration
{
    /// <summary>
    /// Provides metadata for a command class parameter.
    /// </summary>
    public class CommandClassParameterMetadata
    {
        /// <summary>
        /// The name of the property.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the parameter.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The index in the command bit field representation.
        /// </summary>
        public int BitIndex { get; set; }

        /// <summary>
        /// The length in bits in the command bit field representation.
        /// </summary>
        public int BitLength { get; set; }

        /// <summary>
        /// The description of the property.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The optional name of the units used for this property.
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// An optional default value for the property.
        /// </summary>
        public double? DefaultValue { get; set; }

        /// <summary>
        /// An optional minimum value for the property.
        /// </summary>
        public double MinimumValue { get; set; }

        /// <summary>
        /// An optional maximum value for the property.
        /// </summary>
        public double MaximumValue { get; set; }

        /// <summary>
        /// True if the property is read only.
        /// </summary>
        public bool IsReadOnly { get; set; }
    }
}
