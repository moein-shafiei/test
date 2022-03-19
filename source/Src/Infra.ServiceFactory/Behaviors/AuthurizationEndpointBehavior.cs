using DotFramework.Infra.Security;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace DotFramework.Infra.ServiceFactory
{
    /// <summary>
    /// Represents a run-time behavior extension for a client endpoint.
    /// </summary>
    public class AuthorizationEndpointBehavior : CustomEndpointBehavior
    {
        /// <summary>
        /// Implements a modification or extension of the client across an endpoint.
        /// </summary>
        /// <param name="endpoint">The endpoint that is to be customized.</param>
        /// <param name="clientRuntime">The client runtime to be customized.</param>
        public override void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new AuthorizationClientMessageInspector());
        }
    }

    /// <summary>
    /// Represents a message inspector object that can be added to the <c>MessageInspectors</c> collection to view or modify messages.
    /// </summary>
    public class AuthorizationClientMessageInspector : CustomClientMessageInspector
    {
        /// <summary>
        /// Enables inspection or modification of a message before a request message is sent to a service.
        /// </summary>
        /// <param name="request">The message to be sent to the service.</param>
        /// <param name="channel">The WCF client object channel.</param>
        /// <returns>
        /// The object that is returned as the <paramref name="correlationState " /> argument of
        /// the <see cref="M:System.ServiceModel.Dispatcher.IClientMessageInspector.AfterReceiveReply(System.ServiceModel.Channels.Message@,System.Object)" /> method.
        /// This is null if no correlation state is used.The best practice is to make this a <see cref="T:System.Guid" /> to ensure that no two
        /// <paramref name="correlationState" /> objects are the same.
        /// </returns>
        public override object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            throw new NotImplementedException();
            //HttpRequestMessageProperty property = null;

            //if (request.Properties[HttpRequestMessageProperty.Name] != null)
            //{
            //    property = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            //}
            //else
            //{
            //    property = new HttpRequestMessageProperty();
            //}

            //property.Headers["AuthenticationToken"] = AuthService.Instance.AuthorizationToken.Token;
            //property.Headers[SessionKey.UserSessionID] = UserSessionManager.Instance.GetSessionID().ToString();

            //if (request.Properties[HttpRequestMessageProperty.Name] != null)
            //{
            //    request.Properties[HttpRequestMessageProperty.Name] = property;
            //}
            //else
            //{
            //    request.Properties.Add(HttpRequestMessageProperty.Name, property);
            //}

            //return base.BeforeSendRequest(ref request, channel);
        }
    }
}
