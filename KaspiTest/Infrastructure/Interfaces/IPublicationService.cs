using System.Collections.Generic;
using System.Threading.Tasks;
using KaspiTest.Model.ViewModels;

namespace KaspiTest.Infrastructure.Interfaces
{
    interface IPublicationService
    {
        Task AddPublication(PublicationViewModel publication);

        Task AddPublicationsRange(IEnumerable<PublicationViewModel> publications);
    }
}
