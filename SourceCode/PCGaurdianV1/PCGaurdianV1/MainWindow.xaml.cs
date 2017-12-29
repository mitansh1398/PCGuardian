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
using Microsoft.Win32;
using System.Collections;
using PCGaurdianV1;

namespace PCGaurdianV1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        public MainWindow()
        {
            InitializeComponent();
            //MyFunctions.DeleteDirectoryRecursively(isoStore, "PCGuardian");
            if (isoStore.DirectoryExists("PCGuardian"))
            {
                if(isoStore.FileExists("PCGuardian/temp/loggedin.txt"))
                {
                    String user;
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("PCGuardian/temp/loggedin.txt", FileMode.Open, isoStore))
                    {
                        using (StreamReader reader = new StreamReader(isoStream))
                        {
                            user = reader.ReadLine();
                            reader.Close();
                        }
                        isoStream.Close();
                    }
                    isoStore.Close();
                    if(user == "admin")
                    {
                        frame1.NavigationService.Navigate(new adminPortal());
                    }
                    else
                    {
                        Application app = Application.Current;
                        app.Properties["loggeduser"] = user;
                        frame1.NavigationService.Navigate(new userPortal());
                    }
                }
                else
                {
                    isoStore.Close();
                    frame1.NavigationService.Navigate(new startup());
                } 
            }
            else
            {
                frame1.NavigationService.Navigate(new setup());
            }
        }
    }
}
