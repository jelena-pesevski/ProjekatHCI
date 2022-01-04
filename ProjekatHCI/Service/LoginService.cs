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
                    AppService.CurrEmployee = z;
                    if ("M".Equals(z.Tip))
                    {
                        AppService.CurrMajstor = await MajstorService.GetOne(new Model.DTO.Majstor(AppService.CurrEmployeeId));
                    }

                    AppService.SetTheme();
                    if (!z.Jezik.Equals(language))
                    {
                        z.Jezik = language;
                       
                        //TODO sacuvaj jezik u bazu
                    }
                    return z.Tip;
                }
            }
            return null;
        }

    }
}
