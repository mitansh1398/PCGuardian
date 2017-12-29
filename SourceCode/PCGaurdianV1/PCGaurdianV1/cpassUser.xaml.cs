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
    /// Interaction logic for cpassUser.xaml
    /// </summary>
    public partial class cpassUser : Page
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);   
        public cpassUser()
        {
            InitializeComponent();
        }
        String newp, confp;

        private void confPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            confp = confPass.Password;
            if(confPass.Password != newPass.Password)
            {
                error.Text = "Password do not match";
                error.Visibility = Visibility.Visible;
            }
            else
            {
                error.Visibility = Visibility.Collapsed;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            isoStore.Close();
            this.NavigationService.Navigate(new editUser());
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            isoStore.DeleteFile("PCGuardian/users/" + app.Properties["uname"] + "/passwd.txt");
            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(("PCGuardian/users/" + app.Properties["uname"] + "/passwd.txt"), FileMode.Create, isoStore))
            {
                using (StreamWriter writer = new StreamWriter(isoStream))
                {
                    if(newp == confp)
                    {
                        error.Visibility = Visibility.Collapsed;
                        writer.WriteLine(confPass.Password);
                        writer.Close();
                    }
                    else
                    {
                        error.Text = "Please enter the credentials correctly";
                        error.Visibility = Visibility.Visible;
                    }
                }
                isoStream.Close();
            }
            isoStore.Close();
            this.NavigationService.Navigate(new adminPortal());
        }

        private void newPass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            newp = newPass.Password;
        }

    }
}
