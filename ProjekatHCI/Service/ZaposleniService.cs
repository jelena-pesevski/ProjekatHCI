using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI.Service
{
    public class ZaposleniService
    {

        public async static Task UpdateZaposleni(Zaposleni z)
        {
            ZaposleniDAO service = new ZaposleniDAO();
            int result = await service.Update(z);
        }
    }
}
