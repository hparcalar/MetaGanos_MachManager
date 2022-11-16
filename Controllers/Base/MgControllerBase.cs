using Microsoft.AspNetCore.Mvc;
using MachManager.Context;
using MachManager.i18n;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using MachManager.Authentication;

namespace MachManager.Controllers.Base{
    public class MgControllerBase : ControllerBase{
        public MgControllerBase(){
        }

        public MgControllerBase(MetaGanosSchema context){
            _context = context;
            _translator = new Translation(_context);
        }
        public MgControllerBase(MetaGanosSchema context, MgAuth authObject){
            _context = context;
            _authObject = authObject;
            _translator = new Translation(_context);
        }

        public MgControllerBase(MetaGanosSchema context, IWebHostEnvironment environment){
            _context = context;
            _environment = environment;
            _translator = new Translation(_context);
        }

        
        
        protected MetaGanosSchema _context;
        protected IWebHostEnvironment _environment;
        protected Translation _translator;
        protected MgAuth _authObject;
        protected string _userLanguage = "default";
        protected bool _isFactoryOfficer = false;
        protected bool _isDealer = false;
        protected bool _isMachine = false;
        protected int _appUserId = 0;

        protected void ResolveHeaders(HttpRequest request){
            if (request != null && request.Headers != null && request.Headers.ContainsKey("Accept-Language"))
                _userLanguage = request.Headers["Accept-Language"];

            if (request != null)
                ResolveClaims(request.HttpContext);
        }

        private void ResolveClaims(HttpContext httpContext){
            if (httpContext.User != null && httpContext.User.Identity != null){
                var identity = httpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims; 
                    this._isDealer = claims.Any(d => d.Type == ClaimTypes.Role && d.Value == "Dealer");
                    this._isFactoryOfficer = claims.Any(d => d.Type == ClaimTypes.Role && d.Value == "FactoryOfficer");
                    this._isMachine = claims.Any(d => d.Type == ClaimTypes.Role && d.Value == "Machine");
                    
                    if (claims.Any(d => d.Type == ClaimTypes.UserData)){
                        this._appUserId = Convert.ToInt32(
                            claims.Where(d => d.Type == ClaimTypes.UserData)
                                .Select(d => d.Value).First()
                        );
                    }
                }
            }
        }
    }
}