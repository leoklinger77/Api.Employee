using Domain.Notifications;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> FindAlls();
        void Handle(Notification notification);
    }
}
