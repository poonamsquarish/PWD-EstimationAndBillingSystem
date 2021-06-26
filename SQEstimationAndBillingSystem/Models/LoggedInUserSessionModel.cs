namespace SQEstimationAndBillingSystem.Models
{
    public class LoggedInUserSessionModel
    {
        public long LoggedInUserId { get; set; }
        public string LoggedInUserName { get; set; }
        public long SelectedDSRId { get; set; }
        public string SelectedDSRName { get; set; }
        public long SelectedProjectId { get; set; }
        public string SelectedProjectName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsLatestDSR { get; set; }
    }
}