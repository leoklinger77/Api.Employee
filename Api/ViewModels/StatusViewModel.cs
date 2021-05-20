namespace Api.ViewModels
{
    public class StatusViewModel : EntityViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool AllowLogin { get; set; }
    }
}
