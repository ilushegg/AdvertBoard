namespace AdvertBoard.Infrastructure.Identity
{
    public interface IPasswordCryptography
    {
        bool AreEqual(string plainTextInput, string hashedInput, string salt);
        string CreateSalt(int size);
        string GenerateHash(string input, string salt);
    }
}