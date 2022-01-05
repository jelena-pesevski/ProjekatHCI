using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Service
{
    public class PopravkaService
    {
        public static async Task<Boolean> Add(Popravka p)
        {
            PopravkaDAO service = new PopravkaDAO();
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

        public async static Task<List<Popravka>> GetAll()
        {
            PopravkaDAO service = new PopravkaDAO();
            List<Popravka> result = await service.GetAll();
            return result;
        }

        public async static Task<Popravka> GetOne(Popravka p)
        {
            PopravkaDAO service = new PopravkaDAO();
            Popravka one = await service.GetById(p);
            return one;
        }

        public async static Task<List<Popravka>> GetUnfinished()
        {
            PopravkaDAO service = new PopravkaDAO();
            List<Popravka> result = await service.GetAllUnfinishedForRepairman(AppService.CurrMajstor);
            return result;
        }

        public async static Task FinishRepairment(Popravka p)
        {
            PopravkaDAO service = new PopravkaDAO();
            await service.finishRepairment(p);
            return;
        } 
    }
}
