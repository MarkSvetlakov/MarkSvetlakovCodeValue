using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrimesCalculator
{
    public partial class Form1 : Form
    {
        public CancellationTokenSource TokenSource { get; private set; }
        public Form1()
        {
            InitializeComponent();
            this.TokenSource = CreateTokenSource();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            DoWorkAsync(TokenSource);
        }


        private void BtnCalcCount_Click(object sender, EventArgs e)
        {
            CountPrimesAsync(TokenSource);
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            TokenSource.Cancel();
            TokenSource = CreateTokenSource();
        }


        private void TextBoxStart_MouseClick(object sender, MouseEventArgs e)
        {
            this.TextBoxStart.Text = "";
        }


        private void TextBoxEnd_MouseClick(object sender, MouseEventArgs e)
        {
            this.TextBoxEnd.Text = "";
        }


        private void TextBoxOutFile_MouseClick(object sender, MouseEventArgs e)
        {
            this.TextBoxOutFile.Text = "";
        }


        private void BtnViewFile_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())));
        }


        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.ListBoxResult.Items.Clear();
            this.TbFirstNum.Clear();
            this.TbSecondNum.Clear();
            this.LbMessage.Text = "";
        }


        private CancellationTokenSource CreateTokenSource()
        {
            return new CancellationTokenSource();
        }


        private async void DoWorkAsync(CancellationTokenSource tokenSource)
        {
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
                return CalculatePrimes(number1, number2, tokenSource);
            }, tokenSource.Token).ContinueWith(prev =>
            {
                return CalculatePrimes(number1, number2, tokenSource);
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


        private List<int> CalculatePrimes(int firstNum, int secondNum, CancellationTokenSource tokenSource)
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


        private async void CountPrimesAsync(CancellationTokenSource tokenSource)
        {
            int countOfPrimes;
            int firstNumber, secondNumber;
            List<int> list = new List<int>();
            this.BtnViewFile.Visible = false;

            if ((!int.TryParse(TextBoxStart.Text, out firstNumber) || !int.TryParse(TextBoxEnd.Text, out secondNumber) || firstNumber < 0 || firstNumber > secondNumber))
            {
                this.LabelCount.Text = "Invalid numbers";
                return;
            }


            if (this.TextBoxOutFile.Text.Equals("Output File") || string.IsNullOrEmpty(this.TextBoxOutFile.Text))
            {
                this.LabelCount.Text = "Enter File Name";
                return;
            }

            this.BtnCalcCount.Enabled = false;
            this.LabelCount.Text = "Calculating...";


            var workTask = Task.Run(() =>
            {
                return CalculatePrimes(firstNumber, secondNumber, tokenSource);
            }, tokenSource.Token).ContinueWith(prev =>

            {
                return CalculatePrimes(firstNumber, secondNumber, tokenSource);
            }
            );

            list = await workTask;
            countOfPrimes = list.Count;

            if (countOfPrimes > 0)
            {
                this.LabelCount.Text = $"Prime numbers = {countOfPrimes}";
                this.BtnCalcCount.Enabled = true;
                WriteToFile();
            }
            else
            {
                this.LabelCount.Text = "Operation cancelled!";
                this.BtnCalcCount.Enabled = true;
            }
            
        }


        private void WriteToFile()
        {
            string path = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string fileName = Path.Combine(path, this.TextBoxOutFile.Text + ".txt");

            using (StreamWriter sw = (File.Exists(fileName)) ? File.AppendText(fileName) : File.CreateText(fileName))
            {
                sw.WriteLine(this.LabelCount.Text);
            }

            this.BtnViewFile.Visible = true;
        }
    }
}
