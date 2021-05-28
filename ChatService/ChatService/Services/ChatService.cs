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

        public override Task<Empty> ClientToServer(Message request, ServerCallContext context)
        {
            string name = request.ClientName;
            string content = request.Content;
            Helper.Messages.Add(request);

            _logger.Log(LogLevel.Information, name + " said: " + content);

            return Task.FromResult(new Google.Protobuf.WellKnownTypes.Empty());
        }

        public override async Task ServerToClient(Empty request, IServerStreamWriter<Reply> responseStream, ServerCallContext context)
        {
            bool open = true;

            try
            {
                while (open)
                {
                    if (Helper.Messages.Last() != Helper.LastMessageSent)
                    {
                        var message = new Reply()
                        {
                            Message = "\n" + Helper.Messages.Last().ClientName + ": " + Helper.Messages.Last().Content
                        };

                        Helper.LastMessageSent = Helper.Messages.Last();

                        await responseStream.WriteAsync(message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message + "\n");
            }
        }
    }
}
