using System.Configuration;

namespace Missing.Utilities.Globalization.Configuration {
    /// <summary>
    /// Configuration section for adding extra currency ISO codes.
    /// </summary>
    public class CurrencyIsoCodesConfiguration : ConfigurationSection {
        /// <summary>
        /// The collection of configured currency ISO codes.
        /// </summary>
        [ConfigurationProperty("", IsRequired = false, IsKey = false, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(CurrencyIsoCodeCollection))]
        public CurrencyIsoCodeCollection CurrencyIsoCodes {
            get {
                return (CurrencyIsoCodeCollection)base[""];
            }
        }
    }
}