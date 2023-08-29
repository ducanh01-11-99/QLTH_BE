namespace QLTH.Models.CommonDTO
{
    public class DantocDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public DantocDTO(int id, string name) {
            this.ID = id;
            Name = name;
        }
    }
    
}
