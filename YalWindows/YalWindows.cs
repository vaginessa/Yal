﻿using System;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using Utilities;
using PluginInterfaces;

namespace YalWindows
{
    public class YalWindows : IPlugin
    {
        public string Name { get; } = "YalWindows";
        public string Version { get; } = "1.0";
        public string Description { get; } = "Quickly switch between windows using Yal";
        public string Activator { get; } = "$";
        public PluginItemSortingOption SortingOption { get; } = PluginItemSortingOption.ByNameLength;

        public Icon PluginIcon { get; }
        public string HelpText { get; }

        public UserControl PluginUserControl
        {
            get
            {
                if (pluginUserControl == null || pluginUserControl.IsDisposed)
                {
                    pluginUserControl = new YalWindowsUC();
                }
                return pluginUserControl;
            }
        }

        private YalWindowsUC pluginUserControl;

        public YalWindows()
        {
            PluginIcon = Utils.GetPluginIcon(Name);

             HelpText = $@"The plugin's lets you switch between open application
windows. Type '{Activator}', and you will get a list of detected
windows and their titles. Hitting Enter or double clicking on an
entry switches to the underlying window";
        }

        public void SaveSettings()
        {
            pluginUserControl.SaveSettings();
        }

        public List<PluginItem> GetItems(string userInput)
        {
            return Process.GetProcesses().Where(process => process.MainWindowHandle != IntPtr.Zero).Select(process => new PluginItem()
            {
                Name = string.Join(" ", Activator, process.MainWindowTitle), IconLocation = GetProcessFileLocation(process)
            }).ToList();
        }

        public void HandleExecution(string input)
        {
            var windowName = input.Substring(Activator.Length + 1);
            var matchingProcesses = Process.GetProcesses().Where(p => p.MainWindowTitle == windowName).ToArray();

            if (matchingProcesses.Length == 0)
            {
                MessageBox.Show($"Could not find a window named '{windowName}'", Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Utils.ActivateWindowByHandle(matchingProcesses[0].MainWindowHandle);
        }

        private string GetProcessFileLocation(Process process)
        {
            try
            {
                return process.MainModule.FileName;
            }
            // when trying to access a 64 bit module from a 32 bit process or when trying to access
            // system owned processes
            catch (Exception ex) when (ex is Win32Exception)
            {
                return null;
            }
        }
    }
}
