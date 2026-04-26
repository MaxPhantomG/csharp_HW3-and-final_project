namespace MyRepositoryApp.Entities
{
    public class User : MyRepositoryApp.Repositories.IEntity
    {
        public int Id { get; }
        public string Username { get; }

        public User(int id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}
