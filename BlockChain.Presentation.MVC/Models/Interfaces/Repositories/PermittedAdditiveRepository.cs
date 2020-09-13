using BlockChain.Framework.Entities;
using BlockChain.Presentation.MVC.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChain.Presentation.MVC.Models.Interfaces.Repositories
{
    public class PermittedAdditiveRepository : IPermittedAdditives
    {
        public async Task<List<PermittedAdditive>> GetAll(AppDBContext appDBContext)
        {
            return await Task.FromResult(appDBContext.PermittedAdditives.ToList());
        }
    }
}
