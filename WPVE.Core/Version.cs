using System;
namespace WPVE.Core
{
    /// <summary>
    /// Represents system version
    /// </summary>
    public static class Version
    {
        /// <summary>
        /// Gets the major system version
        /// </summary>
        public const string CURRENT_VERSION = "4.60";
        /// <summary>
        /// Gets the minor system version
        /// </summary>
        public const string MINOR_VERSION = "4";
        /// <summary>
        /// Gets the full system version
        /// </summary>
        public const string FULL_VERSION = CURRENT_VERSION + "." + MINOR_VERSION;
    }
}

