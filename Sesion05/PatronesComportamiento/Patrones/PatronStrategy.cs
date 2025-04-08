namespace PatronesComportamiento.Patrones;

using System.Threading;

// Definicion de la estrategia
public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

// Estrategia concreta: Pago con tarjeta
public class CreditCardPaymentStrategy : IPaymentStrategy
{
    private string cardNumber;
    private string cvv;
    private string dateOfExpiry;
    public CreditCardPaymentStrategy(string cardNumber, string cvv, string dateOfExpiry)
    {
        this.cardNumber = cardNumber;
        this.cvv = cvv;
        this.dateOfExpiry = dateOfExpiry;
    }
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Pago de {amount} realizado con tarjeta de cr�dito");
    }
}

// Estrategia concreta: Pago con efectivo
public class CashPaymentStrategy : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Pago de {amount} realizado en efectivo");
    }
}

// Estrategia concreta: Pago mediante transferencia bancaria
public class BankTransferPaymentStrategy : IPaymentStrategy
{
    private string accountNumber;
    private string bankName;
    public BankTransferPaymentStrategy(string accountNumber, string bankName)
    {
        this.accountNumber = accountNumber;
        this.bankName = bankName;
    }
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Pago de {amount} realizado mediante transferencia bancaria");
    }
}

// Contexto que utiliza la estrategia
public class PaymentContext
{
    private IPaymentStrategy _paymentStrategy;

    public PaymentContext(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void SetPaymentStrategy(IPaymentStrategy paymentStrategy)
    {
        _paymentStrategy = paymentStrategy;
    }

    public void ExecutePayment(decimal amount)
    {
        _paymentStrategy.Pay(amount);
    }
}














// Uso
public static class AppStrategy
{

    public static void Ejecutar()
    {
        Console.WriteLine("Patron Strategy");
        var paymentContext = new PaymentContext(new CreditCardPaymentStrategy("1234567890123456", "123", "12/2022"));
        paymentContext.ExecutePayment(100);

        Thread.Sleep(1000);

        paymentContext.SetPaymentStrategy(new CashPaymentStrategy());
        paymentContext.ExecutePayment(200);

        Thread.Sleep(1000);

        paymentContext.SetPaymentStrategy(new BankTransferPaymentStrategy("1234567890", "Banco de Chile"));
        paymentContext.ExecutePayment(300);
    }

}