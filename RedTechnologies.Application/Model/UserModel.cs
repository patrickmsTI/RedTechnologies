namespace RedTechnologies.Application.Model
{
    public class UserModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; } 
    }

    public class UserConstants
    {
        public static List<UserModel> Users = new()
            {
                    new UserModel(){ Username="admin",Password="admin"}
            };
    }
}
