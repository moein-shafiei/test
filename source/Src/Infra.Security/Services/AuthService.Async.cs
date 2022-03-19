using DotFramework.Infra.Security.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotFramework.Infra.Security
{
    public partial class AuthService
    {
        public Task<AuthenticateClientResponse> AuthenticateClientAsync(AuthenticateClientRequest request)
        {
            return Task.Run(() =>
            {
                return AuthenticateClient(request);
            });
        }

        public Task<TokenResponseModel> AuthenticateAsync(LoginBindingModel model)
        {
            return Task.Run(() =>
            {
                return Authenticate(model);
            });
        }

        public Task<TokenResponseModel> ExternalAuthenticateAsync(ObtainLocalAccessTokenBindingModel model)
        {
            return Task.Run(() =>
            {
                return ExternalAuthenticate(model);
            });
        }

        public Task<TokenResponseModel> RefreshTokenAsync(RefreshTokenBindingModel model)
        {
            return Task.Run(() =>
            {
                return RefreshTokenAsync(model);
            });
        }

        public Task<VerifyTokenResponseModel> VerifyTokenAsync(VerifyTokenRequest request)
        {
            return Task.Run(() =>
            {
                return VerifyToken(request);
            });
        }

        public Task<Boolean> SignOutAsync(AuthorizationBindingModel model)
        {
            return Task.Run(() =>
            {
                return SignOut(model);
            });
        }

        public Task RegisterAsync(RegisterBindingModel model)
        {
            return Task.Run(() =>
            {
                Register(model);
            });
        }

        public Task<TokenResponseModel> RegisterAndLoginAsync(RegisterBindingModel model)
        {
            return Task.Run(() =>
            {
                return RegisterAndLogin(model);
            });
        }

        public Task ChangePasswordAsync(ChangePasswordBindingModel model, string accessToken)
        {
            return Task.Run(() =>
            {
                ChangePassword(model, accessToken);
            });
        }

        public Task<Int64> CreateSessionAsync(CreateSessionBindingModel model)
        {
            return Task.Run(() =>
            {
                return CreateSession(model);
            });
        }

        public Task<AuthorizedRolesResponseModel> GetAuthorizedRolesAsync(GetAuthorizedRolesBindingModel model)
        {
            return Task.Run(() =>
            {
                return GetAuthorizedRoles(model);
            });
        }

        public Task<Boolean> GetAuthorizationStatusAsync(GetAuthorizedRolesBindingModel model)
        {
            return Task.Run(() =>
            {
                return GetAuthorizationStatus(model);
            });
        }

        public Task<Boolean> ForgetPasswordAsync(ForgetPasswordBindingModel model)
        {
            return Task.Run(() =>
            {
                return ForgetPassword(model);
            });
        }

        public Task<Boolean> ResetPasswordAsync(ResetPasswordBindingModel model)
        {
            return Task.Run(() =>
            {
                return ResetPassword(model);
            });
        }

        public Task<Boolean> SimpleResetPasswordAsync(SimpleResetPasswordBindingModel model)
        {
            return Task.Run(() =>
            {
                return SimpleResetPassword(model);
            });
        }
    }
}
