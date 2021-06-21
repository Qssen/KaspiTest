using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using KaspiTest.Exceptions;
using KaspiTest.Infrastructure.Interfaces;
using KaspiTest.Model.ViewModels;

namespace KaspiTest.Infrastructure.Impl
{
    class TengriPageCrawler : IPageCrawler, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly IWordProcessor _wordProcessor;

        //TODO: DI
        public TengriPageCrawler(IWordProcessor wordProcessor)
        {
            _httpClient = new HttpClient();
            _wordProcessor = wordProcessor;
        }

        public async IAsyncEnumerable<string> GetAllLinksFromPage(string pageUrl)
        {
            var doc = await DownloadPage(pageUrl);
         
            var newsBlock = doc.DocumentNode.SelectNodes("//div[contains(@class, 'tn-all_news')]");

            var allLinks = newsBlock.Descendants("a");

            foreach (var link in allLinks)
            {
                var href = link.GetAttributeValue("href", string.Empty);

                //TengriNews возвращает относительный путь для внутренних новостей
                //И абсолютный путь для TengriMIX - его игнорирую
                if (Uri.IsWellFormedUriString(href, UriKind.Absolute))
                    continue;

                yield return $"{pageUrl}{href}";
            }
        }

        public async Task<PublicationViewModel> GetPublication(string pageUrl)
        {
            var doc = await DownloadPage(pageUrl);

            try
            {
                var titleAndDateStrs =
                    doc.DocumentNode
                        .SelectNodes("//h1[contains(@class, 'tn-content-title')]")
                        .FirstOrDefault()
                        ?.InnerText.Split('\n');

                var result = new PublicationViewModel();
                result.Title = titleAndDateStrs[0];

                var dtStr = titleAndDateStrs[1].Trim();

                if (DateTime.TryParse(dtStr, out var dt))
                    result.PublicDate = dt;

                var contentBlockNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'tn-news-content')]");
                var sb = new StringBuilder();

                foreach (var node in contentBlockNodes)
                    sb.Append(node.InnerText.Trim());
                
                result.Content = sb.ToString();
                result.Link = pageUrl;

                var words = _wordProcessor.SplitTextToWords(result.Content);

                result.Words = words.Where(w=>!string.IsNullOrWhiteSpace(w))
                    .Select(x => new WordViewModel() {Name = x})
                    .ToList();
                
                return result;
            }
            catch
            {
                throw new PublicationParseException();
            }
        }

        //Тут идет нарушение SRP, но класс настолько небольшой что, ИМХО, - некритично
        private async Task<HtmlDocument> DownloadPage(string uri)
        {
            if (!Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                throw new ArgumentException(nameof(uri));

            var responseMessage = await _httpClient.GetAsync(uri);
            responseMessage.EnsureSuccessStatusCode();

            var responseString = await responseMessage.Content.ReadAsStringAsync();
            
            var doc = new HtmlDocument();
            doc.LoadHtml(responseString);

            return doc;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
