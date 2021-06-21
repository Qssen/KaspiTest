using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KaspiTest.Model.ViewModels;

namespace KaspiTest.Infrastructure.Interfaces
{
    interface IPageCrawler : IDisposable
    {
        IAsyncEnumerable<string> GetAllLinksFromPage(string pageUrl);

        Task<PublicationViewModel> GetPublication(string pageUrl);
    }
}