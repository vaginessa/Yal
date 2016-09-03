﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace Launcher
{
    public partial class Options : Form
    {
        // this type of list signals it's modification which causes our listbox to reread it's
        // contents
        internal BindingList<string> FoldersToIndex { get; set; }

        Launcher MainWindow { get; }

        public Options(Launcher mainWindow)
        {
            InitializeComponent();

            MainWindow = mainWindow;

            UpdateUIVariables();
        }

        private void UpdateUIVariables()
        {
            StringCollection locations = Properties.Settings.Default.FoldersToIndex;
            FoldersToIndex = new BindingList<string>(locations.Cast<string>().ToList<string>());
            listBoxLocations.DataSource = FoldersToIndex;

            UpdateIndexingStatus();

            trackBarOpacity.Value = Properties.Settings.Default.Opacity;

            cbVAlign.Checked = Properties.Settings.Default.VAlignment;

            cbHAlign.Checked = Properties.Settings.Default.HAlignment;

            txtExtensions.Text = Properties.Settings.Default.Extensions;

            cbSubdirs.Checked = Properties.Settings.Default.Subdirectories;

            cbCtrlMove.Checked = Properties.Settings.Default.MoveWithCtrl;

            cbTopMost.Checked = Properties.Settings.Default.TopMost;

            comboBoxHKMod.DataSource = Enum.GetValues(typeof(FsModifier));
            comboBoxHKMod.SelectedItem = Properties.Settings.Default.FocusModifier;

            var items = (Keys[])Enum.GetValues(typeof(Keys));
            comboBoxHKKey.DataSource = items.Where(x => Keys.A <= x && x <= Keys.Z).ToArray();
            comboBoxHKKey.SelectedItem = Properties.Settings.Default.FocusKey;

            cbAutoIndexing.Checked = Properties.Settings.Default.AutoIndexing;

            checkBoxAutostart.Checked = Properties.Settings.Default.Autostart;

            spinMaxFetch.Value = Properties.Settings.Default.MaxFetched;

            spinMaxVisible.Value = Properties.Settings.Default.MaxVisible;

            spinSearchDelay.Value = Properties.Settings.Default.SearchDelay;

            cbShowExt.Checked = Properties.Settings.Default.ExtensionInFileName;

            spinAutoIndexInterval.Value = Properties.Settings.Default.AutoIndexingInterval;

            colorDialog1.Color = Properties.Settings.Default.InterfaceColor;

            cbMatchAnywhere.Checked = Properties.Settings.Default.MatchAnywhere;

            spinMaxHistorySize.Value = Properties.Settings.Default.MaxHistorySize;

            spinMaxHistoryVisible.Value = Properties.Settings.Default.MaxHistoryVisible;
        }

        internal void UpdateIndexingStatus()
        {
            lblIndexStatus.Text = $"{FileManager.DbRowCount()} items indexed at {Properties.Settings.Default.DateLastIndexed}";
        }

        private void Options_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainWindow.optionsWindow = null;
        }

        private void btnAddLocation_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (!FoldersToIndex.Contains(folderBrowserDialog1.SelectedPath))
                {
                    FoldersToIndex.Add(folderBrowserDialog1.SelectedPath);
                }
            }
        }

        private void btnRemoveLocation_Click(object sender, EventArgs e)
        {
            FoldersToIndex.Remove((string)listBoxLocations.SelectedItem);
        }

        private void btnRebuild_Click(object sender, EventArgs e)
        {
            FileManager.RebuildIndex();
            UpdateIndexingStatus();
        }

        private void ManageAppAutoStart()
        {
            string appPath = Application.ExecutablePath;
            using (RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", writable: true))
            {
                if (Properties.Settings.Default.Autostart)
                {
                    object currentValue = rk.GetValue(MainWindow.Text);
                    if (currentValue == null || (string)currentValue != appPath)
                    {
                        rk.SetValue(MainWindow.Text, appPath);

                        using (RegistryKey rk2 = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
                        {
                            object value = rk2.GetValue("ProductName");
                            if (value != null && Regex.IsMatch(value.ToString(), @"^Windows 10[\w\s]+$"))
                            {
                                MessageBox.Show("On Windows 10 you need to enable 'Launcher' from within Task Manager/Startup",
                                                "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    rk.DeleteValue(MainWindow.Text, throwOnMissingValue: false);
                }
            }
        }

        private void btnPickColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }

        private void btnCancelOpt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApplyOptions_Click(object sender, EventArgs e)
        {
            if (ValidFileExtensions())
            {
                Properties.Settings.Default.Extensions = txtExtensions.Text;
            }
            else
            {
                txtExtensions.Text = Properties.Settings.Default.Extensions;
                MessageBox.Show("Invalid file extension(s). Correct format: ext or ext1,ext2,extN",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var hotkeyMod = (FsModifier)comboBoxHKMod.SelectedItem;
            if (hotkeyMod != Properties.Settings.Default.FocusModifier)
            {
                Properties.Settings.Default.FocusModifier = hotkeyMod;
                MainWindow.UpdateHotkey();
            }

            var hotkeyKey = (Keys)comboBoxHKKey.SelectedItem;
            if (hotkeyKey != Properties.Settings.Default.FocusKey)
            {
                Properties.Settings.Default.FocusKey = hotkeyKey;
                MainWindow.UpdateHotkey();
            }

            if (colorDialog1.Color != Properties.Settings.Default.InterfaceColor)
            {
                Properties.Settings.Default.InterfaceColor = colorDialog1.Color;
                MainWindow.UpdateWindowColor();
            }

            if (trackBarOpacity.Value != Properties.Settings.Default.Opacity)
            {
                Properties.Settings.Default.Opacity = trackBarOpacity.Value;
                MainWindow.UpdateWindowOpacity();
            }
            
            Properties.Settings.Default.Subdirectories = cbSubdirs.Checked;
            
            if (cbVAlign.Checked != Properties.Settings.Default.VAlignment)
            {
                Properties.Settings.Default.VAlignment = cbVAlign.Checked;
                MainWindow.UpdateVertAlignment();
            }

            if (cbHAlign.Checked != Properties.Settings.Default.HAlignment)
            {
                Properties.Settings.Default.HAlignment = cbHAlign.Checked;
                MainWindow.UpdateHorizAlignment();
            }
            
            if (cbTopMost.Checked != Properties.Settings.Default.TopMost)
            {
                Properties.Settings.Default.TopMost = cbTopMost.Checked;
                MainWindow.UpdateWindowTopMost();
            }

            Properties.Settings.Default.MoveWithCtrl = cbCtrlMove.Checked;

            if (checkBoxAutostart.Checked != Properties.Settings.Default.Autostart)
            {
                Properties.Settings.Default.Autostart = checkBoxAutostart.Checked;
                ManageAppAutoStart();
            }

            Properties.Settings.Default.MaxFetched = (int)spinMaxFetch.Value;
            Properties.Settings.Default.MaxVisible = (int)spinMaxVisible.Value;
            Properties.Settings.Default.SearchDelay = (int)spinSearchDelay.Value;
            Properties.Settings.Default.ExtensionInFileName = cbShowExt.Checked;

            var autoIndexInterval = (int)spinAutoIndexInterval.Value;
            if (autoIndexInterval != Properties.Settings.Default.AutoIndexingInterval)
            {
                Properties.Settings.Default.AutoIndexingInterval = autoIndexInterval;
                MainWindow.ManageAutoIndexingTimer();
            }

            if (cbAutoIndexing.Checked != Properties.Settings.Default.AutoIndexing)
            {
                Properties.Settings.Default.AutoIndexing = cbAutoIndexing.Checked;
                MainWindow.ManageAutoIndexingTimer();
            }
            
            Properties.Settings.Default.MatchAnywhere = cbMatchAnywhere.Checked;

            Properties.Settings.Default.FoldersToIndex = FoldersToIndex.ToStringCollection();

            Properties.Settings.Default.MaxHistorySize = (int)spinMaxHistorySize.Value;

            Properties.Settings.Default.MaxHistoryVisible = (int)spinMaxHistoryVisible.Value;
        }

        private bool ValidFileExtensions()
        {
            var regex = new Regex(@"^\w+$");
            foreach (string ext in txtExtensions.Text.Split(','))
            {
                if (!regex.IsMatch(ext))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
