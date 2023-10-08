using System;
using System.Collections.Generic;
using System.Text;

// Lớp mô hình tài khoản
class Account
{
    public string AccountNumber { get; set; }
    public string OwnerName { get; set; }
    public string CMND { get; set; }
    public double Balance { get; set; }
    public double InterestRate { get; set; }

    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
}

// Lớp giao dịch
class Transaction
{
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; }
    public double Amount { get; set; }
}

// Lớp ngân hàng
class Bank
{
    private List<Account> accounts = new List<Account>();

    // Mở tài khoản mới
    public void OpenAccount(string accountNumber, string ownerName, string cmnd, double initialBalance, double interestRate)
    {
        Account account = new Account
        {
            AccountNumber = accountNumber,
            OwnerName = ownerName,
            CMND = cmnd,
            Balance = initialBalance,
            InterestRate = interestRate
        };
        accounts.Add(account);

        // Log giao dịch mở tài khoản
        account.Transactions.Add(new Transaction
        {
            TransactionDate = DateTime.Now,
            TransactionType = "Mở tài khoản",
            Amount = initialBalance
        });
    }

    // Nhập tiền vào tài khoản
    public void Deposit(string accountNumber, double amount, DateTime transactionDate)
    {
        Account account = FindAccount(accountNumber);
        if (account != null)
        {
            account.Balance += amount;

            // Log giao dịch nạp tiền
            account.Transactions.Add(new Transaction
            {
                TransactionDate = transactionDate,
                TransactionType = "Nhập tiền",
                Amount = amount
            });
        }
        else
        {
            Console.WriteLine("Không tìm thấy tài khoản.");
        }
    }

    // Rút tiền từ tài khoản
    public void Withdraw(string accountNumber, double amount, DateTime transactionDate)
    {
        Account account = FindAccount(accountNumber);
        if (account != null)
        {
            if (account.Balance >= amount)
            {
                account.Balance -= amount;

                // Log giao dịch rút tiền
                account.Transactions.Add(new Transaction
                {
                    TransactionDate = transactionDate,
                    TransactionType = "Rút tiền",
                    Amount = amount
                });
            }
            else
            {
                Console.WriteLine("Số tiền trong tài khoản không đủ.");
            }
        }
        else
        {
            Console.WriteLine("Không tìm thấy tài khoản.");
        }
    }

    // Xem số tiền hiện có trong tài khoản
    public void CheckBalance(string accountNumber)
    {
        Account account = FindAccount(accountNumber);
        if (account != null)
        {
            Console.WriteLine($"Số tiền hiện có trong tài khoản {accountNumber}: {account.Balance} Euros");
        }
        else
        {
            Console.WriteLine("Không tìm thấy tài khoản.");
        }
    }

    // Tính lãi suất cho tất cả các tài khoản và cập nhật số tiền
    public void CalculateInterest()
    {
        foreach (var account in accounts)
        {
            double interest = account.Balance * (account.InterestRate / 100);
            account.Balance += interest;

            // Log giao dịch tính lãi suất
            account.Transactions.Add(new Transaction
            {
                TransactionDate = DateTime.Now,
                TransactionType = "Tính lãi suất",
                Amount = interest
            });
        }
    }

