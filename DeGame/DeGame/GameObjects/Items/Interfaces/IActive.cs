public interface IActive
{
    int MaxCharges
    {
        get; set;
    }

    int Charges
    {
        get; set;
    }

    string Type
    {
        get;
    }
    void ExecuteActive();
}
