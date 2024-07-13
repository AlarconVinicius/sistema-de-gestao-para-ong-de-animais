namespace SGONGA.Core.Notifications;

public interface INotifier
{
    bool HasNotification();
    List<Notification> GetNotifications();
    void Handle(Notification notification);
    void Clear();
}

public class Notifier : INotifier
{
    private List<Notification> _notifications;

    public Notifier()
    {
        _notifications = new List<Notification>();
    }

    public void Handle(Notification notification)
    {
        _notifications.Add(notification);
    }
    public void Clear()
    {
        _notifications.Clear();
    }
    public List<Notification> GetNotifications()
    {
        return _notifications;
    }

    public bool HasNotification()
    {
        return _notifications.Any();
    }
}