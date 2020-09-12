using BlockChain.Framework.Entities;
using BlockChain.Presentation.MVC.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockChain.Presentation.MVC.Models.Interfaces
{
    public interface IPermittedAdditives
    {
        Task<IEnumerable<PermittedAdditive>> GetAll(AppDBContext appDBContext);
    }
}
