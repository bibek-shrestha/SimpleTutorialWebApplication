namespace SimpleTutorialWebApplication.Models;

public class UserInfo
{
    public int UserId { get; set; }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string City  { get; set; }

    public UserInfo(int userId, string userName, string firstName, string lastName, string city)
    {
        this.UserId = userId;
        this.UserName = userName;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.City = city;
    }
}
