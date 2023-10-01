using System;
using System.Collections.Generic;

class Employee
{
    public string Name { get; set; }

    public Employee(string name)
    {
        Name = name;
    }
}

class Item
{
    public double Price { get; set; }
    public double Discount { get; set; }

    public Item(double price, double discount)
    {
        Price = price;
        Discount = discount;
    }
}

class GroceryBill
{
    private Employee clerk;
    private double total;
    private List<Item> items;

    public GroceryBill(Employee clerk)
    {
        this.clerk = clerk;
        total = 0.0;
        items = new List<Item>();
    }

    public void Add(Item item)
    {
        items.Add(item);
        total += item.Price;
    }

    public double GetTotal()
    {
        return total;
    }

    public void PrintReceipt()
    {
        Console.WriteLine($"Hóa đơn cho Nhân viên: {clerk.Name}");
        foreach (var item in items)
        {
            Console.WriteLine($"Sản phẩm: Giá = ${item.Price}, Giảm giá = ${item.Discount}");
        }
        Console.WriteLine($"Tổng cộng: ${GetTotal()}");
    }
}

class DiscountBill : GroceryBill
{
    private bool preferred;
    private int discountCount;
    private double discountAmount;

    public DiscountBill(Employee clerk, bool preferred) : base(clerk)
    {
        this.preferred = preferred;
        discountCount = 0;
        discountAmount = 0.0;
    }

    public new void Add(Item item)
    {
        if (preferred)
        {
            double itemDiscount = item.Discount;
            if (itemDiscount > 0.0)
            {
                discountCount++;
                discountAmount += itemDiscount;
            }
        }
        base.Add(item);
    }

    public int GetDiscountCount()
    {
        return discountCount;
    }

    public double GetDiscountAmount()
    {
        return discountAmount;
    }

    public double GetDiscountPercent()
    {
        if (base.GetTotal() == 0.0)
        {
            return 0.0;
        }
        return (discountAmount / base.GetTotal()) * 100;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Tạo một nhân viên
        Employee employee = new Employee("John");

        // Tạo các sản phẩm
        Item item1 = new Item(1.35, 0.25);
        Item item2 = new Item(2.50, 0.10);
        Item item3 = new Item(3.99, 0.0);

        // Tạo một đơn hàng GroceryBill và thêm các sản phẩm
        GroceryBill groceryBill = new GroceryBill(employee);
        groceryBill.Add(item1);
        groceryBill.Add(item2);
        groceryBill.Add(item3);

        // In hóa đơn
        groceryBill.PrintReceipt();

        // Tạo một đơn hàng DiscountBill cho khách hàng ưu đãi và thêm các sản phẩm
        DiscountBill discountBill = new DiscountBill(employee, true);
        discountBill.Add(item1);
        discountBill.Add(item2);
        discountBill.Add(item3);

        // In thông tin giảm giá
        Console.WriteLine($"Số lượng sản phẩm được giảm giá: {discountBill.GetDiscountCount()}");
        Console.WriteLine($"Tổng số tiền giảm giá: ${discountBill.GetDiscountAmount()}");
        Console.WriteLine($"Phần trăm giảm giá: {discountBill.GetDiscountPercent()}%");
    }
}
