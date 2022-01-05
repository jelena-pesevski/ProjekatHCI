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
        public async static Task<List<PopravkaRezervniDio>> GetAll()
        {
            PopravkaRezervniDioDAO service = new PopravkaRezervniDioDAO();
            List<PopravkaRezervniDio> result = await service.GetAll();
            return result;
        }

        public static async Task<Boolean> AddRezDio(PopravkaRezervniDio u)
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

        public async static Task<Boolean> UpdateRezDio(PopravkaRezervniDio u)
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

        public async static Task<Boolean> DeleteRezDio(PopravkaRezervniDio u)
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

        public async static Task<Boolean> UpdateIfExists(PopravkaRezervniDio u)
        {

            List<PopravkaRezervniDio> list = await GetAll();
            foreach (PopravkaRezervniDio p in list)
            {
                if (p.IdPopravke == u.IdPopravke && p.Sifra == u.Sifra)
                {
                    //get rezervni dio
                    RezervniDio available = await RezervniDioService.GetOne(new RezervniDio(p.Sifra));
                    if (available.Kolicina >=u.Kolicina) //onoliko koliko zelimo da jos dodamo
                    {
                        p.Kolicina = p.Kolicina + u.Kolicina;
                        p.Cijena = u.Cijena; //new price if old is changed
                        return await UpdateRezDio(p);
                    }
                }
            }
            return false;
        }

        public async static Task<PopravkaRezervniDio> GetOne(PregledRezervniDio u)
        {
            PopravkaRezervniDioDAO service = new PopravkaRezervniDioDAO();
            PopravkaRezervniDio one = await service.GetById(new PopravkaRezervniDio(u.IdPopravke, u.Sifra, 0, 0));
            return one;
        }
    }
}
