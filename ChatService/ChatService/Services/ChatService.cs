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
    }
}
