namespace Domain.Entities.User;

public interface IUser
{
    string UserName { get; }
    bool IsAuthenticated();
}