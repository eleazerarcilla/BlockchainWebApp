using BlockChain.Framework.Entities;
using BlockChain.Presentation.MVC.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChain.Presentation.MVC.Models.Interfaces.Repositories
{
    public class CommonItemsRepository : ICommonItems
    {
        public async Task<bool> Save(AppDBContext appDBContext, List<CommonItems> commonItems)
        {
            bool result = true;
            using (var context = await appDBContext.Database.BeginTransactionAsync())
            {
                try
                {
                    appDBContext.Database.ExecuteSqlRaw("TRUNCATE TABLE [tblCommonItems]");
                    appDBContext.CommonItems.AddRange(commonItems);

                }catch(Exception ex)
                {
                    await context.RollbackAsync();
                    result = false;
                }
                await appDBContext.SaveChangesAsync();
                await context.CommitAsync();
                
            }
            return await Task.FromResult(result);
        }

        public async Task<List<CommonItems>> GetAll(AppDBContext appDBContext)
        {
            return await Task.FromResult(appDBContext.CommonItems.ToList());
        }
    }
}
