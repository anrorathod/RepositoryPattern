using System;
using System.Collections.Generic;
using System.Text;
using Demo.Core.Tables;


namespace Demo.Data.Contracts
{
    public interface ICitiesRepository : IRepository<City>
    {
        Task<bool> CommitAsync();
    }
}

