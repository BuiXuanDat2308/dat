namespace MISA.Web.Api.Model
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public int? Gender { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? IdentityNumber { get; set; }
        public string? IdentityPlace { get; set; }
        public DateTime? IdentityDate { get; set; }
        public string? TaxCode { get; set; }
        public decimal? Salary { get; set; }
        public int? WorkStatus {  get; set; }
        public Guid? PositionId { get; set; }
        public Guid? DepartmentId { get; set; }
        public string GenderName {
            get {
                switch (Gender)
                {
                    case 0:
                        return "Nữ";
                    case 1:
                        return "Nam";
                    default:
                        return "Không xác định";
                }
                //return "Nam";
            } 
        }
        public DateTime JoinDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifiledDate { get; set; }
        public string? ModifiledBy { get; set; }

    }
}
