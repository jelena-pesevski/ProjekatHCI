using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DTO;
using ProjekatHCI.Model.DAO;

namespace ProjekatHCI.Service
{
    public class RezervniDioService
    {
        public async static Task<List<RezervniDio>> GetAllRezDijelovi()
        {
            RezervniDioDAO service = new RezervniDioDAO();
            List<RezervniDio> result = await service.GetAll();
            return result;
        }

        public static async Task<Boolean> AddRezDio(RezervniDio r)
        {
            RezervniDioDAO service = new RezervniDioDAO();
            int result = await service.Insert(r);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Boolean> UpdateRezDio(RezervniDio r)
        {
            RezervniDioDAO service = new RezervniDioDAO();
            int result = await service.Update(r);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Boolean> DeleteRezDio(RezervniDio r)
        {
            RezervniDioDAO service = new RezervniDioDAO();
            int result = await service.Delete(r);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<RezervniDio> GetOne(RezervniDio r)
        {
            RezervniDioDAO service = new RezervniDioDAO();
            RezervniDio one = await service.GetById(r);
            return one;
        }
    }
}
