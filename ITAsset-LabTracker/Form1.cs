using ITAsset_LabTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Globalization;

namespace ITAsset_LabTracker
{
    public partial class Form1 : Form
    {
        // Generic Collection for in-memory storage (Phase 1 & 2 requirement)
        private List<CampusAsset> _assets = new List<CampusAsset>();
        private readonly AssetRepository _repository = new AssetRepository();

        public Form1()
        {
            InitializeComponent();
            InitializeFormControls();
            SeedSampleData();
            RefreshGrid();
        }

        // ==========================================
        // INITIALIZATION & GRID SETUP
        // ==========================================
        private void InitializeFormControls()
        {
            // Populates Type ComboBox matching your derived classes
            cmbType.Items.Clear();
            cmbType.Items.AddRange(new object[] { "Workstation", "NetworkingHardware", "Peripheral", "SoftwareLicense" });
            cmbType.SelectedIndex = 0;

            // Populates Status ComboBox
            cmbStatus.Items.Clear();
            cmbStatus.Items.AddRange(new object[] { "Available", "Checked Out", "In Repair", "Retired" });
            cmbStatus.SelectedIndex = 0;

            // Populates Filter ComboBox for Phase 3 LINQ queries
            cmbFilter.Items.Clear();
            cmbFilter.Items.AddRange(new object[]
            {
                "All assets",
                "Available only",
                "Checked out (IBorrowable)",
                "Borrowable items (IBorrowable)",
                "Networkable devices (INetworkable)",
                "In Repair"
            });
            cmbFilter.SelectedIndex = 0;

            // Configures DataGridView columns
            dgvAssets.AutoGenerateColumns = false;
            dgvAssets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAssets.MultiSelect = false;
            dgvAssets.ReadOnly = true;
            dgvAssets.AllowUserToAddRows = false;

            dgvAssets.Columns.Clear();
            dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTag", HeaderText = "Asset tag", DataPropertyName = "AssetTag", Width = 110 });
            dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Name", DataPropertyName = "Name", Width = 180 });
            dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colType", HeaderText = "Type", DataPropertyName = "AssetTypeName", Width = 140 });
            dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colStatus", HeaderText = "Status", DataPropertyName = "Status", Width = 100 });
            dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Price", DataPropertyName = "PurchasePrice", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2", FormatProvider = CultureInfo.GetCultureInfo("en-PH") }});
            dgvAssets.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMaint", HeaderText = "Maintenance status", DataPropertyName = "MaintenanceSummary", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            // Wire up events
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnCheckOut.Click += btnCheckOut_Click;
            btnReturn.Click += btnReturn_Click;
            btnSaveJson.Click += btnSaveJson_Click;
            btnLoadJson.Click += btnLoadJson_Click;
            txtSearch.TextChanged += (s, e) => RefreshGrid();
            cmbFilter.SelectedIndexChanged += (s, e) => RefreshGrid();
            dgvAssets.SelectionChanged += dgvAssets_SelectionChanged;
        }

        private void SeedSampleData()
        {
            _assets.Add(new Workstation("LAB1-PC01", "Dell OptiPlex 7090", 48500m, "Available"));
            _assets.Add(new NetworkingHardware("NET-SW01", "Cisco Catalyst 2960", 32000m, "Available"));
            _assets.Add(new Peripheral("PER-PRJ02", "Epson EB-X51 Projector", 24000m, "In Repair"));
            _assets.Add(new SoftwareLicense("LIC-VS26", "Visual Studio Enterprise", 15000m, "Available"));
        }

        // ==========================================
        // PHASE 2: CRUD OPERATIONS & VALIDATION
        // ==========================================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string tag = txtAssetTag.Text.Trim().ToUpper();
                if (_assets.Any(a => a.AssetTag == tag))
                {
                    MessageBox.Show($"Asset tag '{tag}' already exists.", "Duplicate Tag", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Please enter a valid numeric purchase price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string name = txtName.Text;
                string selectedType = cmbType.SelectedItem?.ToString() ?? "Workstation";
                string status = cmbStatus.SelectedItem?.ToString() ?? "Available";

                // Polymorphic Factory Instantiation
                CampusAsset newAsset = selectedType switch
                {
                    "Workstation" => new Workstation(tag, name, price, status),
                    "NetworkingHardware" => new NetworkingHardware(tag, name, price, status),
                    "Peripheral" => new Peripheral(tag, name, price, status),
                    "SoftwareLicense" => new SoftwareLicense(tag, name, price, status),
                    _ => throw new ArgumentException("Unknown asset type.")
                };

                _assets.Add(newAsset);
                ClearInputs();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Asset Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CampusAsset selected = GetSelectedAsset();
                if (selected == null) return;

                if (!decimal.TryParse(txtPrice.Text, out decimal price))
                {
                    MessageBox.Show("Please enter a valid numeric purchase price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Encapsulated state update
                selected.UpdateDetails(txtName.Text, price, cmbStatus.SelectedItem?.ToString() ?? "Available");
                RefreshGrid();
                MessageBox.Show("Asset updated successfully.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            CampusAsset selected = GetSelectedAsset();
            if (selected == null) return;

            var confirm = MessageBox.Show($"Delete asset '{selected.AssetTag} - {selected.Name}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                _assets.Remove(selected);
                ClearInputs();
                RefreshGrid();
            }
        }

        // ==========================================
        // PHASE 3: INTERFACE CASTING & BUSINESS LOGIC
        // ==========================================
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            try
            {
                CampusAsset selected = GetSelectedAsset();
                if (selected == null) return;

                // Interface Casting:Implements IBorrowable
                if (selected is IBorrowable borrowableAsset)
                {
                    string borrowerId = PromptForBorrowerId();
                    if (string.IsNullOrWhiteSpace(borrowerId)) return;

                    borrowableAsset.CheckOut(borrowerId);
                    RefreshGrid();
                    MessageBox.Show($"Checked out '{selected.Name}' to {borrowerId}.", "Checked Out", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Assets of type '{selected.AssetTypeName}' cannot be checked out.", "Not Borrowable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Check-Out Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                CampusAsset selected = GetSelectedAsset();
                if (selected == null) return;

                if (selected is IBorrowable borrowableAsset)
                {
                    borrowableAsset.ReturnItem();
                    RefreshGrid();
                    MessageBox.Show($"'{selected.Name}' has been returned and is now Available.", "Returned", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Assets of type '{selected.AssetTypeName}' do not support check-out/return.", "Not Borrowable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Return Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ==========================================
        // PHASE 3: LINQ SEARCH & FILTERING
        // ==========================================
        private void RefreshGrid()
        {
            string search = txtSearch.Text.Trim().ToLower();
            string filter = cmbFilter.SelectedItem?.ToString() ?? "All assets";

            IEnumerable<CampusAsset> query = _assets;

            // 1. Apply Interface / Status LINQ Filter
            query = filter switch
            {
                "Available only" => query.Where(a => a.Status == "Available"),
                "Checked out (IBorrowable)" => query.OfType<IBorrowable>().Where(b => b.IsCheckedOut).Cast<CampusAsset>(),
                "Borrowable items (IBorrowable)" => query.OfType<IBorrowable>().Cast<CampusAsset>(),
                "Networkable devices (INetworkable)" => query.OfType<INetworkable>().Cast<CampusAsset>(),
                "In Repair" => query.Where(a => a.Status == "In Repair"),
                _ => query
            };

            // 2. Apply Search Keyword LINQ Filter
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a =>
                    a.AssetTag.ToLower().Contains(search) ||
                    a.Name.ToLower().Contains(search) ||
                    a.AssetTypeName.ToLower().Contains(search) ||
                    a.Status.ToLower().Contains(search));
            }

            // 3. Project to anonymous view object so polymorphic GetMaintenanceStatus() renders cleanly
            var displayList = query.Select(a => new
            {
                a.AssetTag,
                a.Name,
                a.AssetTypeName,
                a.Status,
                a.PurchasePrice,
                MaintenanceSummary = a.GetMaintenanceStatus()
            }).ToList();

            dgvAssets.DataSource = null;
            dgvAssets.DataSource = displayList;
        }

        // ==========================================
        // PHASE 4: FILE I/O (JSON SERIALIZATION)
        // ==========================================
        private void btnSaveJson_Click(object sender, EventArgs e)
        {
            try
            {
                _repository.SaveToJson(_assets);
                MessageBox.Show("All assets saved to campus_assets.json successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save JSON: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadJson_Click(object sender, EventArgs e)
        {
            try
            {
                _assets = _repository.LoadFromJson();
                RefreshGrid();
                MessageBox.Show($"Loaded {_assets.Count} assets from JSON.", "Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load JSON: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // UI HELPER METHODS
        // ==========================================
        private CampusAsset GetSelectedAsset()
        {
            if (dgvAssets.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an asset from the table first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            string tag = dgvAssets.SelectedRows[0].Cells["colTag"].Value?.ToString();
            return _assets.FirstOrDefault(a => a.AssetTag == tag);
        }

        private void dgvAssets_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAssets.SelectedRows.Count == 0) return;

            string tag = dgvAssets.SelectedRows[0].Cells["colTag"].Value?.ToString();
            CampusAsset selected = _assets.FirstOrDefault(a => a.AssetTag == tag);
            if (selected != null)
            {
                txtAssetTag.Text = selected.AssetTag;
                txtName.Text = selected.Name;
                cmbType.SelectedItem = selected.AssetTypeName;
                txtPrice.Text = selected.PurchasePrice.ToString("0.##");
                cmbStatus.SelectedItem = selected.Status;
            }
        }

        private void ClearInputs()
        {
            txtAssetTag.Clear();
            txtName.Clear();
            txtPrice.Clear();
            cmbType.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            txtAssetTag.Focus();
        }

        // Lightweight dialog box for entering Student/Faculty Borrower ID on Check Out
        private string PromptForBorrowerId()
        {
            using Form prompt = new Form()
            {
                Width = 340,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Check Out Asset",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lbl = new Label() { Left = 15, Top = 15, Width = 290, Text = "Enter Student / Faculty Borrower ID:" };
            TextBox txtInput = new TextBox() { Left = 15, Top = 40, Width = 290 };
            Button btnOk = new Button() { Text = "Confirm", Left = 150, Width = 75, Top = 75, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancel", Left = 230, Width = 75, Top = 75, DialogResult = DialogResult.Cancel };

            prompt.Controls.AddRange(new Control[] { lbl, txtInput, btnOk, btnCancel });
            prompt.AcceptButton = btnOk;
            prompt.CancelButton = btnCancel;

            return prompt.ShowDialog() == DialogResult.OK ? txtInput.Text : string.Empty;
        }
    }
}