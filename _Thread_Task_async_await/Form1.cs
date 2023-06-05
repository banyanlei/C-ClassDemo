namespace _Thread_Task_async_await
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //��û���겻�ܶ������߳�block��
            Thread.Sleep(3000);
            MessageBox.Show("�ز�1������", "������ʾ");
            Thread.Sleep(5000);
            MessageBox.Show("���1������", "������ʾ");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //lambda ���ʽ
            Thread t = new Thread(() =>
            {
                Thread.Sleep(3000);
                MessageBox.Show("�ز�2������", "������ʾ");
                Thread.Sleep(5000);
                MessageBox.Show("���2������", "������ʾ");
            });
            t.Start(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //�ײ�Ҳ��Threadʵ�֣�����չ���Ƽ�
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                MessageBox.Show("�ز�2������", "������ʾ");
                Thread.Sleep(5000);
                MessageBox.Show("���2������", "������ʾ");
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                MessageBox.Show("�ز�2������", "������ʾ");
            });
            Task.Run(() =>
            {
                Thread.Sleep(5000);
                MessageBox.Show("���2������", "������ʾ");
            });
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            //await���ǵȴ���ûawaitֱ�ӾͲ˺��ˡ�����
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
                MessageBox.Show("�ز�2������", "������ʾ");
            });
            await Task.Run(() =>
            {
                Thread.Sleep(5000);
                MessageBox.Show("���2������", "������ʾ");
            });
            MessageBox.Show("�˶������ˣ��������", "������ʾ");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<Task> ts = new List<Task>();
            ts.Add(Task.Run(() =>
            {
                Thread.Sleep(3000);
                MessageBox.Show("�ز�2������", "������ʾ");
            }));

            ts.Add(Task.Run(() =>
            {
                Thread.Sleep(5000);
                MessageBox.Show("���2������", "������ʾ");
            }));
            Task.WhenAll(ts).ContinueWith(t =>
            {
                MessageBox.Show($"�˶������ˣ��������", "������ʾ");

            });
        }
    }
}