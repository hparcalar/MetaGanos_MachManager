using Microsoft.AspNetCore.Mvc;

namespace MachManager.Models.Parameters{
    public class UploadItemModel{
        [FromForm(Name = "File")]
        public IFormFile File { get; set; }

        [FromForm(Name = "PlantId")]
        public int PlantId { get; set; }
    }
}