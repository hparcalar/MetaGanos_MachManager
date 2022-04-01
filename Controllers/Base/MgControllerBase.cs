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

        protected void ResolveHeaders(IHeaderDictionary headers){
            if (headers != null && headers.ContainsKey("Accept-Language"))
                _userLanguage = headers["Accept-Language"];
        }
    }
}