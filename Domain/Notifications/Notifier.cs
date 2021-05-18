using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier(List<Notification> notifications)
        {
            _notifications = notifications;
        }

        public List<Notification> FindAlls()
        {
            return _notifications;
        }

        public void Handle(Notification notification)
        {
            _notifications.Add(notification);
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
