using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;
namespace ProjekatHCI.Service
{
    class PregledRezervniDioService
    {
        public async static Task<List<PregledRezervniDio>> GetAll(Popravka p)
        {
            PregledRezervniDioDAO service = new PregledRezervniDioDAO();
            List<PregledRezervniDio> result = await service.GetAllForPopravka(p);
            return result;
        }
    }
}
