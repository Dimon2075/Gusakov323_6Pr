using System;
using System.Diagnostics;
namespace BankAccountNS
{
    /// <summary>
    /// Класс демонстрирует работу с банковским счётом.
    /// </summary>
    public class BankAccount
    {
        private readonly string m_customerName;
        private double m_balance;

        /// <summary>Текст ошибки, когда сумма списания превышает баланс.</summary>
        public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";
        /// <summary>Текст ошибки, когда сумма списания меньше нуля.</summary>
        public const string DebitAmountLessThanZeroMessage = "Debit amount is less than zero";

        /// <summary>
        /// Приватный конструктор без параметров (не используется).
        /// </summary>
        private BankAccount() { }

        /// <summary>
        /// Инициализирует новый экземпляр класса BankAccount с указанным именем клиента и балансом.
        /// </summary>
        /// <param name="customerName">Имя клиента.</param>
        /// <param name="balance">Начальный баланс счета.</param>
        public BankAccount(string customerName, double balance)
        {
            m_customerName = customerName;
            m_balance = balance;
        }

        /// <summary>
        /// Получает имя клиента.
        /// </summary>
        public string CustomerName
        {
            get { return m_customerName; }
        }

        /// <summary>
        /// Получает текущий баланс счёта.
        /// </summary>
        public double Balance
        {
            get { return m_balance; }
        }

        /// <summary>
        /// Списывает сумму с банковского счёта.
        /// </summary>
        /// <param name="amount">Сумма для списания.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Возникает, если сумма больше текущего баланса или меньше нуля.
        /// </exception>
        public void Debit(double amount)
        {
            if (amount > m_balance)
{
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage);
            }
            if (amount < 0)
{
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);
            }
            m_balance -= amount;
        }

        /// <summary>
        /// Зачисляет сумму на банковский счёт.
        /// </summary>
        /// <param name="amount">Сумма для зачисления.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Возникает, если сумма меньше нуля
        /// </exception>
        public void Credit(double amount)
        {
            if (amount < 0)
{
                throw new ArgumentOutOfRangeException("amount");
            }

            m_balance += amount;
        }

        /// <summary>
        /// Точка входа: демонстрирует работу с классом BankAccount.
        /// </summary>
        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr.Roman Abramovich", 11.99);
            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
            Console.ReadLine();
        }
    }
}