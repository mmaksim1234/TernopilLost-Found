namespace server2MVC.Models
{
    
        public class UserDashboardViewModel
        {
            public User User { get; set; }
            public IEnumerable<Advertismnet> Advertisements { get; set; }
        }
    
}
