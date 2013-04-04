using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace Missing.Utilities.Globalization.Tests {
    public class CurrencyFacts {

        [Theory]
        [PropertyData("Values")]
        public void TestStuffGen<T>( T value/*, string isoCode, string cultureName, string expected*/ ) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T> {
            // Assert.Equal(expected, value.ToCurrency(isoCode, CultureInfo.GetCultureInfo(cultureName)));
        }

        private static IEnumerable<object[]> values = new[]
        {
            // decimal
            new object[] {  10000.57m },
            new object[] { -10000.57m },
            new object[] {      0.57m },
            new object[] {     -0.57m },
            new object[] {  10000m    },
            new object[] { -10000m    },
            new object[] {      0m    },

            // double
            new object[] {  10000.57d },
            new object[] { -10000.57d },
            new object[] {      0.57d },
            new object[] {     -0.57d },
            new object[] {  10000d    },
            new object[] { -10000d    },
            new object[] {      0d    },

            // float
            new object[] {  10000.57f },
            new object[] { -10000.57f },
            new object[] {      0.57f },
            new object[] {     -0.57f },
            new object[] {  10000f    },
            new object[] { -10000f    },
            new object[] {      0f    },

            // short
            new object[] { (short) 10000 },
            new object[] { (short)-10000 },
            new object[] { (short)     0 },

            // ushort
            new object[] { (ushort)10000 },
            new object[] { (ushort)    0 },

            // int
            new object[] { (int) 10000 },
            new object[] { (int)-10000 },
            new object[] { (int)     0 },

            // uint
            new object[] { (uint)10000 },
            new object[] { (uint)    0 },

            // long
            new object[] { (long) 10000 },
            new object[] { (long)-10000 },
            new object[] { (long)     0 },

            // ulong
            new object[] { (ulong)10000 },
            new object[] { (ulong)    0 },
        };

        public static IEnumerable<object[]> Values {
            get { return values; }
        }
    }
}