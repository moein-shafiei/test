using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotFramework.Core;
using DotFramework.Core.Web;
using DotFramework.Infra.Security.Model;
using DotFramework.Infra.Web.API.Auth.Providers;
using DotFramework.Infra.Web.API;
using DotFramework.Infra.Web.API.Auth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using DotFramework.Web.API;

namespace DotFramework.Infra.Web.API.Auth.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("AuthenticateClient")]
        public IActionResult AuthenticateClient()
        {
            try
            {
                var response = AuthenticationProvider.Instance.AuthenticateClient(null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = AuthenticationProvider.Instance.Login(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("Reauthenticate")]
        [SSOAuthorize]
        public IActionResult Reauthenticate(ReauthenticateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = AuthenticationProvider.Instance.Reauthenticate(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpDelete]
        [Route("Logout")]
        [SSOAuthorize]
        public IActionResult Logout()
        {
            try
            {
                AuthenticationProvider.Instance.Logout();
                return Ok();
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpGet]
        [Route("ExternalLogin")]
        public IActionResult GetExternalLogin(string provider, string redirect_url)
        {
            try
            {
                return AuthenticationProvider.Instance.GetExternalLogin(provider, redirect_url);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("ObtainLocalAccessToken")]
        public IActionResult ObtainLocalAccessToken(ObtainLocalAccessTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = AuthenticationProvider.Instance.ObtainLocalAccessToken(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public IActionResult RefreshToken(RefreshTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ObtainLocalAccessTokenResponse response = AuthenticationProvider.Instance.RefreshToken(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                AuthenticateResponseModel response = AuthenticationProvider.Instance.Authenticate(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("ClientEncryptionToken")]
        public IActionResult GenerateClientEncryptionToken(GenerateClientEncryptionTokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                GenerateClientEncryptionTokenResponse response = AuthenticationProvider.Instance.GenerateClientEncryptionToken(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("ActivateClient")]
        public IActionResult ActivateClient(ActivateClientRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ActivateClientResponse response = AuthenticationProvider.Instance.ActivateClient(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("Activate")]
        public IActionResult Activate(ActivateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ActivateResponse response = AuthenticationProvider.Instance.Activate(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("GrantActivation")]
        public IActionResult GrantActivation(GrantActivationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ActivateLocalAccessTokenResponse response = AuthenticationProvider.Instance.GrantActivation(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("VerifyClientToken")]
        [SSOAuthorize]
        public IActionResult VerifyClientToken()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                VerifyClientTokenResponseModel response = AuthenticationProvider.Instance.VerifyClientToken(new VerifyTokenRequest
                {
                    AccessToken = HttpContextProvider.Current?.GetToken()
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("Authorize")]
        //[SSOAuthorize]
        public IActionResult Authorize(AuthorizeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = AuthenticationProvider.Instance.GetUserData();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpGet]
        [Route("UserData")]
        [SSOAuthorize]
        public IActionResult GetUserData()
        {
            try
            {
                var response = AuthenticationProvider.Instance.GetUserData();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpGet]
        [Route("ClientData")]
        [SSOAuthorize]
        public IActionResult GetClientData()
        {
            try
            {
                var response = AuthenticationProvider.Instance.GetClientData();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpGet]
        [Route("ValidateToken")]
        [SSOAuthorize]
        public IActionResult ValidateToken()
        {
            try
            {
                var response = AuthenticationProvider.Instance.ValidateToken();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = AuthenticationProvider.Instance.Register(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        [SSOAuthorize]
        public IActionResult ChangePassword(ChangePasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = AuthenticationProvider.Instance.ChangePassword(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = AuthenticationProvider.Instance.ForgetPassword(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = AuthenticationProvider.Instance.ResetPassword(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return GetErrorResult(ex);
            }
        }

        private IActionResult GetErrorResult(Exception ex)
        {
            if (ex is HttpException<ModelStateErrorResult> exp)
            {
                foreach (var key in exp.ErrorResult.ModelState.Keys)
                {
                    foreach (var errorMessage in exp.ErrorResult.ModelState[key])
                    {
                        ModelState.AddModelError(key, errorMessage);
                    }
                }

                return BadRequest(ModelState);
            }
            else if (ex is HttpException<BadRequestErrorResult> expB)
            {
                return BadRequest(expB.ErrorResult.Message);
            }
            else
            {
                try
                {
                    if (ApiExceptionHandler.Instance.HandleException(ref ex, HttpContext))
                    {
                        throw ex;
                    }
                }
                catch (UnauthorizedHttpException)
                {
                    return Unauthorized();
                }
                catch (Exception)
                {
                    throw;
                }

                return BadRequest(ex.Message);
            }
        }
    }
}
