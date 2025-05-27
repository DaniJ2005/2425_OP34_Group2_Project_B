public class CreateScreen<T> : FormScreen
{
    private readonly Func<T> _mapToModel;
    private readonly Func<T, bool> _submitModel;

    public CreateScreen(string screenName, List<FormField> fields, Func<T> mapToModel, Func<T, bool> submitModel)
    {
        ScreenName = screenName;
        Fields = fields;
        _mapToModel = mapToModel;
        _submitModel = submitModel;
    }

    public override void OnFormSubmit()
    {
        var model = _mapToModel();
        bool success = _submitModel(model);
        Console.WriteLine(success ? "\nCreated successfully!" : "\nCreation failed.");
    }
}
