using System.Security.Cryptography;
using System.Text;

namespace He_Thong_GYM.Helpers;

public static class PasswordHasher
{
    private const int SoVongLap = 210_000;

    public static bool XacThuc(string matKhau, byte[] salt, byte[] hashDaLuu)
    {
        if (string.IsNullOrEmpty(matKhau) || salt.Length == 0 || hashDaLuu.Length == 0) return false;
        var hashNhap = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(matKhau), salt, SoVongLap, HashAlgorithmName.SHA512, hashDaLuu.Length);
        return CryptographicOperations.FixedTimeEquals(hashNhap, hashDaLuu);
    }

    public static (byte[] Hash, byte[] Salt) TaoHash(string matKhau)
    {
        if (string.IsNullOrWhiteSpace(matKhau)) throw new ArgumentException("Mật khẩu không được để trống.");
        var salt = RandomNumberGenerator.GetBytes(32);
        var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(matKhau), salt, SoVongLap, HashAlgorithmName.SHA512, 64);
        return (hash, salt);
    }
}
