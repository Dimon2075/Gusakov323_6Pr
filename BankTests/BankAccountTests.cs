using BankAccountNS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Principal;

namespace BankTests
{
    /// <summary>
    /// Класс содержит модульные тесты для проверки методов Debit и Credit класса BankAccount.
    /// </summary>
    [TestClass]
    public class BankAccountTests
    {
        /// <summary>
        /// Проверяет корректное списание суммы с баланса при допустимых значениях.
        /// </summary>
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr.Roman Abramovich", beginningBalance);
            // Act
            account.Debit(debitAmount);
            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        /// <summary>
        /// Проверяет выброс исключения при попытке списать отрицательную сумму.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr.Roman Abramovich", beginningBalance);
            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() =>
            account.Debit(debitAmount));
        }

        /// <summary>
        /// Проверяет выброс исключения при попытке списать сумму, превышающую баланс.
        /// </summary>
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 23.00;
            BankAccount account = new BankAccount("Mr.Roman Abramovich", beginningBalance);
            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message,
                BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }
        /// <summary>
        /// Проверяет корректное зачисление суммы на счёт.
        /// </summary>
        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 10.00;
            double creditAmount = 5.50;
            double expected = 15.50;
            BankAccount account = new BankAccount("Mr.Roman Abramovich", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            Assert.AreEqual(expected, account.Balance, 0.001, "Account not credited correctly");
        }

        /// <summary>
        /// Проверяет выброс исключения при попытке зачислить отрицательную сумму.
        /// </summary>
        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 10.00;
            double creditAmount = -1.00;
            BankAccount account = new BankAccount("Mr.Roman Abramovich", beginningBalance);

            // Act & Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                account.Credit(creditAmount));
        }

        /// <summary>
        /// Проверяет, что зачисление суммы 0 не меняет баланс.
        /// </summary>
        [TestMethod]
        public void Credit_WhenAmountIsZero_ShouldNotChangeBalance()
        {
            // Arrange
            double beginningBalance = 10.00;
            double creditAmount = 0.00;
            BankAccount account = new BankAccount("Mr.Roman Abramovich", beginningBalance);

            // Act
            account.Credit(creditAmount);

            // Assert
            Assert.AreEqual(beginningBalance, account.Balance, 0.001, "Balance should not change when crediting 0.");
        }
    }
}
