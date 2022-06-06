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
using MachManager.Helpers;
using MachManager.Models.Parameters;
using System.Text.Json;

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
        [Obsolete]
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

                var tokenStr = _authObject.Authenticate(true, model.Login, dbUser.Id, MgAuthType.Dealer);

                return Ok(tokenStr);
            }
            catch (Exception)
            {
                
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Obsolete]
        [Route("LoginOfficer")]
        public IActionResult LoginOfficer([FromBody] UserLoginModel model){
            try
            {
                Officer dbUser = null;

                if (!string.IsNullOrEmpty(model.DealerCode)){
                    var dbDealer = _context.Dealer.FirstOrDefault(d => d.DealerCode == model.DealerCode);
                    if (dbDealer != null)
                        throw new Exception(model.DealerCode + ": " + _translator.Translate(Expressions.DealerNotFound, _userLanguage));

                    var factoriesOfDealer = _context.Plant.Where(d => d.DealerId == dbDealer.Id)
                        .Select(d => d.Id).ToArray();

                    dbUser = _context.Officer.FirstOrDefault(d => d.OfficerCode == model.Login
                        && factoriesOfDealer.Contains(d.PlantId));
                }
                else
                    dbUser = _context.Officer.FirstOrDefault(d => d.OfficerCode == model.Login);

                if (dbUser == null)
                    throw new Exception(model.Login + ": " + _translator.Translate(Expressions.UserNotFound, _userLanguage));

                if (!string.Equals(dbUser.OfficerPassword, model.Password))
                    throw new Exception(_translator.Translate(Expressions.WrongPassword, _userLanguage));
                
                var tokenStr = _authObject.Authenticate(true, model.Login, dbUser.Id, MgAuthType.FactoryOfficer);

                return Ok(tokenStr);
            }
            catch (System.Exception)
            {
                
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginPanelUser")]
        public IActionResult LoginPanelUser([FromBody] UserLoginModel model){
            try
            {
                PanelLoginResult result = new PanelLoginResult();

                var dealerCount = _context.Dealer.Count();

                if (string.IsNullOrEmpty(model.DealerCode) && dealerCount > 1)
                    throw new Exception(_translator.Translate(Expressions.DealerNotFound, _userLanguage));

                if (!string.IsNullOrEmpty(model.DealerCode)){
                    var dbDealer = _context.Dealer.FirstOrDefault(d => d.DealerCode == model.DealerCode);
                    if (dbDealer == null)
                        throw new Exception(_translator.Translate(Expressions.DealerNotFound, _userLanguage));

                    if ((string.IsNullOrEmpty(model.Login) || string.Equals(model.Login, model.DealerCode)) 
                        && string.Equals(dbDealer.DealerPassword, model.Password)){
                        var tokenStr = _authObject.Authenticate(true, model.DealerCode, dbDealer.Id, MgAuthType.Dealer);

                        // DEALER LOGIN
                        result.AuthType = "Dealer";
                        result.DefaultLanguage = dbDealer.DefaultLanguage;
                        result.UserId = dbDealer.Id;
                        result.Username = dbDealer.DealerName;
                        result.Token = tokenStr;

                        return Ok(JsonSerializer.Serialize(result));
                    }

                    var dbOfficer = _context.Officer.FirstOrDefault(d => d.OfficerCode == model.Login);
                    if (dbOfficer == null)
                        throw new Exception(_translator.Translate(Expressions.OfficerNotFound, _userLanguage));

                    if (string.Equals(dbOfficer.OfficerPassword, model.Password)){
                        var tokenStr = _authObject.Authenticate(true, model.Login, dbOfficer.Id, MgAuthType.FactoryOfficer);

                        // OFFICER LOGIN
                        result.AuthType = "FactoryOfficer";
                        result.DefaultLanguage = dbOfficer.DefaultLanguage;
                        result.UserId = dbOfficer.Id;
                        result.Username = dbOfficer.OfficerName;
                        result.FactoryId = dbOfficer.PlantId;
                        result.FactoryName = dbOfficer.Plant != null ? dbOfficer.Plant.PlantName : "";
                        result.Token = tokenStr;

                        return Ok(JsonSerializer.Serialize(result));
                    }
                }
                else{
                    var dbDealer = _context.Dealer.FirstOrDefault(d => d.DealerCode == model.Login);
                    if (dbDealer != null 
                        && string.Equals(dbDealer.DealerPassword, model.Password)){
                        var tokenStr = _authObject.Authenticate(true, model.DealerCode, dbDealer.Id, MgAuthType.Dealer);

                        // DEALER LOGIN
                        result.AuthType = "Dealer";
                        result.DefaultLanguage = dbDealer.DefaultLanguage;
                        result.UserId = dbDealer.Id;
                        result.Username = dbDealer.DealerName;
                        result.Token = tokenStr;

                        return Ok(JsonSerializer.Serialize(result));
                    }

                    var dbOfficer = _context.Officer.FirstOrDefault(d => d.OfficerCode == model.Login);
                    if (dbOfficer == null)
                        throw new Exception(_translator.Translate(Expressions.OfficerNotFound, _userLanguage));

                    if (string.Equals(dbOfficer.OfficerPassword, model.Password)){
                        var tokenStr = _authObject.Authenticate(true, model.Login, dbOfficer.Id, MgAuthType.FactoryOfficer);

                        // OFFICER LOGIN
                        result.AuthType = "FactoryOfficer";
                        result.DefaultLanguage = dbOfficer.DefaultLanguage;
                        result.UserId = dbOfficer.Id;
                        result.Username = dbOfficer.OfficerName;
                        result.FactoryId = dbOfficer.PlantId;
                        result.FactoryName = dbOfficer.Plant != null ? dbOfficer.Plant.PlantName : "";
                        result.Token = tokenStr;

                        return Ok(JsonSerializer.Serialize(result));
                    }
                }
            }
            catch (System.Exception)
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

                var tokenStr = _authObject.Authenticate(true, model.Login, dbUser.Id, MgAuthType.Employee);

                return Ok(tokenStr);
            }
            catch (Exception)
            {
                
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginCard")]
        public CardLoginResult LoginCard([FromBody] UserLoginModel model)
        {
            CardLoginResult result = new CardLoginResult{
                Result = false,
            };

            try
            {
                List<string> possibleKeys = new List<string>(){ model.Login };
                
                #region CALCULATE HEX KEY POSSIBILITES
                // check hex key chars length and store standart version
                try
                {
                    var stdHexKey = Convert.ToInt64(model.Login).ToString("X2");
                    possibleKeys.Add(stdHexKey);
                    if (stdHexKey.Length > 8){
                        possibleKeys.Add(stdHexKey.Substring(0, 8));
                        // possibleKeys.Add(stdHexKey.Substring(2, 8));
                    }
                        
                    
                    // calc and store reversed version
                    var rawHexKey = stdHexKey.Length >= 10 ? stdHexKey.Substring(2,8) : stdHexKey;
                    string reversedHexKey = "";
                    if (rawHexKey.Length % 2 == 0){
                        int hexIndex = rawHexKey.Length - 2;
                        while (hexIndex >= 0){
                            reversedHexKey += rawHexKey.Substring(hexIndex, 2);
                            hexIndex -= 2;
                        }
                    }

                    if (!string.IsNullOrEmpty(reversedHexKey)){
                        possibleKeys.Add(reversedHexKey);
                        try
                        {
                            var hexToDecKey = Convert.ToInt32(reversedHexKey, 16);
                            possibleKeys.Add(hexToDecKey.ToString());
                        }
                        catch (System.Exception)
                        {
                            
                        }
                    }

                    if (!string.IsNullOrEmpty(rawHexKey)){
                        try
                        {
                            var hexToDecKey = Convert.ToInt32(rawHexKey, 16);
                            possibleKeys.Add(hexToDecKey.ToString());
                        }
                        catch (System.Exception)
                        {
                            
                        }
                    }

                }
                catch (System.Exception)
                {
                    
                }
                #endregion

                var dbUser = _context.Employee.Where(d =>
                    d.Plant.PlantCode == model.PlantCode && d.Plant.Dealer.DealerCode == model.DealerCode &&
                    d.EmployeeCard != null &&
                    possibleKeys.Contains(d.EmployeeCard.CardCode)).FirstOrDefault();

                if (dbUser == null)
                    throw new Exception(model.Login + ": " + _translator.Translate(Expressions.UserNotFound, _userLanguage));

                // if (!string.Equals(dbUser.EmployeePassword, model.Password))
                //     throw new Exception(_translator.Translate(Expressions.WrongPassword, _userLanguage));

                var tokenStr = _authObject.Authenticate(true, model.Login, dbUser.Id, MgAuthType.Employee);

                result.Token = tokenStr;
                result.Result=true;
                result.Employee = new EmployeeModel();
                dbUser.MapTo(result.Employee);

                var dbDepartment = _context.Department.FirstOrDefault(d => d.Id == dbUser.DepartmentId);

                result.Employee.DepartmentCode = dbDepartment != null ? dbDepartment.DepartmentCode : "";
                result.Employee.DepartmentName = dbDepartment != null ? dbDepartment.DepartmentName : "";
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginMachine")]
        public IActionResult LoginMachine([FromBody] UserLoginModel model)
        {
            try
            {
                var dbUser = _context.Machine.FirstOrDefault(d => d.MachineCode == model.Login
                    && d.Plant.PlantCode == model.PlantCode && d.Plant.Dealer.DealerCode == model.DealerCode);
                if (dbUser == null)
                    throw new Exception(model.Login + ": " + _translator.Translate(Expressions.UserNotFound, _userLanguage));

                var tokenStr = _authObject.Authenticate(true, model.Login, dbUser.PlantId ?? 0, MgAuthType.Machine);

                return Ok(tokenStr);
            }
            catch (Exception)
            {
                
            }

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("MachineId")]
        public int MachineId([FromBody] UserLoginModel model)
        {
            try
            {
                var dbUser = _context.Machine.FirstOrDefault(d => d.MachineCode == model.Login
                    && d.Plant.PlantCode == model.PlantCode && d.Plant.Dealer.DealerCode == model.DealerCode);
                if (dbUser == null)
                    throw new Exception(model.Login + ": " + _translator.Translate(Expressions.UserNotFound, _userLanguage));

                return dbUser.Id;
            }
            catch (Exception)
            {
                
            }

            return 0;
        }
    
        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        [Route("SetLanguage")]
        public BusinessResult SetLanguage(SetLanguageModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                if (string.Equals(model.AuthType, "Dealer")){
                    var dbDealer = _context.Dealer.FirstOrDefault(d => d.Id == model.UserId);
                    if (dbDealer != null){
                        dbDealer.DefaultLanguage = model.LanguageCode;
                        _context.SaveChanges();
                    }
                }
                else if (string.Equals(model.AuthType, "FactoryOfficer")){
                    var dbOfficer = _context.Officer.FirstOrDefault(d => d.Id == model.UserId);
                    if (dbOfficer != null){
                        dbOfficer.DefaultLanguage = model.LanguageCode;
                        _context.SaveChanges();
                    }
                }

                result.Result=true;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [Authorize]
        [HttpGet]
        [Route("CheckToken")]
        public IActionResult CheckToken(){
            return Ok();
        }
    }
}
