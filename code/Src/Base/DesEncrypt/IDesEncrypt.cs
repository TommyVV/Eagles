namespace Eagles.Base.DesEncrypt
{
    public interface IDesEncrypt:IInterfaceBase
    {
        string Encrypt(string str);

        string Decrypt(string str);

        string EncryptToHex(string str);

        string DecryptToHex(string str);
    }
}
