using System.Threading;
using System;
using System.Collections.Concurrent;
using System.Configuration;


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

        private class VersionInformationItem
        {
            public string AutoConfig { get; set; }
            public string MpSuffix { get; set; }
            public string Description { get; set; }
        }
        private readonly List<VersionInformationItem> VersionItems = new()
        {
            new VersionInformationItem { AutoConfig = "IMeasureIQ", MpSuffix = "VSA", Description = "VSA" },
            new VersionInformationItem { AutoConfig = "IMeasurePower", MpSuffix = "PM", Description = "Power Meter" },
            new VersionInformationItem { AutoConfig = "ISwitch", MpSuffix = "RFMUX", Description = "RF Multiplexer" },
            //new VersionInformationItem { AutoConfig = "IPowerSupply", MpSuffix = "PSU", Description = "Power Supply" },
            //new VersionInformationItem { AutoConfig = "IFanTray", MpSuffix = "FU", Description = "Fan Tray" },
            //new VersionInformationItem { AutoConfig = "MeasClient", MpSuffix = "MSRV", Description = "Measurement Server" },
        };
        public static string GlobalLockName { get; private set; } = "{DCCE47D0-6D43-47D3-B886-4D74868E9620}";

        public async static Task<string> GetDataAsync(string autoConfig)
        {
           
            if (autoConfig == "PM")
            {
                ////������ȥ����Ӱ���������ִ��
                //SystemException ex = new NotImplementedException();
                //throw ex;

                //һֱblock�������
                await Task.Delay(10000);
            }
            MessageBox.Show($"Reading version information for {autoConfig}");
            return await Task.FromResult(autoConfig);
        }
        private async void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Start");
            foreach (var item in VersionItems)
            {

                MessageBox.Show($"Reading version information for {item.AutoConfig}");
                try
                {
                    using (await AsyncLock.CreateAsync(GlobalLockName, 6000))
                    {
                        //string ip = await GetDataAsync(item.MpSuffix);


                        //SystemException ex = new NotImplementedException();
                        ////throw ex;
                        //MessageBox.Show($"Throw��");

                        //�Ľ����
                        var getDataTask = GetDataAsync(item.MpSuffix);
                        var timeoutTask = Task.Delay(6000); // 6�볬ʱ

                        var completedTask = await Task.WhenAny(getDataTask, timeoutTask);
                        if (!getDataTask.IsCompleted)
                        {
                            throw new TimeoutException($"ConnectSocketOnDemand timed out after  6 seconds.");
                        }

                        //if (completedTask == timeoutTask)
                        //{
                        //    MessageBox.Show($"GetDataAsync operation timed out after 6 seconds for {item.MpSuffix}");
                        //    continue; // ������һ��ѭ��
                        //}

                        string ip = await getDataTask;
                        MessageBox.Show($"Successfully reading {ip}");

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show($"Unable to connect to {item.MpSuffix}��{ex.Message}");
                }
                //MessageBox.Show($"Finish");
            }
        }

    }

}