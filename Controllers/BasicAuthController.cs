using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyDHL_API_Implementation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDHL_API_Implementation.Controllers
{
    [Route("api/[controller")]
    [ApiController]
    public class BasicAuthController : Controller
    {
        private readonly BasicAuthModel _authOptions;

        public BasicAuthController(IOptions<BasicAuthModel> authOptions)
        {
            _authOptions = authOptions.Value;
        }
    }
}
