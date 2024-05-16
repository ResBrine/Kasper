using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace ServerHost
{
    static class DataManager
    {
        public static void OpenConfigServer()
        {
            if (File.Exists(INFO.PATH_CONFIG_SERVER))
                using (FileStream fileStream = File.OpenRead(INFO.PATH_CONFIG_SERVER))
                {
                    byte[] buffer = new byte[fileStream.Length]; //Создание буфера
                    fileStream.Read(buffer, 0, buffer.Length); //Чтение всего файла в буфер
                    string textFromFile = Encoding.Default.GetString(buffer); //Конвертация в String буфера
                    Console.WriteLine("Настройка сервера:");
                    foreach (string line in textFromFile.Split('\n')) //Построчное перебирание файла
                    {
                        Console.WriteLine("\t"+line);
                        switch (line.Split(':')[0]) //Деление на КЛЮЧ:ДАННЫЕ
                        {
                            case "PORT":
                                INFO.PORT = int.Parse(line.Split(':')[1]);
                                break;
                        }
                    }
                }
            else
            {
                Console.WriteLine($"Файл конфигурации был не найден");
                Console.WriteLine($"Создаётся новый файл конфиграции по пути {INFO.PATH_CONFIG_SERVER}");
                SaveConfigServer();

            }
        }
        public static void SaveConfigServer() 
        {
            FileStream fileStream = new FileStream(INFO.PATH_CONFIG_SERVER, FileMode.Create);
            WriteLine(fileStream, "PORT:" + INFO.PORT);
            fileStream.Close();

        }

        public static Clients LoadListClients() 
        {
            try
            {
                if (File.Exists(INFO.PATH_LIST_USERS))
                    using (FileStream fileStream = File.OpenRead(INFO.PATH_LIST_USERS))
                    {
                        byte[] buffer = new byte[fileStream.Length]; //Создание буфера
                        fileStream.Read(buffer, 0, buffer.Length); //Чтение всего файла в буфер
                        string textFromFile = Encoding.Default.GetString(buffer); //Конвертация в String буфера
                        Console.WriteLine("Загрузка клиентов");
                        Clients clients = new Clients();
                        foreach (var item in textFromFile.Split('\n'))
                        {
                            if (item.Trim() != "" && item.Trim() != null)
                            {
                                Console.WriteLine("Расшифровка:\n" + item);
                                clients.clients.Add(new Client(JsonSerializer.Deserialize<ClientJson>(item)));
                            }
                        }
                        return clients;
                    }
                else
                {
                    Console.WriteLine($"Файл списка пользователей");
                    Console.WriteLine($"Создаётся новый файл списка пользователей по пути {INFO.PATH_LIST_USERS}");
                    SaveListClients(new Clients());

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
         

            return new Clients();
        }
        private static List<Client> GetClients(string data) {

            List<Client> clients = new List<Client>();
            string stringClient = "";
            int maxLine = data.Split('\n').Length;

            for (int countLines = 0; countLines
                < maxLine; countLines++)
            {
                string lastLine = "";

                if (countLines!=0)
                    lastLine = data.Split('\n')[countLines - 1];

                string line = data.Split('\n')[countLines];

                if (line == "{\r")
                    stringClient = lastLine;
                
                stringClient += line;

                if (line == "}\r" || line == "}")
                    clients.Add(GetClient(stringClient));
                
                if (countLines == maxLine)
                    break;
            }

            return clients;
        }

        private static Client GetClient(string data)
        {
            
            string userName = "";
            string password = "";
            int id = 0;

            for (int countLines = 0; countLines
                < data.Split('\r').Length; countLines++)
            {
                string line = data.Split('\r')[countLines];

                if (countLines == 0)
                    userName = line;
                if (line.StartsWith("id="))
                    id = int.Parse(line.Substring(3));
                if (line.StartsWith("password="))
                    password = line.Substring(9);
            }

            Client client = new Client(id);
            client.setUserName(userName);
            client.setPassword(password);

            return client;
        }
        public static void SaveListClients(Clients clients)
        {
            try
            {
                FileStream fileStream = new FileStream(INFO.PATH_LIST_USERS, FileMode.Create);
                foreach (var item in clients.GetClients())
                {
                    WriteLine(fileStream, JsonSerializer.Serialize(item.GetClientJson()));

                }
                fileStream.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            

        }
        private static void WriteLine(FileStream fileStream, object text)
        {
            byte[] input = Encoding.Default.GetBytes(text.ToString() + "\n");
            fileStream.Write(input, 0, input.Length);
        }
    }
}
