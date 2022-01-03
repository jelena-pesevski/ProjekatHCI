using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Service
{
    public class ZaposleniService
    {

        public static void SetCipher(Zaposleni z)
        {
            string passForView = "";
            for (int i = 0; i < z.Lozinka.Length; i++)
            {
                passForView += "*";
            }
            z.LozinkaZaPrikaz = passForView;
        }

        public async static Task<Boolean> UpdateZaposleni(Zaposleni z)
        {
            ZaposleniDAO service = new ZaposleniDAO();

            string cipher = "";
            for(int i=0; i<z.Lozinka.Length; i++)
            {
                cipher += "*";
            }
            if (z.LozinkaZaPrikaz!=null && !z.LozinkaZaPrikaz.Equals(cipher))
            {
                z.Lozinka = z.LozinkaZaPrikaz;
                SetCipher(z);
            }

            if(!z.Status.Equals("aktivan") && !z.Status.Equals("neaktivan"))
            {
                return false;
            }

            int result = await service.Update(z);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Zaposleni> GetOne(Zaposleni z)
        {
            ZaposleniDAO service = new ZaposleniDAO();
            Zaposleni one = await service.GetById(z);
            SetCipher(one);
            return one;
        }

        public async static Task<List<Zaposleni>> GetAllZaposleni()
        {
            ZaposleniDAO service = new ZaposleniDAO();
            List<Zaposleni> result = await service.GetAll();

            //set LozinkeZaPrikaz
            foreach(Zaposleni z in result)
            {
                SetCipher(z);
            }

            return result;
        }

        public static async Task<Boolean> AddZaposleni(Zaposleni z)
        {
            ZaposleniDAO service = new ZaposleniDAO();
            int result = await service.Insert(z);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<Boolean> DeleteZaposleni(Zaposleni z)
        {
            ZaposleniDAO service = new ZaposleniDAO();
            int result = await service.Delete(z);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
