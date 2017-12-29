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
using Microsoft.Win32;
using System.IO.IsolatedStorage;
using System.IO;

namespace PCGaurdianV1
{
    /// <summary>
    /// Interaction logic for guestSetup.xaml
    /// </summary>
    public partial class guestSetup : Page
    {
        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        public guestSetup()
        {
            InitializeComponent();
            _1stparty.SelectionMode = SelectionMode.Multiple;
            _2ndparty.SelectionMode = SelectionMode.Multiple;
            MyFunctions.Getinstalledsoftware(_1stparty, _2ndparty);
            //_1stparty.Sort();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            isoStore.CreateDirectory("PCGuardian/guest/blocked");
            isoStore.CreateDirectory("PCGuardian/guest/blocked/1party");
            isoStore.CreateDirectory("PCGuardian/guest/blocked/2party");
            foreach (String apps in _1stparty.SelectedItems)
            {
                String appPath = String.Empty;
                try
                {
                    appPath = MyFunctions.GetApplictionInstallPath(apps);
                    //MessageBox.Show(appPath);
                    List<String> ls = new List<String>();
                    MyFunctions.GetFileExeNameByFileDescription(appPath,ref ls,1);
                    String[] allexecutables = ls.ToArray();
                    String file = "PCGuardian/guest/blocked/1party/" + apps + ".txt";
                    using (IsolatedStorageFileStream isoStream1 = new IsolatedStorageFileStream(file, FileMode.CreateNew, isoStore))
                    {
                        using (StreamWriter writer = new StreamWriter(isoStream1))
                        {
                            foreach (String exef in allexecutables)
                            {
                                writer.WriteLine(exef);
                            }
                        }
                    }
                }
                catch (Exception popup)
                {
                    
                    //MessageBox.Show(appPath);
                    
                }
            }

            foreach (String apps in _2ndparty.SelectedItems)
            {
                String appPath = MyFunctions.GetApplictionInstallPath(apps);
                //MessageBox.Show(appPath);
                try
                {
                    List<String> ls = new List<String>();
                    MyFunctions.GetFileExeNameByFileDescription(appPath, ref ls, 1);
                    String[] allexecutables = ls.ToArray();
                    String file2 = "PCGuardian/guest/blocked/2party/" + apps + ".txt";
                    using (IsolatedStorageFileStream isoStream2 = new IsolatedStorageFileStream(file2, FileMode.CreateNew, isoStore))
                    {
                        using (StreamWriter writer2 = new StreamWriter(isoStream2))
                        {
                            foreach (String exef2 in allexecutables)
                            {
                                writer2.WriteLine(exef2);
                            }
                            writer2.Close();
                        }
                        isoStream2.Close();
                    }
                }
                catch(Exception popup)
                {
                    //MessageBox.Show(popup.ToString());
                    //MessageBox.Show(appPath);
                }
            }

            this.NavigationService.Navigate(new createUserSetup());
        }
    }
}
