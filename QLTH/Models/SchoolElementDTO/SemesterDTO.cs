namespace QLTH.Models.SchoolElementDTO
{
    public class SemesterDTO
    {
        public int semesterID { get; set; }
        public int yearSchoolCode { get; set; }
        public DateTime startDate { get; set; }

        public  SemesterDTO(int semesterID, int yearSchoolCode, DateTime date) {
            startDate = date;
            this.semesterID = semesterID;
            this.yearSchoolCode = yearSchoolCode;
        }

    }
}
