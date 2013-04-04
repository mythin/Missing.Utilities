using System.Collections.Generic;
using System.Linq;

namespace Missing.Utilities.Globalization.Configuration {
    class Default {
        /// <summary>
        /// Default values used to populate the CurrencyIsoCodeCollection
        /// </summary>
        public static readonly IEnumerable<CurrencyIsoCode> CurrentIsoCodes = Enumerable.Empty<CurrencyIsoCode>();
    }
}