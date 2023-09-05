using EasyStm.Models;

namespace EasyStm.Reports.Pdf
{
    public interface IStmReports
    {
        Task<ReportResult> PrintPDFAsync<T>(List<ReportObject<T>> Object, ReportInfo info);
        Task<ReportResult> PrintPDFAsync<T>(ReportObject<T> Object, ReportInfo info);
    }
}