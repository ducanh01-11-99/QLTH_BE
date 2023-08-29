namespace QLTH.Models.RegionDTO
{
    public class ProvinceDTO
    {
        public string Code { get; set; }
        public string VNeseName { get; set; }
        public string EngName { get; set; }
        public string VNeseFullName { get; set; }
        public string EngFullName { get; set;}
        public string codeName { get; set; }
        public int administrativeRegionId { get; set; }
        public int administrativeUnitId { get; set; }
        public ProvinceDTO(string code, string vNeseName, string engName, string vNeseFullName, string engFullName, string codeName, int administrativeUnitId, int administrativeRegionId)
        {
            Code = code;
            VNeseName = vNeseName;
            EngName = engName;
            VNeseFullName = vNeseFullName;
            EngFullName = engFullName;
            this.codeName = codeName;
            this.administrativeRegionId = administrativeRegionId;
            this.administrativeUnitId = administrativeUnitId;
        }
    }
}
