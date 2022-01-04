using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Service
{
    public class PrijavaKvaraService
    {
        public async static Task<List<PrijavaKvara>> GetAll()
        {
            PrijavaKvaraDAO service = new PrijavaKvaraDAO();
            List<PrijavaKvara> result = await service.GetAll();
            return result;
        }

        public static async Task<Boolean> Add(PrijavaKvara p)
        {
            PrijavaKvaraDAO service = new PrijavaKvaraDAO();
            int result = await service.Insert(p);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<Boolean> Update(PrijavaKvara p)
        {
            PrijavaKvaraDAO service = new PrijavaKvaraDAO();
            int result = await service.Update(p);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<List<PrijavaKvara>> GetAllForRepairman(Majstor m)
        {
            PrijavaKvaraDAO service = new PrijavaKvaraDAO();
            List<PrijavaKvara> result = await service.GetPrijaveKvaraForMajstor(m);
            return result;
        }

        public async static Task<Boolean> Delete(PrijavaKvara p)
        {
            PrijavaKvaraDAO service = new PrijavaKvaraDAO();
            int result = await service.Delete(p);

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async static Task<PrijavaKvara> GetOne(PrijavaKvara p)
        {
            PrijavaKvaraDAO service = new PrijavaKvaraDAO();
            PrijavaKvara one = await service.GetById(p);
            return one;
        }
    }
}
