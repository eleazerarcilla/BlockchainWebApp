using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChain.Presentation.MVC.Controllers
{
    public class ExportController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            MemoryStream stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage pck = new ExcelPackage(stream))
            {
                List<object[]> test = new List<object[]>() {
                new object[]{"eleaz","pogi" },
                   new object[]{"mead","ganda" },
                };
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("MainView");
                ws.Cells["A1"].LoadFromArrays(test);
                var bytes = pck.GetAsByteArray();
                return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportFile.xlsx");
            }

        }
    }
}
