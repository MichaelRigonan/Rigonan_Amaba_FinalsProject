namespace ITAsset_LabTracker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            pictureBox1 = new PictureBox();
            groupBox2 = new GroupBox();
            txtPrice = new TextBox();
            label3 = new Label();
            label7 = new Label();
            txtAssetTag = new TextBox();
            cmbType = new ComboBox();
            label4 = new Label();
            label6 = new Label();
            cmbStatus = new ComboBox();
            txtName = new TextBox();
            label5 = new Label();
            groupBox3 = new GroupBox();
            cmbFilter = new ComboBox();
            btnAdd = new Button();
            btnDelete = new Button();
            label9 = new Label();
            btnUpdate = new Button();
            btnCheckOut = new Button();
            txtSearch = new TextBox();
            btnReturn = new Button();
            btnSaveJson = new Button();
            label8 = new Label();
            btnLoadJson = new Button();
            dgvAssets = new DataGridView();
            assetTag = new DataGridViewTextBoxColumn();
            nameCol = new DataGridViewTextBoxColumn();
            typeCol = new DataGridViewTextBoxColumn();
            statusCol = new DataGridViewTextBoxColumn();
            priceCol = new DataGridViewTextBoxColumn();
            maintenanceStatusCol = new DataGridViewTextBoxColumn();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAssets).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Highlight;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(468, 40);
            label1.TabIndex = 0;
            label1.Text = "Campus IT Asset and Lab Tracker";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.ForeColor = SystemColors.AppWorkspace;
            label2.Location = new Point(0, 40);
            label2.Name = "label2";
            label2.Size = new Size(431, 15);
            label2.TabIndex = 1;
            label2.Text = "A simple solution for campus equipment tracking, borrowing, and maintenance.";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.ControlLightLight;
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 55);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1413, 100);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(3, 19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1407, 78);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = SystemColors.ControlLightLight;
            groupBox2.Controls.Add(txtPrice);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(txtAssetTag);
            groupBox2.Controls.Add(cmbType);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(cmbStatus);
            groupBox2.Controls.Add(txtName);
            groupBox2.Controls.Add(label5);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            groupBox2.ForeColor = SystemColors.ControlText;
            groupBox2.Location = new Point(0, 155);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1413, 150);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            groupBox2.Text = "Asset Details";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(1095, 48);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(149, 29);
            txtPrice.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F);
            label3.Location = new Point(76, 54);
            label3.Name = "label3";
            label3.Size = new Size(71, 20);
            label3.TabIndex = 4;
            label3.Text = "Asset Tag";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11.25F);
            label7.Location = new Point(986, 54);
            label7.Name = "label7";
            label7.Size = new Size(103, 20);
            label7.TabIndex = 12;
            label7.Text = "Purchase Price";
            // 
            // txtAssetTag
            // 
            txtAssetTag.Location = new Point(153, 50);
            txtAssetTag.Name = "txtAssetTag";
            txtAssetTag.Size = new Size(149, 29);
            txtAssetTag.TabIndex = 5;
            // 
            // cmbType
            // 
            cmbType.FormattingEnabled = true;
            cmbType.Location = new Point(764, 48);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(149, 29);
            cmbType.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11.25F);
            label4.Location = new Point(76, 91);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 6;
            label4.Text = "Status";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(412, 54);
            label6.Name = "label6";
            label6.Size = new Size(49, 20);
            label6.TabIndex = 10;
            label6.Text = "Name";
            // 
            // cmbStatus
            // 
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(153, 87);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(149, 29);
            cmbStatus.TabIndex = 7;
            // 
            // txtName
            // 
            txtName.Location = new Point(467, 48);
            txtName.Name = "txtName";
            txtName.Size = new Size(149, 29);
            txtName.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11.25F);
            label5.Location = new Point(718, 54);
            label5.Name = "label5";
            label5.Size = new Size(40, 20);
            label5.TabIndex = 8;
            label5.Text = "Type";
            // 
            // groupBox3
            // 
            groupBox3.BackColor = SystemColors.ControlLightLight;
            groupBox3.Controls.Add(cmbFilter);
            groupBox3.Controls.Add(btnAdd);
            groupBox3.Controls.Add(btnDelete);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(btnUpdate);
            groupBox3.Controls.Add(btnCheckOut);
            groupBox3.Controls.Add(txtSearch);
            groupBox3.Controls.Add(btnReturn);
            groupBox3.Controls.Add(btnSaveJson);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(btnLoadJson);
            groupBox3.Dock = DockStyle.Top;
            groupBox3.Location = new Point(0, 305);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1413, 118);
            groupBox3.TabIndex = 4;
            groupBox3.TabStop = false;
            // 
            // cmbFilter
            // 
            cmbFilter.FormattingEnabled = true;
            cmbFilter.Location = new Point(494, 74);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.Size = new Size(159, 23);
            cmbFilter.TabIndex = 14;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAdd.Location = new Point(55, 22);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 30);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add Asset";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDelete.Location = new Point(227, 22);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 11.25F);
            label9.Location = new Point(439, 77);
            label9.Name = "label9";
            label9.Size = new Size(40, 20);
            label9.TabIndex = 13;
            label9.Text = "Type";
            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpdate.Location = new Point(141, 22);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 30);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnCheckOut
            // 
            btnCheckOut.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCheckOut.Location = new Point(313, 22);
            btnCheckOut.Name = "btnCheckOut";
            btnCheckOut.Size = new Size(80, 30);
            btnCheckOut.TabIndex = 7;
            btnCheckOut.Text = "Check out";
            btnCheckOut.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(122, 74);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(271, 23);
            txtSearch.TabIndex = 12;
            // 
            // btnReturn
            // 
            btnReturn.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReturn.Location = new Point(399, 22);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(80, 30);
            btnReturn.TabIndex = 8;
            btnReturn.Text = "Return";
            btnReturn.UseVisualStyleBackColor = true;
            // 
            // btnSaveJson
            // 
            btnSaveJson.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSaveJson.Location = new Point(485, 22);
            btnSaveJson.Name = "btnSaveJson";
            btnSaveJson.Size = new Size(80, 30);
            btnSaveJson.TabIndex = 9;
            btnSaveJson.Text = "Save JSON";
            btnSaveJson.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11.25F);
            label8.Location = new Point(55, 77);
            label8.Name = "label8";
            label8.Size = new Size(49, 20);
            label8.TabIndex = 11;
            label8.Text = "Status";
            // 
            // btnLoadJson
            // 
            btnLoadJson.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLoadJson.Location = new Point(571, 22);
            btnLoadJson.Name = "btnLoadJson";
            btnLoadJson.Size = new Size(82, 30);
            btnLoadJson.TabIndex = 10;
            btnLoadJson.Text = "Load JSON";
            btnLoadJson.UseVisualStyleBackColor = true;
            // 
            // dgvAssets
            // 
            dgvAssets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAssets.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvAssets.BackgroundColor = SystemColors.ControlLightLight;
            dgvAssets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAssets.Columns.AddRange(new DataGridViewColumn[] { assetTag, nameCol, typeCol, statusCol, priceCol, maintenanceStatusCol });
            dgvAssets.Dock = DockStyle.Fill;
            dgvAssets.GridColor = SystemColors.ControlText;
            dgvAssets.Location = new Point(0, 423);
            dgvAssets.Name = "dgvAssets";
            dgvAssets.Size = new Size(1413, 212);
            dgvAssets.TabIndex = 5;
            // 
            // assetTag
            // 
            assetTag.HeaderText = "Asset Tag";
            assetTag.Name = "assetTag";
            // 
            // nameCol
            // 
            nameCol.HeaderText = "Name";
            nameCol.Name = "nameCol";
            // 
            // typeCol
            // 
            typeCol.HeaderText = "Type";
            typeCol.Name = "typeCol";
            // 
            // statusCol
            // 
            statusCol.HeaderText = "Status";
            statusCol.Name = "statusCol";
            // 
            // priceCol
            // 
            priceCol.HeaderText = "Price";
            priceCol.Name = "priceCol";
            // 
            // maintenanceStatusCol
            // 
            maintenanceStatusCol.HeaderText = "Maintenance Status";
            maintenanceStatusCol.Name = "maintenanceStatusCol";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(1413, 635);
            Controls.Add(dgvAssets);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAssets).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label3;
        private TextBox txtAssetTag;
        private TextBox txtPrice;
        private Label label7;
        private ComboBox cmbType;
        private Label label4;
        private Label label6;
        private ComboBox cmbStatus;
        private TextBox txtName;
        private Label label5;
        private GroupBox groupBox3;
        private TextBox txtSearch;
        private Label label8;
        private Button btnLoadJson;
        private Button btnSaveJson;
        private Button btnReturn;
        private Button btnCheckOut;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private DataGridView dgvAssets;
        private DataGridViewTextBoxColumn assetTag;
        private DataGridViewTextBoxColumn nameCol;
        private DataGridViewTextBoxColumn typeCol;
        private DataGridViewTextBoxColumn statusCol;
        private DataGridViewTextBoxColumn priceCol;
        private DataGridViewTextBoxColumn maintenanceStatusCol;
        private ComboBox cmbFilter;
        private Label label9;
        private PictureBox pictureBox1;
    }
}
