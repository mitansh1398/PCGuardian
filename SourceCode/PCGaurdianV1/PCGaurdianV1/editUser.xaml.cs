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
    /// Interaction logic for editUser.xaml
    /// </summary>
    public partial class editUser : Page
    {
        public editUser()
        {
            InitializeComponent();
            Application app = Application.Current;
            info.Text = "You are editing " + app.Properties["uname"];
        }

        private void cpass_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new cpassUser());
        }

        private void deluser_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new delUser());
        }

        private void capps_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new adminPortal());
        }
    }
}
