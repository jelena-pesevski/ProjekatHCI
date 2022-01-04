using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Service
{
    class PopravkaUslugaService
    {
        public static async Task<Boolean> AddUsluga(PopravkaUsluga u)
        {
            PopravkaUslugaDAO service = new PopravkaUslugaDAO();
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

        public async static Task<Boolean> UpdateUsluga(PopravkaUsluga u)
        {
            PopravkaUslugaDAO service = new PopravkaUslugaDAO();
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

        public async static Task<Boolean> DeleteUsluga(PopravkaUsluga u)
        {
            PopravkaUslugaDAO service = new PopravkaUslugaDAO();
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
    }
}
