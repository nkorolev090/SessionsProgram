namespace Core.Interfaces
{
    public interface IEncryptionService
    {
        Task<string?> EncryptAsync(string clearText, string passphrase);
    }
}
