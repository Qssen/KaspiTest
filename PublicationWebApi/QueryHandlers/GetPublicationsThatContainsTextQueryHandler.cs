using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using PublicationWebApi.Model;
using PublicationWebApi.Queries;

namespace PublicationWebApi.QueryHandlers
{
    public class GetPublicationsThatContainsTextQueryHandler : IRequestHandler<GetPublicationsThatContainsTextQuery, IEnumerable<PublicationViewModel>>
    {
        private const string SqlQuery = "SELECT * FROM Publications WHERE Content like @text";

        private readonly IDbConnection _dbConnection;

        public GetPublicationsThatContainsTextQueryHandler()
        {
            _dbConnection =
                new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=publicationsDb;Trusted_Connection=True;");

            _dbConnection.Open();
        }

        public async Task<IEnumerable<PublicationViewModel>> Handle(GetPublicationsThatContainsTextQuery request, CancellationToken cancellationToken)
        {
            var parameters = new {text = $"%{request.Text}%"};
            return await _dbConnection.QueryAsync<PublicationViewModel>(SqlQuery, parameters);
        }
    }
}
