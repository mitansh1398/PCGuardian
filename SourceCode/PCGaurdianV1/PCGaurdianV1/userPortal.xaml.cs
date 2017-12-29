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
    /// Interaction logic for userPortal.xaml
    /// </summary>
    public partial class userPortal : Page
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);

        String user;

        public userPortal()
        {
            InitializeComponent();
            try
            {
                using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("PCGuardian/temp/loggedin.txt", FileMode.Open, isoStore))
                {
                    using (StreamReader reader = new StreamReader(isoStream))
                    {
                        user = reader.ReadLine().ToString();
                        info.Text = "You are logged in as " + user;
                        reader.Close();
                    }
                    isoStream.Close();
                }
            }
            catch (Exception popup)
            {
                MessageBox.Show(popup.ToString());
            }
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            isoStore.DeleteFile("PCGuardian/temp/loggedin.txt");
            MyFunctions.deleteExplorer();
            MyFunctions.blockfolder(isoStore, "PCGuardian/guest/blocked/1party");
            MyFunctions.blockfolder(isoStore, "PCGuardian/guest/blocked/2party");
            isoStore.Close();
            this.NavigationService.Navigate(new startup());
        }
    }
}
