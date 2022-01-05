using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;
namespace ProjekatHCI.Service
{
    class PregledUslugaService
    {
        public async static Task<List<PregledUsluga>> GetAll(Popravka p)
        {
            PregledUslugaDAO service = new PregledUslugaDAO();
            List<PregledUsluga> result = await service.GetAllForPopravka(p);
            return result;
        }
    }
}
