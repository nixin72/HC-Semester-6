namespace RAC.RACModels
{
    public class NotificationsViewModel
    {
        public int Id { get; set; }
        public short NotificationType { get; set; }
        public int CandidateId { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }

        public virtual RACUser Candidate { get; set; }
    }
}