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
    /// Interaction logic for newUserBlockSetup.xaml
    /// </summary>
    public partial class newUserBlockSetup : Page
    {
        String uname;
        public newUserBlockSetup()
        {
            InitializeComponent();
            
            _1stparty.SelectionMode = SelectionMode.Multiple;
            _2ndparty.SelectionMode = SelectionMode.Multiple;
            MyFunctions.Getinstalledsoftware(_1stparty, _2ndparty);
            Application app = Application.Current;
            if (app.Properties["uname"] != null)
            {
                instruct.Text = app.Properties["uname"].ToString();
                uname = app.Properties["uname"].ToString();
            }
            else
            {
                MessageBox.Show("problem1");
            }
        }

        IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null);
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            isoStore.CreateDirectory("PCGuardian/users/" + uname + "/blocked/1party");
            isoStore.CreateDirectory("PCGuardian/users/" + uname + "/blocked/2party");
            foreach (String apps in _1stparty.SelectedItems)
            {
                String appPath = String.Empty;
                try
                {
                    appPath = MyFunctions.GetApplictionInstallPath(apps);
                    //MessageBox.Show(appPath);
                    List<String> ls = new List<String>();
                    MyFunctions.GetFileExeNameByFileDescription(appPath, ref ls, 1);
                    String[] allexecutables = ls.ToArray();
                    String file = "PCGuardian/users/" + uname + "/blocked/1party/" + apps + ".txt";
                    using (IsolatedStorageFileStream isoStream1 = new IsolatedStorageFileStream(file, FileMode.CreateNew, isoStore))
                    {
                        using (StreamWriter writer = new StreamWriter(isoStream1))
                        {
                            foreach (String exef in allexecutables)
                            {
                                writer.WriteLine(exef);
                            }
                            writer.Close();
                        }
                        isoStream1.Close();
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
                try
                {
                    List<String> ls = new List<String>();
                    MyFunctions.GetFileExeNameByFileDescription(appPath, ref ls, 1);
                    String[] allexecutables = ls.ToArray();
                    String file2 = "PCGuardian/users/" + uname + "/blocked/2party/" + apps + ".txt";
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
                    MessageBox.Show(appPath);
                    MessageBox.Show(popup.ToString());
                }
            }
            this.NavigationService.Navigate(new askNewUserPage());
        }
    }
}
