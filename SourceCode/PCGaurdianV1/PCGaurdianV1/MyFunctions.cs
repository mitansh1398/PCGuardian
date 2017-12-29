using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.IsolatedStorage;
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
using System.Text.RegularExpressions;

namespace PCGaurdianV1
{
    public static class MyFunctions
    {
        //deleting the directory
        public static void DeleteDirectoryRecursively(this IsolatedStorageFile storageFile, String dirName)
        {
            if(!storageFile.DirectoryExists(dirName))
            {
                return;
            }
            String pattern = dirName + "/*";
            String[] files = storageFile.GetFileNames(pattern);
            foreach (String fName in files)
            {
                String temp = dirName + "/" + fName + "\n";
                if(storageFile.FileExists(temp))
                {
                    storageFile.DeleteFile(temp);
                }
                
            }

            String[] dirs = storageFile.GetDirectoryNames(pattern);
            foreach (String dName in dirs)
            {
                String temp = dirName + "/" + dName;
                if (storageFile.DirectoryExists(temp))
                {
                    DeleteDirectoryRecursively(storageFile, temp);
                }
            }
            storageFile.DeleteDirectory(dirName);
        }


        //getting the list of installed software with path
        public static void Getinstalledsoftware(ListBox _1stparty, ListBox _2ndparty)
        {
            //The registry key:
            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                String SoftwareKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
                using (var rk = hklm.OpenSubKey(SoftwareKey, true))
                {
                    //Let's go through the registry keys and get the info we need:
                    String[] SubKeys = rk.GetSubKeyNames();
                            foreach (String skName in SubKeys)
                            {
                                //var sk = rk.OpenSubKey(skName);
                                using (var sk = rk.OpenSubKey(skName))
                                {
                                    try
                                    {
                                        
                                        if (!(sk.GetValue("DisplayName").ToString().Trim() == ""))
                                        {
                                            //Is the install location known?
                                            
                                            if (sk.GetValue("InstallLocation").ToString().Trim() != "")
                                            {

                                                String name = sk.GetValue("DisplayName").ToString().Trim();
                                                String publisher = sk.GetValue("Publisher").ToString().Trim();
                                                if (publisher == "WildTangent" || publisher == "WildGames")
                                                {
                                                    //ignore them
                                                }
                                                else if (publisher == "Microsoft Corporation")
                                                {
                                                    
                                                    if (!_1stparty.Items.Contains(name))
                                                    {
                                                        _1stparty.Items.Add(name);
                                                    }
                                                }
                                                else
                                                {
                                                    
                                                    if (!_2ndparty.Items.Contains(name))
                                                    {
                                                        _2ndparty.Items.Add(name);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception exp)
                                    {
                                        //No, that exception is not getting away... :P
                                        continue;
                                    }
                                }
                            }
                }
            }

            String SoftwareKeytyp2 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            //using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(SoftwareKeytyp2))
            {
                //Let's go through the registry keys and get the info we need:
                String[] SubKeys = rk.GetSubKeyNames();

                        foreach (String skName in SubKeys)
                        {
                           using (RegistryKey sk = rk.OpenSubKey(skName))
                            {
                                try
                                {
                                    if (!(sk.GetValue("DisplayName").ToString().Trim() == ""))
                                    {
                                        //Is the install location known?
                                        if (sk.GetValue("InstallLocation").ToString().Trim() != "")
                                        {

                                            String name = sk.GetValue("DisplayName").ToString().Trim();
                                            String publisher = sk.GetValue("Publisher").ToString().Trim();
                                            if(publisher == "WildTangent" || publisher == "WildGames")
                                            {
                                                //ignore them
                                            }
                                            else if (publisher == "Microsoft Corporation")
                                            {
                                                if (!_1stparty.Items.Contains(name))
                                                {
                                                    _1stparty.Items.Add(name);
                                                }
                                            }
                                            else
                                            {
                                                if (!_2ndparty.Items.Contains(name))
                                                {
                                                    _2ndparty.Items.Add(name);
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception exp)
                                {
                                    //No, that exception is not getting away... :P
                                    continue;
                                }
                            }
                        }
            }


            using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                String SoftwareKey = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
                using (var rk = hklm.OpenSubKey(SoftwareKey, true))
                {
                    //Let's go through the registry keys and get the info we need:
                    String[] SubKeys = rk.GetSubKeyNames();

                            foreach (String skName in SubKeys)
                            {
                                //var sk = rk.OpenSubKey(skName);
                                using (var sk = rk.OpenSubKey(skName))
                                {
                                    try
                                    { 
                                        if (!(sk.GetValue("DisplayName").ToString().Trim() == ""))
                                        {
                                            //Is the install location known?
                                            if (sk.GetValue("InstallLocation").ToString().Trim() != "")
                                            {

                                                String name = sk.GetValue("DisplayName").ToString().Trim();
                                                String publisher = sk.GetValue("Publisher").ToString().Trim();
                                                if (publisher == "WildTangent" || publisher == "WildGames")
                                                {
                                                    //ignore them
                                                }
                                                else if (publisher == "Microsoft Corporation")
                                                {
                                                    if (!_1stparty.Items.Contains(name))
                                                    {
                                                        _1stparty.Items.Add(name);
                                                    }
                                                }
                                                else
                                                {
                                                    if (!_2ndparty.Items.Contains(name))
                                                    {
                                                        _2ndparty.Items.Add(name);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception exp)
                                    {
                                        //No, that exception is not getting away... :P
                                        continue;
                                    }
                                }
                            }
                }
            }

            SoftwareKeytyp2 = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            //using (var hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(SoftwareKeytyp2))
            {
                //Let's go through the registry keys and get the info we need:
                String[] SubKeys = rk.GetSubKeyNames();

                        foreach (String skName in SubKeys)
                        {
                            using (RegistryKey sk = rk.OpenSubKey(skName))
                            {
                                try
                                {
                                    if (!(sk.GetValue("DisplayName").ToString().Trim() == ""))
                                    {
                                        //Is the install location known?
                                        if (sk.GetValue("InstallLocation").ToString().Trim() != "")
                                        {

                                            String name = sk.GetValue("DisplayName").ToString().Trim();
                                            String publisher = sk.GetValue("Publisher").ToString().Trim();
                                            if (publisher == "WildTangent" || publisher == "WildGames")
                                            {
                                                //ignore them
                                            }
                                            else if (publisher == "Microsoft Corporation")
                                            {
                                                if (!_1stparty.Items.Contains(name))
                                                {
                                                    _1stparty.Items.Add(name);
                                                }
                                            }
                                            else
                                            {
                                                if (!_2ndparty.Items.Contains(name))
                                                {
                                                    _2ndparty.Items.Add(name);
                                                }
                                            }
                                        }
                                    }
                                }
                                catch (Exception exp)
                                {
                                    //No, that exception is not getting away... :P
                                    continue;
                                }
                            }
                        }
            }
        }


        //getting the application path
        public static String GetApplictionInstallPath(String nameOfAppToFind)
        {
            String installedPath;
            String keyName;

            // search in: CurrentUser
            keyName = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";
            installedPath = ExistsInSubKey(Registry.CurrentUser, keyName, "DisplayName", nameOfAppToFind);
            if (!String.IsNullOrEmpty(installedPath))
            {
                return installedPath;
            }
            using (var instance = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
            {
                installedPath = ExistsInSubKey(instance, keyName, "DisplayName", nameOfAppToFind);
                if (!String.IsNullOrEmpty(installedPath))
                {
                    return installedPath;
                }
            }

            // search in: LocalMachine_32
            keyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            installedPath = ExistsInSubKey(Registry.LocalMachine, keyName, "DisplayName", nameOfAppToFind);
            if (!String.IsNullOrEmpty(installedPath))
            {
                return installedPath;
            }
            using (var instance = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                installedPath = ExistsInSubKey(instance, keyName, "DisplayName", nameOfAppToFind);
                if (!String.IsNullOrEmpty(installedPath))
                {
                    return installedPath;
                }
            }


            // search in: LocalMachine_64
            keyName = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            installedPath = ExistsInSubKey(Registry.LocalMachine, keyName, "DisplayName", nameOfAppToFind);
            if (!String.IsNullOrEmpty(installedPath))
            {
                return installedPath;
            }
            using (var instance = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                installedPath = ExistsInSubKey(instance, keyName, "DisplayName", nameOfAppToFind);
                if (!String.IsNullOrEmpty(installedPath))
                {
                    return installedPath;
                }
            }
            return String.Empty;
        }

        //checking existance in subkey
        public static String ExistsInSubKey(RegistryKey root, String subKeyName, String attributeName, String nameOfAppToFind)
        {
            RegistryKey subkey;
            String displayName;

            using (RegistryKey key = root.OpenSubKey(subKeyName))
            {
                if (key != null)
                {
                    foreach (String kn in key.GetSubKeyNames())
                    {
                        using (subkey = key.OpenSubKey(kn))
                        {
                            displayName = subkey.GetValue(attributeName) as String;
                            if (nameOfAppToFind.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
                            {
                                return subkey.GetValue("InstallLocation") as String;
                            }
                        }
                    }
                }
            }
            return String.Empty;
        }

        //getting exe files
        public static void GetFileExeNameByFileDescription(String installPath,ref List<String> ls,int level)
        {
            
            foreach (String filePath in Directory.GetFiles(installPath, "*.exe"))
            {
                int len = filePath.Length;
                int start = len - 1;
                String exeName = String.Empty;
                while (start >= 0 && filePath[start] != '\\')
                {
                    exeName = filePath[start] + exeName;
                    start--;
                }
                ls.Add(exeName);
            }
            if(level > 4)
            {
                return;
            }
            foreach( String newfile in Directory.GetDirectories(installPath))
            {
                GetFileExeNameByFileDescription(newfile, ref ls, level+1);
            }
        }

        //registry setup
        public static RegistryKey getit()
        {
            RegistryKey baseaddress = Registry.CurrentUser;
            try
            {
                RegistryKey rk = baseaddress.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                if (rk == null)
                {
                    rk = baseaddress.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                }
                if(rk.GetValue("DisallowRun")==null)
                {
                    rk.SetValue("DisallowRun", (int)1, RegistryValueKind.DWord);
                }
                if(rk.OpenSubKey("DisallowRun")==null)
                {
                    rk.CreateSubKey("DisallowRun", true);
                }
                rk = rk.OpenSubKey("DisallowRun", true);
                
                return rk;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in creating");
                return null;
            }
        }



        //blocking app
        public static void BlockApp(String exename,String exefile)
        {
            try
            {
                RegistryKey blockapps = getit(); 
                exefile = exefile.Trim();
                blockapps.SetValue(exename, exefile, RegistryValueKind.String);

            }
            catch(Exception es)
            {
                MessageBox.Show("Error");
            }
        }

        //deleting the explorer
        public static void deleteExplorer()
        {
            RegistryKey rk = Registry.CurrentUser;
            RegistryKey prev = rk.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies", true);
            RegistryKey destination = rk.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
            if(destination != null)
            {
                prev.DeleteSubKeyTree("Explorer");
            }
        }

        public static void checkGuest()
        {
            RegistryKey baseaddress = Registry.CurrentUser;
            try
            {
                RegistryKey rk = baseaddress.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                if (rk != null)
                {
                    deleteExplorer();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in processing");
            }
        }

        //for logout
        public static void logout()
        {
            RegistryKey baseaddress = Registry.CurrentUser;
            try
            {
                RegistryKey rk = baseaddress.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                if (rk == null)
                {
                    rk = baseaddress.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                }
                else
                {
                    deleteExplorer();
                    rk = baseaddress.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error in creating");
            }
        }

        public static void blockfolder(IsolatedStorageFile isoStore, String path)
        {
            if(!isoStore.DirectoryExists(path))
            {
                return;
            }
            String pattern = path + "/*";
            String[] blockedFiles = isoStore.GetFileNames((pattern));
            foreach (String file in blockedFiles)
            {
                using (IsolatedStorageFileStream isoStream2 = new IsolatedStorageFileStream((path + "/" + file), FileMode.Open, isoStore))
                {
                    using (StreamReader reader2 = new StreamReader(isoStream2))
                    {
                        String exefile;
                        try
                        {
                            exefile = reader2.ReadLine().ToString();
                            int cnt = 1;
                            while (exefile != "")
                            {
                                if(cnt%100 == 0)
                                {
                                    MessageBox.Show("If this message has appered more than 100 times the number of app you have blocked please kill it from task manager.");
                                }
                                String keyname = file + cnt.ToString();
                                try
                                {
                                    BlockApp(keyname, exefile);
                                    exefile = reader2.ReadLine().ToString();
                                }
                                catch (Exception popup)
                                {
                                    break;
                                }

                                cnt++;
                            }
                        }
                        catch
                        {

                        }
                       
                        reader2.Close();
                    }
                    isoStream2.Close();
                }
            }
        }
    }
}
