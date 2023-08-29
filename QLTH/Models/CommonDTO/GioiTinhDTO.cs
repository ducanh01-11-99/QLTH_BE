namespace QLTH.Models.CommonDTO
{
    public class GioiTinhDTO
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public GioiTinhDTO(int id, string name) {
            Id = id;
            Name = name;
        }
    }
}
