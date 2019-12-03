using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.Models.Reports
{
    public class UnprofitableMoviesReportForm : BaseReportForm
    {
        public float Threshold { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            var parameters = base.GetParameters();
            parameters.Add(UnprofitableMoviesReportFormConstants.Threshold, Threshold);
            return parameters;
        }
    }

    public static class UnprofitableMoviesReportFormConstants
    {
        public static string Threshold => "Threshold";
    }
}