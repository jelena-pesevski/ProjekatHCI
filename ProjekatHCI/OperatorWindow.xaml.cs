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
using System.Windows.Threading;

namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        private ResourceManager mngr;
        private List<Klijent> clients=new List<Klijent>();
        private List<PrijavaKvara> faultReports = new List<PrijavaKvara>();

        public OperatorWindow()
        {
            InitializeComponent();
            mngr = ProjekatHCI.Resources.Strings.Resources.Resource.ResourceManager;
            InitializeWindow();

            clientsDataGrid.ItemsSource = clients;
            faultReportsDataGrid.ItemsSource = faultReports;

            UpdateClients();
            UpdateFaultReports();
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
            LoginWindow win = new LoginWindow();
            win.Show();
            this.Close();
        }

        private async void addBtnClient_Click(object sender, RoutedEventArgs e)
        {
            if (CheckValidityClient())
            {
                bool result = await AddKlijent();
                if (result)
                {
                    MessageBox.Show(mngr.GetString("addSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    ClearFieldsClient();
                    clientsDataGrid.Items.Refresh();
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

        private async void deleteBtnClient_Click(object sender, RoutedEventArgs e)
        {
            Klijent selectedItem = (Klijent)clientsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                bool result = await KlijentService.DeleteKlijent(selectedItem);
                if (result)
                {
                    MessageBox.Show(mngr.GetString("deleteSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    int index = clients.IndexOf(selectedItem);
                    clients.RemoveAt(index);
                    clientsDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show(mngr.GetString("deleteFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
        }

        private async void addFaultReport_Click(object sender, RoutedEventArgs e)
        {
            Klijent selectedItem = (Klijent)clientsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                int clientId = selectedItem.IdKlijenta;

                DescriptionInput dialog = new DescriptionInput();
                bool ok = (bool)dialog.ShowDialog();
                if (ok)
                {
                    string description = dialog.Description;
                    int operatorId = AppService.CurrEmployeeId;

                    PrijavaKvara p = new PrijavaKvara(description, operatorId, clientId);
                    bool result = await AddPrijavaKvara(p);

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
        }

      
        private async void deleteBtnFr_Click(object sender, RoutedEventArgs e)
        {
            PrijavaKvara selectedItem = (PrijavaKvara)faultReportsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                bool result = await PrijavaKvaraService.Delete(selectedItem);
                if (result)
                {
                    MessageBox.Show(mngr.GetString("deleteSuccessMsg", TranslationSource.Instance.CurrentCulture));
                    int index = faultReports.IndexOf(selectedItem);
                    faultReports.RemoveAt(index);
                    faultReportsDataGrid.Items.Refresh();
                }
                else
                {
                    MessageBox.Show(mngr.GetString("deleteFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
        }

        private async void UpdateClients()
        {
            clients.Clear();
            foreach (Klijent k in await KlijentService.GetAllKlijenti())
            {
                if (k != null)
                {
                    clients.Add(k);
                }
            }

            clientsDataGrid.Items.Refresh();
        }

        private async void UpdateFaultReports()
        {
            faultReports.Clear();
            foreach (PrijavaKvara p in await PrijavaKvaraService.GetAll())
            {
                if (p != null)
                {
                    faultReports.Add(p);
                }
            }

            faultReportsDataGrid.Items.Refresh();
        }


        private void ClearFieldsClient()
        {
            firstname.Text = "";
            lastname.Text = "";
            address.Text = "";
            phone.Text = "";
        }

        private async Task<Boolean> AddKlijent()
        {

            Klijent k = new Klijent(0, firstname.Text, lastname.Text, address.Text, phone.Text);

            Boolean result = await KlijentService.AddKlijent(k);
            if (result)
            {
                clients.Add(k);
            }

            return result;

        }

        private bool CheckValidityClient()
        {

            if (String.IsNullOrEmpty(firstname.Text) || String.IsNullOrEmpty(lastname.Text) || String.IsNullOrEmpty(address.Text) || String.IsNullOrEmpty(phone.Text))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<Boolean> AddPrijavaKvara(PrijavaKvara p)
        {
            bool result = await PrijavaKvaraService.Add(p);
            if (result)
            {
                UpdateFaultReports();
            }
            return result;
        }

        private void clientsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ListCollectionView view = CollectionViewSource.GetDefaultView(dg.ItemsSource) as ListCollectionView;
                if (view.IsEditingItem)
                {
                    Klijent updatedItem = (Klijent)view.CurrentEditItem;

                    ConfirmationBox box = new ConfirmationBox();
                    bool result = (bool)box.ShowDialog();
                    if (result)
                    {
                        this.Dispatcher.BeginInvoke(new DispatcherOperationCallback(param =>
                        {

                            PerformUpdateClient(updatedItem);

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

        private async void PerformUpdateClient(Klijent updatedItem)
        {
            bool result = await KlijentService.UpdateKlijent(updatedItem);
            if (result)
            {
                MessageBox.Show(mngr.GetString("updateSuccessMsg", TranslationSource.Instance.CurrentCulture));
            }
            else
            {
                MessageBox.Show(mngr.GetString("updateFailedMsg", TranslationSource.Instance.CurrentCulture));
                Klijent old = await KlijentService.GetOne(updatedItem);
                int index = clients.IndexOf(updatedItem);
                if (old != null)
                {
                    clients.RemoveAt(index);
                    clients.Add(old);

                }
            }
            clientsDataGrid.Items.Refresh();
        }

        private void faultReportsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ListCollectionView view = CollectionViewSource.GetDefaultView(dg.ItemsSource) as ListCollectionView;
                if (view.IsEditingItem)
                {
                    PrijavaKvara updatedItem = (PrijavaKvara)view.CurrentEditItem;

                    ConfirmationBox box = new ConfirmationBox();
                    bool result = (bool)box.ShowDialog();
                    if (result)
                    {
                        this.Dispatcher.BeginInvoke(new DispatcherOperationCallback(param =>
                        {

                            PerformUpdateFaultReport(updatedItem);

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

        private async void PerformUpdateFaultReport(PrijavaKvara updatedItem)
        {
            bool result = await PrijavaKvaraService.Update(updatedItem);
            if (result)
            {
                MessageBox.Show(mngr.GetString("updateSuccessMsg", TranslationSource.Instance.CurrentCulture));
            }
            else
            {
                MessageBox.Show(mngr.GetString("updateFailedMsg", TranslationSource.Instance.CurrentCulture));
                PrijavaKvara old = await PrijavaKvaraService.GetOne(updatedItem);
                int index = faultReports.IndexOf(updatedItem);
                if (old != null)
                {
                    faultReports.RemoveAt(index);
                    faultReports.Add(old);

                }
            }
            faultReportsDataGrid.Items.Refresh();
        }
    }
}
