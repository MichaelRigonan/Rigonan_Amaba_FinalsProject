using Microsoft.AspNetCore.Mvc;
using ITAsset_LabTracker;

namespace CampusITAssetTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly AssetRepository _repository = new AssetRepository();

        // GET: api/assets (Reads all assets from campus_assets.json)
        [HttpGet]
        public ActionResult<IEnumerable<object>> GetAllAssets()
        {
            var assets = _repository.LoadFromJson();
            return Ok(assets.Select(a => new
            {
                a.AssetTag,
                a.Name,
                Type = a.AssetTypeName,
                a.Status,
                a.PurchasePrice,
                MaintenanceStatus = a.GetMaintenanceStatus()
            }));
        }

        // POST: api/assets (Creates a new asset and saves to campus_assets.json)
        [HttpPost]
        public IActionResult CreateAsset([FromBody] AssetDto dto)
        {
            try
            {
                var assets = _repository.LoadFromJson();

                if (assets.Any(a => a.AssetTag.Equals(dto.AssetTag, StringComparison.OrdinalIgnoreCase)))
                    return BadRequest(new { error = $"Asset tag '{dto.AssetTag}' already exists." });

                CampusAsset newAsset = dto.AssetType switch
                {
                    "Workstation" => new Workstation(dto.AssetTag, dto.Name, dto.PurchasePrice, dto.Status),
                    "NetworkingHardware" => new NetworkingHardware(dto.AssetTag, dto.Name, dto.PurchasePrice, dto.Status),
                    "Peripheral" => new Peripheral(dto.AssetTag, dto.Name, dto.PurchasePrice, dto.Status),
                    "SoftwareLicense" => new SoftwareLicense(dto.AssetTag, dto.Name, dto.PurchasePrice, dto.Status),
                    _ => throw new ArgumentException("Invalid AssetType. Use Workstation, NetworkingHardware, Peripheral, or SoftwareLicense.")
                };

                assets.Add(newAsset);
                _repository.SaveToJson(assets);

                return Ok(new { message = $"Asset '{newAsset.AssetTag}' saved to JSON." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: api/assets/{tag}/checkout (Tests IBorrowable interface via Swagger)
        [HttpPost("{tag}/checkout")]
        public IActionResult CheckOutAsset(string tag, [FromQuery] string borrowerId)
        {
            try
            {
                var assets = _repository.LoadFromJson();
                var asset = assets.FirstOrDefault(a => a.AssetTag.Equals(tag, StringComparison.OrdinalIgnoreCase));

                if (asset == null)
                    return NotFound(new { error = $"Asset '{tag}' not found." });

                if (asset is IBorrowable borrowable)
                {
                    borrowable.CheckOut(borrowerId);
                    _repository.SaveToJson(assets);
                    return Ok(new { message = $"Checked out '{asset.Name}' to {borrowerId}." });
                }

                return BadRequest(new { error = $"'{asset.AssetTypeName}' does not implement IBorrowable." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: api/assets/{tag}/return (Returns a borrowed asset via Swagger)
        [HttpPost("{tag}/return")]
        public IActionResult ReturnAsset(string tag)
        {
            try
            {
                var assets = _repository.LoadFromJson();
                var asset = assets.FirstOrDefault(a => a.AssetTag.Equals(tag, StringComparison.OrdinalIgnoreCase));

                if (asset == null)
                    return NotFound(new { error = $"Asset '{tag}' not found." });

                if (asset is IBorrowable borrowable)
                {
                    borrowable.ReturnItem();
                    _repository.SaveToJson(assets);
                    return Ok(new { message = $"'{asset.Name}' returned and is now Available." });
                }

                return BadRequest(new { error = $"'{asset.AssetTypeName}' does not implement IBorrowable." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // DELETE: api/assets/{tag}
        [HttpDelete("{tag}")]
        public IActionResult DeleteAsset(string tag)
        {
            var assets = _repository.LoadFromJson();
            var asset = assets.FirstOrDefault(a => a.AssetTag.Equals(tag, StringComparison.OrdinalIgnoreCase));

            if (asset == null)
                return NotFound(new { error = $"Asset '{tag}' not found." });

            assets.Remove(asset);
            _repository.SaveToJson(assets);

            return Ok(new { message = $"Deleted '{tag}' from JSON." });
        }
    }

    public record AssetDto(string AssetTag, string Name, string AssetType, decimal PurchasePrice, string Status = "Available");
}