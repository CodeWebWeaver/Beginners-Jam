using System;

[Serializable]
public class RandomServiceSettings {
    public string seedString = "I like onions!";
    public int Seed => GetDeterministicSeed();

    public int GetDeterministicSeed() {
        if (string.IsNullOrEmpty(seedString)) return 0;
        using var sha = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(seedString);
        var hash = sha.ComputeHash(bytes);
        return BitConverter.ToInt32(hash, 0);
    }

}