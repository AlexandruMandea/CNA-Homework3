using ChatService.Helpers;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService
{
    public class ChatService : Chat.ChatBase
    {
        private readonly ILogger<ChatService> _logger;


        public ChatService(ILogger<ChatService> logger)
        {
            _logger = logger;
        }

        public override Task<Empty> Join(Message message, ServerCallContext context)
        {
            string infoAboutJoining = message.ClientName + " joined the chat.";

            _logger.Log(LogLevel.Information, infoAboutJoining);

            Helper.Messages.Add(new Message()
            {
                ClientName = message.ClientName,
                Content = " joined the chat."
            });

            return Task.FromResult(new Google.Protobuf.WellKnownTypes.Empty());
        }

        public override Task<Empty> Leave(Message message, ServerCallContext context)
        {
            string infoAboutLeaving = message.ClientName + " left the chat.";

            _logger.Log(LogLevel.Information, infoAboutLeaving);

            Helper.Messages.Add(new Message()
            {
                ClientName = message.ClientName,
                Content = " left the chat."
            });

            return Task.FromResult(new Google.Protobuf.WellKnownTypes.Empty());
        }
    }
}
