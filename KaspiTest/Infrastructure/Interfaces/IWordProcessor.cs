using System.Collections;

namespace KaspiTest.Infrastructure.Interfaces
{
    interface IWordProcessor
    {
        string[] SplitTextToWords(string text);
    }
}
