using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;
namespace ProjekatHCI.Service
{
    class StavkePregledService
    {
        public async static Task<List<StavkePregled>> GetAll(Racun r)
        {
            StavkePregledDAO service = new StavkePregledDAO();
            List<StavkePregled> result = await service.GetAllForRacun(r);
            return result;
        }
    }
}
