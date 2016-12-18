using Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class ServerR
    {
        static int port = 8005; // порт для приема входящих запросов
        Clients workerWithClients;
       public ServerR() { 
            List<Clients> clients = new List<Clients>();
            clients.Add(new Clients("Кубович", "Кубовчич Костянтин"));
            clients.Add(new Clients("Галяга", "Галяга Владислав"));
            clients.Add(new Clients("Дусанюк", "Дусанюк Ярослав"));

            // получаем адреса для запуска сокета
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(10);

                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    // получаем сообщение
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0; // количество полученных байтов
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    bool isClient = false;
                    string message = "";
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": Запрос на данные пользователя " + builder.ToString());
                    foreach (Clients client in clients)
                    {
                        if (builder.ToString() == client.ReturnName())
                        {
                            message = client.ReturnInformation();
                            workerWithClients = client;
                            isClient = true;

                        }
                    }
                    // отправляем ответ
                    if (!isClient)
                    {
                        data = Encoding.Unicode.GetBytes("Не найдено пользователя с таким именем");
                        handler.Send(data);
                    }
                    else
                    {
                        Console.WriteLine("Отправка нужной информации...");
                        data = Encoding.Unicode.GetBytes(message);
                        handler.Send(data);
                        StringBuilder builder2 = new StringBuilder();
                        do
                        {
                            Console.WriteLine("Получение обновленной информации...");
                            bytes = handler.Receive(data);
                            builder2.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (handler.Available > 0);
                        workerWithClients.ChoiseInformation(builder2.ToString());
                        // закрываем сокет
                        handler.Shutdown(SocketShutdown.Both);
                        handler.Close();
                    }
                   

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
