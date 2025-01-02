using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Core.Tables;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.Service.Contracts
{
    public interface ICommonService : IService<Setting>
    {
        Task<bool> SentEmail(string from, string Recipients, string RecipientsCC, string RecipientsBCC, string mailbody, string Subject, string ReplyTo = "");
    }
}
