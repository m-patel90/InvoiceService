using Invoice.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Applicaion.Interfaces
{
    public interface ITokenService
    {
        public string BuildToken(string key,string issuerer,UserModel user);
        public bool IsValidToken(string key,string issuerer,string audience,string token);
    }
}
