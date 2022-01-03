using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DTO;
using ProjekatHCI.Model.DAO;

namespace ProjekatHCI.Service
{
    public class UslugaService
    {
        public async static Task<List<Usluga>> GetAllUsluge()
        {
            UslugaDAO service = new UslugaDAO();
            List<Usluga> result = await service.GetAll();
            return result;
        }

        public static async Task<Boolean> AddUsluga(Usluga u)
        {
            UslugaDAO service = new UslugaDAO();
            int result = await service.Insert(u);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Boolean> UpdateUsluga(Usluga u)
        {
            UslugaDAO service = new UslugaDAO();
            int result = await service.Update(u);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Boolean> DeleteUsluga(Usluga u)
        {
            UslugaDAO service = new UslugaDAO();
            int result = await service.Delete(u);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Usluga> GetOne(Usluga u)
        {
            UslugaDAO service = new UslugaDAO();
            Usluga one = await service.GetById(u);
            return one;
        }
    }
}
