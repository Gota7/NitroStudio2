namespace NitroStudio2 {
    partial class CreateStreamTool {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateStreamTool));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.impFileBox = new System.Windows.Forms.TextBox();
            this.impFileButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.outFileBox = new System.Windows.Forms.TextBox();
            this.outFileButton = new System.Windows.Forms.Button();
            this.outputFormat = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.7971F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.2029F));
            this.tableLayoutPanel1.Controls.Add(this.impFileBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.impFileButton, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 29);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 29);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // impFileBox
            // 
            this.impFileBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.impFileBox.Location = new System.Drawing.Point(3, 3);
            this.impFileBox.Name = "impFileBox";
            this.impFileBox.ReadOnly = true;
            this.impFileBox.Size = new System.Drawing.Size(289, 20);
            this.impFileBox.TabIndex = 0;
            // 
            // impFileButton
            // 
            this.impFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.impFileButton.Location = new System.Drawing.Point(298, 3);
            this.impFileButton.Name = "impFileButton";
            this.impFileButton.Size = new System.Drawing.Size(44, 23);
            this.impFileButton.TabIndex = 1;
            this.impFileButton.Text = "...";
            this.impFileButton.UseVisualStyleBackColor = true;
            this.impFileButton.Click += new System.EventHandler(this.impFileButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(345, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input File:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(345, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Output File:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.7971F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.2029F));
            this.tableLayoutPanel2.Controls.Add(this.outFileBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.outFileButton, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 80);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(345, 29);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // outFileBox
            // 
            this.outFileBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outFileBox.Location = new System.Drawing.Point(3, 3);
            this.outFileBox.Name = "outFileBox";
            this.outFileBox.ReadOnly = true;
            this.outFileBox.Size = new System.Drawing.Size(289, 20);
            this.outFileBox.TabIndex = 0;
            // 
            // outFileButton
            // 
            this.outFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outFileButton.Location = new System.Drawing.Point(298, 3);
            this.outFileButton.Name = "outFileButton";
            this.outFileButton.Size = new System.Drawing.Size(44, 23);
            this.outFileButton.TabIndex = 1;
            this.outFileButton.Text = "...";
            this.outFileButton.UseVisualStyleBackColor = true;
            this.outFileButton.Click += new System.EventHandler(this.outFileButton_Click);
            // 
            // outputFormat
            // 
            this.outputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputFormat.FormattingEnabled = true;
            this.outputFormat.Items.AddRange(new object[] {
            "PCM8",
            "PCM16",
            "IMA-ADPCM"});
            this.outputFormat.Location = new System.Drawing.Point(12, 134);
            this.outputFormat.Name = "outputFormat";
            this.outputFormat.Size = new System.Drawing.Size(345, 21);
            this.outputFormat.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(12, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(345, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Format:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(12, 161);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(345, 23);
            this.exportButton.TabIndex = 6;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // CreateStreamTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 204);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputFormat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CreateStreamTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Stream";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox impFileBox;
        private System.Windows.Forms.Button impFileButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TextBox outFileBox;
        private System.Windows.Forms.Button outFileButton;
        private System.Windows.Forms.ComboBox outputFormat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button exportButton;
    }
}