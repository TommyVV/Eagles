namespace Eagles.Base.ValidateVode
{
    public interface IValidateCode:IInterfaceBase
    {
        string GenerateValidCodeToBase64(int validCode);
    }
}
