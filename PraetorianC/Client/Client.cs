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

namespace PraetorianC.Client
{
    class Connection
    {
        private TcpClient client = null;
        private String IP;
        private int socket;
        public NetworkStream stream = null;

        public Connection() { }


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


        public void Reg(Commands.AppComands.ClientCommands command, String data)
        {
            if (stream == null)
            {
                MessageBox.Show("Отсутствует соединение с сервером", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            //подготовка данных
            ulong length = (ulong)data.Length;
            byte[] convertedData = Encoding.Default.GetBytes(data);
            byte[] convertedlength = BitConverter.GetBytes(length);
            //Отправка данныъ
            stream.WriteByte((byte)command);
            stream.Write(convertedlength, 0, convertedlength.Length);
            stream.Write(convertedData, 0, data.Length);
            //Получение ответа от сервера
            byte[] answer = new byte[1];
            if (client.Available != 0)
            {
                stream.Read(answer, 0, 1);
                if (answer[0] == (byte)AppComands.ServerAnswers.OK)
                {
                    MessageBox.Show("Вы успешно зарегестрированы", "Ошибка", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Ошибка регистрации", "Ошибка", MessageBoxButtons.OK);
                }
            }
        }


        public bool Auth(Commands.AppComands.ClientCommands command, String data)
        {
            if (stream == null)
            {
                MessageBox.Show("Отсутствует соединение с сервером", "Ошибка", MessageBoxButtons.OK);
                return false;
            }
            //подготовка данных
            ulong length = (ulong)data.Length;
            byte[] convertedData = Encoding.Default.GetBytes(data);
            byte[] convertedlength = BitConverter.GetBytes(length);
            //Отправка данныъ
            stream.WriteByte((byte)command);
            stream.Write(convertedlength, 0, convertedlength.Length);
            stream.Write(convertedData, 0, data.Length);
            //Приём ответа от сервера
            byte[] answer = new byte[1];
            //stream.ReadTimeout = 1000;
            stream.Read(answer, 0, 1);
            if (answer[0] == (byte)Commands.AppComands.ServerAnswers.OK)
            {
                MessageBox.Show("Авторизация успешна", "", MessageBoxButtons.OK);
                return true;
            }
            else
            {
                MessageBox.Show("Ошибка:\n Неверный логин или пароль", "", MessageBoxButtons.OK);
                return false;
            }
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
                    stream.WriteByte((byte)command);
                    stream.Write(convertedLength, 0, convertedLength.Length);
                    stream.Write(convertedData, 0, convertedData.Length);
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
                        stream.WriteByte((byte)command);
                        stream.Write(convertedLength, 0, convertedLength.Length);
                        stream.Write(convertedData, 0, convertedData.Length);
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
    }

    class Client
    {

        #region FIELDS

            private bool isAuthorised = false;
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
            private String password;
        #endregion
    }
}
