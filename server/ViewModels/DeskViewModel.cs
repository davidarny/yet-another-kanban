using System.Collections.Generic;

namespace server.ViewModels
{
    public class DeskViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICollection<UserViewModel> Users { get; set; } = new HashSet<UserViewModel>();
    }
}
