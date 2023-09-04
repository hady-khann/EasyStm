EasyStm Tool Is Made to Use StimulSoft In Easier Way And Clean Code

First Of All Add Below Config In AppSetting :

  "Stimulsoft": {
    "License": "Your License"
  }


Usage Example: 

            var reportBusinessObjects = new List<ReportRequest<object>>() {
                new ReportRequest<object>{ BusinessObjectName = "PersonDetails" , Data = getDataService.PersonDetails},
                new ReportRequest<object>{ BusinessObjectName = "JobDetails" , Data = getDataService.JobDetails},
                new ReportRequest<object>{ BusinessObjectName = "CompanyDetails" , Data = getDataService.CompanyDetails}
            };

            var info = new ReportInfo()
            {
                FontName = Your FontName ,
                FontExtension = "ttf", // Defualt Is "ttf"
                ReportName = Stimul Report File Name .mrt ,
                FileName = pdf Result FileName ,
            };



            await _report.PrintPDFAsync(reportBusinessObjects, info);
