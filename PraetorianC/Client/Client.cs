using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using PraetorianC.Commands;
using System.IO;
using System.Security.Cryptography;
using Server.Security;
using ConsoleApplication2;

namespace PraetorianC.Client
{
    class Connection
    {
        #region FIELDS
        private TcpClient client = null;
        private String IP;
        private int socket;
        public NetworkStream stream = null;
        //private AES_DiffieHellman AES = new AES_DiffieHellman();
        private DIFFIE_HELMAN AES = new DIFFIE_HELMAN();
        #endregion 

        #region CONSTRUCTORS
        public Connection() { }
        #endregion

        #region METHODS

        public void templateSend(AppComands.ClientCommands command, byte[] length, byte[] data, NetworkStream stream)
        {
            stream.WriteByte((byte)command);
            stream.Write(length, 0, length.Length);
            stream.Write(data, 0, data.Length);
        }

        public void setConncetion(int socket, String IP)
        {
            try
            {
                this.IP = IP;
                this.socket = socket;

                client = new TcpClient();
                client.Connect(IPAddress.Parse(this.IP), this.socket);
                stream = client.GetStream();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK);
            }
        }


        public void Reg(Commands.AppComands.ClientCommands command, String login, String password)
        {
            if (stream == null)
            {
                MessageBox.Show("Отсутствует соединение с сервером", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            try
            {
                SHA256 SHA = SHA256Managed.Create();
                //подготовка к отправке логина
                byte[] convertedData = SHA.ComputeHash(Encoding.Default.GetBytes(login));
                ulong length = (ulong)convertedData.Length;
                byte[] convertedlength = BitConverter.GetBytes(length);
                //Отправка логина
                templateSend(command, convertedlength, convertedData, stream);
                /*stream.WriteByte((byte)command);
                stream.Write(convertedlength, 0, convertedlength.Length);
                stream.Write(convertedData, 0, convertedData.Length);*/
                //подготовка к отправке пароля
                convertedData = SHA.ComputeHash(Encoding.Default.GetBytes(password));
                length = (ulong)convertedData.Length;
                convertedlength = BitConverter.GetBytes(length);
                //отправка пароля
                stream.Write(convertedlength, 0, convertedlength.Length);
                stream.Write(convertedData, 0, convertedData.Length);
                //Получение ответа от сервера
                byte[] answer = new byte[1];
                
                    stream.Read(answer, 0, 1);
                    if (answer[0] == (byte)AppComands.ServerAnswers.OK)
                    {
                        MessageBox.Show("Вы успешно зарегестрированы", "Praetorian", MessageBoxButtons.OK);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка регистрации", "Ошибка", MessageBoxButtons.OK);
                    }
                
            } catch(Exception error)
            {
                MessageBox.Show("Неожиданно разорвалось соединение", "Ошибка", MessageBoxButtons.OK);
            }
        }


        public bool Auth(Commands.AppComands.ClientCommands command, String login, String password)
        {
            //AES.GetSecretKey(new byte[140], new byte[16]);
            if (stream == null)
            {
                MessageBox.Show("Отсутствует соединение с сервером", "Ошибка", MessageBoxButtons.OK);
                return false;
            }
            SHA256 SHA = SHA256Managed.Create();

            try
            {
                //подготовка к отправке логина
                byte[] convertedData = SHA.ComputeHash(Encoding.Default.GetBytes(login));
                ulong length = (ulong)convertedData.Length;
                byte[] convertedlength = BitConverter.GetBytes(length);
                //Отправка логина
                templateSend(command, convertedlength, convertedData, stream);
                /*stream.WriteByte((byte)command);
                stream.Write(convertedlength, 0, convertedlength.Length);
                stream.Write(convertedData, 0, convertedData.Length);*/
                //подготовка к отправке пароля
                convertedData = SHA.ComputeHash(Encoding.Default.GetBytes(password));
                length = (ulong)convertedData.Length;
                convertedlength = BitConverter.GetBytes(length);
                //отправка пароля
                stream.Write(convertedlength, 0, convertedlength.Length);
                stream.Write(convertedData, 0, convertedData.Length);
                //Приём ответа от сервера
                byte[] answer = new byte[1];
                //stream.ReadTimeout = 1000;
                stream.Read(answer, 0, 1);
                if (answer[0] == (byte)Commands.AppComands.ServerAnswers.OK)
                {
                    //MessageBox.Show("Авторизация успешна", "", MessageBoxButtons.OK);
                        //byte[] convertedData = null;
                        //ulong length = 0;
                        //Получаем длину ключа и ключ
                        convertedData = new byte[sizeof(ulong)];
                        stream.Read(convertedData, 0, sizeof(ulong));
                        length = BitConverter.ToUInt64(convertedData, 0);
                        //ключ шифрования
                        byte[] key = new byte[length];
                        stream.Read(key, 0, key.Length);

                        //Получаем длину потока инициализации и сам поток

                        stream.Read(convertedData, 0, sizeof(ulong));//поток инициализации тут не нужен
                        length = BitConverter.ToUInt64(convertedData, 0);
                        byte[] initStream = new byte[length];
                        stream.Read(initStream, 0, initStream.Length);
                        AES.CreateSecretKey(key);
                        //Отправка зашифрованных хешей
                        //подготовка к отправке логина
                        convertedData = SHA.ComputeHash(Encoding.Default.GetBytes(login));
                        convertedData = AES.Send(AES.secretKey, convertedData);
                        length = (ulong)convertedData.Length;
                        convertedlength = BitConverter.GetBytes(length);
                    //Отправка логина
                        templateSend(command, convertedlength, convertedData, stream);
                        /*stream.WriteByte((byte)command);
                        stream.Write(convertedlength, 0, convertedlength.Length);
                        stream.Write(convertedData, 0, convertedData.Length);*/
                        //подготовка к отправке пароля
                        convertedData = SHA.ComputeHash(Encoding.Default.GetBytes(password));
                        convertedData = AES.Send(AES.secretKey, convertedData);
                        length = (ulong)convertedData.Length;
                        convertedlength = BitConverter.GetBytes(length);
                        //отправка пароля
                        stream.Write(convertedlength, 0, convertedlength.Length);
                        stream.Write(convertedData, 0, convertedData.Length);

                        stream.Read(answer, 0, 1);
                        if (answer[0] == (byte)Commands.AppComands.ServerAnswers.OK)
                        {
                            MessageBox.Show("Авторизация успешна", "", MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка второго этапа авторизации\n Вероятность попытки взлома", "", MessageBoxButtons.OK);
                            return false;
                        }

                    
                    return true;
                }
                else
                {
                    MessageBox.Show("Ошибка:\n Неверный логин или пароль", "", MessageBoxButtons.OK);
                    return false;
                }
            }catch(Exception error)
            {
                MessageBox.Show("Неожиданно разорвалось соединение", "", MessageBoxButtons.OK);
            }
            return false;
        }


        public void Get(Client client, Commands.AppComands.ClientCommands command, String data)
        {
            if (data == null)
            {
                MessageBox.Show("Выберите файл, который хотите получить", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            if (client.Authorised)
            {
                //обработка данных
                ulong dataLength = (ulong)data.Length;
                byte[] convertedLength = null;
                byte[] convertedData = null;

                convertedLength = BitConverter.GetBytes(dataLength);
                convertedData = Encoding.Default.GetBytes(data);

                //Отправка на сервер первой порции данных
                if (stream != null)
                {
                    templateSend(command, convertedLength, convertedData, stream);
                    /*stream.WriteByte((byte)command);
                    stream.Write(convertedLength, 0, convertedLength.Length);
                    stream.Write(convertedData, 0, convertedData.Length);*/
                }
                else
                {
                    MessageBox.Show("Ошибка отправки, соединение не существует", "Ошибка", MessageBoxButtons.OK);
                    return;
                }

                //получение файла
                byte[] serverAnswer = new byte[1];
                stream.Read(serverAnswer, 0, sizeof(byte));

                if (serverAnswer[0] == (byte)Commands.AppComands.ServerAnswers.OK)
                {
                    stream.Read(convertedLength, 0, sizeof(ulong));
                    dataLength = BitConverter.ToUInt64(convertedLength, 0);
                    convertedData = new byte[dataLength];
                    stream.Read(convertedData, 0, convertedData.Length);
                }
                else
                {
                    MessageBox.Show("Ошибка сервера", "Ошибка", MessageBoxButtons.OK);
                }

                SaveFileDialog dialog = new SaveFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (FileStream fs = new FileStream(data, FileMode.OpenOrCreate))
                        {
                            fs.Write(convertedData, 0, convertedData.Length);
                        }
                    } catch(Exception error)
                    {
                        MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK);
                    }
                }

            }
            else
            {
                MessageBox.Show("Пожалуйста авторизуйтесь", "Ошибка", MessageBoxButtons.OK);
            }
        }

        public void send(Client client,Commands.AppComands.ClientCommands command, String data, ref ListBox FileBox)
        {
            if (stream == null)
            {
                MessageBox.Show("Отсутствует соединение с сервером", "Ошибка", MessageBoxButtons.OK);
                return;
            }

            if (client.Authorised)
            {
                if (command == AppComands.ClientCommands.LOAD)
                {
                    //обработка данных
                    ulong dataLength = (ulong)data.Length;
                    byte[] convertedLength = null;
                    byte[] convertedData = null;

                    convertedLength = BitConverter.GetBytes(dataLength);
                    convertedData = Encoding.Default.GetBytes(data);
                    
                    //Отправка на сервер первой порции данных
                    if (stream != null)
                    {
                        templateSend(command, convertedLength, convertedData, stream);
                        /*stream.WriteByte((byte)command);
                        stream.Write(convertedLength, 0, convertedLength.Length);
                        stream.Write(convertedData, 0, convertedData.Length);*/
                    }
                    else
                    {
                        return;
                    }
                    //Подготовка к отправке второй порции данных

                    try
                    {
                        using (FileStream fs = new FileStream(data, FileMode.Open))
                        {
                            convertedData = new byte[fs.Length];
                            fs.Read(convertedData, 0, convertedData.Length);
                        }
                    } catch (Exception error)
                    {
                        MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK);
                    }

                    //шифруем перед отправкой
                    //AES.Encrypt(ref convertedData);

                    convertedLength = BitConverter.GetBytes((ulong)convertedData.Length);

                    //Отправка второй порции данных
                    stream.Write(convertedLength, 0, convertedLength.Length);
                    stream.Write(convertedData, 0, convertedData.Length);

                    if (!FileBox.Items.Contains(data))
                        FileBox.Items.Add(Path.GetFileName(data));
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста авторизуйтесь", "Ошибка", MessageBoxButtons.OK);
            }
        }

        #endregion
    }

    class Client
    {

        #region FIELDS

            private bool isAuthorised = true;
            public bool Authorised
            {
                set
                {
                    isAuthorised = value;
                }
                get
                {
                    return isAuthorised;
                }
            }


            private ulong key;
            public ulong Key
            {
                get
                {

                    return key;
                }
                set
                {
                    key = value;
                }
            }


            private String login;
            public String Login
            {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
            }
            private String password;
            public String Password
            {
                get
                {
                    return password;
                }
                set
                {
                    password = value;
                }
            }
        #endregion
    }
}
