namespace NumberToChars
{
    public static class ToChars
    {
        /// <summary> Converts `int` to `chars` without allocation by repeated division by 10 to get the digit. </summary>
        /// <param name="buffer">The chars will be written to the END of this buffer. Size must fit the intended integer or it will throw</param>
        /// <returns>The slice of buffer where the integer characters were written to</returns>
        public static Span<char> Int(int value, Span<char> buffer)
        {
            // Writes to the span backwards
            int bufferIndex = buffer.Length - 1;
            // Handle negative values
            bool negative = false;
            if (value < 0)
            {
                negative = true;
                value = -value;
            }

            do
            {
                //ensures compiler combines divide and modulo
                value = Math.DivRem(value, 10, out int remainder);
                buffer[bufferIndex--] = (char)(remainder + '0');
            }
            while (value > 0 && bufferIndex >= 0);

            if (value > 0) //buffer was filled before all chars were pulled
                throw new ArgumentException($"Span<Char>[{buffer.Length}] is too small to hold the integer portion of ({value})");

            if (negative)
            {
                if (bufferIndex < 0) //cannot fit the negative sign
                    throw new ArgumentException($"Span<Char>[{buffer.Length}] is too small to hold the integer portion of ({value})");

                buffer[bufferIndex--] = '-';
            }

            return buffer.Slice(bufferIndex + 1);
        }

        /// <summary> <inheritdoc cref="ToCharsInternal"/> </summary>
        public static Span<char> Float(float value, Span<char> buffer, int digitPrecision = 7) => ToCharsInternal(value, buffer, digitPrecision); //floats are precise to 7 digits

        /// <summary> <inheritdoc cref="ToCharsInternal"/> </summary>
        public static Span<char> Double(double value, Span<char> buffer, int digitPrecision = 15) => ToCharsInternal(value, buffer, digitPrecision); //doubles are precise to 15 digits

        /// <summary> Converts to `chars` without allocation by repeatedly dividing the int part; and multiplying the frac part by 10; to get their digits. </summary>
        /// <param name="buffer">The chars will be written to the END of this buffer. Size must fit the intended integer or it will throw</param>
        /// <returns>The slice of buffer where the characters were written to</returns>
        private static Span<char> ToCharsInternal(double value, Span<char> buffer, int digitsRemaining)
        {
            int bufferIndex = 0;
            int bufferLength = buffer.Length;

            // Handle negative values
            bool negative = false;
            if (value < 0)
            {
                buffer[bufferIndex++] = '-';
                negative = true;
                value = -value;
            }

            long intPart = (long)value;
            double fracPart = value - intPart;
            bool leading_0 = intPart == 0;

            //if the exponent of the floating point is negative, then the integer part must be 0. Leading 0s do not affect the precision
            if (!leading_0)
            {
                do
                {
                    intPart = Math.DivRem(intPart, 10, out long remainder);
                    buffer[bufferIndex++] = (char)(remainder + '0');
                    digitsRemaining--;
                }
                while (intPart > 0 && bufferIndex < bufferLength);

                if (intPart > 0) //buffer was filled before all chars were pulled
                    throw new ArgumentException($"Span<Char>[{buffer.Length}] is too small to hold the integer portion of ({value})");

                int intStart = negative ? 1 : 0;
                int intCount = bufferIndex - intStart;
                buffer.Slice(intStart, intCount).Reverse();
            }
            else
            {
                buffer[bufferIndex++] = '0';
            }


            // if there is a fractional part && theres space left in the buffer for '.' and at least 1 digit && digits are precise
            if (fracPart > 0 && bufferIndex < (buffer.Length - 1) && digitsRemaining > 0)
            {
                buffer[bufferIndex++] = '.';
                do
                {
                    fracPart *= 10.0d;
                    int digit = (int)fracPart;
                    buffer[bufferIndex++] = (char)('0' + digit);
                    fracPart -= digit;

                    if (leading_0)
                    {
                        if (digit != 0)
                        {
                            leading_0 = false;
                            digitsRemaining--;
                        }
                        continue;
                    }

                    digitsRemaining--;
                }
                while (bufferIndex < bufferLength && digitsRemaining > 0);
            }

            return buffer.Slice(0, bufferIndex);
        }
    }

    public static class ToCharsExtensions
    {
        /// <summary> <inheritdoc cref="ToChars.Int"/> </summary>
        public static Span<char> IntToChars(this int value, Span<char> buffer) => ToChars.Int(value, buffer);

        /// <summary> <inheritdoc cref="ToChars.Float"/> </summary>
        public static Span<char> FloatToChars(this float value, Span<char> buffer, int digitPrecision = 7) => ToChars.Float(value, buffer, digitPrecision); //floats are precise to 7 digits

        /// <summary> <inheritdoc cref="ToChars.Double"/> </summary>
        public static Span<char> DoubleToChars(this double value, Span<char> buffer, int digitPrecision = 15) => ToChars.Double(value, buffer, digitPrecision); //doubles are precise to 15 digits
    }
}