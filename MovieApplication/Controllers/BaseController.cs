using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MovieApplication.Core.Repositories.Interfaces;
using MovieApplication.Domain.Dto.Models;

namespace MovieApplication.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUserRepository _userRepository;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public BaseController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string CurrentUserToken
        {
            get
            {
                HttpContext.Request.Headers.TryGetValue("Authorization", out var token);

                if (!string.IsNullOrEmpty(token))
                {
                    return token.ToString().StartsWith("Bearer") ? token.ToString().Substring(7) : token.ToString();
                }

                return null;
            }
        }
        private User _currentUser;

        public int CurrentUserID
        {
            get
            {
                if (CurrentUser != null)
                {
                    return CurrentUser.Id;
                }

                return 0;
            }
        }

        public User CurrentUser
        {
            get
            {
                if (_currentUser != null)
                    return _currentUser;

                if (HttpContext == null)
                    return null;

                if (!string.IsNullOrEmpty(CurrentUserToken))
                {
                    _currentUser = _userRepository.GetUserByToken(CurrentUserToken).Result;
                }

                return _currentUser;
            }
        }
    }
}