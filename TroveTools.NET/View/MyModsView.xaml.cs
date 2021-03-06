﻿using log4net;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using TroveTools.NET.Properties;
using TroveTools.NET.ViewModel;

namespace TroveTools.NET.View
{
    /// <summary>
    /// Interaction logic for MyModsView.xaml
    /// </summary>
    public partial class MyModsView : UserControl
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MyModsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handle Add Mods Click event here in the view code behind so that we can display an Open
        /// File Dialog box and pass the filenames as strings to the view model's AddModCommand
        /// </summary>
        private void AddModsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vm = DataContext as MyModsViewModel;
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Title = Strings.MyMods_OpenFileDialog_Title;
                dialog.Filter = string.Format("{0}|*.zip;*.tmod", Strings.MyMods_OpenFileDialog_Filter);
                dialog.CheckFileExists = true;
                dialog.Multiselect = true;

                string lastLocation = vm.LastAddModLocation;
                if (!string.IsNullOrEmpty(lastLocation)) dialog.InitialDirectory = lastLocation;

                if (dialog.ShowDialog() == true)
                {
                    vm.LastAddModLocation = Path.GetDirectoryName(dialog.FileName);
                    foreach (string file in dialog.FileNames)
                    {
                        vm.AddModCommand.Execute(file);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error adding mods", ex);
            }
        }

        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindowView.HideToolbarOverflow(sender as ToolBar);
        }
    }
}
