using System;
using System.Linq;
using KaspiTest.Infrastructure.Interfaces;

namespace KaspiTest.Infrastructure.Impl
{
    class WordProcessor : IWordProcessor
    {
        private static readonly char[] NotAllowedChars = new[] {',', ' ', '-', '.', '\"'};

        public string[] SplitTextToWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(nameof(text));

            return text.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim(NotAllowedChars).ToUpper())
                .ToArray();
        }
    }
}
