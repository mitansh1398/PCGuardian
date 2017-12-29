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
    /// Interaction logic for adminLogin.xaml
    /// </summary>
    public partial class adminLogin : Page
    {
        public adminLogin()
        {
            InitializeComponent();
        }

        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        private void login_Click(object sender, RoutedEventArgs e)
        {
            String location = "PCGuardian/admin/uname.txt";
            using (IsolatedStorageFileStream isoStreamuname = new IsolatedStorageFileStream(location, FileMode.Open, isoStore))
            {
                using (StreamReader readeruname = new StreamReader(isoStreamuname))
                {
                    String saveduname = readeruname.ReadLine();
                    if (saveduname == unametxt.Text)
                    {
                        location = "PCGuardian/admin/passwd.txt";
                        using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(location, FileMode.Open, isoStore))
                        {
                            using (StreamReader reader = new StreamReader(isoStream))
                            {
                                String savedPasswd = reader.ReadLine();
                                if (savedPasswd == passtxt.Password)
                                {
                                    reader.Close();
                                    isoStream.Close();
                                    readeruname.Close();
                                    isoStreamuname.Close();
                                    using (IsolatedStorageFileStream isoStream3 = new IsolatedStorageFileStream("PCGuardian/temp/loggedin.txt", FileMode.Create, isoStore))
                                    {
                                        using (StreamWriter writer3 = new StreamWriter(isoStream3))
                                        {
                                            writer3.WriteLine("admin");
                                            writer3.Close();
                                        }
                                        isoStream3.Close();
                                    }
                                    isoStore.Close();
                                    MyFunctions.deleteExplorer();
                                    this.NavigationService.Navigate(new adminPortal());
                                }
                                else
                                {
                                    nomatch.Visibility = Visibility.Visible;
                                }
                            }
                        }
                    }
                    else
                    {
                        nomatch.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            isoStore.Close();
            this.NavigationService.Navigate(new startup());
        }
    }
}
