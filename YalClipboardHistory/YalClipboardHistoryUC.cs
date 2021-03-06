﻿using System.Windows.Forms;

namespace YalClipboardHistory
{
    public partial class YalClipboardHistoryUC : UserControl
    {
        public YalClipboardHistoryUC()
        {
            InitializeComponent();

            cbStoreInDb.Checked = Properties.Settings.Default.StoreInDb;
            spinMaxHistorySize.Value = Properties.Settings.Default.MaxHistorySize;
        }

        internal void SaveSettings()
        {
            Properties.Settings.Default.StoreInDb = cbStoreInDb.Checked;
            Properties.Settings.Default.MaxHistorySize = (int)spinMaxHistorySize.Value;
            Properties.Settings.Default.Save();
        }
    }
}
