using DotFramework.Core;
using DotFramework.Core.Web;
using DotFramework.Infra.Security.Model;
using DotFramework.Web.API;
using System;
using System.Web.Http;

namespace DotFramework.Infra.Web.API.Auth.Controllers
{
    [RoutePrefix("Auth")]
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("AuthenticateClient")]
        public IHttpActionResult AuthenticateClient(AuthenticateClientRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = AuthenticationProvider.Instance.AuthenticateClient(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login(LoginRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Reauthenticate")]
        [SSOAuthorize]
        public IHttpActionResult Reauthenticate(ReauthenticateRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ExternalLogin")]
        public IHttpActionResult GetExternalLogin(string provider, string redirect_url)
        {
            try
            {
                return AuthenticationProvider.Instance.GetExternalLogin(provider, redirect_url);
            }
            catch (Exception ex)
            {
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ObtainLocalAccessToken")]
        public IHttpActionResult ObtainLocalAccessToken(ObtainLocalAccessTokenRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RefreshToken")]
        public IHttpActionResult RefreshToken(RefreshTokenRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Authenticate")]
        public IHttpActionResult Authenticate(AuthenticateRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ClientEncryptionToken")]
        public IHttpActionResult GenerateClientEncryptionToken(GenerateClientEncryptionTokenRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ActivateClient")]
        public IHttpActionResult ActivateClient(ActivateClientRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Activate")]
        public IHttpActionResult Activate(ActivateRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("GrantActivation")]
        public IHttpActionResult GrantActivation(GrantActivationRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("VerifyClientToken")]
        public IHttpActionResult VerifyClientToken()
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Authorize")]
        [SSOAuthorize]
        public IHttpActionResult Authorize(AuthorizeRequest request)
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
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("UserData")]
        [SSOAuthorize]
        public IHttpActionResult GetUserData()
        {
            try
            {
                var response = AuthenticationProvider.Instance.GetUserData();
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ClientData")]
        [SSOAuthorize]
        public IHttpActionResult GetClientData()
        {
            try
            {
                var response = AuthenticationProvider.Instance.GetClientData();
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("ValidateToken")]
        [SSOAuthorize]
        public IHttpActionResult ValidateToken()
        {
            try
            {
                var response = AuthenticationProvider.Instance.ValidateToken();
                return Ok(response);
            }
            catch (Exception ex)
            {
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(RegisterRequest request)
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
            catch (HttpException<ModelStateErrorResult> ex)
            {
                return GetErrorResult(ex);
            }
            catch (Exception ex)
            {
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ChangePassword")]
        public IHttpActionResult ChangePassword(ChangePasswordRequest request)
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
            catch (HttpException<ModelStateErrorResult> ex)
            {
                return GetErrorResult(ex);
            }
            catch (Exception ex)
            {
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IHttpActionResult ForgetPassword(ForgetPasswordRequest request)
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
            catch (HttpException<ModelStateErrorResult> ex)
            {
                return GetErrorResult(ex);
            }
            catch (Exception ex)
            {
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IHttpActionResult ResetPassword(ResetPasswordRequest request)
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
            catch (HttpException<ModelStateErrorResult> ex)
            {
                return GetErrorResult(ex);
            }
            catch (Exception ex)
            {
                if (ApiExceptionHandler.Instance.HandleException(ref ex, ControllerContext, ActionContext))
                {
                    throw ex;
                }

                return BadRequest(ex.Message);
            }
        }

        private IHttpActionResult GetErrorResult(HttpException<ModelStateErrorResult> ex)
        {
            foreach (var key in ex.ErrorResult.ModelState.Keys)
            {
                foreach (var errorMessage in ex.ErrorResult.ModelState[key])
                {
                    ModelState.AddModelError(key, errorMessage);
                }
            }

            return BadRequest(ModelState);
        }
    }
}
