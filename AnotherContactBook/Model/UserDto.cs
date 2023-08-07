namespace AnotherContactBook.Model
{
    //The UserDto enables the user to register and login
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; } //Password should be in Plain Text
        //public string Role { get; set; }
    }
}
