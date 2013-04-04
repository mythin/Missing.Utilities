using System.Configuration;
using System.Globalization;

namespace Missing.Utilities.Globalization.Configuration {
    /// <summary>
    /// Holds the properties of a specific ISO 4217 currency specification.
    /// </summary>
    public class CurrencyIsoCode : ConfigurationElement {
        // Magic strings all in one place.
        private const string AlphaCodeName = "alphaCode";
        private const string NumericCodeName = "numericCode";
        private const string SymbolName = "symbol";
        private const string PrecisionName = "precision";
        private const string DecimalSeparatorName = "decimalSeparator";

        /// <summary>
        /// Default constructor for this thing.
        /// </summary>
        internal CurrencyIsoCode() { }

        /// <summary>
        /// Value based constructor for this thing.
        /// </summary>
        /// <param name="alphaCode">The 3 character alpha ISO code.</param>
        /// <param name="numericCode">The 3 digit numeric ISO code.</param>
        /// <param name="symbol">The symbol for this currency.</param>
        /// <param name="precision">The number of decimal places this currency has.</param>
        /// <param name="decimalSeparator">Any overriding decimal separator for this currency.</param>
        internal CurrencyIsoCode( string alphaCode, ushort numericCode, string symbol = "", ushort precision = 2, string decimalSeparator = "" ) {
            this.AlphaCode = alphaCode;
            this.NumericCode = numericCode;
            this.CurrencySymbol = symbol;
            this.Precision = precision;
            this.DecimalSeparator = decimalSeparator;
        }

        /// <summary>
        /// The 3 character alpha ISO code for this currency.
        /// </summary>
        [ConfigurationProperty(AlphaCodeName, IsRequired = true, IsKey = true)]
        public string AlphaCode {
            get { return (string)this[AlphaCodeName]; }
            private set {
                ValidateAlphaCode(value);

                this[AlphaCodeName] = value; }
        }

        /// <summary>
        /// The 3 digit numeric ISO code being defined.
        /// </summary>
        [ConfigurationProperty(NumericCodeName, IsRequired = true, IsKey = true)]
        public ushort NumericCode {
            get { return (ushort)this[NumericCodeName]; }
            private set {
                ValidateNumericCode(value);

                this[NumericCodeName] = value;
            }
        }

        /// <summary>
        /// The symbol for this currency.  This may be null or empty, implying no currency symbol.
        /// </summary>
        [ConfigurationProperty(SymbolName, IsRequired=false, DefaultValue = null)]
        public string CurrencySymbol {
            get {
                if ( this[SymbolName] == null ) {
                    return AlphaCode;
                }

                return (string)this[SymbolName];
            }
            private set { this[SymbolName] = value; }
        }

        /// <summary>
        /// The number of decimal places this currency has.  Must be between 0 and 3 inclusive.
        /// </summary>
        [ConfigurationProperty(PrecisionName, IsRequired = false, DefaultValue = (ushort)2)]
        public ushort Precision {
            get { return (ushort)this[PrecisionName]; }
            private set {
                if ( value > 3 ) {
                    throw new ConfigurationErrorsException("ISO 4217 precision must be between 0 and 3, inclusive.");
                }

                this[PrecisionName] = value;
            }
        }

        /// <summary>
        /// Any overriding decimal separator for this currency.
        /// This should not be set in most cases and should instead allow the culture
        /// to determine this value.
        /// </summary>
        [ConfigurationProperty(DecimalSeparatorName, IsRequired = false, DefaultValue = null)]
        public string DecimalSeparator {
            get { return (string)this[DecimalSeparatorName]; }
            private set { this[DecimalSeparatorName] = value; }
        }

        /// <summary>
        /// Validates that the alpha ISO code is 3 characters long.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Thrown if the ISO code is not valid.</exception>
        internal static void ValidateAlphaCode( string value ) {
            if ( value == null || value.Length != 3 ) {
                throw new ConfigurationErrorsException("Alpha ISO codes must be 3 characters long.  See ISO 4217.");
            }
        }

        /// <summary>
        /// Validates that the numeric ISO code is 3 characters long.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <exception cref="System.Configuration.ConfigurationErrorsException">Thrown if the ISO code is not valid.</exception>
        internal static void ValidateNumericCode( ushort value ) {
            if ( value < 100 || value > 999 ) {
                throw new ConfigurationErrorsException("Numeric ISO codes must be 3 digits long.  See ISO 4217.");
            }
        }

        /// <summary>
        /// Formats a decimal value to a string using the properties of this class and the <see cref="System.Globalization.NumberFormatInfo" /> parameter.
        /// </summary>
        /// <param name="value">The value to stringify.</param>
        /// <param name="numberFormatInfo">The <see cref="System.Globalization.NumberFormatInfo" /> defining how the numbers should be formatted before considering the currency info.</param>
        /// <returns>A formatted currency string.</returns>
        internal string Format( decimal value, System.Globalization.NumberFormatInfo numberFormatInfo ) {
            numberFormatInfo = (NumberFormatInfo)numberFormatInfo.Clone();

            if ( CurrencySymbol != null ) {
                numberFormatInfo.CurrencySymbol = CurrencySymbol;
            }

            if ( !string.IsNullOrEmpty(DecimalSeparator) ) {
                numberFormatInfo.CurrencyDecimalSeparator = DecimalSeparator;
            }

            numberFormatInfo.CurrencyDecimalDigits = Precision;

            return value.ToString("c", numberFormatInfo).Trim();
        }
    }
}