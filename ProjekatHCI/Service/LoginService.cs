using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;
namespace ProjekatHCI.Service
{
    public class LoginService
    {
        public static async Task<string> CheckLogin(string username, string password, string language)
        {
            ZaposleniDAO service = new ZaposleniDAO();
            List<Zaposleni> list = await service.GetAll();

            foreach(Zaposleni z in list)
            {
                if(username.Equals(z.KorisnickoIme) && password.Equals(z.Lozinka))
                {
                    if (!z.Jezik.Equals(language))
                    {
                        z.Jezik = language;
                       
                    }
                    return z.Tip;
                }
            }
            return null;
        }

    }
}
