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
    /// Interaction logic for createUser.xaml
    /// </summary>
    public partial class createUser : Page
    {
        public createUser()
        {
            InitializeComponent();
        }

        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        private void newUser_Click(object sender, RoutedEventArgs e)
        {
                if(unametxt.Text != "")
                {
                    blkuname.Visibility = Visibility.Visible;
                    if (!isoStore.DirectoryExists("PCGuardian/users/" + unametxt.Text))
                    {
                        if (passtxt.Password == cpasstxt.Password && passtxt.Password != "")
                        {
                            nomatch.Visibility = Visibility.Collapsed;
                            String location = "PCGuardian/users/" + unametxt.Text;
                            isoStore.CreateDirectory(location);
                            isoStore.CreateDirectory(location + "/blocked");
                            String passwd = location + "/passwd.txt";
                            using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(passwd, FileMode.CreateNew, isoStore))
                            {
                                using (StreamWriter writer = new StreamWriter(isoStream))
                                {
                                    writer.WriteLine(passtxt.Password);
                                    writer.Close();
                                }
                                isoStream.Close();
                            }
                            isoStore.Close();
                            Application app = Application.Current;
                            app.Properties["uname"] = unametxt.Text;
                            this.NavigationService.Navigate(new newUserBlock());
                        }
                        else if (passtxt.Password == "" || cpasstxt.Password == "")
                        {
                            blkpass.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            passtxt.Password = "";
                            cpasstxt.Password = "";
                            nomatch.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        exist.Visibility = Visibility.Visible;
                    }
                }
        }

        private void cpasstxt_PasswordChanged(object sender, RoutedEventArgs e)
        {
            string p1 = passtxt.Password;
            string p2 = cpasstxt.Password;
            if (p1 == p2)
            {
                nomatch.Visibility = Visibility.Collapsed;
            }
            else
            {
                nomatch.Visibility = Visibility.Visible;

            }
        }

        private void unametxt_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            if (isoStore.DirectoryExists("PCGuardian/users/" + unametxt.Text))
            {
                exist.Visibility = Visibility.Visible;
            }
            else
            {
                exist.Visibility = Visibility.Collapsed;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            isoStore.Close();
            this.NavigationService.Navigate(new adminPortal());
        } 
    }
}
