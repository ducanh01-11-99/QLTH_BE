using QLTH.Models.DTO;

namespace QLTH.Data
{
    public static class SchoolStore
    {
        public static List<SchoolDTO> schoolList = new List<SchoolDTO>
        {
            new SchoolDTO { Id = 1, Name="THPT Quang Trung"},
            new SchoolDTO { Id = 2, Name="THPT Ninh Giang"}
        };
    }
}
