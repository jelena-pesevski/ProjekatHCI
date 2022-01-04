using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Service
{
    public class KlijentService
    {
        public async static Task<List<Klijent>> GetAllKlijenti()
        {
            KlijentDAO service = new KlijentDAO();
            List<Klijent> result = await service.GetAll();
            return result;
        }

        public static async Task<Boolean> AddKlijent(Klijent k)
        {
            KlijentDAO service = new KlijentDAO();
            int result = await service.Insert(k);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Boolean> UpdateKlijent(Klijent k)
        {
            KlijentDAO service = new KlijentDAO();
            int result = await service.Update(k);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Boolean> DeleteKlijent(Klijent k)
        {
            KlijentDAO service = new KlijentDAO();
            int result = await service.Delete(k);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Klijent> GetOne(Klijent k)
        {
            KlijentDAO service = new KlijentDAO();
            Klijent one = await service.GetById(k);
            return one;
        }
    }
}
