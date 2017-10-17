using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrossMachineManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenList_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (openFileDialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(openFileDialog.FileName);
                List<VM> vms = new List<VM>();
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (!string.IsNullOrEmpty(lines[i]))
                    {
                        vms.Add(new VM()
                        {
                            MachineName = lines[i].Trim(),
                            CommandResult = string.Empty,
                            IsChecked = true
                        });
                    }
                }
                VMList.ItemsSource = vms;
            }
            else
            {
                MessageBox.Show("Please select a txt file!");
                return;
            }
        }

        private void SaveResults_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                string content = string.Empty;
                foreach (var item in (List<VM>)VMList.ItemsSource)
                {
                    content += item.MachineName + ":" + item.CommandResult + Environment.NewLine;
                }
                File.WriteAllText(fileName, content);
                MessageBox.Show("Save results successfully!");
                Process.Start("notepad.exe", fileName);
            }
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            string user = UserTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            foreach (var item in (List<VM>)VMList.ItemsSource)
            {
                string clientname = item.MachineName;
                if (item.IsChecked)
                {
                    try
                    {
                        InvokeSSH ssh = new InvokeSSH(clientname, user, password);
                        item.CommandResult = ssh.Run(CommandTextBox.Text.Trim());
                        if (!string.IsNullOrEmpty(ssh.ErrorMessage))
                        {
                            item.CommandResult = ssh.ErrorMessage;
                        }
                    }
                    catch (Exception exca2)
                    {
                        item.CommandResult = "Error:" + exca2.ToString();
                    }
                }
                VMList.Items.Refresh();
            }

            VMList.Items.Refresh();
        }

        private void VMList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            CheckBox cbox = new CheckBox();
            cbox.IsChecked = true;
            cbox.Checked += myCheckBox_Checked;
            cbox.Unchecked += myCheckBox_Checked;
            if (e.Column.Header.ToString() == "IsChecked")
                e.Column.Header = cbox;
        }

        private void myCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in (List<VM>)VMList.ItemsSource)
            {
                item.IsChecked = item.IsChecked ? false : true;
            }

            VMList.Items.Refresh();
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                string user = UserTextBox.Text.Trim();
                string password = PasswordTextBox.Text.Trim();
                foreach (var item in (List<VM>)VMList.ItemsSource)
                {
                    string clientname = item.MachineName;
                    if (item.IsChecked)
                    {
                        try
                        {
                            InvokeSSH ssh = new InvokeSSH(clientname, user, password);
                            ssh.Run("mkdir /tmp");
                            ssh.Upload("/tmp/"+ System.IO.Path.GetFileName(fileName), fileName);
                            item.CommandResult = "Done uploading.";
                        }
                        catch (Exception exca2)
                        {
                            item.CommandResult = "Error:" + exca2.ToString();
                        }
                    }
                    VMList.Items.Refresh();
                }

                VMList.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Please select a file to upload!");
                return;
            }
        }
    }
}
