using BuySite.Models;
using BuySite.Models.Entity;
using BuySite.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Project.Common;
using Project.Common.Helper;
using Project.Models.Models.Entity;
using Project.Models.Models.ViewModel;
using Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace BuySite.Controllers
{
    public class AccountController : Controller
    {
        private readonly FakeDb db;
        private readonly BuySiteDBContext _buySiteDBContext;
        private UserManager<User> _userManger;
        private readonly ILogger<AccountController> _logger;
        private SignInManager<User> _signInManager;
        private readonly ProductRepository _productRepository;
        private readonly AppInfo _appinfo;
        private readonly IConfiguration _configruration;
        private readonly IDbConnection _conn;
        public AccountController(BuySiteDBContext buySiteDBContext, UserManager<User> userManger, SignInManager<User> signInManager, ILogger<AccountController> logger, ProductRepository productRepository, IDbConnection conn)
        {
            _userManger = userManger;
            _signInManager = signInManager;
            _buySiteDBContext = buySiteDBContext;
            _logger = logger;
            _conn = conn;
            _productRepository = new ProductRepository(_appinfo, _configruration,_conn,_buySiteDBContext);

            db = new FakeDb
            {
                User = new List<FakeUser>() {
                new FakeUser
                {
                id=1,
                Name="Reds",
                Account="Reds@gmail.com",
                Password="a123",
                Roles = new string[]{"admin"},
                Salary = 1000
                },new FakeUser
                {
                 id=2,
                Name="Harry",
                Account="harry@gmail.com",
                Password="123456789",
                Roles = new string[]{"employee"},
                Salary = 200000
                },new FakeUser
                {
                id=2,
                Name="William",
                Account="william@gmail.com",
                Password="a1234",
                Roles = new string[]{"employee", "admin" },
                Salary = 3000
                }

              }
            };

        }
        public IActionResult Index()
        {
            return View();
        }
        //註冊畫面
        public IActionResult Register()
        {
            return View();
        }
        //登入畫面
        public IActionResult Login()
        {
            return View();
        }
        //Fb登陸
        public IActionResult FbLogin()
        {
            var p = new AuthenticationProperties()
            {
                RedirectUri = Url.Action("Response")
            };
            return Challenge(p, FacebookDefaults.AuthenticationScheme);
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
                    //Password = model.Password,
                    //Phone = model.Phone,
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
                    {
                        //創建一個購物車
                        var shoppingCart = new ShoppingCart()
                        {
                            Id = Guid.NewGuid(),
                            Userid = user.Id
                        };
                        await _productRepository.CreatShoppingCart(shoppingCart);
                        await _buySiteDBContext.SaveChangesAsync();
                        return Ok();
                    }
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
            //return View(model);

            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            //var result = _buySiteDBContext.Users.Where(x => x.Email == model.Email).FirstOrDefault();
            //if (result == null)
            //{
            //    var user = new User()
            //    {
            //        Email = model.Email,
            //        Password = model.Password,
            //        Name = model.Name,
            //        Phone = model.Phone,
            //        Address = model.Address,
            //        Birth = model.Birth,
            //        NickName = model.NickName
            //    };
            //    _buySiteDBContext.Users.Add(user);
            //    await _buySiteDBContext.SaveChangesAsync();
            //}

            //return View(model);
        }
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManger.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await _userManger.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        //登陸
        //public async Task<IActionResult> Loing([FromBody] LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var result = await _signInManager.PasswordSignInAsync(model.Account, model.Password, isPersistent: false, lockoutOnFailure: false );
        //    if (!result.Succeeded)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok();
        //}
        [HttpPost]
        public async Task<IActionResult> Loging(LoginViewModel login)
        {

            if (ModelState.IsValid)
            {
                User appUser = await _userManger.FindByEmailAsync(login.Account);
                if (appUser != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                    if (result.Succeeded)
                    {
                        var user = _buySiteDBContext.Users.Where(x => x.Email == login.Account).FirstOrDefault();
                        var role = await _userManger.GetRolesAsync(user);

                        var jsonstring = JsonConvert.SerializeObject(new
                        {
                            user.Email,
                            user.NickName,
                            user.Id
                        });
                        HttpContext.Session.SetString("user_id", jsonstring);
                        var claims = new List<Claim>()
                        {
                          new Claim(ClaimTypes.Name, user.Name),
                          new Claim(ClaimTypes.Email,user.Email),
                          new Claim("User_id",user.Id.ToString()),
                          new Claim(ClaimTypes.Role,string.Join(',',user.State)),
                        };
                        //這邊要寫什麼都可以
                       var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity); //ClaimsPrincipal原則
                        
                       await HttpContext.SignInAsync(claimsPrincipal);
                                              
                        //return Redirect("https://localhost:5001/");
                        return Ok("成功");
                      
                           
                    }


                    bool emailStatus = await _userManger.IsEmailConfirmedAsync(appUser);

                    if (emailStatus == false)
                    {
                        ModelState.AddModelError(nameof(login.Account), "Email is unconfirmed, please confirm it first");
                        return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(modelError => modelError.ErrorMessage).ToList());
                    }
                }
                ModelState.AddModelError(nameof(login.Account), "Login Failed: Invalid Email or password");
                return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(modelError => modelError.ErrorMessage).ToList());
            }
            return View(login);
        }
        //登出
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> ResponseAsync()
        {
            var res = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var data = res.Principal.Claims.Select(x => new
            {
                x.Type,
                x.Value,
                x.Issuer,
                x.OriginalIssuer
            });
            return Json(data);
        }
        [HttpPost]
        public async Task<bool> LoginUser([FromBody] LoginViewModel model)
        {
            var user = db.User.Where(x => x.Account == model.Account && x.Password == model.Password).FirstOrDefault();
            if (user == null)
            {
                return false;
                //return View("Login");
            }
            else
            {
                var r = user.Roles.Select(x => new Claim(ClaimTypes.Role, x.ToString()));
                var claims = new List<Claim>()
                {
                  new Claim(ClaimTypes.Name, user.Name),
                  new Claim(ClaimTypes.Email,user.Account),
                  new Claim("User_id",user.id.ToString()),
                  new Claim(ClaimTypes.Role,string.Join(',',user.Roles)),
                  new Claim("Hello",user.Salary.ToString())
                };
                claims.AddRange(r);
                //這邊要寫什麼都可以
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity); //ClaimsPrincipal原則

                await HttpContext.SignInAsync(claimsPrincipal);
                return true;
                //return RedirectToAction("Index", "Home");
            }
        }

    }
}
