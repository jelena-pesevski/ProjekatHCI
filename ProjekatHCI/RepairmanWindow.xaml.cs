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
using ProjekatHCI.Util;
using System.Resources;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;

namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for RepairmanWindow.xaml
    /// </summary>
    public partial class RepairmanWindow : Window
    {
        private ResourceManager mngr;
        private List<PrijavaKvara> faultReports = new List<PrijavaKvara>();
        private List<Popravka> unfinishedRepairments = new List<Popravka>();
        private List<Usluga> services = new List<Usluga>();
        private List<RezervniDio> spareParts = new List<RezervniDio>();
        public RepairmanWindow()
        {
            InitializeComponent();
            mngr = ProjekatHCI.Resources.Strings.Resources.Resource.ResourceManager;
            InitializeWindow();

            faultReportsDataGrid.ItemsSource = faultReports;
            unfinishedRepGrid.ItemsSource = unfinishedRepairments;

            SetServices();
            SetSpareParts();

            servicesCB.ItemsSource = services;
            sparePartsCB.ItemsSource = spareParts;

            UpdateFaultReports();
            UpdateUnfinishedRepairments();
        }

        private async void SetSpareParts()
        {
            spareParts = await RezervniDioService.GetAllRezDijelovi();
        }

        private async void SetServices()
        {
            services = await UslugaService.GetAllUsluge();
        }

        private async void UpdateFaultReports()
        {
            faultReports.Clear();
            foreach (PrijavaKvara p in await PrijavaKvaraService.GetAllForRepairman(AppService.CurrMajstor))
            {
                if (p != null)
                {
                    faultReports.Add(p);
                    Console.WriteLine(p.IdPrijave);
                }
            }

            faultReportsDataGrid.Items.Refresh();
        }

        private async void UpdateUnfinishedRepairments()
        {
            unfinishedRepairments.Clear();
            foreach (Popravka p in await PopravkaService.GetUnfinished())
            {
                if (p != null)
                {
                    unfinishedRepairments.Add(p);
                }
            }

            unfinishedRepGrid.Items.Refresh();
        }


        private void InitializeWindow()
        {

            languageSelector.SelectedItem = languageSelector.Items[0];

            AppService.SetLanguage(AppService.CurrEmployee.Jezik);

            if (AppService.CurrEmployee.Jezik.Equals("en"))
            {
                languageSelector.SelectedItem = languageSelector.Items[0];
            }
            else
            {
                languageSelector.SelectedItem = languageSelector.Items[1];
            }

            themeSelector.SelectedItem = themeSelector.Items[AppService.CurrEmployee.Tema - 1];
        }

        private void LanguageChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            ComboBoxItem typeItem = (ComboBoxItem)cmb.SelectedItem;
            AppService.SetLanguage(typeItem.Name);
        }

        private void ThemeChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            ComboBoxItem typeItem = (ComboBoxItem)cmb.SelectedItem;
            AppService.ChangeTheme(typeItem.Name);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            AppService.CurrEmployee = null;
            AppService.CurrMajstor = null;
            LoginWindow win = new LoginWindow();
            win.Show();
            this.Close();
        }

        private async void startBtn_Click(object sender, RoutedEventArgs e)
        {
            PrijavaKvara selectedItem = (PrijavaKvara)faultReportsDataGrid.SelectedItem;
            if (selectedItem != null && selectedItem.Status.Equals("nije zapoceto"))
            {
                Popravka p = new Popravka(selectedItem.Majstor_IdZaposlenog, selectedItem.IdPrijave);
                Boolean result = await PopravkaService.Add(p);
                if (result)
                {
                    selectedItem.Status = "u toku";
                    Boolean res = await PrijavaKvaraService.Update(selectedItem);
                    if (res)
                    {
                        faultReportsDataGrid.Items.Refresh();
                        MessageBox.Show(mngr.GetString("repairmentStartSuccess", TranslationSource.Instance.CurrentCulture));
                    }
                    else
                    {
                        MessageBox.Show(mngr.GetString("repairmentStartError", TranslationSource.Instance.CurrentCulture));
                    }
                }
                else
                {
                    MessageBox.Show(mngr.GetString("repairmentStartError", TranslationSource.Instance.CurrentCulture));
                }

            }
        }

        private async void detailsBtn_Click(object sender, RoutedEventArgs e)
        {
            PrijavaKvara selectedItem = (PrijavaKvara)faultReportsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                Klijent k = await KlijentService.GetOne(new Klijent(selectedItem.IdKlijenta));
                ClientDetailsWindow cdw = new ClientDetailsWindow(k);
                cdw.Show();
            }
        }

        private async void addServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            Popravka selectedPopravka = (Popravka)unfinishedRepGrid.SelectedItem;
            Usluga selectedItem = (Usluga)servicesCB.SelectedItem;
            if (selectedPopravka!=null && selectedItem != null)
            {
                if (!String.IsNullOrEmpty(serviceAmount.Text))
                {
                    int value = 0;
                    try
                    {
                        value = Int32.Parse(serviceAmount.Text);
                    }
                    catch (Exception ex)
                    {
                        return;
                    }

                    PopravkaUsluga pu = new PopravkaUsluga(selectedPopravka.IdPopravke, selectedItem.IdUsluge, value, selectedItem.Cijena);
                    Boolean result = await PopravkaUslugaService.AddUsluga(pu);

                    if (result)
                    {
                        MessageBox.Show(mngr.GetString("addSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    }
                    else
                    {
                        MessageBox.Show(mngr.GetString("addFailedMsg", TranslationSource.Instance.CurrentCulture));
                    }
                }
            }

            servicesCB.SelectedIndex = -1;
            serviceAmount.Text = "";
        }

        private void addSparePartBtn_Click(object sender, RoutedEventArgs e)
        {
            //gledati koliko je dostupno rezervnog dijela
        }
    }
}
