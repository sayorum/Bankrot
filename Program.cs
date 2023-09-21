using System;

public class Money
{
    private decimal amount;

    public Money(decimal initialAmount)
    {
        if (initialAmount < 0)
        {
            throw new BankruptException("Банкрот");
        }

        this.amount = initialAmount;
    }

    public decimal Amount
    {
        get { return amount; }
    }

    public void IncreaseByCents(int cents)
    {
        if (cents < 0)
        {
            throw new BankruptException("Банкрот");
        }
        this.amount += (decimal)cents / 100;
    }

    public void DecreaseByCents(int cents)
    {
        if (cents < 0)
        {
            throw new BankruptException("Банкрот");
        }
        this.amount -= (decimal)cents / 100;
    }

    public static Money operator +(Money money1, Money money2)
    {
        decimal result = money1.amount + money2.amount;
        if (result < 0)
        {
            throw new BankruptException("Банкрот");
        }
        return new Money(result);
    }

    public static Money operator -(Money money1, Money money2)
    {
        decimal result = money1.amount - money2.amount;
        if (result < 0)
        {
            throw new BankruptException("Банкрот");
        }
        return new Money(result);
    }

    public static Money operator /(Money money, int divisor)
    {
        if (divisor == 0)
        {
            throw new DivideByZeroException("Деление на ноль");
        }

        decimal result = money.amount / divisor;
        if (result < 0)
        {
            throw new BankruptException("Банкрот");
        }
        return new Money(result);
    }

    public static Money operator *(Money money, int multiplier)
    {
        decimal result = money.amount * multiplier;
        if (result < 0)
        {
            throw new BankruptException("Банкрот");
        }
        return new Money(result);
    }

    public static Money operator ++(Money money)
    {
        decimal result = money.amount + 0.01m;
        if (result < 0)
        {
            throw new BankruptException("Банкрот");
        }
        return new Money(result);
    }

    public static Money operator --(Money money)
    {
        decimal result = money.amount - 0.01m;
        if (result < 0)
        {
            throw BankruptException("Банкрот");
        }
        return new Money(result);
    }

    public override string ToString()
    {
        return amount.ToString("C2");
    }
}

public class BankruptException : Exception
{
    public BankruptException(string message) : base(message)
    {
    }
}

class Program
{
    static void Main()
    {
        try
        {
            Console.Write("Введите начальную сумму: ");
            decimal initialAmount = decimal.Parse(Console.ReadLine());
            Money money = new Money(initialAmount);

            while (true)
            {
                Console.WriteLine("Выберите операцию:");
                Console.WriteLine("1. Сложение");
                Console.WriteLine("2. Вычитание");
                Console.WriteLine("3. Деление");
                Console.WriteLine("4. Умножение");
                Console.WriteLine("5. Увеличение на 1 копейку");
                Console.WriteLine("6. Уменьшение на 1 копейку");
                Console.WriteLine("7. Выйти");
                Console.WriteLine("8. Увеличение на указанное количество копеек");
                Console.WriteLine("9. Уменьшение на указанное количество копеек");
                Console.Write("Введите номер операции: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Введите сумму для сложения: ");
                        decimal addAmount = decimal.Parse(Console.ReadLine());
                        money = money + new Money(addAmount); // Сохраняем результат в переменной money
                        break;

                    case 2:
                        Console.Write("Введите сумму для вычитания: ");
                        decimal subtractAmount = decimal.Parse(Console.ReadLine());
                        money = money - new Money(subtractAmount); // Сохраняем результат в переменной money
                        break;

                    case 3:
                        Console.Write("Введите делитель: ");
                        int divisor = int.Parse(Console.ReadLine());
                        money = money / divisor; // Сохраняем результат в переменной money
                        break;

                    case 4:
                        Console.Write("Введите множитель: ");
                        int multiplier = int.Parse(Console.ReadLine());
                        money = money * multiplier; // Сохраняем результат в переменной money
                        break;

                    case 5:
                        money = ++money; // Увеличиваем money на 1 копейку и сохраняем результат
                        break;

                    case 6:
                        money = --money; // Уменьшаем money на 1 копейку и сохраняем результат
                        break;

                    case 7:
                        Environment.Exit(0);
                        break;

                    case 8:
                        Console.Write("Введите количество копеек для увеличения: ");
                        int increaseCents = int.Parse(Console.ReadLine());
                        money.IncreaseByCents(increaseCents); // Увеличиваем на указанное количество копеек и сохраняем результат
                        break;

                    case 9:
                        Console.Write("Введите количество копеек для уменьшения: ");
                        int decreaseCents = int.Parse(Console.ReadLine());
                        money.DecreaseByCents(decreaseCents); // Уменьшаем на указанное количество копеек и сохраняем результат
                        break;

                    default:
                        Console.WriteLine("Неверная операция.");
                        break;
                }

                Console.WriteLine("Результат: " + money);
            }
        }
        catch (BankruptException ex)
        {
            Console.WriteLine("Исключительная ситуация: " + ex.Message);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Исключительная ситуация: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Исключительная ситуация: " + ex.Message);
        }
    }
}
