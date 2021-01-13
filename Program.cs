using System;
using System.Drawing;
using System.Threading;
using Console = Colorful.Console;
using Leaf.xNet;
using System.IO;
using System.Collections.Generic;

namespace N1tr0
{
    class Program
    {
        public const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        public static string type = "";

        static void Main(string[] args)
        {
            var proxyList = new List<string>() { "http", "socks4", "socks5" };

            Logo();

            Console.Write("> ", Color.Red);
            Console.Write("Proxies choice (http, socks4, socks5): ");
            string proxy = Console.ReadLine();
            Console.WriteLine();
            type += proxy;

            if (!proxyList.Contains(proxy))
            {
                Console.WriteLine("Invalid choice", Color.Red);
                System.Threading.Thread.Sleep(3000);
                Environment.Exit(0);
            }

            for (int i = 0; i < 10; i += 1)
            {
            Thread t = new Thread(Run);
            t.Start();
            }
        }

        public static void Run()
        {
            Console.Title = $"Nitro Gen By >_Monsτεгεd#0069";

            while (true)
                {
                var code = GenCode();

                foreach (var proxy in File.ReadAllLines("proxies.txt"))
                {
                    if (Check(code, Convert.ToString(proxy), type))
                    {
                        Console.WriteLine($"[+] https://discord.gift/{code}", Color.Green);
                        SaveHits(code);
                    }
                    else
                    {
                        Console.WriteLine($"[-] https://discord.gift/{code}", Color.Red);
                    }
                }
            }
        }
        
        public static string GenCode()
        {
            var chars = new char[16];
            var random = new Random();
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = letters[random.Next(letters.Length)];
            }
            return new string(chars);
        }

        public static bool Check(string code, string proxies, string type)
        {

            try
            {
                var req = new HttpRequest();

                req.UserAgentRandomize();

                req.Get($"https://discord.com/api/v8/entitlements/gift-codes/{code}?with_application=false&with_subscription_plan=true");
                req.UserAgent = Http.RandomUserAgent();

                switch (type)
                {
                    case "http":
                        req.Proxy = HttpProxyClient.Parse(proxies);
                        break;
                    case "socks4":
                        req.Proxy = Socks4ProxyClient.Parse(proxies);
                        break;
                    case "socks5":
                        req.Proxy = Socks5ProxyClient.Parse(proxies);
                        break;

                    default:
                        req.Proxy = null;
                            break;
                }
                

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Logo()
        {
            Console.WriteLine("");
            Console.WriteLine($"\t\t██████╗ ██╗███████╗ ██████╗ ██████╗ ██████╗ ██████╗     ███╗   ██╗    ██████╗    ", Color.Red);
            Console.WriteLine($"\t\t██╔══██╗██║██╔════╝██╔════╝██╔═══██╗██╔══██╗██╔══██╗    ████╗  ██║   ██╔════╝    ", Color.Red);
            Console.WriteLine($"\t\t██║  ██║██║███████╗██║     ██║   ██║██████╔╝██║  ██║    ██╔██╗ ██║   ██║  ███╗   ", Color.Red);
            Console.WriteLine($"\t\t██║  ██║██║╚════██║██║     ██║   ██║██╔══██╗██║  ██║    ██║╚██╗██║   ██║   ██║   ", Color.Red);
            Console.WriteLine($"\t\t██████╔╝██║███████║╚██████╗╚██████╔╝██║  ██║██████╔╝    ██║ ╚████║██╗╚██████╔╝██╗", Color.Red);
            Console.WriteLine($"\t\t╚═════╝ ╚═╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝  ╚═╝╚═════╝     ╚═╝  ╚═══╝╚═╝ ╚═════╝ ╚═╝", Color.Red);
            Console.WriteLine("");
        }

        public static void SaveHits(string text)
        {
            File.WriteAllText("hits.txt", text);
        }
    }
}
