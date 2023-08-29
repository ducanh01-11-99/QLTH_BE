namespace QLTH.Models.RegionDTO
{
    public class WardDTO : Region
    {
        public string districtCode {  get; set; }
        public WardDTO(string code, string vNeseName, string engName, string vNeseFullName, string engFullName, string codeName, string provinceCode, int administrativeUnitId) 
            :base(code, vNeseName, engName, vNeseName, engName, codeName, administrativeUnitId) {
            this.districtCode = districtCode;
        }
    }
}
