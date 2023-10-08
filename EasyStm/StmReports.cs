using EasyStm.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Stimulsoft.Report.Export;

namespace EasyStm.Reports.Pdf;

public class StmReports : IStmReports
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    private readonly IConfiguration _configuration;

    public StmReports(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
    {
        _webHostEnvironment = webHostEnvironment;
        _configuration = configuration;
        StiLicense.LoadFromString(_configuration["Stimulsoft:License"]);
    }

    public async Task<ReportResult> PrintPDFAsync<T>(ReportObject<T> Object, ReportInfo info)
    {
        StiReport stiReport = new StiReport();
        string projectRootPath = $"{_webHostEnvironment.WebRootPath}/reports/{info.ReportFileName}.mrt";

        stiReport.Load(projectRootPath);
        stiReport.RegBusinessObject(Object.BusinessObjectName, Object.ObjectData);
        await stiReport.RenderAsync();

        MemoryStream memoryStream = new MemoryStream();

        info.Fonts.ForEach(x =>
        {
            StiFontCollection.AddFontFile($"{_webHostEnvironment.WebRootPath}/fonts/{x.FontName}.{x.FontExtension}");
        });

        stiReport.LoadFonts();

        StiOptions.Export.Pdf.AllowInvokeWindowsLibraries = true;
        stiReport.ExportDocument(StiExportFormat.Pdf, memoryStream, new StiPdfExportSettings()
        {
            Compressed = true,
            AllowEditable = StiPdfAllowEditable.No,
            ImageQuality = 1,
            EmbeddedFonts = true,
            StandardPdfFonts = true,
            ImageCompressionMethod = StiPdfImageCompressionMethod.Jpeg
        });

        byte[] bytesInStream = memoryStream.ToArray();
        await memoryStream.WriteAsync(bytesInStream, 0, bytesInStream.Length);
        memoryStream.Position = 0;

        return new ReportResult
        {
            ReportFile = memoryStream.GetBuffer(),
            PdfFileName = info.PdfFileName
        };
    }

    public async Task<ReportResult> PrintPDFAsync<T>(List<ReportObject<T>> Object, ReportInfo info)
    {
        StiReport stiReport = new StiReport();
        string projectRootPath = $"{_webHostEnvironment.WebRootPath}/reports/{info.ReportFileName}.mrt";

        stiReport.Load(projectRootPath);
        Object.ForEach(x =>
        {
            stiReport.RegBusinessObject(x.BusinessObjectName, x.ObjectData);
        });

        info.Fonts.ForEach(x =>
        {
            string fontPath = $"{_webHostEnvironment.WebRootPath}/fonts/{x.FontName}.{x.FontExtension}";

            if (File.Exists(fontPath))
                StiFontCollection.AddFontFile(fontPath);
        });

        stiReport.LoadFonts();

        await stiReport.RenderAsync();

        MemoryStream memoryStream = new MemoryStream();

        StiOptions.Export.Pdf.AllowInvokeWindowsLibraries = true;
        stiReport.ExportDocument(StiExportFormat.Pdf, memoryStream, new StiPdfExportSettings()
        {
            Compressed = true,
            AllowEditable = StiPdfAllowEditable.No,
            ImageQuality = 1,
            EmbeddedFonts = true,
            StandardPdfFonts = false,
            ImageCompressionMethod = StiPdfImageCompressionMethod.Jpeg
        });

        byte[] bytesInStream = memoryStream.ToArray();
        await memoryStream.WriteAsync(bytesInStream, 0, bytesInStream.Length);
        memoryStream.Position = 0;

        return new ReportResult
        {
            ReportFile = memoryStream.GetBuffer(),
            PdfFileName = info.PdfFileName
        };
    }
}