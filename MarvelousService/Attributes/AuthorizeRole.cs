using MarvelousService.BusinessLayer.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace CRM.APILayer.Attribites
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRole : AuthorizeAttribute
    {
        public AuthorizeRole(params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new TypeMismatchException("The passed argument is not an enum.");

            Roles = string.Join(",", roles.Select(r => Enum.GetName(r.GetType(), r)));
        }
    }
}