    // In ra báo cáo
    public void GenerateReport()
    {
        foreach (var account in accounts)
        {
            Console.WriteLine($"Số hiệu tài khoản: {account.AccountNumber}");
            Console.WriteLine($"Số tiền hiện có: {account.Balance} Euros");
            Console.WriteLine("Các giao dịch:");
            foreach (var transaction in account.Transactions)
            {
                Console.WriteLine($"  Ngày: {transaction.TransactionDate.ToShortDateString()}");
                Console.WriteLine($"  Loại giao dịch: {transaction.TransactionType}");
                Console.WriteLine($"  Số tiền: {transaction.Amount} Euros");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    private Account FindAccount(string accountNumber)
    {
        return accounts.Find(account => account.AccountNumber == accountNumber);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Bank bank = new Bank();

        // Dữ liệu ban đầu
        bank.OpenAccount("001", "Alice", "901", 100, 5);
        bank.OpenAccount("002", "Bob", "902", 50, 5);
        bank.OpenAccount("003", "Alice", "901", 200, 10);
        bank.OpenAccount("004", "Eve", "903", 200, 10);

        while (true)
        {
            Console.WriteLine("Chọn một nhiệm vụ:");
            Console.WriteLine("1. Mở một tài khoản mới");
            Console.WriteLine("2. Nhập tiền vào tài khoản");
            Console.WriteLine("3. Rút tiền từ tài khoản");
            Console.WriteLine("4. Xem số tiền hiện có trong tài khoản");
            Console.WriteLine("5. Tính lãi suất và cập nhật số tiền");
            Console.WriteLine("6. In ra báo cáo");
            Console.WriteLine("7. Thoát");
            Console.Write("Nhập lựa chọn của bạn: ");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        OpenAccount(bank);
                        break;
                    case 2:
                        Deposit(bank);
                        break;
                    case 3:
                        Withdraw(bank);
                        break;
                    case 4:
                        CheckBalance(bank);
                        break;
                    case 5:
                        bank.CalculateInterest();
                        Console.WriteLine("Lãi suất đã được tính và số tiền đã được cập nhật.");
                        break;
                    case 6:
                        bank.GenerateReport();
                        break;
                    case 7:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Đầu vào không hợp lệ. Vui lòng nhập một số hợp lệ.");
            }
        }
    }

    static void OpenAccount(Bank bank)
    {
        Console.Write("Nhập số tài khoản: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Nhập tên chủ tài khoản: ");
        string ownerName = Console.ReadLine();
        Console.Write("Nhập số CMND: ");
        string cmnd = Console.ReadLine();
        Console.Write("Nhập số tiền ban đầu: ");
        double initialBalance;
        while (!double.TryParse(Console.ReadLine(), out initialBalance) || initialBalance < 0)
        {
            Console.Write("Số tiền không hợp lệ, vui lòng nhập lại: ");
        }
        Console.Write("Nhập lãi suất (%): ");
        double interestRate;
        while (!double.TryParse(Console.ReadLine(), out interestRate) || interestRate < 0)
        {
            Console.Write("Lãi suất không hợp lệ, vui lòng nhập lại: ");
        }

        bank.OpenAccount(accountNumber, ownerName, cmnd, initialBalance, interestRate);
        Console.WriteLine("Tài khoản đã được mở thành công.");
    }

    static void Deposit(Bank bank)
    {
        Console.Write("Nhập số tài khoản: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Nhập số tiền cần nạp: ");
        double amount;
        while (!double.TryParse(Console.ReadLine(), out amount) || amount < 0)
        {
            Console.Write("Số tiền không hợp lệ, vui lòng nhập lại: ");
        }
        Console.Write("Nhập ngày giao dịch (yyyy-MM-dd): ");
        DateTime transactionDate;
        while (!DateTime.TryParse(Console.ReadLine(), out transactionDate))
        {
            Console.Write("Ngày không hợp lệ, vui lòng nhập lại (yyyy-MM-dd): ");
        }

        bank.Deposit(accountNumber, amount, transactionDate);
        Console.WriteLine("Giao dịch nạp tiền thành công.");
    }

    static void Withdraw(Bank bank)
    {
        Console.Write("Nhập số tài khoản: ");
        string accountNumber = Console.ReadLine();
        Console.Write("Nhập số tiền cần rút: ");
        double amount;
        while (!double.TryParse(Console.ReadLine(), out amount) || amount < 0)
        {
            Console.Write("Số tiền không hợp lệ, vui lòng nhập lại: ");
        }
        Console.Write("Nhập ngày giao dịch (yyyy-MM-dd): ");
        DateTime transactionDate;
        while (!DateTime.TryParse(Console.ReadLine(), out transactionDate))
        {
            Console.Write("Ngày không hợp lệ, vui lòng nhập lại (yyyy-MM-dd): ");
        }

        bank.Withdraw(accountNumber, amount, transactionDate);
        Console.WriteLine("Giao dịch rút tiền thành công.");
    }

    static void CheckBalance(Bank bank)
    {
        Console.Write("Nhập số tài khoản: ");
        string accountNumber = Console.ReadLine();
        bank.CheckBalance(accountNumber);
    }
}
