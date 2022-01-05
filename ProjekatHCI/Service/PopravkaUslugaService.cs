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

        public async static Task<List<PopravkaUsluga>> GetAll()
        {
            PopravkaUslugaDAO service = new PopravkaUslugaDAO();
            List<PopravkaUsluga> result = await service.GetAll();
            return result;
        }

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

        public async static Task<Boolean> UpdateIfExists(PopravkaUsluga u)
        {

            List<PopravkaUsluga> list = await GetAll();
            foreach(PopravkaUsluga p in list) {
                if(p.IdPopravke==u.IdPopravke && p.IdUsluge == u.IdUsluge)
                {
                    p.Kolicina = p.Kolicina + u.Kolicina;
                    p.Cijena = u.Cijena; //new price if old is changed
                    return await UpdateUsluga(p);
                }
            
            }

            return false;
        }

        public async static Task<PopravkaUsluga> GetOne(PregledUsluga u)
        {
            PopravkaUslugaDAO service = new PopravkaUslugaDAO();
            PopravkaUsluga one = await service.GetById(new PopravkaUsluga(u.IdPopravke, u.IdUsluge, 0,0));
            return one;
        }
    }
}
