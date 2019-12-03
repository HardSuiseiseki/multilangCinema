using System.Collections.Generic;

namespace Cinema.Reports
{
    public class UnprofitableMoviesReportModel
    {
        public IEnumerable<UnprofitableMoviesReportRow> Rows { get; set; }
    }
}