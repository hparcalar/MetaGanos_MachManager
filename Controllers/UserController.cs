using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MachManager.Context;
using MachManager.Models;
using MachManager.Models.Operational;
using MachManager.Controllers.Base;
using MachManager.i18n;
using Microsoft.AspNetCore.Cors;
using MachManager.Authentication;

namespace MachManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class UserController : MgControllerBase
    {
        public UserController(MetaGanosSchema context, MgAuth authObject) : base(context, authObject){ }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginDealer")]
        public IActionResult LoginDealer([FromBody] UserLoginModel model)
        {
            try
            {
                Dealer dbUser = null;

                // create default dealer
                if (!_context.Dealer.Any()){
                    dbUser = new Dealer{
                        DealerCode = "MetaGanos",
                        DealerName = "MetaGanos",
                        DealerPassword = "MgAdmin",
                        Explanation = "",
                        IsActive = true,
                    };
                    _context.Dealer.Add(dbUser);
                    _context.SaveChanges();
                }
                else
                {
                    dbUser = _context.Dealer.FirstOrDefault(d => d.DealerCode == model.Login);
                    if (dbUser == null)
                        throw new Exception(model.Login + ": " + _translator.Translate(Expressions.UserNotFound, _userLanguage));
                }

                if (!string.Equals(dbUser.DealerPassword, model.Password))
                    throw new Exception(_translator.Translate(Expressions.WrongPassword, _userLanguage));

                var tokenStr = _authObject.Authenticate(true, model.Login, MgAuthType.Dealer);

                return Ok(tokenStr);
            }
            catch (Exception)
            {
                
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginEmployee")]
        public IActionResult LoginEmployee([FromBody] UserLoginModel model)
        {
            try
            {
                var dbUser = _context.Employee.FirstOrDefault(d => d.EmployeeCode == model.Login);
                if (dbUser == null)
                    throw new Exception(model.Login + ": " + _translator.Translate(Expressions.UserNotFound, _userLanguage));

                if (!string.Equals(dbUser.EmployeePassword, model.Password))
                    throw new Exception(_translator.Translate(Expressions.WrongPassword, _userLanguage));

                var tokenStr = _authObject.Authenticate(true, model.Login, MgAuthType.Employee);

                return Ok(tokenStr);
            }
            catch (Exception)
            {
                
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginMachine")]
        public IActionResult LoginMachine([FromBody] UserLoginModel model)
        {
            try
            {
                var dbUser = _context.Machine.FirstOrDefault(d => d.MachineCode == model.Login);
                if (dbUser == null)
                    throw new Exception(model.Login + ": " + _translator.Translate(Expressions.UserNotFound, _userLanguage));

                var tokenStr = _authObject.Authenticate(true, model.Login, MgAuthType.Machine);

                return Ok(tokenStr);
            }
            catch (Exception)
            {
                
            }

            return Unauthorized();
        }
    }
}
