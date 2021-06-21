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
    public class GetMostUsedWordsQueryHandler : IRequestHandler<GetMostUsedWordsQuery, IEnumerable<GetMostUsedWordsQueryResponseModel>>
    {
        private const string SqlQuery =
            "SELECT TOP (@top) COUNT(NAME) as WordCount, NAME FROM Words GROUP BY Name HAVING LEN(Name) > 3 ORDER BY WordCount DESC";

        private readonly IDbConnection _dbConnection;

        public GetMostUsedWordsQueryHandler()
        {
            _dbConnection =
                new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=publicationsDb;Trusted_Connection=True;");

            _dbConnection.Open();
        }


        public async Task<IEnumerable<GetMostUsedWordsQueryResponseModel>> Handle(GetMostUsedWordsQuery request, CancellationToken cancellationToken)
        {
            var parameters = new {top = request.Top};
            var result = await _dbConnection.QueryAsync<GetMostUsedWordsQueryResponseModel>(SqlQuery, parameters);
            return result;
        }
    }
}
