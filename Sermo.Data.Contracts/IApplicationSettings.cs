namespace Sermo.Data.Contracts
{
    public interface IApplicationSettings
    {
        string GetValue(string setting);
    }
}