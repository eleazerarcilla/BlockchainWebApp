using BlockChain.Framework.Entities;
using BlockChain.Presentation.MVC.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlockChain.Presentation.MVC.Models.Interfaces.Repositories
{
    public class ProductListRepository : IProductList
    {
        public async Task<bool> Save(AppDBContext appDBContext, List<ProductList> productList)
        {
            bool result = true;
            using(var context = await appDBContext.Database.BeginTransactionAsync())
            {
                try
                {   
                    await appDBContext.ProductList.AddRangeAsync(productList);
                }
                catch(Exception ex)
                {
                    appDBContext.Database.RollbackTransaction();
                    result = false;
                }

                await appDBContext.SaveChangesAsync();

                context.Commit();

            }
            return await Task.FromResult(result);
           
        }
    }
}
