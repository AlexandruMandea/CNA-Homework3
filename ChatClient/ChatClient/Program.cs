using System;

namespace ChatClient
{
    class Program
    {
        private static readonly GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001");
        private static readonly Greeter.GreeterClient client = new Greeter.GreeterClient(channel);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
