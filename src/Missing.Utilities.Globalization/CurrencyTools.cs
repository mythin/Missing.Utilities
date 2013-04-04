using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using Missing.Utilities.Globalization.Configuration;

namespace Missing.Utilities.Globalization {
    /// <summary>
    /// Currency helper extension methods.
    /// </summary>
    public static class CurrencyTools {
        /// <summary>
        /// Lazily initialize getting the configuration.
        /// </summary>
        private static readonly Lazy<CurrencyIsoCodesConfiguration> config = new Lazy<CurrencyIsoCodesConfiguration>(() => {
            var configSection = ConfigurationManager.OpenExeConfiguration("").Sections.OfType<CurrencyIsoCodesConfiguration>().FirstOrDefault();

            if ( configSection == null ) {
                configSection = new CurrencyIsoCodesConfiguration();
            }

            return configSection;
        }, true);

        #region decimal type

        /// <summary>
        /// Stringify a decimal value to a formatted currency value using the CultureInfo.CurrentCulture.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the CultureInfo.CurrentCulture.</returns>
        public static string ToCurrency( this decimal value ) {
            return value.ToString("c", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Stringify a decimal value to a formatted currency value using the provided numeric ISO code and the CultureInfo.CurrentCulture.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="isoCode">The numeric ISO code used in formatting.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the <paramref name="isoCode" /> and the CultureInfo.CurrentCulture.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the specified ISO code does not exist.</exception>
        public static string ToCurrency( this decimal value, ushort isoCode ) {
            return value.ToCurrency(isoCode, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Stringify a decimal value to a formatted currency value using the provided numeric ISO code and the provided CultureInfo.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="isoCode">The numeric ISO code used in formatting.</param>
        /// <param name="provider">The CultureInfo to use for number formatting.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the <paramref name="isoCode" /> and the <paramref name="provider" />.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the specified ISO code does not exist.</exception>
        public static string ToCurrency( this decimal value, ushort isoCode, CultureInfo provider ) {
            Configuration.CurrencyIsoCode.ValidateNumericCode(isoCode);

            return ToCurrency(value, isoCode, provider.NumberFormat);
        }

        /// <summary>
        /// Stringify a decimal value to a formatted currency value using the provided alpha ISO code and the CultureInfo.CurrentCulture.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="isoCode">The alpha ISO code used in formatting.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the <paramref name="isoCode" /> and the CultureInfo.CurrentCulture.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the specified ISO code does not exist.</exception>
        public static string ToCurrency( this decimal value, string isoCode ) {
            return value.ToCurrency(isoCode, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Stringify a decimal value to a formatted currency value using the provided alpha ISO code and the provided CultureInfo.
        /// </summary>
        /// <param name="value">The value to format.</param>
        /// <param name="isoCode">The alpha ISO code used in formatting.</param>
        /// <param name="provider">The CultureInfo to use for number formatting.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the <paramref name="isoCode" /> and the <paramref name="provider" />.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the specified ISO code does not exist.</exception>
        public static string ToCurrency( this decimal value, string isoCode, CultureInfo cultureInfo ) {
            CurrencyIsoCode.ValidateAlphaCode(isoCode);

            return ToCurrency(value, isoCode, cultureInfo.NumberFormat);
        }

        #endregion

        #region generic value type convertible to decimal type
        // These generics are constrained as close as we can get to saying "Must be a number" or "Must be castable to a decimal".

        /// <summary>
        /// Stringify a value to a formatted currency value using the CultureInfo.CurrentCulture.
        /// </summary>
        /// <typeparam name="T">The type of the value to format.</typeparam>
        /// <param name="value">The value to format.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the CultureInfo.CurrentCulture.</returns>
        public static string ToCurrency<T>( this T value ) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T> {
            return ( value.ToDecimal(CultureInfo.InvariantCulture) ).ToCurrency();
        }


        /// <summary>
        /// Stringify a value to a formatted currency value using the provided numeric ISO code and the CultureInfo.CurrentCulture.
        /// </summary>
        /// <typeparam name="T">The type of the value to format.</typeparam>
        /// <param name="value">The value to format.</param>
        /// <param name="isoCode">The numeric ISO code used in formatting.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the <paramref name="isoCode" /> and the CultureInfo.CurrentCulture.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the specified ISO code does not exist.</exception>
        public static string ToCurrency<T>( this T value, ushort isoCode ) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T> {
            return ( value.ToDecimal(CultureInfo.InvariantCulture) ).ToCurrency(isoCode);
        }

        /// <summary>
        /// Stringify a value to a formatted currency value using the provided numeric ISO code and the provided CultureInfo.
        /// </summary>
        /// <typeparam name="T">The type of the value to format.</typeparam>
        /// <param name="value">The value to format.</param>
        /// <param name="isoCode">The numeric ISO code used in formatting.</param>
        /// <param name="provider">The CultureInfo to use for number formatting.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the <paramref name="isoCode" /> and the <paramref name="provider" />.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the specified ISO code does not exist.</exception>
        public static string ToCurrency<T>( this T value, ushort isoCode, CultureInfo provider ) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T> {
            return ( value.ToDecimal(CultureInfo.InvariantCulture) ).ToCurrency(isoCode, provider);
        }

        /// <summary>
        /// Stringify a value to a formatted currency value using the provided alpha ISO code and the CultureInfo.CurrentCulture.
        /// </summary>
        /// <typeparam name="T">The type of the value to format.</typeparam>
        /// <param name="value">The value to format.</param>
        /// <param name="isoCode">The alpha ISO code used in formatting.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the <paramref name="isoCode" /> and the CultureInfo.CurrentCulture.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the specified ISO code does not exist.</exception>
        public static string ToCurrency<T>( this T value, string isoCode ) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T> {
            return ( value.ToDecimal(CultureInfo.InvariantCulture) ).ToCurrency(isoCode);
        }

        /// <summary>
        /// Stringify a value to a formatted currency value using the provided alpha ISO code and the provided CultureInfo.
        /// </summary>
        /// <typeparam name="T">The type of the value to format.</typeparam>
        /// <param name="value">The value to format.</param>
        /// <param name="isoCode">The alpha ISO code used in formatting.</param>
        /// <param name="provider">The CultureInfo to use for number formatting.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the <paramref name="isoCode" /> and the <paramref name="provider" />.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the specified ISO code does not exist.</exception>
        public static string ToCurrency<T>( this T value, string isoCode, CultureInfo cultureInfo ) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T> {
            return ( value.ToDecimal(CultureInfo.InvariantCulture) ).ToCurrency(isoCode, cultureInfo);
        }

        #endregion

        /// <summary>
        /// The actual code for converting a decimal value to a current format.
        /// </summary>
        /// <typeparam name="TKey">The type of the isoCode.</typeparam>
        /// <param name="value">The value to format.</param>
        /// <param name="isoCode">The alpha or numeric ISO code used in formatting.</param>
        /// <param name="numberFormatInfo">The number formatting to apply.</param>
        /// <returns>A currency string representation of the <paramref name="value" /> based on the <paramref name="isoCode" /> and the <paramref name="numberFormatInfo" />.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the specified ISO code does not exist.</exception>
        private static string ToCurrency<TKey>( decimal value, TKey isoCode, NumberFormatInfo numberFormatInfo ) {
            CurrencyIsoCode currencyIsoCode;

            if ( !config.Value.CurrencyIsoCodes.TryGetValue(isoCode, out currencyIsoCode) ) {
                throw new ArgumentOutOfRangeException("isoCode", string.Format("No currency specified for ISO code '{0}'.", isoCode));
            }

            return currencyIsoCode.Format(value, numberFormatInfo);
        }
    }
}