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
    /// Interaction logic for adminPortal.xaml
    /// </summary>
    public partial class adminPortal : Page
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        public adminPortal()
        {
            InitializeComponent();
            hiname();
        }

        private void hiname()
        {
            using (IsolatedStorageFileStream isoStreaman = new IsolatedStorageFileStream("PCGuardian/admin/uname.txt", FileMode.Open, isoStore))
            {
                using (StreamReader readeran = new StreamReader(isoStreaman))
                {
                    String hitext = "Hi " + readeran.ReadLine() + "!";
                    welcomeText.Text = hitext;
                    readeran.Close();
                }
                isoStreaman.Close();
            }
            String pattern = "PCGuardian/users/*";
            String[] users = isoStore.GetDirectoryNames(pattern);
            userList.Items.Add("ADMIN");
            foreach(String user in users)
            {
                userList.Items.Add(user);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            isoStore.Close();
            Application.Current.Shutdown();
        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            isoStore.Close();
            this.NavigationService.Navigate(new reset());
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            // Block all guest user app
            MyFunctions.deleteExplorer();
            MyFunctions.blockfolder(isoStore, "PCGuardian/guest/blocked/1party");
            MyFunctions.blockfolder(isoStore, "PCGuardian/guest/blocked/2party");
            isoStore.DeleteFile("PCGuardian/temp/loggedin.txt");
            isoStore.Close();
            Application.Current.Shutdown();
        }

        private void newUser_Click(object sender, RoutedEventArgs e)
        {
            isoStore.Close();
            this.NavigationService.Navigate(new createUser());
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void editUser_Click(object sender, RoutedEventArgs e)
        {
            Application app = Application.Current;
            String selectedUser="";
            try
            {
                selectedUser = userList.SelectedItem.ToString();
            }
            catch
            {

            }
            if(selectedUser == "")
            {
                return;
            }
            else if (selectedUser == "ADMIN")
            {
                this.NavigationService.Navigate(new adminPassChange());
            }
            else
            {
                app.Properties["uname"] = selectedUser;
                this.NavigationService.Navigate(new editUser());
            }
        }
    }
}
