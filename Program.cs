using Controllers;
using Models;

class Program
{
    public static List<Flavors> products;
    public static string input;
    public static bool isCancel = false;
    public static string divider = "===============================";

    public static async Task Main()
    {
        Console.WriteLine(divider);
        Console.WriteLine("\tWELCOME TO ShakeHUB");
        Console.WriteLine($"\nDate: {DateTime.Now}");
        Console.WriteLine(divider);
        do
        {
            Console.WriteLine("\tMake your Order");
            Console.WriteLine("[1] Menu\n[2] Dine-in\n[3] Take-out\n[4] Show Orders\n[5] Cancel");
            Console.Write("Enter: ");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    isCancel = false;
                    await Menu();
                    break;
                case "2":
                    isCancel = false;
                    await DineIn();
                    break;
                case "3":
                    isCancel = false;
                    await TakeOut();
                    break;
                case "4":
                    isCancel = false;
                    await ShowOrders();
                    break;
                case "5":
                    isCancel = true;
                    break;
                default:
                    Console.WriteLine("Exited...");
                    Environment.Exit(0);
                    break;
            }
        } while (isCancel);
    }
    public static async Task Display()
    {
        FlavourController Controller = new FlavourController();
        List<Flavors> flavors = products?.Count() > 0 ? products : await Controller.GetAll();
        products = flavors;
        List<Flavors> RegularFlavours = [];
        List<Flavors> SpecialFlavours = [];
        foreach (var flavor in flavors)
        {
            Flavors regular = flavor;
            if (flavor.variant == 1)
            {
                RegularFlavours.Add(regular);
            }
            if (flavor.variant == 2)
            {
                SpecialFlavours.Add(regular);
            }
        }
        Console.WriteLine();
        Console.WriteLine("Flavours");
        Console.WriteLine(divider);
        Console.WriteLine("\nRegular");
        foreach (var flavor in RegularFlavours)
        {
            Console.WriteLine($"[{flavor.id}] {flavor.flavor} - {flavor.price}");
        }
        Console.WriteLine(divider);
        Console.WriteLine("\nSpecial OFFERS!");
        foreach (var flavor in SpecialFlavours)
        {
            Console.WriteLine($"[{flavor.id}] {flavor.flavor} - {flavor.price}");
        }
    }
    public static async Task Menu()
    {
        Console.WriteLine(divider);
        Console.WriteLine("Menu");
        Console.WriteLine(divider);
        await Display();
        Console.WriteLine(divider);
        Console.WriteLine("\t<- Go Back");
        string selected;
        do
        {
            Console.Write("Choose to Proceed y/n: ");
            selected = Console.ReadLine();
            Console.WriteLine("Please enter y or n");
            Console.WriteLine(divider);
            if (selected == "y")
            {
                isCancel = true;
                break;
            }
            else if (selected == "n")
            {
                Console.WriteLine("Exiting...");
                Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
        while (selected != "y" || selected != "n");
    }
    public static async Task ShowOrders()
    {
        OrderController Controller = new();
        Console.WriteLine(divider);
        Console.WriteLine("Recently Orders");
        Console.WriteLine("-------------------------------");
        List<Orders> orders = await Controller.GetAll();
        Console.WriteLine("ID   PRODUCT\tVARIANT\tAMOUNT\tSTATUS");
        foreach (Orders order in orders)
        {
            Console.WriteLine($"{order.id}  | {order.product} | {order.variant} | {order.amount} | {order.status}");
        }
    }
    public static async Task<AddOrder> Order()
    {
        FlavourController Controller = new FlavourController();
        Console.Write("Menu no: ");
        string? flavor_input = Console.ReadLine();

        int flavor_i = int.Parse(flavor_input);
        Flavors flavor = await Controller.GetData(flavor_i);

        Console.Write("Quantity: ");
        string quantity_input = Console.ReadLine();
        int quantity = int.Parse(quantity_input);

        decimal vat = 1.12M;
        decimal pretotal = flavor.price * quantity;
        decimal total = pretotal * vat;
        AddOrder order = new()
        {
            shake_id = flavor.id,
            product = flavor.flavor,
            quantity = quantity,
            variant = flavor.name,
            amount = decimal.Parse(total.ToString("F"))
        };
        await Receipt(order);
        return order;
    }
    public static async Task DineIn()
    {
        FlavourController Controller = new FlavourController();
        Console.WriteLine(divider);
        Console.WriteLine("Dine-in");
        await Display();
        Console.WriteLine(divider);
        Console.WriteLine("Choose your order");
        Console.WriteLine(divider);

        AddOrder order = await Order();
        Console.Write("Proceed? y/n: ");
        string proceed = Console.ReadLine();
        if (proceed == "y" || proceed == "Y")
            await Controller.Create(order);
        else if (proceed == "y" || proceed == "Y")
            Console.WriteLine("Thank you for time.");
        return;
    }
    public static async Task TakeOut()
    {
        FlavourController Controller = new FlavourController();
        Console.WriteLine(divider);
        Console.WriteLine("Take-out");
        await Display();
        Console.WriteLine(divider);
        Console.WriteLine("Choose your order");
        Console.WriteLine(divider);
        AddOrder order = await Order();
        Console.Write("Proceed? y/n: ");
        string proceed = Console.ReadLine();
        if (proceed == "y" || proceed == "Y")
            await Controller.Create(order);
        else if (proceed == "y" || proceed == "Y")
            Console.WriteLine("Thank you for time.");
        return;
    }
    public async static Task Receipt(AddOrder order)
    {
        Flavors product = products?.Find(prod => prod.id == order.shake_id);
        decimal vat = 0.12M;
        decimal subtotal = product.price * order.quantity;
        decimal vatAmount = subtotal * vat;
        decimal myVat = decimal.Parse(vatAmount.ToString("F"));
        Console.WriteLine(divider);
        Console.WriteLine("ORDER SUMMARY");
        Console.WriteLine(divider);
        Console.WriteLine("\t---Your Order---");
        Console.WriteLine($"Product: {order.product} | {order.variant}");
        Console.WriteLine($"Price: {product?.price} ({order.quantity})");
        Console.WriteLine($"Quantity: {order.quantity}");
        Console.WriteLine($"Subtotal: {product.price * order.quantity}");
        Console.WriteLine($"VAT(12%): {myVat}");
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Total: P{order.amount}");
        Console.WriteLine(divider);
    }
}