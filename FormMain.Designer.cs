namespace ISO.PDFSearchApp
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelD = new System.Windows.Forms.Panel();
            this.buttonStartCopy = new System.Windows.Forms.Button();
            this.buttonBrowstarget = new System.Windows.Forms.Button();
            this.textBoxTargetFolder = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.buttonRefreshFolder = new System.Windows.Forms.Button();
            this.groupBoxNotInclude = new System.Windows.Forms.GroupBox();
            this.textBoxNotInlude6 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxNotInlude5 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxNotInlude4 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxNotInlude3 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxNotInlude2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxNotInlude1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.groupBoxInclude = new System.Windows.Forms.GroupBox();
            this.textBoxInlude6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxInlude5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxInlude4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxInlude3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxInlude2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxInlude1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBrowser = new System.Windows.Forms.Button();
            this.textBoxSourceFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewSearchResult = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMenuStripGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemOpenPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonOpenMustFile = new System.Windows.Forms.Button();
            this.buttonOpenNotMustFile = new System.Windows.Forms.Button();
            this.labelOpenMustFileCount = new System.Windows.Forms.Label();
            this.labelOpenNotMustFileCount = new System.Windows.Forms.Label();
            this.buttonOpenNotMustFileClear = new System.Windows.Forms.Button();
            this.buttonOpenMustFileClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelD.SuspendLayout();
            this.groupBoxNotInclude.SuspendLayout();
            this.groupBoxInclude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearchResult)).BeginInit();
            this.cMenuStripGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelD);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewSearchResult);
            this.splitContainer1.Size = new System.Drawing.Size(1208, 704);
            this.splitContainer1.SplitterDistance = 491;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelD
            // 
            this.panelD.AutoScroll = true;
            this.panelD.Controls.Add(this.buttonStartCopy);
            this.panelD.Controls.Add(this.buttonBrowstarget);
            this.panelD.Controls.Add(this.textBoxTargetFolder);
            this.panelD.Controls.Add(this.label14);
            this.panelD.Controls.Add(this.buttonRefreshFolder);
            this.panelD.Controls.Add(this.groupBoxNotInclude);
            this.panelD.Controls.Add(this.buttonClear);
            this.panelD.Controls.Add(this.groupBoxInclude);
            this.panelD.Controls.Add(this.buttonSearch);
            this.panelD.Controls.Add(this.buttonBrowser);
            this.panelD.Controls.Add(this.textBoxSourceFolder);
            this.panelD.Controls.Add(this.label1);
            this.panelD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelD.Location = new System.Drawing.Point(0, 0);
            this.panelD.Name = "panelD";
            this.panelD.Size = new System.Drawing.Size(491, 704);
            this.panelD.TabIndex = 0;
            // 
            // buttonStartCopy
            // 
            this.buttonStartCopy.Image = ((System.Drawing.Image)(resources.GetObject("buttonStartCopy.Image")));
            this.buttonStartCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStartCopy.Location = new System.Drawing.Point(281, 113);
            this.buttonStartCopy.Name = "buttonStartCopy";
            this.buttonStartCopy.Size = new System.Drawing.Size(123, 37);
            this.buttonStartCopy.TabIndex = 11;
            this.buttonStartCopy.Text = "Kopyala";
            this.buttonStartCopy.UseVisualStyleBackColor = true;
            this.buttonStartCopy.Click += new System.EventHandler(this.buttonStartCopy_Click);
            // 
            // buttonBrowstarget
            // 
            this.buttonBrowstarget.Image = ((System.Drawing.Image)(resources.GetObject("buttonBrowstarget.Image")));
            this.buttonBrowstarget.Location = new System.Drawing.Point(322, 70);
            this.buttonBrowstarget.Name = "buttonBrowstarget";
            this.buttonBrowstarget.Size = new System.Drawing.Size(50, 24);
            this.buttonBrowstarget.TabIndex = 10;
            this.buttonBrowstarget.Text = "..";
            this.buttonBrowstarget.UseVisualStyleBackColor = true;
            this.buttonBrowstarget.Click += new System.EventHandler(this.buttonBrowstarget_Click);
            // 
            // textBoxTargetFolder
            // 
            this.textBoxTargetFolder.Location = new System.Drawing.Point(125, 67);
            this.textBoxTargetFolder.Name = "textBoxTargetFolder";
            this.textBoxTargetFolder.Size = new System.Drawing.Size(190, 27);
            this.textBoxTargetFolder.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 70);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(107, 20);
            this.label14.TabIndex = 8;
            this.label14.Text = "Hedef Klasör";
            // 
            // buttonRefreshFolder
            // 
            this.buttonRefreshFolder.Image = ((System.Drawing.Image)(resources.GetObject("buttonRefreshFolder.Image")));
            this.buttonRefreshFolder.Location = new System.Drawing.Point(378, 27);
            this.buttonRefreshFolder.Name = "buttonRefreshFolder";
            this.buttonRefreshFolder.Size = new System.Drawing.Size(43, 24);
            this.buttonRefreshFolder.TabIndex = 7;
            this.buttonRefreshFolder.Text = "..";
            this.buttonRefreshFolder.UseVisualStyleBackColor = true;
            this.buttonRefreshFolder.Click += new System.EventHandler(this.buttonRefreshFolder_Click);
            // 
            // groupBoxNotInclude
            // 
            this.groupBoxNotInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxNotInclude.Controls.Add(this.labelOpenNotMustFileCount);
            this.groupBoxNotInclude.Controls.Add(this.buttonOpenNotMustFileClear);
            this.groupBoxNotInclude.Controls.Add(this.buttonOpenNotMustFile);
            this.groupBoxNotInclude.Controls.Add(this.textBoxNotInlude6);
            this.groupBoxNotInclude.Controls.Add(this.label8);
            this.groupBoxNotInclude.Controls.Add(this.textBoxNotInlude5);
            this.groupBoxNotInclude.Controls.Add(this.label9);
            this.groupBoxNotInclude.Controls.Add(this.textBoxNotInlude4);
            this.groupBoxNotInclude.Controls.Add(this.label10);
            this.groupBoxNotInclude.Controls.Add(this.textBoxNotInlude3);
            this.groupBoxNotInclude.Controls.Add(this.label11);
            this.groupBoxNotInclude.Controls.Add(this.textBoxNotInlude2);
            this.groupBoxNotInclude.Controls.Add(this.label12);
            this.groupBoxNotInclude.Controls.Add(this.textBoxNotInlude1);
            this.groupBoxNotInclude.Controls.Add(this.label13);
            this.groupBoxNotInclude.Location = new System.Drawing.Point(7, 420);
            this.groupBoxNotInclude.Name = "groupBoxNotInclude";
            this.groupBoxNotInclude.Size = new System.Drawing.Size(464, 243);
            this.groupBoxNotInclude.TabIndex = 4;
            this.groupBoxNotInclude.TabStop = false;
            this.groupBoxNotInclude.Text = "Olmaması Gereken Kelimeler";
            // 
            // textBoxNotInlude6
            // 
            this.textBoxNotInlude6.Location = new System.Drawing.Point(97, 196);
            this.textBoxNotInlude6.Name = "textBoxNotInlude6";
            this.textBoxNotInlude6.Size = new System.Drawing.Size(227, 27);
            this.textBoxNotInlude6.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 199);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "Kelime 6";
            // 
            // textBoxNotInlude5
            // 
            this.textBoxNotInlude5.Location = new System.Drawing.Point(97, 163);
            this.textBoxNotInlude5.Name = "textBoxNotInlude5";
            this.textBoxNotInlude5.Size = new System.Drawing.Size(227, 27);
            this.textBoxNotInlude5.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 166);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "Kelime 5";
            // 
            // textBoxNotInlude4
            // 
            this.textBoxNotInlude4.Location = new System.Drawing.Point(97, 130);
            this.textBoxNotInlude4.Name = "textBoxNotInlude4";
            this.textBoxNotInlude4.Size = new System.Drawing.Size(227, 27);
            this.textBoxNotInlude4.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 133);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 20);
            this.label10.TabIndex = 6;
            this.label10.Text = "Kelime 4";
            // 
            // textBoxNotInlude3
            // 
            this.textBoxNotInlude3.Location = new System.Drawing.Point(97, 97);
            this.textBoxNotInlude3.Name = "textBoxNotInlude3";
            this.textBoxNotInlude3.Size = new System.Drawing.Size(227, 27);
            this.textBoxNotInlude3.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(17, 100);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 20);
            this.label11.TabIndex = 4;
            this.label11.Text = "Kelime 3";
            // 
            // textBoxNotInlude2
            // 
            this.textBoxNotInlude2.Location = new System.Drawing.Point(97, 64);
            this.textBoxNotInlude2.Name = "textBoxNotInlude2";
            this.textBoxNotInlude2.Size = new System.Drawing.Size(227, 27);
            this.textBoxNotInlude2.TabIndex = 3;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(17, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(74, 20);
            this.label12.TabIndex = 2;
            this.label12.Text = "Kelime 2";
            // 
            // textBoxNotInlude1
            // 
            this.textBoxNotInlude1.Location = new System.Drawing.Point(97, 31);
            this.textBoxNotInlude1.Name = "textBoxNotInlude1";
            this.textBoxNotInlude1.Size = new System.Drawing.Size(227, 27);
            this.textBoxNotInlude1.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(17, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "Kelime 1";
            // 
            // buttonClear
            // 
            this.buttonClear.Image = ((System.Drawing.Image)(resources.GetObject("buttonClear.Image")));
            this.buttonClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonClear.Location = new System.Drawing.Point(12, 113);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(123, 37);
            this.buttonClear.TabIndex = 6;
            this.buttonClear.Text = "Temizle";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // groupBoxInclude
            // 
            this.groupBoxInclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxInclude.Controls.Add(this.labelOpenMustFileCount);
            this.groupBoxInclude.Controls.Add(this.buttonOpenMustFileClear);
            this.groupBoxInclude.Controls.Add(this.buttonOpenMustFile);
            this.groupBoxInclude.Controls.Add(this.textBoxInlude6);
            this.groupBoxInclude.Controls.Add(this.label7);
            this.groupBoxInclude.Controls.Add(this.textBoxInlude5);
            this.groupBoxInclude.Controls.Add(this.label6);
            this.groupBoxInclude.Controls.Add(this.textBoxInlude4);
            this.groupBoxInclude.Controls.Add(this.label5);
            this.groupBoxInclude.Controls.Add(this.textBoxInlude3);
            this.groupBoxInclude.Controls.Add(this.label4);
            this.groupBoxInclude.Controls.Add(this.textBoxInlude2);
            this.groupBoxInclude.Controls.Add(this.label3);
            this.groupBoxInclude.Controls.Add(this.textBoxInlude1);
            this.groupBoxInclude.Controls.Add(this.label2);
            this.groupBoxInclude.Location = new System.Drawing.Point(7, 170);
            this.groupBoxInclude.Name = "groupBoxInclude";
            this.groupBoxInclude.Size = new System.Drawing.Size(464, 244);
            this.groupBoxInclude.TabIndex = 3;
            this.groupBoxInclude.TabStop = false;
            this.groupBoxInclude.Text = "Olması Gereken Kelimeler";
            // 
            // textBoxInlude6
            // 
            this.textBoxInlude6.Location = new System.Drawing.Point(97, 196);
            this.textBoxInlude6.Name = "textBoxInlude6";
            this.textBoxInlude6.Size = new System.Drawing.Size(227, 27);
            this.textBoxInlude6.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "Kelime 6";
            // 
            // textBoxInlude5
            // 
            this.textBoxInlude5.Location = new System.Drawing.Point(97, 163);
            this.textBoxInlude5.Name = "textBoxInlude5";
            this.textBoxInlude5.Size = new System.Drawing.Size(227, 27);
            this.textBoxInlude5.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Kelime 5";
            // 
            // textBoxInlude4
            // 
            this.textBoxInlude4.Location = new System.Drawing.Point(97, 130);
            this.textBoxInlude4.Name = "textBoxInlude4";
            this.textBoxInlude4.Size = new System.Drawing.Size(227, 27);
            this.textBoxInlude4.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Kelime 4";
            // 
            // textBoxInlude3
            // 
            this.textBoxInlude3.Location = new System.Drawing.Point(97, 97);
            this.textBoxInlude3.Name = "textBoxInlude3";
            this.textBoxInlude3.Size = new System.Drawing.Size(227, 27);
            this.textBoxInlude3.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Kelime 3";
            // 
            // textBoxInlude2
            // 
            this.textBoxInlude2.Location = new System.Drawing.Point(97, 64);
            this.textBoxInlude2.Name = "textBoxInlude2";
            this.textBoxInlude2.Size = new System.Drawing.Size(227, 27);
            this.textBoxInlude2.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Kelime 2";
            // 
            // textBoxInlude1
            // 
            this.textBoxInlude1.Location = new System.Drawing.Point(97, 31);
            this.textBoxInlude1.Name = "textBoxInlude1";
            this.textBoxInlude1.Size = new System.Drawing.Size(227, 27);
            this.textBoxInlude1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Kelime 1";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Image = ((System.Drawing.Image)(resources.GetObject("buttonSearch.Image")));
            this.buttonSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSearch.Location = new System.Drawing.Point(141, 113);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(123, 37);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Ara";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonBrowser
            // 
            this.buttonBrowser.Image = ((System.Drawing.Image)(resources.GetObject("buttonBrowser.Image")));
            this.buttonBrowser.Location = new System.Drawing.Point(322, 27);
            this.buttonBrowser.Name = "buttonBrowser";
            this.buttonBrowser.Size = new System.Drawing.Size(50, 24);
            this.buttonBrowser.TabIndex = 2;
            this.buttonBrowser.Text = "..";
            this.buttonBrowser.UseVisualStyleBackColor = true;
            this.buttonBrowser.Click += new System.EventHandler(this.buttonBrowser_Click);
            // 
            // textBoxSourceFolder
            // 
            this.textBoxSourceFolder.Location = new System.Drawing.Point(125, 24);
            this.textBoxSourceFolder.Name = "textBoxSourceFolder";
            this.textBoxSourceFolder.Size = new System.Drawing.Size(190, 27);
            this.textBoxSourceFolder.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kaynak Klasör";
            // 
            // dataGridViewSearchResult
            // 
            this.dataGridViewSearchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSearchResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.FileName,
            this.FilePath});
            this.dataGridViewSearchResult.ContextMenuStrip = this.cMenuStripGrid;
            this.dataGridViewSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSearchResult.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSearchResult.Name = "dataGridViewSearchResult";
            this.dataGridViewSearchResult.RowHeadersWidth = 51;
            this.dataGridViewSearchResult.RowTemplate.Height = 24;
            this.dataGridViewSearchResult.Size = new System.Drawing.Size(713, 704);
            this.dataGridViewSearchResult.TabIndex = 0;
            this.dataGridViewSearchResult.SelectionChanged += new System.EventHandler(this.dataGridViewSearchResult_SelectionChanged);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 20;
            this.Id.Name = "Id";
            this.Id.Width = 125;
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "Dosya Adı";
            this.FileName.MinimumWidth = 6;
            this.FileName.Name = "FileName";
            this.FileName.Width = 125;
            // 
            // FilePath
            // 
            this.FilePath.DataPropertyName = "FilePath";
            this.FilePath.HeaderText = "Dosya Yolu";
            this.FilePath.MinimumWidth = 6;
            this.FilePath.Name = "FilePath";
            this.FilePath.Width = 250;
            // 
            // cMenuStripGrid
            // 
            this.cMenuStripGrid.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cMenuStripGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemOpenPDF});
            this.cMenuStripGrid.Name = "cMenuStripGrid";
            this.cMenuStripGrid.Size = new System.Drawing.Size(130, 30);
            // 
            // MenuItemOpenPDF
            // 
            this.MenuItemOpenPDF.Image = ((System.Drawing.Image)(resources.GetObject("MenuItemOpenPDF.Image")));
            this.MenuItemOpenPDF.Name = "MenuItemOpenPDF";
            this.MenuItemOpenPDF.Size = new System.Drawing.Size(129, 26);
            this.MenuItemOpenPDF.Text = "PDF Aç";
            this.MenuItemOpenPDF.Click += new System.EventHandler(this.MenuItemOpenPDF_Click);
            // 
            // buttonOpenMustFile
            // 
            this.buttonOpenMustFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenMustFile.Image")));
            this.buttonOpenMustFile.Location = new System.Drawing.Point(254, 2);
            this.buttonOpenMustFile.Name = "buttonOpenMustFile";
            this.buttonOpenMustFile.Size = new System.Drawing.Size(54, 23);
            this.buttonOpenMustFile.TabIndex = 12;
            this.buttonOpenMustFile.UseVisualStyleBackColor = true;
            this.buttonOpenMustFile.Click += new System.EventHandler(this.buttonOpenMustFile_Click);
            // 
            // buttonOpenNotMustFile
            // 
            this.buttonOpenNotMustFile.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenNotMustFile.Image")));
            this.buttonOpenNotMustFile.Location = new System.Drawing.Point(254, 0);
            this.buttonOpenNotMustFile.Name = "buttonOpenNotMustFile";
            this.buttonOpenNotMustFile.Size = new System.Drawing.Size(54, 23);
            this.buttonOpenNotMustFile.TabIndex = 13;
            this.buttonOpenNotMustFile.Text = "...";
            this.buttonOpenNotMustFile.UseVisualStyleBackColor = true;
            this.buttonOpenNotMustFile.Click += new System.EventHandler(this.buttonOpenNotMustFile_Click);
            // 
            // labelOpenMustFileCount
            // 
            this.labelOpenMustFileCount.AutoSize = true;
            this.labelOpenMustFileCount.Location = new System.Drawing.Point(374, 5);
            this.labelOpenMustFileCount.Name = "labelOpenMustFileCount";
            this.labelOpenMustFileCount.Size = new System.Drawing.Size(29, 20);
            this.labelOpenMustFileCount.TabIndex = 14;
            this.labelOpenMustFileCount.Text = "    ";
            // 
            // labelOpenNotMustFileCount
            // 
            this.labelOpenNotMustFileCount.AutoSize = true;
            this.labelOpenNotMustFileCount.Location = new System.Drawing.Point(374, 3);
            this.labelOpenNotMustFileCount.Name = "labelOpenNotMustFileCount";
            this.labelOpenNotMustFileCount.Size = new System.Drawing.Size(29, 20);
            this.labelOpenNotMustFileCount.TabIndex = 15;
            this.labelOpenNotMustFileCount.Text = "    ";
            // 
            // buttonOpenNotMustFileClear
            // 
            this.buttonOpenNotMustFileClear.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenNotMustFileClear.Image")));
            this.buttonOpenNotMustFileClear.Location = new System.Drawing.Point(314, 0);
            this.buttonOpenNotMustFileClear.Name = "buttonOpenNotMustFileClear";
            this.buttonOpenNotMustFileClear.Size = new System.Drawing.Size(54, 23);
            this.buttonOpenNotMustFileClear.TabIndex = 14;
            this.buttonOpenNotMustFileClear.Text = "...";
            this.buttonOpenNotMustFileClear.UseVisualStyleBackColor = true;
            this.buttonOpenNotMustFileClear.Click += new System.EventHandler(this.buttonOpenNotMustFileClear_Click);
            // 
            // buttonOpenMustFileClear
            // 
            this.buttonOpenMustFileClear.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenMustFileClear.Image")));
            this.buttonOpenMustFileClear.Location = new System.Drawing.Point(314, 2);
            this.buttonOpenMustFileClear.Name = "buttonOpenMustFileClear";
            this.buttonOpenMustFileClear.Size = new System.Drawing.Size(54, 23);
            this.buttonOpenMustFileClear.TabIndex = 13;
            this.buttonOpenMustFileClear.UseVisualStyleBackColor = true;
            this.buttonOpenMustFileClear.Click += new System.EventHandler(this.buttonOpenMustFileClear_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1208, 704);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF Arama";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelD.ResumeLayout(false);
            this.panelD.PerformLayout();
            this.groupBoxNotInclude.ResumeLayout(false);
            this.groupBoxNotInclude.PerformLayout();
            this.groupBoxInclude.ResumeLayout(false);
            this.groupBoxInclude.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearchResult)).EndInit();
            this.cMenuStripGrid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonBrowser;
        private System.Windows.Forms.TextBox textBoxSourceFolder;
        private System.Windows.Forms.GroupBox groupBoxInclude;
        private System.Windows.Forms.GroupBox groupBoxNotInclude;
        private System.Windows.Forms.TextBox textBoxNotInlude6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxNotInlude5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxNotInlude4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxNotInlude3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxNotInlude2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxNotInlude1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxInlude6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxInlude5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxInlude4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxInlude3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxInlude2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxInlude1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonRefreshFolder;
        private System.Windows.Forms.DataGridView dataGridViewSearchResult;
        private System.Windows.Forms.Button buttonBrowstarget;
        private System.Windows.Forms.TextBox textBoxTargetFolder;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonStartCopy;
        private System.Windows.Forms.ContextMenuStrip cMenuStripGrid;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOpenPDF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.Button buttonOpenNotMustFile;
        private System.Windows.Forms.Button buttonOpenMustFile;
        private System.Windows.Forms.Label labelOpenMustFileCount;
        private System.Windows.Forms.Label labelOpenNotMustFileCount;
        private System.Windows.Forms.Button buttonOpenNotMustFileClear;
        private System.Windows.Forms.Button buttonOpenMustFileClear;
    }
}

