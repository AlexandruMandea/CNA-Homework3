using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Service;
using System;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        private static readonly GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
        private static readonly Chat.ChatClient client = new Chat.ChatClient(channel);

        private static async void ServerToClient()
        {
            await Task.Run(async () =>
            {
                var dataStream = client.ServerToClient(new Empty());

                try
                {
                    await foreach (var message in dataStream.ResponseStream.ReadAllAsync())
                    {
                        Console.WriteLine(message.Message);
                    }
                }
                catch (RpcException e) when (e.StatusCode == StatusCode.Cancelled)
                {
                    Console.WriteLine("Stream cancelled by client.");
                }
            });
        }

        static async Task Main(string[] args)
        {
            
        }
    }
}
