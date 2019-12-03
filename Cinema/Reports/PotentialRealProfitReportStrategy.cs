using System;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AutoMapper;
using NPOI.SS.UserModel;

namespace Cinema.Reports
{
    public class PotentialRealProfitReportStrategy : ReportBaseStrategy<PotentialRealProfitReportModel>
    {
        public PotentialRealProfitReportStrategy(IMapper mapper) : base(mapper)
        {
        }

        protected override string InternalGetTemplateFileName()
        {
            return "PotentialRealProfitReport.xlsx";
        }

        protected override string InternalGetDownloadFileName()
        {
            return "PotentialRealProfitReport";
        }

        protected override PotentialRealProfitReportModel GetDataModel()
        {
            var parameters = new[]
            {
                new SqlParameter("@DateFrom", (DateTime)Parameters["DateFrom"]),
                new SqlParameter("@DateTo", (DateTime)Parameters["DateTo"])
            };

            var reportRows = DatabaseUtil.Execute<PotentialRealProfitReportRow>("PotentialRealProfit", parameters);

            return new PotentialRealProfitReportModel
            {
                Rows = reportRows
            };
        }

        protected override void ProcessWorkBook(IWorkbook workbook, PotentialRealProfitReportModel model)
        {
            var sheet = workbook.GetSheetAt(0);
            var rowIndex = 1;
            foreach (var row in model.Rows)
            {
                var documentRow = sheet.CreateRow(rowIndex);
                documentRow.CreateCell(SummaryColumns.MovieName).SetCellValue(row.Name);
                documentRow.CreateCell(SummaryColumns.GuaranteedProfit).SetCellValue(row.GuaranteedProfit);
                documentRow.CreateCell(SummaryColumns.PotentialProfit).SetCellValue(row.PotentialProfit);
                rowIndex++;
            }
            sheet.AutoSizeColumn(SummaryColumns.MovieName);
            sheet.AutoSizeColumn(SummaryColumns.GuaranteedProfit);
            sheet.AutoSizeColumn(SummaryColumns.PotentialProfit);
        }

        private static class SummaryColumns
        {
            public const int MovieName = 0;
            public const int GuaranteedProfit = 1;
            public const int PotentialProfit = 2;
        }
    }
}