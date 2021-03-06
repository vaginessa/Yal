﻿namespace Yal
{
    partial class OutputWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputWindow));
            this.listViewOutput = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // listViewOutput
            // 
            this.listViewOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewOutput.FullRowSelect = true;
            this.listViewOutput.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewOutput.HideSelection = false;
            this.listViewOutput.Location = new System.Drawing.Point(0, 0);
            this.listViewOutput.MultiSelect = false;
            this.listViewOutput.Name = "listViewOutput";
            this.listViewOutput.ShowGroups = false;
            this.listViewOutput.Size = new System.Drawing.Size(265, 300);
            this.listViewOutput.TabIndex = 0;
            this.listViewOutput.TileSize = new System.Drawing.Size(248, 30);
            this.listViewOutput.UseCompatibleStateImageBehavior = false;
            this.listViewOutput.View = System.Windows.Forms.View.Tile;
            this.listViewOutput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewOutput_KeyDown);
            this.listViewOutput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewOutput_KeyPress);
            this.listViewOutput.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listViewOutput_MouseClick);
            this.listViewOutput.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewOutput_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Item";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Details";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // OutputWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 300);
            this.Controls.Add(this.listViewOutput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OutputWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Yal output window";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ListView listViewOutput;
        internal System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}