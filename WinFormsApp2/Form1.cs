using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private Queue<Panel> productQueue;
        private TcpListener server; // Сетевой сервер
        private Thread serverThread; // Поток для сервера

        public Form1()
        {
            InitializeComponent();
            productQueue = new Queue<Panel>();

            // Запуск сервера
            serverThread = new Thread(StartServer);
            serverThread.IsBackground = true;
            serverThread.Start();
        }

        // Метод для добавления продукта в очередь
        public void AddProduct(bool isGood)
        {
            if (queuePanel.InvokeRequired)
            {
                queuePanel.Invoke(new Action(() => AddProduct(isGood)));
                return;
            }

            if (productQueue.Count >= 5)
            {
                MessageBox.Show("Очередь переполнена. Добавление невозможно.");
                return;
            }

            // Создаём новый элемент
            Panel product = new Panel
            {
                Size = new Size(50, 50),
                BackColor = isGood ? Color.Green : Color.Yellow,
            };

            productQueue.Enqueue(product);
            queuePanel.Controls.Add(product); // Добавляем элемент на панель
            queuePanel.Controls.SetChildIndex(product, 0);

            MessageBox.Show($"Добавлен продукт: {(isGood ? "годный" : "брак")}");
            UpdateQueueDisplay();
        }

        // Метод для удаления продукта из очереди
        public void RemoveProduct()
        {
            if (queuePanel.InvokeRequired)
            {
                queuePanel.Invoke(new Action(() => RemoveProduct()));
                return;
            }

            if (productQueue.Count == 0 || queuePanel.Controls.Count == 0)
            {
                MessageBox.Show("Очередь пуста. Удаление невозможно.");
                return;
            }

            var panelToRemove = queuePanel.Controls[queuePanel.Controls.Count - 1];
            queuePanel.Controls.Remove(panelToRemove);
            panelToRemove.Dispose();
            productQueue.Dequeue();

            MessageBox.Show("Продукт удалён из очереди.");
            UpdateQueueDisplay();
        }

        // Метод для обновления отображения очереди
        private void UpdateQueueDisplay()
        {
            int x = 10; // Начальная позиция
            int y = 10; // Фиксированная вертикальная позиция

            foreach (Control product in productQueue)
            {
                product.Location = new Point(x, y);
                x += product.Width + 10; // Сдвиг вправо
            }
        }

        // Метод для запуска TCP-сервера
        private void StartServer()
        {
            try
            {
                server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
                server.Start();
                MessageBox.Show("Сервер запущен на 127.0.0.1:5000");

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient(); // Ожидание подключения клиента
                    Task.Run(() => HandleClient(client));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сервера: {ex.Message}");
            }
        }

        private async Task HandleClient(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();

                byte[] buffer = new byte[1024];
                int bytesRead;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    if (message == "ADD_GOOD")
                    {
                        AddProduct(true);
                    }
                    else if (message == "ADD_BAD")
                    {
                        AddProduct(false);
                    }
                    else if (message == "REMOVE")
                    {
                        RemoveProduct();
                    }
                    else
                    {
                        MessageBox.Show("Неверная команда");
                    }
                }
                client.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            server?.Stop();
        }
    }
}
