using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Service
{
    public class MajstorService
    {

        public async static Task<Majstor> GetOne(Majstor u)
        {
            MajstorDAO service = new MajstorDAO();
            Majstor one = await service.GetById(u);
            return one;
        }
    }
}
