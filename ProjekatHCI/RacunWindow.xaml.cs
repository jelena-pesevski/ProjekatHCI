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
using ProjekatHCI.Service;
using ProjekatHCI.Model.DTO;
namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for RacunWindow.xaml
    /// </summary>
    public partial class RacunWindow : Window
    { 
        private Racun currRacun;
        private List<StavkePregled> items = new List<StavkePregled>();
        public RacunWindow(Racun r)
        {
            InitializeComponent();
            currRacun = r;

            totalAmount.Content = r.Cijena;
            itemsDataGrid.ItemsSource = items;
            UpdateStavke();
        }

        private async void UpdateStavke()
        {
            items.Clear();
            foreach (StavkePregled u in await StavkePregledService.GetAll(currRacun))
            {
                if (u != null)
                {
                    items.Add(u);
                }
            }

            itemsDataGrid.Items.Refresh();
        }
    }
}
