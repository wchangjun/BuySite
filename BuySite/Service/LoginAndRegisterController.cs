using BuySite.Models;
using BuySite.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Common;
using Project.Models.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuySite.Service
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAndRegisterController : ControllerBase
    {
        private readonly BuySiteDBContext _buySiteDBContext;
        private UserManager<User> _userManger;
        private readonly ILogger<LoginAndRegisterController> _logger;
        private SignInManager<User> _signInManager;
        public LoginAndRegisterController(BuySiteDBContext buySiteDBContext, UserManager<User> userManger, SignInManager<User> signInManager, ILogger<LoginAndRegisterController> logger)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _buySiteDBContext = buySiteDBContext;
            _logger = logger;
        }
        //註冊
        [HttpPost]
        public async Task<IActionResult> Registerse([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(modelError => modelError.ErrorMessage).ToList());

                var user = new User()
                {
                    Email = model.Email,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = model.Password,
                    NickName = model.NickName,
                    Address = model.Address,
                    Birth = model.Birth,
                    Sex = model.Sex,
                    UserName = model.Email

                };
                IdentityResult result = await _userManger.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //return BadRequest(result.Errors.Select(x => x.Description).ToList());
                    var token = await _userManger.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, Request.Scheme);
                    EmailHelper emailHelper = new EmailHelper();
                    bool emailResponse = emailHelper.SendEmail(user.Email, confirmationLink);
                    if (emailResponse)
                        return Ok();
                    else
                    {
                        // log email failed 
                    }
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);

                }
            }
            //await _signInManager.SignInAsync(user, false);
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(modelError => modelError.ErrorMessage).ToList());

        }
        //public async Task<IActionResult> ConfirmEmail(string token, string email)
        //{
        //    var user = await _userManger.FindByEmailAsync(email);
        //    if (user == null)
        //        return  View("Error");

        //    var result = await _userManger.ConfirmEmailAsync(user, token);
        //    return View(result.Succeeded ? "ConfirmEmail" : "Error");
        //}
    }
}
