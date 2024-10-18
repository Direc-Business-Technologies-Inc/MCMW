namespace MCWM.Services.Security
{
    public interface ILoginService
    {
        bool LoginViaLocalDatabase(string userName, string passWord, out string err);
    }
}