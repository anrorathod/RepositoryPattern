using System.Security.Claims;

namespace Demo.API
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
        : base(principal)
        {
        }

        public string Name
        {
            get
            {
                return this.FindFirst(ClaimTypes.Name).Value;
            }
        }

        public string Country
        {
            get
            {
                return this.FindFirst(ClaimTypes.Country).Value;
            }
        }
        public int UserID
        {
            get
            {
                
                return Convert.ToInt32(this.FindFirst("userid").Value);
                //return this.FindFirst(ClaimTypes.NameIdentifier).Value;
                //return this.FindFirst(a => a.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            }
        }
        public string CreatedDate
        {
            get
            {
                return this.FindFirst(ClaimTypes.DateOfBirth).Value;
            }
        }
    }
}