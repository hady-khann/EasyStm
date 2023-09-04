﻿namespace EasyStm.Models;

public class ReportInfo
{
    public string ReportFileName { get; set; }
    public string PdfFileName { get; set; }
    public string FontName { get; set; }
    public string FontExtension { get; set; } = "ttf";
}

public class ReportObject<T>
{
    public T ObjectData { get; set; }
    public string BusinessObjectName { get; set; }
}

public class ReportResult
{
    public string PdfFileName { get; set; }
    public byte[] ReportFile { get; set; }
}