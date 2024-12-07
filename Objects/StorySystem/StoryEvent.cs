namespace TextAdventureGame.Objects.StorySystem
{
    public class StoryEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsChoiceEvent { get; set; }
        public Func<bool> TriggerCondition { get; set; }
        public List<string> Dependencies { get; set; }
        public Dictionary<int, Action> Consequences { get; set; }
        public bool IsCompleted { get; set; } = false;

        public StoryEvent(string id, string title, string description, bool isChoiceEvent)
        {
            Id = id;
            Title = title;
            Description = description;
            IsChoiceEvent = isChoiceEvent;
            Consequences = [];
            Dependencies = [];
        }
    }
}
