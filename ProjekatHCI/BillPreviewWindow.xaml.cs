using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
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
using System.Windows.Threading;
using ProjekatHCI.Model.DAO;
using ProjekatHCI.Model.DTO;
using ProjekatHCI.Service;
using ProjekatHCI.Util;
namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for BillPreviewWindow.xaml
    /// </summary>
    public partial class BillPreviewWindow : Window
    {
        private ResourceManager mngr;
        private List<PregledUsluga> services = new List<PregledUsluga>();
        private List<PregledRezervniDio> parts = new List<PregledRezervniDio>();
        private Popravka currPopravka;
        public BillPreviewWindow(Popravka p)
        {
            InitializeComponent();
            mngr = ProjekatHCI.Resources.Strings.Resources.Resource.ResourceManager;
            currPopravka = p;

            servicesDataGrid.ItemsSource = services;
            partsDataGrid.ItemsSource = parts;

            UpdateServices();
            UpdateParts();
        }

        private async void UpdateServices()
        {
            services.Clear();
            foreach (PregledUsluga u in await PregledUslugaService.GetAll(currPopravka)) { 
                if (u != null)
                {
                    services.Add(u);
                }
            }

            servicesDataGrid.Items.Refresh();
        }

        private async void UpdateParts()
        {
            parts.Clear();
            foreach (PregledRezervniDio u in await PregledRezervniDioService.GetAll(currPopravka))
            {
                if (u != null)
                {
                    parts.Add(u);
                }
            }

            partsDataGrid.Items.Refresh();
        }

        private async void deleteRezDioBtn_Click(object sender, RoutedEventArgs e)
        {
            PregledRezervniDio selectedItem = (PregledRezervniDio)partsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                Boolean result = await PopravkaRezervniDioService.DeleteRezDio(new PopravkaRezervniDio(selectedItem.IdPopravke, selectedItem.Sifra, selectedItem.Kolicina, selectedItem.Cijena));

                if (!result)
                {
                    MessageBox.Show(mngr.GetString("deleteFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
                else
                {
                    parts.Remove(selectedItem);
                    partsDataGrid.Items.Refresh();
                    MessageBox.Show(mngr.GetString("deleteSuccessMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
        }

        private async void deleteUslugaBtn_Click(object sender, RoutedEventArgs e)
        {
            PregledUsluga selectedItem = (PregledUsluga)servicesDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                Boolean result = await PopravkaUslugaService.DeleteUsluga(new PopravkaUsluga(selectedItem.IdPopravke, selectedItem.IdUsluge, selectedItem.Kolicina, selectedItem.Cijena));

                if (!result)
                {
                    MessageBox.Show(mngr.GetString("deleteFailedMsg", TranslationSource.Instance.CurrentCulture));
                }
                else
                {
                    services.Remove(selectedItem);
                    servicesDataGrid.Items.Refresh();
                    MessageBox.Show(mngr.GetString("deleteSuccessMsg", TranslationSource.Instance.CurrentCulture));
                }
            }
        }

        private void servicesDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ListCollectionView view = CollectionViewSource.GetDefaultView(dg.ItemsSource) as ListCollectionView;
                if (view.IsEditingItem)
                {
                    PregledUsluga updatedItem = (PregledUsluga)view.CurrentEditItem;

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

        private async void PerformUpdateService(PregledUsluga u)
        {
            bool result = await PopravkaUslugaService.UpdateUsluga(new PopravkaUsluga(u.IdPopravke, u.IdUsluge, u.Kolicina, u.Cijena));
            if (result)
            {
                MessageBox.Show(mngr.GetString("updateSuccessMsg", TranslationSource.Instance.CurrentCulture));
            }
            else
            {
                MessageBox.Show(mngr.GetString("updateFailedMsg", TranslationSource.Instance.CurrentCulture));
                PopravkaUsluga old = await PopravkaUslugaService.GetOne(u);
                u.Kolicina = old.Kolicina;
             
            }
            servicesDataGrid.Items.Refresh();
        }

        private void partsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            if (e.EditAction == DataGridEditAction.Commit)
            {
                ListCollectionView view = CollectionViewSource.GetDefaultView(dg.ItemsSource) as ListCollectionView;
                if (view.IsEditingItem)
                {
                    PregledRezervniDio updatedItem = (PregledRezervniDio)view.CurrentEditItem;

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
        private async void PerformUpdatePart(PregledRezervniDio r)
        {
            bool result = await PopravkaRezervniDioService.UpdateRezDio(new PopravkaRezervniDio(r.IdPopravke, r.Sifra, r.Kolicina, r.Cijena));
            if (result)
            {
                MessageBox.Show(mngr.GetString("updateSuccessMsg", TranslationSource.Instance.CurrentCulture));
            }
            else
            {
                MessageBox.Show(mngr.GetString("updateFailedMsg", TranslationSource.Instance.CurrentCulture));
                PopravkaRezervniDio old = await PopravkaRezervniDioService.GetOne(r);
                r.Kolicina = old.Kolicina;

            }
            servicesDataGrid.Items.Refresh();
        }
    }
}
