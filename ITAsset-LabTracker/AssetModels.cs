using System;
using System.Text.Json.Serialization;

namespace ITAsset_LabTracker
{
    // ==========================================
    // 1. CAPABILITY INTERFACES
    // ==========================================
    public interface IBorrowable
    {
        string BorrowerId { get; }
        bool IsCheckedOut { get; }
        void CheckOut(string borrowerId);
        void ReturnItem();
    }

    public interface INetworkable
    {
        string IpAddress { get; set; }
        string MacAddress { get; set; }
        string GetNetworkSummary();
    }

    // ==========================================
    // 2. ABSTRACT BASE CLASS (ENCAPSULATION + POLYMORPHISM)
    // ==========================================
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$assetType")]
    [JsonDerivedType(typeof(Workstation), typeDiscriminator: "Workstation")]
    [JsonDerivedType(typeof(NetworkingHardware), typeDiscriminator: "NetworkingHardware")]
    [JsonDerivedType(typeof(Peripheral), typeDiscriminator: "Peripheral")]
    [JsonDerivedType(typeof(SoftwareLicense), typeDiscriminator: "SoftwareLicense")]
    public abstract class CampusAsset
    {
        // Encapsulated fields: readable publicly, modifiable ONLY via validated methods
        public string AssetTag { get; private set; }
        public string Name { get; private set; }
        public decimal PurchasePrice { get; private set; }
        public string Status { get; private set; }
        public DateTime PurchaseDate { get; private set; }

        public abstract string AssetTypeName { get; }

        // Parameterless constructor required for JSON deserialization
        protected CampusAsset() { }

        protected CampusAsset(string assetTag, string name, decimal purchasePrice, string status = "Available")
        {
            if (string.IsNullOrWhiteSpace(assetTag))
                throw new ArgumentException("Asset Tag cannot be empty.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Asset Name cannot be empty.");
            if (purchasePrice < 0)
                throw new ArgumentException("Purchase price cannot be negative.");

            AssetTag = assetTag.Trim().ToUpper();
            Name = name.Trim();
            PurchasePrice = purchasePrice;
            Status = string.IsNullOrWhiteSpace(status) ? "Available" : status;
            PurchaseDate = DateTime.Now;
        }

        // Validated state-change method (Encapsulation)
        public void UpdateDetails(string name, decimal purchasePrice, string newStatus)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Asset Name cannot be empty.");
            if (purchasePrice < 0)
                throw new ArgumentException("Purchase price cannot be negative.");

            Name = name.Trim();
            PurchasePrice = purchasePrice;
            SetStatus(newStatus);
        }

        protected void SetStatus(string newStatus)
        {
            string[] validStatuses = { "Available", "Checked Out", "In Repair", "Retired" };
            if (Array.IndexOf(validStatuses, newStatus) == -1)
                throw new ArgumentException($"Invalid status: {newStatus}");

            Status = newStatus;
        }

