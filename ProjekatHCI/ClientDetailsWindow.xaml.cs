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
using System.Windows.Shapes;
using ProjekatHCI.Model.DTO;
namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for ClientDetailsWindow.xaml
    /// </summary>
    public partial class ClientDetailsWindow : Window
    {
        public ClientDetailsWindow(Klijent k)
        {
            InitializeComponent();

            firstname.Content = k.Ime;
            lastname.Content = k.Prezime;
            address.Content = k.Adresa;
            phone.Content = k.Telefon;
        }
    }
}
