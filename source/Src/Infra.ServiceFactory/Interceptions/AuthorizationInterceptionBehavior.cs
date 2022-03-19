using System;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace DotFramework.Infra.ServiceFactory
{
    public class AuthorizationInterceptionBehavior : ServiceInterceptionBehavior
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            ISecureServiceBase service = input.Target as ISecureServiceBase;

            bool isAuthorized = false;

            //if (input.MethodBase.GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Length == 0 &&
            //    service.GetType().GetCustomAttributes(typeof(AllowAnonymousAttribute), false).Length == 0)
            //{
            //    try
            //    {
            //        var operationTypeAtrributes = input.MethodBase.GetCustomAttributes(typeof(OperationTypeAttribute), false);

            //        isAuthorized = AuthService.Instance.GetAuthorizationStatus(new GetAuthorizedRolesBindingModel
            //        {
            //            ObjectType = AuthObjectType.Service,
            //            EndpointType = service.GetType().FullName,
            //            OperationName = input.MethodBase.Name,
            //            OperationType = operationTypeAtrributes.Length != 0 ? (operationTypeAtrributes.First() as OperationTypeAttribute).Name : null
            //        });
            //    }
            //    catch
            //    {
            //        isAuthorized = false;
            //    }
            //}
            //else
            //{
            //    isAuthorized = true;
            //}

            isAuthorized = true;

            if (isAuthorized)
            {
                return base.Invoke(input, getNext);
            }
            else
            {
                Exception ex = new UnauthorizedAccessException();

                if (HandleException(ref ex, input))
                {
                    throw ex;
                }

                return null;
            }
        }
    }
}
