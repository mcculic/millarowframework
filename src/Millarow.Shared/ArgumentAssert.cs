using System;
using System.Collections;
using System.Globalization;

namespace Millarow
{
    [System.Diagnostics.DebuggerStepThrough]
    internal static class ArgumentAssert
    {
        #region NotNull

        public static void AssertNotNull<T>(this T argumentValue, string argumentName)
            where T : class
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName, ValueCannotBeNull);
        }

        public static void AssertNotNull<T>(this T? argumentValue, string argumentName)
            where T : struct
        {
            if (argumentValue == null)
                throw new ArgumentNullException(argumentName, ValueCannotBeNull);
        }

        #endregion

        #region NotNullOrEmpty

        public static void AssertNotNullOrEmpty(this string argumentValue, string argumentName)
        {
            if (argumentValue == null)
                throw new ArgumentNullException(ValueCannotBeNull, argumentName);

            if (string.IsNullOrEmpty(argumentValue))
                throw new ArgumentException(ValueCannotBeEmpty, argumentName);
        }

        public static void AssertNotNullOrEmpty<T>(this T argumentValue, string argumentName)
            where T : ICollection
        {
            if (argumentValue == null)
                throw new ArgumentNullException(ValueCannotBeNull, argumentName);

            if (argumentValue.Count == 0)
                throw new ArgumentException(ValueCannotBeEmpty, argumentName);
        }

        #endregion

        #region NotZero

        public static void AssertNotZero(this sbyte argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this byte argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this short argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this ushort argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this int argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this uint argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this long argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this ulong argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this float argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this double argumentValue, string argumentName)
        {
            if (argumentValue == 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this IntPtr argumentValue, string argumentName)
        {
            if (argumentValue == IntPtr.Zero)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        public static void AssertNotZero(this UIntPtr argumentValue, string argumentName)
        {
            if (argumentValue == UIntPtr.Zero)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeZero);
        }

        #endregion

        #region GreaterThanZero

        public static void AssertGreaterThanZero(this sbyte argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        public static void AssertGreaterThanZero(this byte argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        public static void AssertGreaterThanZero(this short argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        public static void AssertGreaterThanZero(this ushort argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        public static void AssertGreaterThanZero(this int argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        public static void AssertGreaterThanZero(this uint argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        public static void AssertGreaterThanZero(this long argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        public static void AssertGreaterThanZero(this ulong argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        public static void AssertGreaterThanZero(this float argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        public static void AssertGreaterThanZero(this double argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueMustBeGreaterThanZero);
        }

        #endregion

        #region NotNegative

        public static void AssertNotNegative(this sbyte argumentValue, string argumentName)
        {
            if (argumentValue < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeEmpty);
        }

        public static void AssertNotNegative(this short argumentValue, string argumentName)
        {
            if (argumentValue < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeNegative);
        }

        public static void AssertNotNegative(this int argumentValue, string argumentName)
        {
            if (argumentValue < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeNegative);
        }

        public static void AssertNotNegative(this long argumentValue, string argumentName)
        {
            if (argumentValue < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeNegative);
        }

        public static void AssertNotNegative(this float argumentValue, string argumentName)
        {
            if (argumentValue < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeNegative);
        }

        public static void AssertNotNegative(this double argumentValue, string argumentName)
        {
            if (argumentValue < 0)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeNegative);
        }

        #endregion

        #region InRange

        public static void AssertInRange(this byte argumentValue, string argumentName, byte minInclusive, byte maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertInRange(this sbyte argumentValue, string argumentName, sbyte minInclusive, sbyte maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertInRange(this short argumentValue, string argumentName, short minInclusive, short maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertInRange(this ushort argumentValue, string argumentName, ushort minInclusive, ushort maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertInRange(this int argumentValue, string argumentName, int minInclusive, int maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertInRange(this uint argumentValue, string argumentName, uint minInclusive, uint maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertInRange(this long argumentValue, string argumentName, long minInclusive, long maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertInRange(this ulong argumentValue, string argumentName, ulong minInclusive, ulong maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertInRange(this float argumentValue, string argumentName, float minInclusive, float maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertInRange(this double argumentValue, string argumentName, double minInclusive, double maxInclusive)
        {
            if (argumentValue < minInclusive || argumentValue > maxInclusive)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

		#endregion

		#region Smaller

		public static void AssertSmaller(this byte argumentValue, string argumentName, byte maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		public static void AssertSmaller(this sbyte argumentValue, string argumentName, sbyte maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		public static void AssertSmaller(this short argumentValue, string argumentName, short maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		public static void AssertSmaller(this ushort argumentValue, string argumentName, ushort maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		public static void AssertSmaller(this int argumentValue, string argumentName, int maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		public static void AssertSmaller(this uint argumentValue, string argumentName, uint maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		public static void AssertSmaller(this long argumentValue, string argumentName, long maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		public static void AssertSmaller(this ulong argumentValue, string argumentName, ulong maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		public static void AssertSmaller(this float argumentValue, string argumentName, float maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		public static void AssertSmaller(this double argumentValue, string argumentName, double maxInclusive)
		{
			if (argumentValue > maxInclusive)
				throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
		}

		#endregion

		#region AssertListIndex

		public static void AssertListIndex(this byte argumentValue, string argumentName, int listCount)
        {
            if (argumentValue >= listCount)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertListIndex(this sbyte argumentValue, string argumentName, sbyte listCount)
        {
            if (argumentValue >= listCount)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertListIndex(this short argumentValue, string argumentName, short listCount)
        {
            if (argumentValue >= listCount)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertListIndex(this ushort argumentValue, string argumentName, ushort listCount)
        {
            if (argumentValue >= listCount)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertListIndex(this int argumentValue, string argumentName, int listCount)
        {
            if (argumentValue >= listCount)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        #endregion

        public static void AssertRange<T>(this T argumentValue, string argumentName, bool failed)
            where T: struct
        {
            if (failed)
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueOutOfRange);
        }

        public static void AssertIsNumber(this double argumentValue, string argumentName)
        {
            if (double.IsNaN(argumentValue) || double.IsInfinity(argumentValue))
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeNegative);
        }

        public static void AssertNotInfinity(this double argumentValue, string argumentName)
        {
            if (double.IsInfinity(argumentValue))
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, ValueCannotBeNegative);
        }

        public static void AssertEnumMember<TEnum>(this TEnum argumentValue, string argumentName)
            where TEnum : struct, IConvertible
        {
            bool throwEx = false;

            if (Attribute.IsDefined(typeof(TEnum), typeof(FlagsAttribute), false))
            {
                var longValue = argumentValue.ToInt64(CultureInfo.InvariantCulture);

                if (longValue == 0)
                {
                    throwEx = !Enum.IsDefined(typeof(TEnum), default(TEnum));
                }
                else
                {
                    foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
                    {
                        longValue &= ~value.ToInt64(CultureInfo.InvariantCulture);
                    }

                    throwEx = longValue != 0;
                }
            }
            else
            {
                throwEx = !Enum.IsDefined(typeof(TEnum), argumentValue);
            }

            if (throwEx)
            {
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, InvalidEnumValue);
            }
        }

        private const string ValueCannotBeNull = "Cannot be null.";
        private const string ValueCannotBeEmpty = "Cannot be empty.";
        private const string ValueCannotBeZero = "Cannot be zero.";
        private const string ValueCannotBeNegative = "Cannot be negative.";
        private const string ValueMustBeGreaterThanZero = "Value must be greater than zero.";
        private const string ValueOutOfRange = "Value out of range.";
        private const string InvalidEnumValue = "Invalid enum value.";
        private const string InvalidValueType = "Invalid value type.";
    }
}