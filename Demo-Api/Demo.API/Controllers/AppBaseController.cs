using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.API.Controllers
{
    public abstract class AppBaseController : Controller
    {
        public AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }
    }
}
