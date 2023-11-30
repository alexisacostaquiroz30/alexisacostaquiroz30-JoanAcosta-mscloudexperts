using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaJoanAcosta.Data
{
    public class LoginDto
    {
        public string CorreoUsuario { get; set; }
        public string ContrasenaUsuario { get; set; }
    }

}
