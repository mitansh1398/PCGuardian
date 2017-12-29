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
    /// Interaction logic for otherUserLogin.xaml
    /// </summary>
    public partial class otherUserLogin : Page
    {
        public otherUserLogin()
        {
            InitializeComponent();
        }
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        private void login_Click(object sender, RoutedEventArgs e)
        {

            String location = "PCGuardian/users/" + unametxt.Text;
            try
            {
                if (isoStore.DirectoryExists(location))
                {
                    location += "/passwd.txt";
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream(location, FileMode.Open, isoStore))
                    {
                        using (StreamReader reader = new StreamReader(isoStream))
                        {
                            String savedPasswd = reader.ReadLine();
                            if (savedPasswd == passtxt.Password)
                            {
                                // block and unblock apps
                                isoStream.Close();
                                reader.Close();
                                using (IsolatedStorageFileStream isoStream3 = new IsolatedStorageFileStream("PCGuardian/temp/loggedin.txt", FileMode.Create, isoStore))
                                {
                                    using (StreamWriter writer3 = new StreamWriter(isoStream3))
                                    {
                                        writer3.WriteLine(unametxt.Text);
                                        writer3.Close();
                                    }
                                    isoStream3.Close();
                                }
                                MyFunctions.deleteExplorer();
                                MyFunctions.blockfolder(isoStore, ("PCGuardian/users/" + unametxt.Text + "/blocked/1party"));
                                MyFunctions.blockfolder(isoStore, ("PCGuardian/users/" + unametxt.Text + "/blocked/2party"));
                                isoStore.Close();
                                this.NavigationService.Navigate(new userPortal());
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
            catch
            {
                MessageBox.Show("problem");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            isoStore.Close();
            this.NavigationService.Navigate(new startup());
        }
    }
}
