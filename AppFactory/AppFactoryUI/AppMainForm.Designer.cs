namespace AppFactory.UI
{
    partial class AppMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AppMainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnConnectD365 = new System.Windows.Forms.Button();
            this.cmbSolutions = new System.Windows.Forms.ComboBox();
            this.chkCreateApp = new System.Windows.Forms.CheckBox();
            this.lblD365Connection = new System.Windows.Forms.Label();
            this.chkCreateDataMigrationPackage = new System.Windows.Forms.CheckBox();
            this.btnUndoMigration = new System.Windows.Forms.Button();
            this.btnMigrate = new System.Windows.Forms.Button();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpFields = new System.Windows.Forms.GroupBox();
            this.tabDetails = new System.Windows.Forms.TabControl();
            this.Felder = new System.Windows.Forms.TabPage();
            this.grdTableColumns = new System.Windows.Forms.DataGridView();
            this.colMigrate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colFieldname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colForeignKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSchemaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrimAttribute = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Relations = new System.Windows.Forms.TabPage();
            this.grdRelations = new System.Windows.Forms.DataGridView();
            this.colFkName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFkPrimaryTable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFkAttributePrimary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFkAttributeForeign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnExamineDB = new System.Windows.Forms.Button();
            this.grpTables = new System.Windows.Forms.GroupBox();
            this.grdTables = new System.Windows.Forms.DataGridView();
            this.colTblMigrate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTblTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTblActivities = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTblNotes = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colTblSchemaname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTblSingular = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTblPlural = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLookupDB = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblSolutionPrefix = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNewSolution = new System.Windows.Forms.Button();
            this.lblCustomizationPrefix = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkImportData = new System.Windows.Forms.CheckBox();
            this.chkCreateSecurityRole = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtAppSchemaname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAppName = new System.Windows.Forms.TextBox();
            this.chkPublishApp = new System.Windows.Forms.CheckBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.grpZiel = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSource = new System.Windows.Forms.TabPage();
            this.tabInfoLog = new System.Windows.Forms.TabPage();
            this.logText = new System.Windows.Forms.RichTextBox();
            this.grpFields.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.Felder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTableColumns)).BeginInit();
            this.Relations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRelations)).BeginInit();
            this.grpTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTables)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grpZiel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabSource.SuspendLayout();
            this.tabInfoLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            this.openFileDialog1.InitialDirectory = "c:\\Users";
            // 
            // btnConnectD365
            // 
            resources.ApplyResources(this.btnConnectD365, "btnConnectD365");
            this.btnConnectD365.Name = "btnConnectD365";
            this.btnConnectD365.UseVisualStyleBackColor = true;
            this.btnConnectD365.Click += new System.EventHandler(this.BtnConnectD365_Click);
            // 
            // cmbSolutions
            // 
            this.cmbSolutions.FormattingEnabled = true;
            resources.ApplyResources(this.cmbSolutions, "cmbSolutions");
            this.cmbSolutions.Name = "cmbSolutions";
            this.cmbSolutions.SelectedIndexChanged += new System.EventHandler(this.cmbSolutions_SelectedIndexChanged);
            this.cmbSolutions.SelectionChangeCommitted += new System.EventHandler(this.cmbSolutions_SelectionChangeCommitted);
            // 
            // chkCreateApp
            // 
            resources.ApplyResources(this.chkCreateApp, "chkCreateApp");
            this.chkCreateApp.Name = "chkCreateApp";
            this.chkCreateApp.UseVisualStyleBackColor = true;
            // 
            // lblD365Connection
            // 
            resources.ApplyResources(this.lblD365Connection, "lblD365Connection");
            this.lblD365Connection.Name = "lblD365Connection";
            // 
            // chkCreateDataMigrationPackage
            // 
            resources.ApplyResources(this.chkCreateDataMigrationPackage, "chkCreateDataMigrationPackage");
            this.chkCreateDataMigrationPackage.Name = "chkCreateDataMigrationPackage";
            this.chkCreateDataMigrationPackage.UseVisualStyleBackColor = true;
            // 
            // btnUndoMigration
            // 
            resources.ApplyResources(this.btnUndoMigration, "btnUndoMigration");
            this.btnUndoMigration.Name = "btnUndoMigration";
            this.btnUndoMigration.UseVisualStyleBackColor = true;
            this.btnUndoMigration.Click += new System.EventHandler(this.btnUndoMigration_Click);
            // 
            // btnMigrate
            // 
            resources.ApplyResources(this.btnMigrate, "btnMigrate");
            this.btnMigrate.Name = "btnMigrate";
            this.btnMigrate.UseVisualStyleBackColor = true;
            this.btnMigrate.Click += new System.EventHandler(this.BtnMigrate_Click);
            // 
            // txtDatabase
            // 
            resources.ApplyResources(this.txtDatabase, "txtDatabase");
            this.txtDatabase.Name = "txtDatabase";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // grpFields
            // 
            resources.ApplyResources(this.grpFields, "grpFields");
            this.grpFields.Controls.Add(this.tabDetails);
            this.grpFields.Name = "grpFields";
            this.grpFields.TabStop = false;
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.Felder);
            this.tabDetails.Controls.Add(this.Relations);
            resources.ApplyResources(this.tabDetails, "tabDetails");
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.SelectedIndex = 0;
            // 
            // Felder
            // 
            this.Felder.Controls.Add(this.grdTableColumns);
            resources.ApplyResources(this.Felder, "Felder");
            this.Felder.Name = "Felder";
            this.Felder.UseVisualStyleBackColor = true;
            // 
            // grdTableColumns
            // 
            this.grdTableColumns.AllowUserToAddRows = false;
            this.grdTableColumns.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTableColumns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdTableColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTableColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMigrate,
            this.colFieldname,
            this.colDataType,
            this.colPrimaryKey,
            this.colForeignKey,
            this.colSchemaName,
            this.colPrimAttribute});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTableColumns.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.grdTableColumns, "grdTableColumns");
            this.grdTableColumns.Name = "grdTableColumns";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTableColumns.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdTableColumns.RowTemplate.Height = 24;
            this.grdTableColumns.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTableColumns_CellClick);
            this.grdTableColumns.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTableColumns_CellContentClick);
            // 
            // colMigrate
            // 
            resources.ApplyResources(this.colMigrate, "colMigrate");
            this.colMigrate.Name = "colMigrate";
            // 
            // colFieldname
            // 
            resources.ApplyResources(this.colFieldname, "colFieldname");
            this.colFieldname.Name = "colFieldname";
            // 
            // colDataType
            // 
            resources.ApplyResources(this.colDataType, "colDataType");
            this.colDataType.Name = "colDataType";
            // 
            // colPrimaryKey
            // 
            resources.ApplyResources(this.colPrimaryKey, "colPrimaryKey");
            this.colPrimaryKey.Name = "colPrimaryKey";
            // 
            // colForeignKey
            // 
            resources.ApplyResources(this.colForeignKey, "colForeignKey");
            this.colForeignKey.Name = "colForeignKey";
            // 
            // colSchemaName
            // 
            resources.ApplyResources(this.colSchemaName, "colSchemaName");
            this.colSchemaName.Name = "colSchemaName";
            // 
            // colPrimAttribute
            // 
            resources.ApplyResources(this.colPrimAttribute, "colPrimAttribute");
            this.colPrimAttribute.Name = "colPrimAttribute";
            // 
            // Relations
            // 
            this.Relations.Controls.Add(this.grdRelations);
            resources.ApplyResources(this.Relations, "Relations");
            this.Relations.Name = "Relations";
            this.Relations.UseVisualStyleBackColor = true;
            // 
            // grdRelations
            // 
            this.grdRelations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRelations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFkName,
            this.colFkPrimaryTable,
            this.colFkAttributePrimary,
            this.colFkAttributeForeign});
            resources.ApplyResources(this.grdRelations, "grdRelations");
            this.grdRelations.Name = "grdRelations";
            // 
            // colFkName
            // 
            resources.ApplyResources(this.colFkName, "colFkName");
            this.colFkName.Name = "colFkName";
            // 
            // colFkPrimaryTable
            // 
            resources.ApplyResources(this.colFkPrimaryTable, "colFkPrimaryTable");
            this.colFkPrimaryTable.Name = "colFkPrimaryTable";
            // 
            // colFkAttributePrimary
            // 
            resources.ApplyResources(this.colFkAttributePrimary, "colFkAttributePrimary");
            this.colFkAttributePrimary.Name = "colFkAttributePrimary";
            // 
            // colFkAttributeForeign
            // 
            resources.ApplyResources(this.colFkAttributeForeign, "colFkAttributeForeign");
            this.colFkAttributeForeign.Name = "colFkAttributeForeign";
            // 
            // btnExamineDB
            // 
            resources.ApplyResources(this.btnExamineDB, "btnExamineDB");
            this.btnExamineDB.Name = "btnExamineDB";
            this.btnExamineDB.UseVisualStyleBackColor = true;
            this.btnExamineDB.Click += new System.EventHandler(this.btnExamine_Click);
            // 
            // grpTables
            // 
            resources.ApplyResources(this.grpTables, "grpTables");
            this.grpTables.Controls.Add(this.grdTables);
            this.grpTables.Name = "grpTables";
            this.grpTables.TabStop = false;
            // 
            // grdTables
            // 
            this.grdTables.AllowUserToAddRows = false;
            this.grdTables.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTables.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grdTables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdTables.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTblMigrate,
            this.colTblTableName,
            this.colTblActivities,
            this.colTblNotes,
            this.colTblSchemaname,
            this.colTblSingular,
            this.colTblPlural});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdTables.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.grdTables, "grdTables");
            this.grdTables.MultiSelect = false;
            this.grdTables.Name = "grdTables";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdTables.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.grdTables.RowTemplate.Height = 24;
            this.grdTables.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdTables.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTables_CellClick);
            this.grdTables.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdTables_CellContentClick);
            this.grdTables.SelectionChanged += new System.EventHandler(this.GrdTables_SelectionChanged);
            // 
            // colTblMigrate
            // 
            resources.ApplyResources(this.colTblMigrate, "colTblMigrate");
            this.colTblMigrate.Name = "colTblMigrate";
            // 
            // colTblTableName
            // 
            resources.ApplyResources(this.colTblTableName, "colTblTableName");
            this.colTblTableName.Name = "colTblTableName";
            // 
            // colTblActivities
            // 
            resources.ApplyResources(this.colTblActivities, "colTblActivities");
            this.colTblActivities.Name = "colTblActivities";
            // 
            // colTblNotes
            // 
            resources.ApplyResources(this.colTblNotes, "colTblNotes");
            this.colTblNotes.Name = "colTblNotes";
            // 
            // colTblSchemaname
            // 
            resources.ApplyResources(this.colTblSchemaname, "colTblSchemaname");
            this.colTblSchemaname.Name = "colTblSchemaname";
            // 
            // colTblSingular
            // 
            resources.ApplyResources(this.colTblSingular, "colTblSingular");
            this.colTblSingular.Name = "colTblSingular";
            // 
            // colTblPlural
            // 
            resources.ApplyResources(this.colTblPlural, "colTblPlural");
            this.colTblPlural.Name = "colTblPlural";
            // 
            // btnLookupDB
            // 
            resources.ApplyResources(this.btnLookupDB, "btnLookupDB");
            this.btnLookupDB.Name = "btnLookupDB";
            this.btnLookupDB.UseVisualStyleBackColor = true;
            this.btnLookupDB.Click += new System.EventHandler(this.btnLookupDB_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblSolutionPrefix);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.btnNewSolution);
            this.groupBox6.Controls.Add(this.lblCustomizationPrefix);
            this.groupBox6.Controls.Add(this.label22);
            this.groupBox6.Controls.Add(this.cmbSolutions);
            resources.ApplyResources(this.groupBox6, "groupBox6");
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.TabStop = false;
            // 
            // lblSolutionPrefix
            // 
            resources.ApplyResources(this.lblSolutionPrefix, "lblSolutionPrefix");
            this.lblSolutionPrefix.Name = "lblSolutionPrefix";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnNewSolution
            // 
            resources.ApplyResources(this.btnNewSolution, "btnNewSolution");
            this.btnNewSolution.Name = "btnNewSolution";
            this.btnNewSolution.UseVisualStyleBackColor = true;
            this.btnNewSolution.Click += new System.EventHandler(this.btnNewSolution_Click_1);
            // 
            // lblCustomizationPrefix
            // 
            resources.ApplyResources(this.lblCustomizationPrefix, "lblCustomizationPrefix");
            this.lblCustomizationPrefix.Name = "lblCustomizationPrefix";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkImportData);
            this.groupBox5.Controls.Add(this.chkCreateSecurityRole);
            this.groupBox5.Controls.Add(this.chkCreateDataMigrationPackage);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // chkImportData
            // 
            resources.ApplyResources(this.chkImportData, "chkImportData");
            this.chkImportData.Name = "chkImportData";
            this.chkImportData.UseVisualStyleBackColor = true;
            // 
            // chkCreateSecurityRole
            // 
            resources.ApplyResources(this.chkCreateSecurityRole, "chkCreateSecurityRole");
            this.chkCreateSecurityRole.Name = "chkCreateSecurityRole";
            this.chkCreateSecurityRole.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.checkBox3);
            this.groupBox4.Controls.Add(this.checkBox4);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            resources.ApplyResources(this.checkBox4, "checkBox4");
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtAppSchemaname);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtAppName);
            this.groupBox3.Controls.Add(this.chkPublishApp);
            this.groupBox3.Controls.Add(this.chkCreateApp);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // txtAppSchemaname
            // 
            resources.ApplyResources(this.txtAppSchemaname, "txtAppSchemaname");
            this.txtAppSchemaname.Name = "txtAppSchemaname";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtAppName
            // 
            resources.ApplyResources(this.txtAppName, "txtAppName");
            this.txtAppName.Name = "txtAppName";
            this.txtAppName.Leave += new System.EventHandler(this.txtAppName_Leave);
            // 
            // chkPublishApp
            // 
            resources.ApplyResources(this.chkPublishApp, "chkPublishApp");
            this.chkPublishApp.Name = "chkPublishApp";
            this.chkPublishApp.UseVisualStyleBackColor = true;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoMigration);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            resources.ApplyResources(this.toolStripStatusLabel, "toolStripStatusLabel");
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // grpZiel
            // 
            this.grpZiel.Controls.Add(this.btnConnectD365);
            this.grpZiel.Controls.Add(this.groupBox6);
            this.grpZiel.Controls.Add(this.groupBox5);
            this.grpZiel.Controls.Add(this.groupBox4);
            this.grpZiel.Controls.Add(this.groupBox3);
            this.grpZiel.Controls.Add(this.lblD365Connection);
            resources.ApplyResources(this.grpZiel, "grpZiel");
            this.grpZiel.Name = "grpZiel";
            this.grpZiel.TabStop = false;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSource);
            this.tabControl1.Controls.Add(this.tabInfoLog);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabSource
            // 
            this.tabSource.BackColor = System.Drawing.SystemColors.Control;
            this.tabSource.Controls.Add(this.grpFields);
            this.tabSource.Controls.Add(this.label1);
            this.tabSource.Controls.Add(this.grpTables);
            this.tabSource.Controls.Add(this.btnExamineDB);
            this.tabSource.Controls.Add(this.btnLookupDB);
            this.tabSource.Controls.Add(this.txtDatabase);
            resources.ApplyResources(this.tabSource, "tabSource");
            this.tabSource.Name = "tabSource";
            // 
            // tabInfoLog
            // 
            this.tabInfoLog.Controls.Add(this.logText);
            resources.ApplyResources(this.tabInfoLog, "tabInfoLog");
            this.tabInfoLog.Name = "tabInfoLog";
            this.tabInfoLog.UseVisualStyleBackColor = true;
            // 
            // logText
            // 
            resources.ApplyResources(this.logText, "logText");
            this.logText.Name = "logText";
            // 
            // AppMainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpZiel);
            this.Controls.Add(this.btnUndoMigration);
            this.Controls.Add(this.btnMigrate);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.Name = "AppMainForm";
            this.Load += new System.EventHandler(this.AppMainForm_Load);
            this.grpFields.ResumeLayout(false);
            this.tabDetails.ResumeLayout(false);
            this.Felder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTableColumns)).EndInit();
            this.Relations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRelations)).EndInit();
            this.grpTables.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTables)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grpZiel.ResumeLayout(false);
            this.grpZiel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabSource.ResumeLayout(false);
            this.tabSource.PerformLayout();
            this.tabInfoLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnConnectD365;
        private System.Windows.Forms.ComboBox cmbSolutions;
        private System.Windows.Forms.CheckBox chkCreateApp;
        private System.Windows.Forms.Label lblD365Connection;
        private System.Windows.Forms.CheckBox chkCreateDataMigrationPackage;
        private System.Windows.Forms.CheckBox chkCreateSecurityRole;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox chkPublishApp;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAppSchemaname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAppName;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label lblCustomizationPrefix;
        private System.Windows.Forms.CheckBox chkImportData;
        private System.Windows.Forms.Button btnNewSolution;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSolutionPrefix;
        private System.Windows.Forms.Button btnUndoMigration;
        private System.Windows.Forms.Button btnMigrate;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpFields;
        private System.Windows.Forms.DataGridView grdTableColumns;
        private System.Windows.Forms.Button btnExamineDB;
        private System.Windows.Forms.GroupBox grpTables;
        private System.Windows.Forms.DataGridView grdTables;
        private System.Windows.Forms.Button btnLookupDB;
        private System.Windows.Forms.GroupBox grpZiel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabDetails;
        private System.Windows.Forms.TabPage Felder;
        private System.Windows.Forms.TabPage Relations;
        private System.Windows.Forms.DataGridView grdRelations;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colTblMigrate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTblTableName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colTblActivities;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colTblNotes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTblSchemaname;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTblSingular;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTblPlural;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFkName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFkPrimaryTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFkAttributePrimary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFkAttributeForeign;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSource;
        private System.Windows.Forms.TabPage tabInfoLog;
        private System.Windows.Forms.RichTextBox logText;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colMigrate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldname;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colPrimaryKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colForeignKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSchemaName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colPrimAttribute;
    }
}

