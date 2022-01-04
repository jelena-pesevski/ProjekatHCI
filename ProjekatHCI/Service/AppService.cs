using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProjekatHCI.Model.DTO;
using System.Windows;
using ProjekatHCI.Util;
namespace ProjekatHCI.Service
{
    public class AppService
    {
        public static Zaposleni CurrEmployee { get; set; }

        public static Majstor CurrMajstor { get; set; }

        public static int CurrEmployeeId { get { return CurrEmployee.IdZaposlenog;} }

        public static void SetTheme()
        {
            int themeNum = CurrEmployee.Tema;
            ResourceDictionary resourceDictionary = new ResourceDictionary();

            if (themeNum == 1)
            {
                resourceDictionary.Source = new Uri("pack://application:,,,/Resources/Themes/FirstTheme.xaml");
            }else if (themeNum == 2)
            {
                resourceDictionary.Source = new Uri("pack://application:,,,/Resources/Themes/ScndTheme.xaml");
            }else if (themeNum == 3)
            {
                resourceDictionary.Source = new Uri("pack://application:,,,/Resources/Themes/ThirdTheme.xaml");
            }

            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        public static void SetLanguage(String lang)
        {
            if ("sr".Equals(lang))
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("sr");
            }
            else
            {
                TranslationSource.Instance.CurrentCulture = new System.Globalization.CultureInfo("en");
            }
        }

        public async static void ChangeTheme(string theme)
        {
            if ("first".Equals(theme))
            {
                CurrEmployee.Tema = 1;

            }
            else if ("scnd".Equals(theme))
            {
                CurrEmployee.Tema = 2;
            }
            else
            {
                CurrEmployee.Tema = 3;
            }

            SetTheme();
            await ZaposleniService.UpdateZaposleni(CurrEmployee);
        }
    }
}
