using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Service
{
    class RacunService
    {
        public async static Task<List<Racun>> GetAll()
        {
            RacunDAO service = new RacunDAO();
            List<Racun> result = await service.GetAll();
            return result;
        }

        public static async Task<Boolean> AddRacun(Racun r)
        {
            RacunDAO service = new RacunDAO();
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

        public static async Task PopulateBill(Racun r)
        {
            RacunStavkaDAO service = new RacunStavkaDAO();
            await service.InsertItemsIntoBill(r);
            return;
        }

        public async static Task<Racun> GetOne(Racun u)
        {
            RacunDAO service = new RacunDAO();
            Racun one = await service.GetById(u);
            return one;
        }
    }
}
