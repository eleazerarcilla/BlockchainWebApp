using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlockChain.Framework.Entities;
using BlockChain.Presentation.MVC.Contexts;
using BlockChain.Presentation.MVC.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using static BlockChain.Framework.Common.GlobalHelpers;

namespace BlockChain.Presentation.MVC.Controllers
{
    public class ProductListUploadController : Controller
    {
        private readonly AppDBContext _appDBContext;
        private readonly IProductList _productRepo;
        public IActionResult Index()
        {
            return View();
        }

        public ProductListUploadController(AppDBContext appDBContext, IProductList productRepo)
        {
            this._appDBContext = appDBContext;
            this._productRepo = productRepo;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            if (uploadedFile.IsFileValid())
                return Content("File Not Selected");

            string fileExtension = Path.GetExtension(uploadedFile.FileName);

            if (fileExtension == ".xls" || fileExtension == ".xlsx")
            {
                var rootFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files";
                var fileLocation = await uploadedFile.GetFileLocationAndSaveFile(rootFolder);

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(fileLocation))
                {
                    
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                    int totalRows = workSheet.Dimension.Rows;
                    List<ProductList> productList = new List<ProductList>();

                    for (int i = 2; i <= totalRows; i++)
                    {
                        productList.Add(new ProductList
                        {
                            TX_JAN = workSheet.Cells[i, 1].Value != null ? workSheet.Cells[i, 1].Value.ToString() : string.Empty,
                            TX_SKU = workSheet.Cells[i, 2].Value != null ? workSheet.Cells[i, 2].Value.ToString() : string.Empty,
                            TX_CATEGORY = workSheet.Cells[i, 3].Value != null ? workSheet.Cells[i, 3].Value.ToString(): string.Empty,
                            TX_SUBCATEGORY = workSheet.Cells[i, 4].Value != null ? workSheet.Cells[i, 4].Value.ToString() : string.Empty,
                            TX_INGREDIENTS = workSheet.Cells[i, 5].Value != null ? workSheet.Cells[i, 5].Value.ToString() : string.Empty
                        });
                    }
                    if (await this._productRepo.Save(this._appDBContext, productList))
                        return Content("Successfully Uploaded");
                }
            }

            return BadRequest();
        }
    }
}