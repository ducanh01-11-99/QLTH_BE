namespace QLTH.Models.RegionDTO
{
    public class DistrictDTO : Region
    {
        public string ProvinceCode { get; set; }
        public DistrictDTO(string code, string vNeseName, string engName, string vNeseFullName, string engFullName, string codeName, string provinceCode, int administrativeUnitId) 
            : base(code, vNeseName, engName, vNeseName, engName, codeName, administrativeUnitId) {
            this.ProvinceCode = provinceCode;
        }
    }
 }
