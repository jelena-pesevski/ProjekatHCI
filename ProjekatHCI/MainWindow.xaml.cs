using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjekatHCI.Util;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeLang(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("cao");
             SetLanguageAsync();
        }

        public static async Task SetLanguageAsync()
        {
            TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr");

            ZaposleniDAO service = new ZaposleniDAO();
            List<Zaposleni> zaposleni = await service.GetAll();
            foreach(Zaposleni z in zaposleni)
            {
                Console.WriteLine(z.Ime+ " "+z.Prezime );
            }

        /*    Zaposleni zz= await service.GetById(new Zaposleni(4, null, null, null, null, null, 1, null));
            Console.WriteLine(zz.Ime + " " + zz.Prezime);

            Zaposleni novi = new Zaposleni(3, "Admin", "Admin1", "admin", "admin", "A", 1, "en");
            service.Update(novi);
            service.Delete(new Zaposleni(4, "Admin", "Admin1", "admin", "admin", "O", 1, "en"));*/

         //   OperaterDAO servisOp = new OperaterDAO();
         //   servisOp.Delete(zz);
          //  service.Delete(zz);
            //service.Insert(novi);

        }
    }
}
