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
using System.Resources;
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

        //  TODO dodati processing za login
        private async void Login(object sender, RoutedEventArgs e)
        {
            ResourceManager mngr = ProjekatHCI.Resources.Strings.Resources.Resource.ResourceManager;
            if (!String.IsNullOrEmpty(usernameInput.Text) && !String.IsNullOrEmpty(passwrdInput.Password))
            {
                string type = await LoginService.CheckLogin(usernameInput.Text, passwrdInput.Password, ((ComboBoxItem)languageSelector.SelectedItem).Name);
                if (type != null)
                {
                    if ("A".Equals(type))
                    {
                        AdminWindow win = new AdminWindow();
                        win.Show();
                        this.Close();
                    }else if ("M".Equals(type))
                    {
                        RepairmanWindow win = new RepairmanWindow();
                        win.Show();
                        this.Close();
                    }
                    else
                    {
                        OperatorWindow win = new OperatorWindow();
                        win.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(mngr.GetString("loginFailMsg", TranslationSource.Instance.CurrentCulture));
                    usernameInput.Text = "";
                    passwrdInput.Password = "";
                }
            }
            else
            {
                MessageBox.Show(mngr.GetString("emptyFieldsMsg", TranslationSource.Instance.CurrentCulture));
            }
        }

        private void LanguageChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            ComboBoxItem typeItem = (ComboBoxItem)cmb.SelectedItem;
            AppService.SetLanguage(typeItem.Name);
        }
    }
}
