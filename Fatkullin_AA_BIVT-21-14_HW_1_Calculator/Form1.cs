using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Fatkullin_AA_BIVT_21_14_HW_1_Calculator
{
    public partial class Form1 : Form
    {
        class Calculator                            // Класс калькулятор
        {
            private string number1="", number2="";        //
            private string operation = "";
            private double dn1=0, dn2=0;                // Поля класс калькулятор
            public double Dn1
            { get { return dn1;} set { dn1 = value; } }
            public double Dn2
            { get { return dn2; } set { dn2 = value; } }
            public string Operation
            {get{return operation;}set{operation = value;}}
            public string Number1
            { get { return number1; } set { number1 = value; } }
            public string Number2
            { get { return number2; } set { number2 = value; } }                    // Свойства для доступа к полям класса
            public double Plus(double a, double b)
            {
                return a + b;
            }                                   // Методы для арифметических операций
            public double Minus(double a, double b)
            {
                return a - b;
            }
            public double Mltiply(double a, double b)
            {
                return a * b;
            }
            public double IntregerDivision(double a, double b)
            {
                return a / b;
            }
            public double RemainderDivision(double a, double b)
            {
                return a % b;
            }                      //
            public string Clear(string a) => "0";       // Метод для очистки поля ввода
        }

        class RomanianNumbers                       // Класс для конвертации римских чисел в арабские 
        {
            private static readonly Dictionary<char, int> romanDigits =
        new Dictionary<char, int> {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };
            public static int Decode(string s)
            {
                int total = 0;
                int prev = 0;
                for(int i = s.Length-1; i >= 0; i--)
                {
                    int curr = romanDigits[s[i]];
                    if (curr < prev)
                        total = total - curr;
                    else if (curr >= prev)
                        total = total + curr;
                    prev = curr;
                }
                return total;
            }
        }

        private bool TryCatch(string field)          // Метод на проверку правильности ввода текста
        {
            Regex IsRome = new Regex(@"[^IVXLCDM]");
            Regex Letters = new Regex(@"/[A-Za-z\p{IsCyrillic}]");
            Regex Numbers = new Regex(@"[^\d]");
            bool checkLetters = Letters.IsMatch(field);
            bool checkIsRome = IsRome.IsMatch(field);
            bool checkNumbers = Numbers.IsMatch(field);
            if (!checkIsRome && checkNumbers && !checkLetters) return true;
            else if (checkIsRome && !checkNumbers && !checkLetters) return true;
            else return false;
        }

        private static string NumberToRoman(double number)
        {
            if (number < 0 || number > 3999)
            {
                MessageBox.Show("Римские числа могут быть только в диапазоне: 0 - 3,999.");
                return "";
            }
            if (number == 0) return "N";
            int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] numerals = new string[]
            { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < 13; i++)
            {
                while (number >= values[i])
                {
                    number -= values[i];
                    result.Append(numerals[i]);
                }
            }
            return result.ToString();
        }   // Метод конвертации арабских цифр в римские

        Calculator calculator = new Calculator();   // Создание экземпляра класса калькулятор

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "0";
            Logger.Init();
        }

        private void ExchangeColor(Button a)
        {
            a.BackColor = Color.Coral;
        }       // Метод возвращения цвета кнопки к исходному

        private void button_clear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = calculator.Clear(richTextBox1.Text);
            label2.Text = "";
            calculator.Dn1 = 0;
            calculator.Dn2 = 0;
            calculator.Number1 = "";
            calculator.Number2 = "";
            calculator.Operation = "";
            ExchangeColor(button_multiply); ExchangeColor(button_plus);
            ExchangeColor(button_minus); ExchangeColor(button_intdivide);
            ExchangeColor(button_remdivide);
            Logger.EndLine();
            Logger.Log("Clear");
            Logger.EndLine();
        }

        private double result = 0;
        private bool Flag = false;
        Regex IsRome = new Regex(@"[^IVXLCDM]");
        Regex Numbers = new Regex(@"[\d]");

        private void button_equal_Click(object sender, EventArgs e)
        {
            Logger.Init();
            ExchangeColor(button_multiply); ExchangeColor(button_plus);
            ExchangeColor(button_minus); ExchangeColor(button_intdivide);
            ExchangeColor(button_remdivide);
            if (!TryCatch(richTextBox1.Text) || IsRome.IsMatch(richTextBox1.Text))
            {
                MessageBox.Show("Ошибка! Неверный формат введённых данных. Исходными данными могуты быть ТОЛЬКО римские числа");
                Logger.Log(richTextBox1.Text + " Ошибка! Неверный формат введённых данных. Исходными данными могуты быть ТОЛЬКО римские числа");
                Logger.EndLine();
            }
            else if (calculator.Operation == "")
            {
                MessageBox.Show("Необходимо выбрать операцию над числами");
                Logger.Log(richTextBox1.Text +  " Необходимо выбрать операцию над числами");
                Logger.EndLine();
            }
            else if (calculator.Number1 == "")
            {
                MessageBox.Show("Необходимо ввести два числа");
                Logger.Log(richTextBox1.Text +  " Необходимо ввести два числа");
                Logger.EndLine();
            }
            else if (Numbers.IsMatch(calculator.Number1) || Numbers.IsMatch(calculator.Number2))
            {
                MessageBox.Show("Ошибка! Неверный формат введённых данных. Исходными данными могут быть ТОЛЬКО римские числа");
                Logger.Log(richTextBox1.Text +  " Ошибка! Неверный формат введённых данных. Исходными данными могут быть ТОЛЬКО римские числа");
                Logger.EndLine();
            }
            else
            {
                if (!IsRome.IsMatch(richTextBox1.Text))
                {
                    calculator.Number2 = richTextBox1.Text;
                    calculator.Dn1 = RomanianNumbers.Decode(calculator.Number1);
                    calculator.Dn2 = RomanianNumbers.Decode(richTextBox1.Text);
                    switch (calculator.Operation)
                    {
                        case "+":
                            result = calculator.Plus(calculator.Dn1, calculator.Dn2);
                            break;
                        case "-":
                            result = calculator.Minus(calculator.Dn1, calculator.Dn2);
                            break;
                        case "*":
                            result = calculator.Mltiply(calculator.Dn1, calculator.Dn2);
                            break;
                        case "/":
                            result = calculator.IntregerDivision(calculator.Dn1, calculator.Dn2);
                            break;
                        case "%":
                            result = calculator.RemainderDivision(calculator.Dn1, calculator.Dn2);
                            break;
                    }
                    string temp = richTextBox1.Text;
                    if(result < 0)
                    {
                        MessageBox.Show("Римские числа не могут быть отрицательными!");
                        Logger.Log(temp + " Римские числа не могут быть отрицательными!");
                        Logger.EndLine();
                        return;
                    }
                    else if (result < 1)
                    {
                        label2.Text = richTextBox1.Text;
                        richTextBox1.Text = "0";
                        Logger.Log(temp + " = 0");
                        return;
                    }
                    else if(Numbers.IsMatch(richTextBox1.Text))
                    {
                        Logger.Log("Ошибка! Неверный формат введённых данных.Исходными данными могуты быть ТОЛЬКО римские числа");
                        Logger.EndLine();
                    }
                    else
                    {
                        richTextBox1.Text = NumberToRoman(result);
                        if (result > 3999)
                        {
                            Logger.Log(temp + " Римские числа могут быть только в диапозоне: 0 - 3,999.");
                            Logger.EndLine();
                            return;
                        }
                    }
                    Logger.Log(calculator.Number2 + " = " + richTextBox1.Text);
                    label2.Text += calculator.Number2 + " = " + richTextBox1.Text;
                    calculator.Dn1 = 0;
                    calculator.Dn2 = 0;
                    calculator.Number1 = "";
                    calculator.Number2 = "";
                    calculator.Operation = "";
                    Flag = true;
                    Logger.EndLine();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)                  // События нажатия на Римские цифры
        {
            Button button = (Button)sender;
            if (Flag)
            {
                Flag = false;
                richTextBox1.Text = button.Text;
            }
            else
            {
                if (IsRome.IsMatch(richTextBox1.Text)) richTextBox1.Text = button.Text;
                else richTextBox1.Text = richTextBox1.Text + button.Text;
            }
        }
        private void button_multiply_Click(object sender, EventArgs e)          // Событие нажатия на клавиши операций
        {
            Logger.Init();
            Button button = (Button)sender;
            if (!TryCatch(richTextBox1.Text) || Numbers.IsMatch(richTextBox1.Text))
            {
                MessageBox.Show("Ошибка! Неверный формат введённых данных. Исходными данными могуты быть ТОЛЬКО римские числа");
                Logger.EndLine();
                Logger.Log(richTextBox1.Text + button.Text + "Ошибка! Неверный формат введённых данных. Исходными данными могуты быть ТОЛЬКО римские числа");
            }
            else
            {
                button.BackColor = Color.Orange;
                calculator.Operation = button.Text;
                calculator.Number1 = richTextBox1.Text;
                label2.Text = calculator.Number1 + " " + calculator.Operation + " ";
                Flag = true;
                Logger.Log(richTextBox1.Text + " "+ calculator.Operation);
            }
        }
        private void button13_Click(object sender, EventArgs e)                 // Событие нажатия на клавиушу Перевести в арабскую систему
        {
            if (!TryCatch(richTextBox1.Text))
            {
                MessageBox.Show("Ошибка! Неверный формат введённых данных.");
                Logger.Log("Ошибка! Неверный формат введённых данных.");
                Logger.EndLine();
            }
            else if (!IsRome.IsMatch(richTextBox1.Text))
            {
                string temp = richTextBox1.Text;
                int a = RomanianNumbers.Decode(richTextBox1.Text);
                richTextBox1.Text = a.ToString();
                Logger.Log(temp+" = " + a.ToString() + " перевод из римской системы в арабскую");
                Logger.EndLine();
            }
        }

        private void button14_Click(object sender, EventArgs e)                 // Событие нажатия на клавиушу Перевести в римскую систему
        {
            if (!TryCatch(richTextBox1.Text))
            {
                MessageBox.Show("Ошибка! Неверный формат введённых данных.");
                Logger.Log("Ошибка! Неверный формат введённых данных.");
                Logger.EndLine();
            }
            else if (Numbers.IsMatch(richTextBox1.Text))
            {
                string temp = richTextBox1.Text;
                string a = NumberToRoman(Convert.ToDouble(richTextBox1.Text));
                if(Convert.ToDouble(richTextBox1.Text) > 3999)
                {
                    Logger.EndLine();
                    Logger.Log("Римские числа могут быть только в диапазоне: 0 - 3,999! " + richTextBox1.Text + " > 3999");
                    Logger.EndLine();
                    return;
                }
                richTextBox1.Text = a.ToString();
                Logger.Log(temp + " = " + a.ToString() + " перевод из арабской системы в римскую");
                Logger.EndLine();
            }
        }

        private void button_OpenLog_Click(object sender, EventArgs e)
        {
            new LogFileReader().ShowDialog();
        }       // Открывает журнал 

        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Logger.Close();
        }
    }
}