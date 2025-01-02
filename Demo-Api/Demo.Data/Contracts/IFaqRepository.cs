using System;
using System.Collections.Generic;
using System.Text;
using Demo.Core.Tables;


namespace Demo.Data.Contracts
{
    public interface IFaqRepository : IRepository<Faq>
    {
        Task<bool> CommitAsync();
    }
}

