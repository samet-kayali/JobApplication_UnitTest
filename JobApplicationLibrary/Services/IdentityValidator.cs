using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary.Services
{
    public class IdentityValidator : IIdentityValidator
    {
        //Kimlik doğrulaması için oluşturuldu.
        public bool IsValid(string identityNumber)
        {
            return identityNumber.Length == 11;
        }
    }
}
