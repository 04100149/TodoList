namespace TodoList.Data
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime TargetDate { get; set; }
        public string Memo { get; set; }
        public DateTime EndDate { get; set; } = DateTime.MinValue;
        public bool IsDone { get; set; } = false;
    }
}
