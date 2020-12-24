namespace NitroStudio2 {
    partial class WaveMapper {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveMapper));
            this.mapGrid = new System.Windows.Forms.DataGridView();
            this.finishedButton = new System.Windows.Forms.Button();
            this.playColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.waveId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.waveArchive = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mapGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mapGrid
            // 
            this.mapGrid.AllowUserToAddRows = false;
            this.mapGrid.AllowUserToDeleteRows = false;
            this.mapGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mapGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mapGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playColumn,
            this.waveId,
            this.waveArchive});
            this.mapGrid.Location = new System.Drawing.Point(12, 12);
            this.mapGrid.Name = "mapGrid";
            this.mapGrid.Size = new System.Drawing.Size(541, 268);
            this.mapGrid.TabIndex = 0;
            // 
            // finishedButton
            // 
            this.finishedButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finishedButton.Location = new System.Drawing.Point(12, 287);
            this.finishedButton.Name = "finishedButton";
            this.finishedButton.Size = new System.Drawing.Size(541, 23);
            this.finishedButton.TabIndex = 1;
            this.finishedButton.Text = "Finished";
            this.finishedButton.UseVisualStyleBackColor = true;
            this.finishedButton.Click += new System.EventHandler(this.finishedButton_Click);
            // 
            // playColumn
            // 
            this.playColumn.FillWeight = 57.19987F;
            this.playColumn.HeaderText = "Play Sample";
            this.playColumn.Name = "playColumn";
            this.playColumn.Text = "Play";
            this.playColumn.UseColumnTextForButtonValue = true;
            // 
            // waveId
            // 
            this.waveId.FillWeight = 53.29949F;
            this.waveId.HeaderText = "Wave Id";
            this.waveId.Name = "waveId";
            this.waveId.ReadOnly = true;
            this.waveId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // waveArchive
            // 
            this.waveArchive.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.waveArchive.FillWeight = 189.5006F;
            this.waveArchive.HeaderText = "Wave Archive";
            this.waveArchive.Name = "waveArchive";
            // 
            // WaveMapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 322);
            this.Controls.Add(this.finishedButton);
            this.Controls.Add(this.mapGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "WaveMapper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wave Mapper";
            ((System.ComponentModel.ISupportInitialize)(this.mapGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView mapGrid;
        private System.Windows.Forms.Button finishedButton;
        private System.Windows.Forms.DataGridViewButtonColumn playColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn waveId;
        private System.Windows.Forms.DataGridViewComboBoxColumn waveArchive;
    }
}