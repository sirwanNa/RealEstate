namespace RealEstate.Application.DTOs.Common
{
    public class FileUploadViewModelPost:BaseModel
    {
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
    }
}
