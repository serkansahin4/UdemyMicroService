using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.Shared.Services
{
    public interface ISharedIdentityService
    {
        public string UserId { get; }
    }
}
