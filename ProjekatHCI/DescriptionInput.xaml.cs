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

namespace ProjekatHCI
{
    /// <summary>
    /// Interaction logic for DescriptionInput.xaml
    /// </summary>
    public partial class DescriptionInput : Window
    {
        public DescriptionInput()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public string Description
        {
            get
            {
                return description.Text;
            }
        }
    }
}
