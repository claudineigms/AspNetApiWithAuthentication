using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithAuthentication.Models
{
    public class UserLogin
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Passworld { get; set; }
        public bool RememberLogin { get; set; }
    }
}