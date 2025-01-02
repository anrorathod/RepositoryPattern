using System;
using System.Collections.Generic;
using System.Text;
using Demo.Core.Tables;


namespace Demo.Data.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> CommitAsync();
    }
}

