using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AnotherContactBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    [Authorize(Roles = "Administrator")]
    public class AdminController : ControllerBase
    {
        
    }
}
