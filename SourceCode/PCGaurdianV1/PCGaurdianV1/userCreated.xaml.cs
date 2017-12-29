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

namespace PCGaurdianV1
{
    /// <summary>
    /// Interaction logic for userCreated.xaml
    /// </summary>
    public partial class userCreated : Page
    {
        public userCreated()
        {
            InitializeComponent();
        }

        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new createUserSetup());
        }

        private void adminPortal_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new adminPortal());
        }
    }
}
