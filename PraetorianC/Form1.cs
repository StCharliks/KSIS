using System;
using System.Windows.Forms;
using PraetorianC.Client;
using PraetorianC.Commands;

namespace PraetorianC
{
    public partial class Form1 : Form
    {
        private Connection connection = new Connection();
        private Client.Client client = new Client.Client();

        public Form1()
        {
            InitializeComponent();
            connection.setConncetion(8080, "192.168.43.44");
            //client.Authorised = true;
        }

        private void push_button_Click(object sender, EventArgs e)
        {
            if (!client.Authorised)
            {
                MessageBox.Show("Вы не авторизованы", "Ошибка", MessageBoxButtons.OK);
                return;
            }
            //connection.setConncetion(8888, "127.0.0.1");
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                connection.send(client, Commands.AppComands.ClientCommands.LOAD, dialog.FileName,ref FileNameBox);
            }
        }

        private void signIn_Click(object sender, EventArgs e)
        {
            String login = loginBox.Text;
            String password = passwordBox.Text;
            client.Password = password;
            client.Login = login;
            //connection.setConncetion(8888, "127.0.0.1");
            client.Authorised = connection.Auth(AppComands.ClientCommands.AUTH, login, password);
        }

        private void reg_button_Click(object sender, EventArgs e)
        {
            String login = loginBox.Text;
            String password = passwordBox.Text;

            connection.Reg(AppComands.ClientCommands.REG, login, password);
        }

        private void FileNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void load_button_Click(object sender, EventArgs e)
        {
            if (!client.Authorised)
            {
                MessageBox.Show("Вы не авторизованы", "Ошибка", MessageBoxButtons.OK);
                return;
            }
        }

        private void reconnectButton_Click(object sender, EventArgs e)
        {
            if (connection.stream == null)
            {
                MessageBox.Show("После восстановления соединения необходмо переавторизоваться", "Ошибка", MessageBoxButtons.OK);
                try
                {
                    connection.setConncetion(8080, "192.168.43.44");
                } catch(Exception error)
                {
                    MessageBox.Show(error.Message, "Ошибка", MessageBoxButtons.OK);
                }
                finally
                {
                    client.Authorised = false;
                }
            }
            else
            {
                MessageBox.Show("Соединение уже установлено", "Ошибка", MessageBoxButtons.OK);
            }
        }
    }
}
