using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace Missing.Utilities.Globalization.Configuration {
    /// <summary>
    /// A collection of <see cref="Missing.Utilities.Globalization.Configuration.CurrencyIsoCode" />s.
    /// </summary>
    public class CurrencyIsoCodeCollection : ConfigurationElementCollection {
        // These are used for fast lookup by ISO codes.
        private IDictionary<ushort, CurrencyIsoCode> numericLookup = new Dictionary<ushort, CurrencyIsoCode>();
        // Alpha ISO codes are always uppercase, so treat whatever is passed in as culture invariant case insensitive
        private IDictionary<string, CurrencyIsoCode> alphaLookup = new Dictionary<string, CurrencyIsoCode>(StringComparer.InvariantCultureIgnoreCase);

        /// <summary>
        /// Create a new <see cref="Missing.Utilities.Globalization.Configuration.CurrencyIsoCodeCollection" /> instance.
        /// </summary>
        public CurrencyIsoCodeCollection() : base() { SetDefaults(); }

        /// <summary>
        /// Create a new <see cref="Missing.Utilities.Globalization.Configuration.CurrencyIsoCodeCollection" /> instance
        /// with the specified <see cref="System.Collections.IComparer" />.
        /// </summary>
        public CurrencyIsoCodeCollection( IComparer comparer ) : base(comparer) { SetDefaults(); }

        /// <summary>
        /// Both of the constructors call this to set the ISO codes set by default as part of this library.
        /// </summary>
        private void SetDefaults() {
            foreach ( var item in Default.CurrentIsoCodes ) {
                BaseAdd(item);
            }
        }

        /// <summary>
        /// Create a new <see cref="Missing.Utilities.Globalization.Configuration.CurrencyIsoCode" /> element.
        /// </summary>
        /// <returns>A new <see cref="Missing.Utilities.Globalization.Configuration.CurrencyIsoCode" />.</returns>
        protected override ConfigurationElement CreateNewElement() {
            return new CurrencyIsoCode();
        }

        /// <summary>
        /// Get the key for a given <see cref="Missing.Utilities.Globalization.Configuration.CurrencyIsoCode" /> element.
        /// </summary>
        /// <param name="element">The <see cref="Missing.Utilities.Globalization.Configuration.CurrencyIsoCode" /> element.</param>
        /// <returns>The NumericCode of the <paramref name="element" />.</returns>
        protected override object GetElementKey( ConfigurationElement element ) {
            return ( (CurrencyIsoCode)element ).NumericCode;
        }

        /// <summary>
        /// Called when adding an element to this collection.
        /// </summary>
        /// <param name="element">The <see cref="System.Configuration.ConfigurationElement" /> element to add.</param>
        protected override void BaseAdd( ConfigurationElement element ) {
            Add(element as CurrencyIsoCode);

            base.BaseAdd(element);
        }

        /// <summary>
        /// Called when adding an element to this collection at a specific index.
        /// </summary>
        /// <param name="index">The index at which to add this <paramref name="element" />.</param>
        /// <param name="element">The <see cref="System.Configuration.ConfigurationElement" /> element to add.</param>
        protected override void BaseAdd( int index, ConfigurationElement element ) {
            Add(element as CurrencyIsoCode);

            base.BaseAdd(index, element);
        }

        /// <summary>
        /// Add the currencyIsoCode to the two dictionaries when it's added for fast lookups.
        /// </summary>
        /// <param name="currencyIsoCode">The <see cref="System.Configuration.ConfigurationElement" /> element to add.</param>
        private void Add( CurrencyIsoCode currencyIsoCode ) {
            if ( currencyIsoCode != null ) {
                alphaLookup[currencyIsoCode.AlphaCode] = currencyIsoCode;
                numericLookup[currencyIsoCode.NumericCode] = currencyIsoCode;
            }
        }

        /// <summary>
        /// Try to retrieve a CurrencyIsoCode based on the given alpha or numeric ISO 4217 code.
        /// </summary>
        /// <param name="key">The alpha or numeric ISO code.</param>
        /// <param name="value">The retrieved <see cref="Missing.Utilities.Globalization.Configuration.CurrencyIsoCode" />, or null if none could be retrieved.</param>
        /// <returns><c>True</c> if the retrieval was successful, otherwise <c>false</c>.</returns>
        internal bool TryGetValue( object key, out CurrencyIsoCode value ) {
            string stringKey = key as string;

            if ( stringKey != null ) {
                return alphaLookup.TryGetValue(stringKey, out value);
            }

            if ( key is ushort ) {
                return numericLookup.TryGetValue((ushort)key, out value);
            }

            value = null;

            return false;
        }
    }
}