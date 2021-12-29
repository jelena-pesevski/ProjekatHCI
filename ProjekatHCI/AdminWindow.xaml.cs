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

namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private ResourceManager mngr;
        public AdminWindow()
        {
            InitializeComponent();
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
    }
}
