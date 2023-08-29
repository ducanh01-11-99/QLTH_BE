namespace QLTH.Models.SchoolElementDTO
{
    public class YearSchoolDTO
    {
        public int yearID { get; set; }
        public string yearName { get; set; }
        public DateTime startDate { get; set; }

        public YearSchoolDTO(int yearID, string yearName, DateTime date)
        {
            startDate = date;
            this.yearName = yearName;
            this.yearID = yearID;
        }
    }
}
