using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using PublicationWebApi.Model;
using PublicationWebApi.Queries;

namespace PublicationWebApi.QueryHandlers
{
    public class GetPublicationByTimeRangeQueryHandler : IRequestHandler<GetPublicationsByTimeRangeQuery, IEnumerable<PublicationViewModel>>
    {
        private readonly IDbConnection _dbConnection;

        public GetPublicationByTimeRangeQueryHandler()
        {
            _dbConnection =
                new SqlConnection(@"Server=(localdb)\mssqllocaldb;Database=publicationsDb;Trusted_Connection=True;");

            _dbConnection.Open();
        }

        private const string SqlQuery = "SELECT * FROM Publications WHERE PublicDate BETWEEN @dateFrom AND @dateTo";

        public async Task<IEnumerable<PublicationViewModel>> Handle(GetPublicationsByTimeRangeQuery request, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                dateFrom = request.DateFrom.GetValueOrDefault(SqlDateTime.MinValue.Value), 
                dateTo = request.DateTo.GetValueOrDefault(DateTime.MaxValue)
            };

            var publications = await _dbConnection.QueryAsync<PublicationViewModel>(SqlQuery, parameters);

            return publications;
        }
    }
}
