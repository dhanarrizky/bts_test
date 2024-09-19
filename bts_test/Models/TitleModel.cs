namespace bts_test.Models;
public class TitleModel
{
    public int TitleID { get; set; }
    public string TitleName { get; set; }
    public ICollection<Task> Tasks { get; set; }
    public Title()
    {
        Tasks = new List<Task>();
    }
}
