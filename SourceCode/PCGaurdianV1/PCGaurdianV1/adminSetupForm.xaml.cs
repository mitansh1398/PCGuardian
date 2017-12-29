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
    /// Interaction logic for adminSetupForm.xaml
    /// </summary>
    public partial class adminSetupForm : Page
    {
        public adminSetupForm()
        {
            InitializeComponent();
        }
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (adminNametxt.Text != "")
            {
                blkuname.Visibility = Visibility.Collapsed;
                if (passtxt.Password == cpasstxt.Password && passtxt.Password != "")
                {
                    isoStore.CreateDirectory("PCGuardian");
                    isoStore.CreateDirectory("PCGuardian/admin");
                    isoStore.CreateDirectory("PCGuardian/users");
                    isoStore.CreateDirectory("PCGuardian/guest");
                    isoStore.CreateDirectory("PCGuardian/temp");
                    using (IsolatedStorageFileStream logStream = new IsolatedStorageFileStream("PCGuardian/temp/loggedin.txt", FileMode.Create, isoStore))
                    {
                        using (StreamWriter logw = new StreamWriter(logStream))
                        {
                            logw.WriteLine("admin");
                            logw.Close();
                        }
                        logStream.Close();
                    }
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("PCGuardian/admin/uname.txt", FileMode.CreateNew, isoStore))
                    {
                        using (StreamWriter writer = new StreamWriter(isoStream))
                        {
                            writer.WriteLine(adminNametxt.Text);
                            writer.Close();
                        }
                        nomatch.Visibility = Visibility.Collapsed;
                        using (IsolatedStorageFileStream isoStream2 = new IsolatedStorageFileStream("PCGuardian/admin/passwd.txt", FileMode.CreateNew, isoStore))
                        {
                            using (StreamWriter writer = new StreamWriter(isoStream2))
                            {
                                writer.WriteLine(passtxt.Password);
                                writer.Close();
                            }

                            isoStream2.Close();
                            isoStream.Close();
                        }
                        isoStore.Close();
                        this.NavigationService.Navigate(new guestSetup());
                    }
                }
                else if(passtxt.Password == "" || cpasstxt.Password == "")
                {
                    blkpass.Visibility = Visibility.Visible;
                }
                else
                {
                    passtxt.Password = "";
                    cpasstxt.Password = "";
                    nomatch.Visibility = Visibility.Visible;
                    blkpass.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                blkuname.Visibility = Visibility.Visible;
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            isoStore.Close();
            this.NavigationService.Navigate(new setup());
        }
    }
}
