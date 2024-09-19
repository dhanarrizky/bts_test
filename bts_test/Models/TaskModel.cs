namespace bts_test.Models;
public class TaskModel
{
    public int TaskID { get; set; }
    public string TaskDescription { get; set; }
    public bool IsCompleted { get; set; }
    public int TitleID { get; set; }
    public Title Title { get; set; }
}