        // Polymorphic methods implemented differently by each subclass
        public abstract decimal CalculateDepreciation();
        public abstract string GetMaintenanceStatus();
    }

    // ==========================================
    // 3. DERIVED ASSET CLASSES
    // ==========================================
    public class Workstation : CampusAsset, IBorrowable, INetworkable
    {
        public override string AssetTypeName => "Workstation";
        public string BorrowerId { get; private set; } = string.Empty;
        public bool IsCheckedOut => Status == "Checked Out";
        public string IpAddress { get; set; } = "192.168.1.100";
        public string MacAddress { get; set; } = "00:1B:44:11:3A:B7";

        public Workstation() { }
        public Workstation(string tag, string name, decimal price, string status = "Available")
            : base(tag, name, price, status) { }

        public void CheckOut(string borrowerId)
        {
            if (Status == "In Repair" || Status == "Retired")
                throw new InvalidOperationException($"Cannot check out '{Name}' because its status is '{Status}'.");
            if (IsCheckedOut)
                throw new InvalidOperationException($"'{Name}' is already checked out to {BorrowerId}.");
            if (string.IsNullOrWhiteSpace(borrowerId))
                throw new ArgumentException("Borrower ID is required.");

            BorrowerId = borrowerId.Trim();
            SetStatus("Checked Out");
        }

        public void ReturnItem()
        {
            if (!IsCheckedOut)
                throw new InvalidOperationException($"'{Name}' is not currently checked out.");

            BorrowerId = string.Empty;
            SetStatus("Available");
        }

        public string GetNetworkSummary() => $"IP: {IpAddress} | MAC: {MacAddress}";

        // 5-year hardware depreciation cycle
        public override decimal CalculateDepreciation() => Math.Round(PurchasePrice / 5m, 2);

        public override string GetMaintenanceStatus()
        {
            if (Status == "In Repair") return "Hardware Diagnostics Required";
            if (IsCheckedOut) return $"Loaned to {BorrowerId} | Deprec: ₱{CalculateDepreciation()}/yr";
            return $"Healthy (5-Yr Cycle: ₱{CalculateDepreciation()}/yr)";
        }
    }

    public class NetworkingHardware : CampusAsset, INetworkable
    {
        public override string AssetTypeName => "NetworkingHardware";
        public string IpAddress { get; set; } = "10.0.0.1";
        public string MacAddress { get; set; } = "AA:BB:CC:DD:EE:FF";

        public NetworkingHardware() { }
        public NetworkingHardware(string tag, string name, decimal price, string status = "Available")
            : base(tag, name, price, status) { }

        public string GetNetworkSummary() => $"Gateway IP: {IpAddress} | MAC: {MacAddress}";

        // 7-year infrastructure cycle
        public override decimal CalculateDepreciation() => Math.Round(PurchasePrice / 7m, 2);

        public override string GetMaintenanceStatus()
        {
            if (Status == "In Repair") return "Port/Firmware Fault - Offline";
            return $"Online ({IpAddress}) | Deprec: ₱{CalculateDepreciation()}/yr";
        }
    }

    public class Peripheral : CampusAsset, IBorrowable
    {
        public override string AssetTypeName => "Peripheral";
        public string BorrowerId { get; private set; } = string.Empty;
        public bool IsCheckedOut => Status == "Checked Out";

        public Peripheral() { }
        public Peripheral(string tag, string name, decimal price, string status = "Available")
            : base(tag, name, price, status) { }

        public void CheckOut(string borrowerId)
        {
            if (Status == "In Repair" || Status == "Retired")
                throw new InvalidOperationException($"Cannot check out '{Name}' while '{Status}'.");
            if (IsCheckedOut)
                throw new InvalidOperationException($"'{Name}' is already borrowed by {BorrowerId}.");
            if (string.IsNullOrWhiteSpace(borrowerId))
                throw new ArgumentException("Borrower ID is required.");

            BorrowerId = borrowerId.Trim();
            SetStatus("Checked Out");
        }

        public void ReturnItem()
        {
            if (!IsCheckedOut)
                throw new InvalidOperationException($"'{Name}' is not checked out.");

            BorrowerId = string.Empty;
            SetStatus("Available");
        }

        // 3-year accessory cycle
        public override decimal CalculateDepreciation() => Math.Round(PurchasePrice / 3m, 2);

        public override string GetMaintenanceStatus()
        {
            if (Status == "In Repair") return "Needs Replacement / Cable Check";
            if (IsCheckedOut) return $"Borrowed by {BorrowerId}";
            return $"Ready for Lab Use | Deprec: ₱{CalculateDepreciation()}/yr";
        }
    }

    public class SoftwareLicense : CampusAsset
    {
        public override string AssetTypeName => "SoftwareLicense";
        public int SubscriptionMonthsRemaining { get; set; } = 12;

        public SoftwareLicense() { }
        public SoftwareLicense(string tag, string name, decimal price, string status = "Available")
            : base(tag, name, price, status) { }

        // Monthly subscription amortization
        public override decimal CalculateDepreciation() =>
            SubscriptionMonthsRemaining > 0 ? Math.Round(PurchasePrice / SubscriptionMonthsRemaining, 2) : 0;

        public override string GetMaintenanceStatus()
        {
            if (Status == "In Repair") return "License Key Activation Issue";
            return $"{SubscriptionMonthsRemaining} mos left (₱{CalculateDepreciation()}/mo)";
        }
    }
}