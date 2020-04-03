using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Controls
{
    public class EmbeddedKeyboardLayout
    {
        private readonly CultureInfo _cultureInfo;
        private readonly Dictionary<Key, EmbeddedKeyboardSymbol> _symbols;

        public EmbeddedKeyboardLayout(string cultureName)
        {
            cultureName.AssertNotNull(nameof(cultureName));

            _cultureInfo = cultureName == "Special" ? null : CultureInfo.GetCultureInfo(cultureName);
            _symbols = new Dictionary<Key, EmbeddedKeyboardSymbol>();
        }

        protected void Set(Key key, string value, string alternateValue = null)
        {
            _symbols[key] = new EmbeddedKeyboardSymbol(key, value, alternateValue);
        }

        public EmbeddedKeyboardSymbol GetSymbol(Key key)
        {
            return _symbols[key];
        }

        public bool TryGetSymbol(Key key, out EmbeddedKeyboardSymbol symbol)
        {
            return _symbols.TryGetValue(key, out symbol);
        }

        public CultureInfo CultureInfo => _cultureInfo;

        public string Name => _cultureInfo.Name ?? "Special";

        public bool ShiftSupported { get; protected set; }

        public IReadOnlyCollection<EmbeddedKeyboardSymbol> Symbols => _symbols.Values;

        public static EmbeddedKeyboardLayout English { get; } = new EnglishLayout();

        public static EmbeddedKeyboardLayout Russian { get; } = new RussianLayout();

        public static EmbeddedKeyboardLayout Special { get; } = new SpecialLayout();

        private sealed class EnglishLayout : EmbeddedKeyboardLayout
        {
            public EnglishLayout() : base("en-US")
            {
                ShiftSupported = true;

                Set(Key.Q, "q", "Q");
                Set(Key.W, "w", "W");
                Set(Key.E, "e", "E");
                Set(Key.R, "r", "R");
                Set(Key.T, "t", "T");
                Set(Key.Y, "y", "Y");
                Set(Key.U, "u", "U");
                Set(Key.I, "i", "I");
                Set(Key.O, "o", "O");
                Set(Key.P, "p", "P");
                Set(Key.OemOpenBrackets, "[", "{");
                Set(Key.Oem6, "]", "}");

                Set(Key.A, "a", "A");
                Set(Key.S, "s", "S");
                Set(Key.D, "d", "D");
                Set(Key.F, "f", "F");
                Set(Key.G, "g", "G");
                Set(Key.H, "h", "H");
                Set(Key.J, "j", "J");
                Set(Key.K, "k", "K");
                Set(Key.L, "l", "L");
                Set(Key.Oem1, ";", ":");
                Set(Key.OemQuotes, "'", "\"");

                Set(Key.Z, "z", "Z");
                Set(Key.X, "x", "X");
                Set(Key.C, "c", "C");
                Set(Key.V, "v", "V");
                Set(Key.B, "b", "B");
                Set(Key.N, "n", "N");
                Set(Key.M, "m", "M");
                Set(Key.OemComma, ",", "<");
                Set(Key.OemPeriod, ".", ">");
                Set(Key.OemQuestion, "/", "?");
            }
        }

        private sealed class RussianLayout : EmbeddedKeyboardLayout
        {
            public RussianLayout() : base("ru-RU")
            {
                ShiftSupported = true;

                Set(Key.Q, "й", "Й");
                Set(Key.W, "ц", "Ц");
                Set(Key.E, "у", "У");
                Set(Key.R, "к", "К");
                Set(Key.T, "е", "Е");
                Set(Key.Y, "н", "Н");
                Set(Key.U, "г", "Г");
                Set(Key.I, "ш", "Ш");
                Set(Key.O, "щ", "Щ");
                Set(Key.P, "з", "З");
                Set(Key.OemOpenBrackets, "х", "Х");
                Set(Key.Oem6, "ъ", "Ъ");

                Set(Key.A, "ф", "Ф");
                Set(Key.S, "ы", "Ы");
                Set(Key.D, "в", "В");
                Set(Key.F, "а", "А");
                Set(Key.G, "п", "П");
                Set(Key.H, "р", "Р");
                Set(Key.J, "о", "О");
                Set(Key.K, "л", "Л");
                Set(Key.L, "д", "Д");
                Set(Key.Oem1, "ж", "Ж");
                Set(Key.OemQuotes, "э", "Э");

                Set(Key.Z, "я", "Я");
                Set(Key.X, "ч", "Ч");
                Set(Key.C, "с", "С");
                Set(Key.V, "м", "М");
                Set(Key.B, "и", "И");
                Set(Key.N, "т", "Т");
                Set(Key.M, "ь", "Ь");
                Set(Key.OemComma, "б", "Б");
                Set(Key.OemPeriod, "ю", "Ю");
                Set(Key.OemQuestion, ".", ",");
            }
        }

        private sealed class SpecialLayout : EmbeddedKeyboardLayout
        {
            public SpecialLayout() : base("Special")
            {
                ShiftSupported = false;

                Set(Key.Q, "1");
                Set(Key.W, "2");
                Set(Key.E, "3");
                Set(Key.R, "4");
                Set(Key.T, "5");
                Set(Key.Y, "6");
                Set(Key.U, "7");
                Set(Key.I, "8");
                Set(Key.O, "9");
                Set(Key.P, "0");
                Set(Key.OemOpenBrackets, "(");
                Set(Key.Oem6, ")");

                Set(Key.A, "@");
                Set(Key.S, "#");
                Set(Key.D, "$");
                Set(Key.F, "&");
                Set(Key.G, "_");
                Set(Key.H, "-");
                Set(Key.J, "+");
                Set(Key.K, "*");
                Set(Key.L, "/");
                Set(Key.Oem1, "%");
                Set(Key.OemQuotes, "^");

                Set(Key.Z, "\"");
                Set(Key.X, "'");
                Set(Key.C, ":");
                Set(Key.V, ";");
                Set(Key.B, "~");
                Set(Key.N, "\"");
                Set(Key.M, "!");
                Set(Key.OemComma, ",");
                Set(Key.OemPeriod, ".");
                Set(Key.OemQuestion, "?");
            }
        }
    }
}