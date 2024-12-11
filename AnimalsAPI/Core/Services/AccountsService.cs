using AutoMapper;
using Core.Exceptions;
using Core.IServices;
using Core.Models;
using Data;
using Data.Enteties;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountsService(AnimalsDbContext context,
        IMapper mapper,
        UserManager<User> userManager,
        SignInManager<User> signInManager) : IAccountsService
    {
        public async Task Register(RegisterModel model)
        {
            var user = mapper.Map<User>(model);

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new HttpException(result.Errors.First().Description, HttpStatusCode.BadRequest);
            }
        }

        public async Task Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                throw new HttpException("Invalid login or password.", HttpStatusCode.BadRequest);
            }

            await signInManager.SignInAsync(user, true);
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }
    }
}
