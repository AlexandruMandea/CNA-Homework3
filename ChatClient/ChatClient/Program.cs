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

        static async Task Main(string[] args)
        {
            await Task.Run(async () =>
            {
                var dataStream = client.ServerToClient(new Empty());

                    await foreach (var message in dataStream.ResponseStream.ReadAllAsync())
                    {
                        Console.WriteLine(message.Message);
                    }
            });
        }
    }
}
