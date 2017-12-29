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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using System.IO;

namespace PCGaurdianV1
{
    /// <summary>
    /// Interaction logic for askNewUserPage.xaml
    /// </summary>
    public partial class askNewUserPage : Page
    {
        public askNewUserPage()
        {
            InitializeComponent();
        }
        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new createUserSetup());
        }

        private void endSetup_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("It is recommended to restart your PC after you are done with setup.");
            Application.Current.Shutdown();
        }

        private void adminPortal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("It is recommended to restart your PC after you are done with setup.");
            this.NavigationService.Navigate(new adminPortal());
        }
    }
}
