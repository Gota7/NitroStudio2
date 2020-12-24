namespace NitroStudio2 {
    partial class InstrumentSelector {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstrumentSelector));
            this.instGrid = new System.Windows.Forms.DataGridView();
            this.finishedButton = new System.Windows.Forms.Button();
            this.playColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.instrumentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.instrumentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.useInstrument = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.checkMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uncheckAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.instGrid)).BeginInit();
            this.checkMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // instGrid
            // 
            this.instGrid.AllowUserToAddRows = false;
            this.instGrid.AllowUserToDeleteRows = false;
            this.instGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.instGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.instGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playColumn,
            this.instrumentId,
            this.instrumentName,
            this.useInstrument});
            this.instGrid.ContextMenuStrip = this.checkMenu;
            this.instGrid.Location = new System.Drawing.Point(12, 12);
            this.instGrid.Name = "instGrid";
            this.instGrid.Size = new System.Drawing.Size(518, 292);
            this.instGrid.TabIndex = 0;
            // 
            // finishedButton
            // 
            this.finishedButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finishedButton.Location = new System.Drawing.Point(12, 311);
            this.finishedButton.Name = "finishedButton";
            this.finishedButton.Size = new System.Drawing.Size(518, 23);
            this.finishedButton.TabIndex = 1;
            this.finishedButton.Text = "Finished";
            this.finishedButton.UseVisualStyleBackColor = true;
            this.finishedButton.Click += new System.EventHandler(this.finishedButton_Click);
            // 
            // playColumn
            // 
            this.playColumn.HeaderText = "Play Sample";
            this.playColumn.Name = "playColumn";
            this.playColumn.Text = "Play";
            this.playColumn.UseColumnTextForButtonValue = true;
            // 
            // instrumentId
            // 
            this.instrumentId.HeaderText = "Instrument Id";
            this.instrumentId.Name = "instrumentId";
            this.instrumentId.ReadOnly = true;
            this.instrumentId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // instrumentName
            // 
            this.instrumentName.HeaderText = "Instrument Name";
            this.instrumentName.Name = "instrumentName";
            this.instrumentName.ReadOnly = true;
            this.instrumentName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.instrumentName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // useInstrument
            // 
            this.useInstrument.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.useInstrument.HeaderText = "Use Instrument";
            this.useInstrument.Name = "useInstrument";
            // 
            // checkMenu
            // 
            this.checkMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllToolStripMenuItem,
            this.uncheckAllToolStripMenuItem});
            this.checkMenu.Name = "checkMenu";
            this.checkMenu.Size = new System.Drawing.Size(181, 70);
            // 
            // checkAllToolStripMenuItem
            // 
            this.checkAllToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Save;
            this.checkAllToolStripMenuItem.Name = "checkAllToolStripMenuItem";
            this.checkAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.checkAllToolStripMenuItem.Text = "Check All";
            this.checkAllToolStripMenuItem.Click += new System.EventHandler(this.checkAllToolStripMenuItem_Click);
            // 
            // uncheckAllToolStripMenuItem
            // 
            this.uncheckAllToolStripMenuItem.Image = global::NitroStudio2.Properties.Resources.Save_As;
            this.uncheckAllToolStripMenuItem.Name = "uncheckAllToolStripMenuItem";
            this.uncheckAllToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uncheckAllToolStripMenuItem.Text = "Uncheck All";
            this.uncheckAllToolStripMenuItem.Click += new System.EventHandler(this.uncheckAllToolStripMenuItem_Click);
            // 
            // InstrumentSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 346);
            this.Controls.Add(this.finishedButton);
            this.Controls.Add(this.instGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "InstrumentSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Instrument Selector";
            ((System.ComponentModel.ISupportInitialize)(this.instGrid)).EndInit();
            this.checkMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView instGrid;
        private System.Windows.Forms.Button finishedButton;
        private System.Windows.Forms.DataGridViewButtonColumn playColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn instrumentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn instrumentName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn useInstrument;
        private System.Windows.Forms.ContextMenuStrip checkMenu;
        private System.Windows.Forms.ToolStripMenuItem checkAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uncheckAllToolStripMenuItem;
    }
}