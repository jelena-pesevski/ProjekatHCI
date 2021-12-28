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
using ProjekatHCI.Util;
using ProjekatHCI.Service;

namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(usernameInput.Text) && !String.IsNullOrEmpty(passwrdInput.Password))
            {
                string type = await LoginService.CheckLogin(usernameInput.Text, passwrdInput.Password, ((ComboBoxItem)languageSelector.SelectedItem).Name);
                if (type != null)
                {
                    MainWindow w = new MainWindow();
                    w.Show();
                }
            }
        }

        private void LanguageChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            ComboBoxItem typeItem = (ComboBoxItem)cmb.SelectedItem;
            Console.WriteLine(typeItem.Name);

            if ("sr".Equals(typeItem.Name))
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr");
            }
            else
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en");
            }
        }
    }
}
