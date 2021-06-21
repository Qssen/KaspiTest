using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KaspiTest.DataAccess;
using KaspiTest.Infrastructure.Interfaces;
using KaspiTest.Model.Domain;
using KaspiTest.Model.ViewModels;

namespace KaspiTest.Infrastructure.Impl
{
    class PublicationService : IPublicationService
    {
        public async Task AddPublication(PublicationViewModel publicationVm)
        {
            await using var context = new PublicationContext();
            
            var publication = PublicationVmToPublication(publicationVm);
            await context.Publications.AddAsync(publication);
            
            await context.SaveChangesAsync();
        }

        public async Task AddPublicationsRange(IEnumerable<PublicationViewModel> publications)
        {
            await using var context = new PublicationContext();
            
            foreach (var publicationVm in publications)
            {
                var publication = PublicationVmToPublication(publicationVm);
                await context.Publications.AddAsync(publication);
            }
            await context.SaveChangesAsync();
        }

        //В prod-е можно использовать autoMapper для таких целей
        private Publication PublicationVmToPublication(PublicationViewModel publicationViewModel)
        {
            var publication = new Publication()
            {
                Content = publicationViewModel.Content,
                CreationDate = DateTime.Now,
                Link = publicationViewModel.Link,
                PublicDate = publicationViewModel.PublicDate,
                Title = publicationViewModel.Title,
                Words = publicationViewModel.Words.Select(x => new Word() {Name = x.Name}).ToList()
            };
            return publication;
        }
    }
}
