public class FormField
{
    public string Label { get; set; }
    public bool MaskInput { get; set; }
    public Func<string, (bool isValid, string errorMessage)> Validator { get; set; }
    public string Value { get; set; } = "";
    public bool IsActive { get; set; } = false;

    public FormField(string label, bool maskInput, Func<string, (bool, string)> validator)
    {
        Label = label;
        MaskInput = maskInput;
        Validator = validator;
    }
}
