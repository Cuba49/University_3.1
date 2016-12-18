using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Client
{
    public class ClientR
    {
        static int port = 8005; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
                                             // адрес и порт сервера, к которому будем подключаться
        private string message;
        private byte[] data;

        public ClientR()
        {
            Base();
        }

        public void Base()
        {
           
       
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // подключаемся к удаленному хосту
                socket.Connect(ipPoint);
                Console.Write("Введите имя пользователя:");
                message = Console.ReadLine();
                data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256]; // буфер для ответа
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байт

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                } while (socket.Available > 0);
                Console.WriteLine("Ответ сервера(Информация): " + builder.ToString());
                if (builder.ToString() != "Не найдено пользователя с таким именем")
                {
                    Console.WriteLine("Желаете изменить информацию? (1-нет, 2-да)");
                    int i = int.Parse(Console.ReadLine());
                    switch (i)
                    {
                        case 1:
                            {

                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Введите новую информацию:");
                                message = Console.ReadLine();
                                data = Encoding.Unicode.GetBytes(message);
                                socket.Send(data);
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Некорректно введено число");
                                Console.ReadLine();
                                break;
                            }
                    }
                }
                Console.WriteLine("Информация успешно изменена!");
                // закрываем сокет
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                Base();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           



        }
    }
}
