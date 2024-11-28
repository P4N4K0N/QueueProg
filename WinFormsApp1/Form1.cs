using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private TcpClient client;

        public Form1()
        {
            InitializeComponent();
            client = new TcpClient("127.0.0.1", 5000);
        }

        private void BtnCamera_Click(object sender, EventArgs e)
        {
            if (switchState.Checked)
            {
                _ = SendCommand("ADD_GOOD");
            }
            else
            {
                _ = SendCommand("ADD_BAD");
            }
        }

        private void BtnPusher_Click(object sender, EventArgs e)
        {
            _ = SendCommand("REMOVE");
        }

        private async Task SendCommand(string command)
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 5000);
                NetworkStream stream = client.GetStream();

                byte[] data = Encoding.UTF8.GetBytes(command);
                await stream.WriteAsync(data, 0, data.Length);
                await stream.FlushAsync();

                stream.Close();
                client.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка отправки команды: {ex.Message}");
            }
        }

        private void switchState_TextChange(object sender, EventArgs e)
        {
            if (switchState.Checked)
            {
                switchState.Text = "Годный";
            }
            else
            {
                switchState.Text = "Брак";
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Close();
        }
    }
}
