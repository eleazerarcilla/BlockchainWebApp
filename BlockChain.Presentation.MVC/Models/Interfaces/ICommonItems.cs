using BlockChain.Framework.Entities;
using BlockChain.Presentation.MVC.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChain.Presentation.MVC.Models.Interfaces
{
    public interface ICommonItems
    {
        Task<bool> Save(AppDBContext appDBContext, List<CommonItems> commonItems);
        Task<List<CommonItems>> GetAll(AppDBContext appDBContext);
    }
}
