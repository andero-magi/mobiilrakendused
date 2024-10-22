namespace sci_calculator;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NCalc;

[INotifyPropertyChanged]
public partial class CalculatorViewModel
{
    [ObservableProperty]
    private string _inputText = "";

    [ObservableProperty]
    private string _result = "0";

    private bool _needsClosingBracket = false;

    [RelayCommand]
    void Reset()
    {
        Result = "0";
        InputText = "";
        _needsClosingBracket = false;
    }

    [RelayCommand]
    void Calculate()
    {
        if (string.IsNullOrEmpty(InputText))
        {
            return;
        }

        if (_needsClosingBracket)
        {
            InputText += ")";
            _needsClosingBracket = false;
        }

        try
        {
            var input = NormalizeInputString();
            var expr = new Expression(input);
            var res = expr.Evaluate();

            Result = res.ToString();
        } 
        catch (Exception ex) 
        {
            Result = "NaN";
        }
    }

    private string NormalizeInputString()
    {
        Dictionary<string, string> _opMapper = new()
        {
            {"×", "*"},
            {"÷", "/"},
            {"SIN", "Sin"},
            {"COS", "Cos"},
            {"TAN", "Tan"},
            {"ASIN", "Asin"},
            {"ACOS", "Acos"},
            {"ATAN", "Atan"},
            {"LOG", "Log"},
            {"EXP", "Exp"},
            {"LOG10", "Log10"},
            {"POW", "Pow"},
            {"SQRT", "Sqrt"},
            {"ABS", "Abs"},
        };

        var retString = InputText;

        foreach (var key in _opMapper.Keys)
        {
            retString = retString.Replace(key, _opMapper[key]);
        }

        return retString;
    }

    [RelayCommand]
    private void Backspace()
    {
        if (InputText.Length < 1)
        {
            return;
        }

        InputText = InputText.Substring(0, InputText.Length - 1);
    }

    [RelayCommand]
    private void NumberInput(string key)
    {
        InputText += key;
    }

    [RelayCommand]
    private void MathOperator(string op)
    {
        if (_needsClosingBracket)
        {
            InputText += ")";
            _needsClosingBracket = false;
        }

        InputText += $" {op} ";
    }

    [RelayCommand]
    private void RegionOperator(string op)
    {
        if (op == ")")
        {
            _needsClosingBracket = false;
        }

        InputText += op;
    }

    [RelayCommand]
    private void ScientificOperator(string op)
    {
        InputText += $"{op}(";
        _needsClosingBracket = true;
    }

}
