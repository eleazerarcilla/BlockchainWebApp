﻿using System;
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
        public async Task<IActionResult> UploadFile(IFormFile productListFile)
        {
            if (productListFile == null || productListFile.Length == 0)
                return Content("File Not Selected");

            string fileExtension = Path.GetExtension(productListFile.FileName);

            if (fileExtension == ".xls" || fileExtension == ".xlsx")
            {
                var rootFolder = $"{Directory.GetCurrentDirectory()}\\wwwroot\\ProductListFiles";
                var fileName = productListFile.FileName;
                var filePath = Path.Combine(rootFolder, fileName);
                var fileLocation = new FileInfo(filePath);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await productListFile.CopyToAsync(fileStream);
                }

                if (productListFile.Length <= 0)
                    return BadRequest("Empty File");

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage package = new ExcelPackage(fileLocation))
                {
                    
                    ExcelWorksheet workSheet = package.Workbook.Worksheets[0];
                    //var workSheet = package.Workbook.Worksheets.First();
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