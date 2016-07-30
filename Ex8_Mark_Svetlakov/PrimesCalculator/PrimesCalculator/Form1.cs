using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimesCalculator
{
    public partial class Form1 : Form
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            DoWorkAsync();
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }


        private async void DoWorkAsync()
        {
            tokenSource = new CancellationTokenSource();
            int number1, number2;
            List<int> list = new List<int>();

            if ((!int.TryParse(TbFirstNum.Text, out number1) || !int.TryParse(TbSecondNum.Text, out number2) || number1 < 0 || number1 > number2))
            {
                this.LbMessage.Text = "Invalid numbers";
                return;
            }

            this.BtnCalculate.Enabled = false;
            this.LbMessage.Text = "Calculating...";


            var someTask = Task.Run(() =>
            {
                return CalculatePrimes(number1, number2);
            }, tokenSource.Token).ContinueWith(prev =>

            {
                return CalculatePrimes(number1, number2);
            });

            list = await someTask;

            foreach (var item in list)
            {
                this.Invoke((MethodInvoker)(() =>
                this.ListBoxResult.Items.Add(item)
                ));
            }
            this.LbMessage.Text = "";
            this.BtnCalculate.Enabled = true;
        }



        private List<int> CalculatePrimes(int firstNum, int secondNum)
        {
            List<int> list = new List<int>();
            if (!tokenSource.IsCancellationRequested)
            {
                bool flag = false;
                if (firstNum == 1)
                {
                    firstNum++;
                }

                for (int i = firstNum; i <= secondNum; i++)
                {
                    for (int j = firstNum; j <= secondNum; j++)
                    {
                        tokenSource.Token.ThrowIfCancellationRequested();
                        if (i == j)
                        {
                            continue;
                        }
                        else if (i % j == 0)
                        {
                            flag = false;
                            break;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        list.Add(i);
                    }
                }
            }
            return list;
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.ListBoxResult.Items.Clear();
            this.TbFirstNum.Clear();
            this.TbSecondNum.Clear();
            this.LbMessage.Text = "";
        }
    }
}
