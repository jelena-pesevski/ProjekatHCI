using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Service
{
    class PopravkaRezervniDioService
    {
        public static async Task<Boolean> AddUsluga(PopravkaRezervniDio u)
        {
            PopravkaRezervniDioDAO service = new PopravkaRezervniDioDAO();
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

        public async static Task<Boolean> UpdateUsluga(PopravkaRezervniDio u)
        {
            PopravkaRezervniDioDAO service = new PopravkaRezervniDioDAO();
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

        public async static Task<Boolean> DeleteUsluga(PopravkaRezervniDio u)
        {
            PopravkaRezervniDioDAO service = new PopravkaRezervniDioDAO();
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
