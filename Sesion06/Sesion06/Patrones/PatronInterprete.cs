using System.Text.RegularExpressions;

namespace Sesion06.Patrones;

public interface IExpression
{
    double Interpret(Dictionary<string, double> context);
}

public class VariableExpression : IExpression
{
    private readonly string _name;

    public VariableExpression(string name)
    {
        _name = name;
    }

    public double Interpret(Dictionary<string, double> context) =>
        context.ContainsKey(_name) ? context[_name] : throw new Exception($"Variable {_name} no definida");
}

public class SumExpression : IExpression
{
    private readonly IExpression _left;
    private readonly IExpression _right;

    public SumExpression(IExpression left, IExpression right)
    {
        _left = left;
        _right = right;
    }

    public double Interpret(Dictionary<string, double> context) =>
        _left.Interpret(context) + _right.Interpret(context);
}

public class SubstractExpression : IExpression
{
    private readonly IExpression _left;
    private readonly IExpression _right;
    public SubstractExpression(IExpression left, IExpression right)
    {
        _left = left;
        _right = right;
    }
    public double Interpret(Dictionary<string, double> context) =>
        _left.Interpret(context) - _right.Interpret(context);
}

// Expresion para multiplicar
public class MultiplyExpression : IExpression
{
    private readonly IExpression _left;
    private readonly IExpression _right;
    public MultiplyExpression(IExpression left, IExpression right)
    {
        _left = left;
        _right = right;
    }
    public double Interpret(Dictionary<string, double> context) =>
        _left.Interpret(context) * _right.Interpret(context);
}

// Creamos el parser
public class ExpressionParser
{
    public static IExpression Parse(string input)
    {
     
        var tokens = Regex.Split(input, @"(?<=[\}\d])(?=[\+\-\*/])|(?<=[\+\-\*/])");

        IExpression expression = null;

        // Iterar sobre cada token
        foreach (var token in tokens.Select(t => t.Trim()))
        {
            if (string.IsNullOrEmpty(token)) continue;

            IExpression next;

            // Si el token es una variable
            if (Regex.IsMatch(token, @"^\{[A-Za-z]\}$"))
            {
                next = new VariableExpression(token.Trim('{', '}'));
            }
            // Entonces es un operador suma
            else
            {
                var indexOf = Array.IndexOf(tokens, token);
                if (token == "+")
                {
                    var right = Parse(tokens[++indexOf]);
                    expression = new SumExpression(expression, right);
                    continue;
                }
                // Si el operador es de resta
                else if (token == "-")
                {
                    var right = Parse(tokens[++indexOf]);
                    expression = new SubstractExpression(expression, right);
                    continue;
                }
                // Si el operador es de multiplicacion
                else if (token == "*")
                {
                    var right = Parse(tokens[++indexOf]);
                    expression = new MultiplyExpression(expression, right);
                    continue;
                }
                else
                {
                    throw new Exception("Token invalido: " + token);
                }
            }

            expression ??= next;

        }

        return expression;
    }
}

public class AppInterpreter
{
    public static void Ejecutar()
    {
        string input = "{x}+{y}+{z}*{j}";
        var context = new Dictionary<string, double>()
        {
            { "x", 5 },
            { "y", 3 },
            { "z", 2 },
            { "j", 10}
        };

        IExpression expression = ExpressionParser.Parse(input);
        double result = expression.Interpret(context);

        Console.WriteLine($"Resultado: {result} del input {input}");
    }
}