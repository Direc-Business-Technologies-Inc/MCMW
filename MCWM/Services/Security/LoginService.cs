using DirecLayer;

namespace MCWM.Services.Security
{
    public class LoginService : ILoginService
    {
        public bool LoginViaLocalDatabase(string userName, string passWord, out string err)
        {
            if (Validation.isNull(userName, passWord))
            {
                //login into database
                err = "Please provide username and/or password.";
                return false;
            }
            else
            {
                err = "";
                return true;
            }
        }
    }
}
