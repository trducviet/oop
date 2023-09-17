using System;
using System.Globalization;
using System.Threading;

class Person
{
    private string name;
    private string address;
    private double salary;

    public Person(string name, string address, double salary)
    {
        this.name = name;
        this.address = address;
        this.salary = salary;
    }

    public string GetName()
    {
        return name;
    }

    public string GetAddress()
    {
        return address;
    }

    public double GetSalary()
    {
        return salary;
    }

    public static Person InputPersonInfo()
    {
        string name, address, sSalary;
        double salary;

        Console.WriteLine("Nhập thông tin của Người");
        Console.Write("Họ và tên: ");
        name = Console.ReadLine();

        Console.Write("Địa chỉ: ");
        address = Console.ReadLine();

        while (true)
        {
            Console.Write("Lương: ");
            sSalary = Console.ReadLine();
            if (double.TryParse(sSalary, out salary))
            {
                if (salary >= 0)
                    break;
                else
                    Console.WriteLine("Lương phải là số dương.");
            }
            else
            {
                Console.WriteLine("Bạn phải nhập một số hợp lệ cho lương.");
            }
        }

        return new Person(name, address, salary);
    }

    public static void DisplayPersonInfo(Person person)
    {
        Console.WriteLine("Thông tin của Người bạn đã nhập:");
        Console.WriteLine($"Họ và tên: {person.GetName()}");
        Console.WriteLine($"Địa chỉ: {person.GetAddress()}");
        Console.WriteLine($"Lương: {person.GetSalary().ToString("C0", CultureInfo.GetCultureInfo("vi-VN"))}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("vi-VN");
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=====Chương trình Quản lý Người=====");
        int n;
        while (true)
        {
            Console.Write("Nhập số người cần quản lý: ");
            if (int.TryParse(Console.ReadLine(), out n) && n > 0)
                break;
            else
                Console.WriteLine("Vui lòng nhập một số nguyên dương.");
        }

        Person[] persons = new Person[n];

        for (int i = 0; i < n; i++)
        {
            persons[i] = Person.InputPersonInfo();
        }

        Console.WriteLine("Thông tin của Người bạn đã nhập:");

        foreach (Person person in persons)
        {
            Person.DisplayPersonInfo(person);
        }

        Console.WriteLine("Đang sắp xếp theo lương tăng dần...");
        persons = SortBySalary(persons);

        Console.WriteLine("Thông tin của Người sau khi sắp xếp theo lương:");

        foreach (Person person in persons)
        {
            Person.DisplayPersonInfo(person);
        }
    }

    static Person[] SortBySalary(Person[] persons)
    {
        try
        {
            for (int i = 0; i < persons.Length - 1; i++)
            {
                for (int j = 0; j < persons.Length - 1 - i; j++)
                {
                    if (persons[j].GetSalary() > persons[j + 1].GetSalary())
                    {
                        Person temp = persons[j];
                        persons[j] = persons[j + 1];
                        persons[j + 1] = temp;
                    }
                }
            }
            return persons;
        }
        catch
        {
            throw new Exception("Không thể sắp xếp thông tin Người");
        }
    }
}
