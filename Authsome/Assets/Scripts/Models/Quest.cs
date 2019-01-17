namespace Assets.Scripts.Models
{
    public class Quest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object userQuests { get; set; }
        public object[] questReward { get; set; }
        public object[] questTasks { get; set; }
    }

}
