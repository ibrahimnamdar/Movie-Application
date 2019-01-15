using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieApplication.Core.Repositories;
using MovieApplication.Core.Repositories.Interfaces;
using MovieApplication.Core.Service.Service.ServiceInterfaces;
using MovieApplication.Core.Service.Service.Services;

namespace MovieApplication.Helper
{
    public class TokenValidationAttribute : ActionFilterAttribute
    {
        private IUserService _userService;
        private IMovieApplicationUnitOfWork _uow;


        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            _userService = new UserService(new MovieApplication.Core.Data.MovieApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<MovieApplication.Core.Data.MovieApplicationDbContext>()));


            if (!string.IsNullOrEmpty(actionContext.HttpContext.Request.Cookies["SessionToken"]))
            {
                var value = actionContext.HttpContext.Request.Cookies["SessionToken"];
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(value) as JwtSecurityToken;
                var jti = tokenS.Claims.FirstOrDefault(claim => claim.Type == "unique_name").Value;
                var user = _userService.GetUserByUsername(jti);

                if (user == null)
                {
                    actionContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Home/Index.cshtml"
                    };
                }
                base.OnActionExecuting(actionContext);
            }
            else
            {
                actionContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Home/Index.cshtml"
                };
                base.OnActionExecuting(actionContext);
            }
        }
    }
}
