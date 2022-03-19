using System;
using System.Collections.Generic;
using NetMQ;
using NetMQ.Sockets;
using Newtonsoft.Json;

namespace DotFramework.Infra.Messaging.ZeroMq
{
    public class ZeroMqMessageQueueAsync : BaseZeroMqMessageQueue, IMessageQueue
    {
        private NetMQSocket _socket;

        public ZeroMqMessageQueueAsync()
        {
            
        }
        public ZeroMqMessageQueueAsync(Dictionary<string, string> addressMapping) : base(addressMapping)
        {
        }

        public void InitializeInbound(string name, MessagePattern pattern)
        {
            var config = new MessageQueueConfig(name, pattern);
            InitializeInbound(config);
        }

        public void InitializeInbound(MessageQueueConfig config)
        {
            Config = config;

            switch (Config.MessagePattern)
            {
                case MessagePattern.RequestResponse:
                    _socket = new RouterSocket();
                    _socket.Bind(Address);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(config.Name),
                        config.MessagePattern, null);
            }
        }


        public void InitializeOutbound(string name, MessagePattern pattern)
        {
            Config = new MessageQueueConfig(name, pattern);
            switch (Config.MessagePattern)
            {
                case MessagePattern.RequestResponse:
                    _socket = new RequestSocket();
                    _socket.Connect(Address);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(name),
                        pattern, null);
            }
        }

        public void Send(Message message)
        {
            var multipartMessage = new NetMQMessage();


            if (message.ResponseKey == null || message.ResponseKey.Length == 0)
            {
                multipartMessage.Append(JsonConvert.SerializeObject(message));
                _socket.SendMultipartMessage(multipartMessage);
            }
            else
            {
                multipartMessage.Append(new NetMQFrame(message.ResponseKey));
                multipartMessage.AppendEmptyFrame();
                multipartMessage.Append(JsonConvert.SerializeObject(message));
                _socket.SendMultipartMessage(multipartMessage);
            }
        }

        public void Send(Message message, string key)
        {
            throw new NotImplementedException();
        }

        public void Listen(Action<Message> onMessageReceived)
        {
            while (true)
                Received(onMessageReceived);
        }

        public void Listen(Action<Message> onMessageReceived, string key)
        {
            throw new NotImplementedException();
        }

        public void Received(Action<Message> onMessageReceived)
        {
            var receiveMessage = _socket.ReceiveMultipartMessage();
            onMessageReceived(Map(receiveMessage));
        }


        public IMessageQueue GetResponseQueue()
        {
            return this;
        }

        public IMessageQueue GetReplyQueue(Message message)
        {
            return this;
        }


        public void Dispose()
        {
            _socket?.Dispose();
        }

        private Message Map(NetMQMessage receiveMessage)
        {
            if (receiveMessage.FrameCount > 1)
            {
                var message = JsonConvert.DeserializeObject<Message>(receiveMessage[2].ConvertToString());
                message.ResponseKey = receiveMessage[0].ToByteArray();
                return message;
            }

            else
            {
                var message = JsonConvert.DeserializeObject<Message>(receiveMessage[0].ConvertToString());
                return message;
            }
        }

        public void InitializeOutbound(string exchangeName, string queueName, MessagePattern pattern)
        {
            throw new NotImplementedException();
        }

        public void InitializeInbound(string exchangeName, string queueName, MessagePattern pattern)
        {
            throw new NotImplementedException();
        }
    }
}