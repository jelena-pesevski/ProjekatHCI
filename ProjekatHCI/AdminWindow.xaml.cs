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
using ProjekatHCI.Model.DTO;
using System.Windows.Threading;

namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private ResourceManager mngr;
        private List<Zaposleni> employees= new List<Zaposleni>();
        private List<Usluga> services=new List<Usluga>();
        private List<RezervniDio> spareParts=new List<RezervniDio>();
        private List<Popravka> repairments = new List<Popravka>();
        private List<Racun> bills = new List<Racun>();
        public AdminWindow()
        {
            InitializeComponent();
            mngr = ProjekatHCI.Resources.Strings.Resources.Resource.ResourceManager;

            InitializeWindow();
            SetDataSources();
            UpdateDataGrids();
           
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

        private void SetDataSources()
        {
            employeesDataGrid.ItemsSource = employees;
            servicesDataGrid.ItemsSource = services;
            sparePartsDataGrid.ItemsSource = spareParts;
            repairmentsDataGrid.ItemsSource = repairments;
            billsDataGrid.ItemsSource = bills;
        }

        private async void UpdateEmployees()
        {
            employees.Clear();
            foreach (Zaposleni z in await ZaposleniService.GetAllZaposleni())
            {
                if (z != null)
                {
                    employees.Add(z);
                }

            }

            employeesDataGrid.Items.Refresh();
        }

        private async void UpdateServices()
        {
            services.Clear();
            foreach (Usluga u in await UslugaService.GetAllUsluge())
            {
                if (u != null)
                {
                    services.Add(u);
                }
            }

            servicesDataGrid.Items.Refresh();
        }

        private async void UpdateSpareParts()
        {
            spareParts.Clear();
            foreach (RezervniDio r in await RezervniDioService.GetAllRezDijelovi())
            {
                if (r != null)
                {
                    spareParts.Add(r);
                }
            }

            sparePartsDataGrid.Items.Refresh();
        }

        private async void UpdateRepairments()
        {
            repairments.Clear();
            foreach (Popravka p in await PopravkaService.GetAll())
            {
                if (p != null)
                {
                    repairments.Add(p);
                }
            }

            repairmentsDataGrid.Items.Refresh();
        }
        private async void UpdateBills()
        {
            bills.Clear();
            foreach (Racun r in await RacunService.GetAll())
            {
                if (r != null)
                {
                    bills.Add(r);
                }
            }

            billsDataGrid.Items.Refresh();
        }

        private async void UpdateDataGrids()
        {
            UpdateEmployees();
            UpdateServices();
            UpdateSpareParts();
            UpdateRepairments();
            UpdateBills();
        }


        private void LanguageChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            ComboBoxItem typeItem =(ComboBoxItem) cmb.SelectedItem;
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
            LoginWindow win = new LoginWindow();
            win.Show();
            this.Close();
        }

        private async void addBtnEmp_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidityEmployee())
            {
                bool result = await AddZaposleni();
                if (result)
                {
                    MessageBox.Show(mngr.GetString("addSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    ClearFieldsEmployees();
                    UpdateEmployees();
                }
                else
                {
                    MessageBox.Show(mngr.GetString("addFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
            else
            {
                MessageBox.Show(mngr.GetString("emptyFieldsWarn", TranslationSource.Instance.CurrentCulture));
            }
        }

        private async void updateBtnEmp_Click(object sender, RoutedEventArgs e)
        {
            Zaposleni selectedItem = (Zaposleni)employeesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                bool result = await ZaposleniService.UpdateZaposleni(selectedItem);
                if (result)
                {
                    MessageBox.Show(mngr.GetString("updateSuccessMsg", TranslationSource.Instance.CurrentCulture));

                }
                else
                {
                    MessageBox.Show(mngr.GetString("updateFailedMsg", TranslationSource.Instance.CurrentCulture));
                    Zaposleni z = await ZaposleniService.GetOne(selectedItem);
                    int index = employees.IndexOf(selectedItem);
                    employees[index] = z;
                }
                employeesDataGrid.Items.Refresh();
            }
        }

        private async void deleteBtnEmp_Click(object sender, RoutedEventArgs e)
        {
            Zaposleni selectedItem = (Zaposleni)employeesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                bool result = await ZaposleniService.DeleteZaposleni(selectedItem);
                if (result)
                {
                    MessageBox.Show(mngr.GetString("deleteSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    UpdateEmployees();
                }
                else
                {
                    MessageBox.Show(mngr.GetString("deleteFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
        }

        private async void addBtnService_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidityService())
            {
                bool result = await AddUsluga();
                if (result)
                {
                    MessageBox.Show(mngr.GetString("addSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    ClearFieldsService();
                    servicesDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show(mngr.GetString("addFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
            else
            {
                MessageBox.Show(mngr.GetString("emptyFieldsWarn", TranslationSource.Instance.CurrentCulture));
            }
        }

        private void updateBtnService_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void deleteBtnService_Click(object sender, RoutedEventArgs e)
        {
            Usluga selectedItem = (Usluga)servicesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                bool result = await UslugaService.DeleteUsluga(selectedItem);
                if (result)
                {
                    MessageBox.Show(mngr.GetString("deleteSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    int index = services.IndexOf(selectedItem);
                    services.RemoveAt(index);
                    servicesDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show(mngr.GetString("deleteFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
        }

        private async void addBtnParts_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidityParts())
            {
                bool result = await AddRezDio();
                if (result)
                {
                    MessageBox.Show(mngr.GetString("addSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    ClearFieldsSparePart();
                    sparePartsDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show(mngr.GetString("addFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
            else
            {
                MessageBox.Show(mngr.GetString("emptyFieldsWarn", TranslationSource.Instance.CurrentCulture));
            }
        }

        private void updateBtnParts_Click(object sender, RoutedEventArgs e)
        {
      
        }

        private async void deleteBtnParts_Click(object sender, RoutedEventArgs e)
        {
            RezervniDio selectedItem = (RezervniDio)sparePartsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                bool result = await RezervniDioService.DeleteRezDio(selectedItem);
                if (result)
                {
                    MessageBox.Show(mngr.GetString("deleteSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    int index = spareParts.IndexOf(selectedItem);
                    spareParts.RemoveAt(index);
                    sparePartsDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show(mngr.GetString("deleteFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
        }

        private bool CheckValidityEmployee()
        {

            if (String.IsNullOrEmpty(firstname.Text) || String.IsNullOrEmpty(lastname.Text) || String.IsNullOrEmpty(username.Text) ||
                String.IsNullOrEmpty(password.Text) || type.SelectedIndex < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckValidityService()
        {
            if (String.IsNullOrEmpty(serviceName.Text) || String.IsNullOrEmpty(serviceCost.Text))
            {
                return false;
            }
            else
            {
                try
                {
                    Double.Parse(serviceCost.Text);
                }
                catch(Exception e)
                {
                    return false;
                }

                return true;
            }
        }

        private bool CheckValidityParts()
        {
            if (String.IsNullOrEmpty(partName.Text) || String.IsNullOrEmpty(partCost.Text) || String.IsNullOrEmpty(partAmount.Text))
            {
                return false;
            }
            else
            {
                try
                {
                    Double.Parse(partCost.Text);
                    Int32.Parse(partAmount.Text);
                }
                catch (Exception e)
                {
                    return false;
                }

                return true;
            }
        }

        private async Task<Boolean> AddZaposleni()
        {
            string selectedType = ((ComboBoxItem)type.SelectedItem).Name;
            string selectedStatus = ((ComboBoxItem)status.SelectedItem).Name;
            Zaposleni z = new Zaposleni(0, firstname.Text, lastname.Text, username.Text, password.Text, selectedType, 1, "en", selectedStatus);

            Boolean result = await ZaposleniService.AddZaposleni(z);
            return result;
                
        }

        private async Task<Boolean> AddUsluga()
        {

            Usluga u = new Usluga(0, serviceName.Text, Double.Parse(serviceCost.Text));

            Boolean result = await UslugaService.AddUsluga(u);
            if (result)
            {
                services.Add(u);
            }

            return result;

        }
        private async Task<Boolean> AddRezDio()
        {

            RezervniDio r = new RezervniDio(0, partName.Text, Double.Parse(partCost.Text), Int32.Parse(partAmount.Text));

            Boolean result = await RezervniDioService.AddRezDio(r);
            if (result)
            {
                spareParts.Add(r);
            }
            return result;

        }

        private void ClearFieldsEmployees()
        {
            firstname.Text = "";
            lastname.Text = "";
            username.Text = "";
            password.Text = "";
            type.SelectedIndex = -1;
            status.SelectedIndex = 1;
        }

        private void ClearFieldsService()
        {
            serviceName.Text = "";
            serviceCost.Text = "";
        }

        private void ClearFieldsSparePart()
        {
            partName.Text = "";
            partCost.Text = "";
            partAmount.Text = "";
        }

     /*   private void employeesDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            Undo.Visibility = Visibility.Visible;
        }

        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            UpdateEmployees();
            Undo.Visibility = Visibility.Hidden;
        }*/

        private void employeesDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if(e.EditAction == DataGridEditAction.Commit)
            {
                ListCollectionView view = CollectionViewSource.GetDefaultView(dg.ItemsSource) as ListCollectionView;
                if (view.IsEditingItem)
                {
                    Zaposleni updatedItem = (Zaposleni)view.CurrentEditItem;

                    ConfirmationBox box = new ConfirmationBox();
                    bool result = (bool)box.ShowDialog();
                    if (result)
                    {
                        this.Dispatcher.BeginInvoke(new DispatcherOperationCallback(param =>
                        {

                            PerformUpdateEmployee(updatedItem);

                            return null;
                        }), DispatcherPriority.Background, new object[] { null });
                    }
                    else
                    {
                        dg.CancelEdit();
                    }
                }
            }
        }



        private async void PerformUpdateEmployee(Zaposleni z)
        {
            bool result = await ZaposleniService.UpdateZaposleni(z);
            if (result)
            {
                MessageBox.Show(mngr.GetString("updateSuccessMsg", TranslationSource.Instance.CurrentCulture));
                ZaposleniService.SetCipher(z);
            }
            else
            {
                MessageBox.Show(mngr.GetString("updateFailedMsg", TranslationSource.Instance.CurrentCulture));
                Zaposleni old = await ZaposleniService.GetOne(z);
                int index = employees.IndexOf(z);
                if (old != null)
                {
                    employees.RemoveAt(index);
                    employees.Add(old);
                   
                }
            }
            employeesDataGrid.Items.Refresh();
        }

        private void servicesDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ListCollectionView view = CollectionViewSource.GetDefaultView(dg.ItemsSource) as ListCollectionView;
                if (view.IsEditingItem)
                {
                    Usluga updatedItem = (Usluga)view.CurrentEditItem;

                    ConfirmationBox box = new ConfirmationBox();
                    bool result = (bool)box.ShowDialog();
                    if (result)
                    {
                        this.Dispatcher.BeginInvoke(new DispatcherOperationCallback(param =>
                        {

                            PerformUpdateService(updatedItem);

                            return null;
                        }), DispatcherPriority.Background, new object[] { null });
                    }
                    else
                    {
                        dg.CancelEdit();
                    }
                }
            }
        }

        private async void PerformUpdateService(Usluga u)
        {
            bool result = await UslugaService.UpdateUsluga(u);
            if (result)
            {
                MessageBox.Show(mngr.GetString("updateSuccessMsg", TranslationSource.Instance.CurrentCulture));
            }
            else
            {
                MessageBox.Show(mngr.GetString("updateFailedMsg", TranslationSource.Instance.CurrentCulture));
                Usluga old = await UslugaService.GetOne(u);
                int index = services.IndexOf(u);
                if (old != null)
                {
                    services.RemoveAt(index);
                    services.Add(old);

                }
            }
            servicesDataGrid.Items.Refresh();
        }

        private void sparePartsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ListCollectionView view = CollectionViewSource.GetDefaultView(dg.ItemsSource) as ListCollectionView;
                if (view.IsEditingItem)
                {
                    RezervniDio updatedItem = (RezervniDio)view.CurrentEditItem;

                    ConfirmationBox box = new ConfirmationBox();
                    bool result = (bool)box.ShowDialog();
                    if (result)
                    {
                        this.Dispatcher.BeginInvoke(new DispatcherOperationCallback(param =>
                        {

                            PerformUpdatePart(updatedItem);

                            return null;
                        }), DispatcherPriority.Background, new object[] { null });
                    }
                    else
                    {
                        dg.CancelEdit();
                    }
                }
            }
        }

        private async void PerformUpdatePart(RezervniDio r)
        {
           
            bool result = await RezervniDioService.UpdateRezDio(r);
            if (result)
            {
                MessageBox.Show(mngr.GetString("updateSuccessMsg", TranslationSource.Instance.CurrentCulture));
            }
            else
            {
                MessageBox.Show(mngr.GetString("updateFailedMsg", TranslationSource.Instance.CurrentCulture));
                RezervniDio old = await RezervniDioService.GetOne(r);
                int index = spareParts.IndexOf(r);
                if (old != null)
                {
                    spareParts.RemoveAt(index);
                    spareParts.Add(old);

                }
            }
            sparePartsDataGrid.Items.Refresh();
        }
    }
}
