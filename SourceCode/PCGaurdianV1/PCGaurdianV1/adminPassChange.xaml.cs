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
using System.IO;
using System.IO.IsolatedStorage;
namespace PCGaurdianV1
{
    /// <summary>
    /// Interaction logic for adminPassChange.xaml
    /// </summary>
    public partial class adminPassChange : Page
    {
        String ori, newp, confp;
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        private void confpass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            confp = confpass.Password;
            if (confp != newp)
            {
                checkrr.Text = "Password doesn't match";
                checkrr.Visibility = Visibility.Visible;
            }
            else
            {
                checkrr.Visibility = Visibility.Collapsed;
            }
        }

        private void newpass_PasswordChanged(object sender, RoutedEventArgs e)
        {
            newp = newpass.Password;
        }

        private void original_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(original.Password != ori)
            {
                checkori.Text = "Password doesn't match with old one";
                checkori.Visibility = Visibility.Visible;
            }
            else
            {
                checkori.Visibility = Visibility.Collapsed;
            }
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                isoStore.DeleteFile("PCGuardian/admin/passwd.txt");
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(("PCGuardian/admin/passwd.txt"), FileMode.Create, isoStore))
                {
                    using (StreamWriter writer = new StreamWriter(isoStream))
                    {
                        if (newp == confp && ori==original.Password)
                        {
                            checkrr.Visibility = Visibility.Collapsed;
                            writer.WriteLine(newp);
                            writer.Close();
                        }
                        else
                        {
                            checkrr.Text = "Please enter the credentials correctly";
                            checkrr.Visibility = Visibility.Visible;
                            writer.Close();
                        }
                    }
                    isoStream.Close();
                }
                isoStore.Close();
                this.NavigationService.Navigate(new adminPortal());
            }
            catch (Exception exp)
            {
                MessageBox.Show("Internal Error!!");
            }
        }

        public adminPassChange()
        {
            InitializeComponent();
            try
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("PCGuardian/admin/passwd.txt", FileMode.Open, isoStore))
                {
                    using (StreamReader reader = new StreamReader(isoStream))
                    {
                        ori = reader.ReadLine();
                        reader.Close();
                    }
                    isoStream.Close();

                }
            }
            catch (Exception exp2)
            {
                MessageBox.Show("Error!!");
            }

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new adminPortal());
        }
    }
}
