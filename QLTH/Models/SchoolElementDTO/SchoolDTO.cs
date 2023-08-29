namespace QLTH.Models.SchoolElementDTO
{
    public class SchoolDTO
    {
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string EngName { get; set; }
        public string DiaChi { get; set; }
        public int PhuongXaID { get; set; }
        public int QuanHuyenID { get; set; }
        public int TinhThanhPhoID { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        public SchoolDTO(int id, string name, string enName, string address, int wardID, int districtID, int provinceID, string phoneNumber, string fax, string Email, string website) {
            this.SchoolID = id;
            this.SchoolName = name;
            this.EngName = enName;
            this.DiaChi = address;
            this.TinhThanhPhoID = provinceID; this.Phone = phoneNumber;
            this.PhuongXaID = wardID;
            this.QuanHuyenID = districtID;
            this.Fax = fax;
            this.Website = website;
            this.Email = Email;
        }
    }
}
