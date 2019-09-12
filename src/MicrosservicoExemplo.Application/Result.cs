using System.Collections.Generic;
using Flunt.Notifications;

namespace Fleury.Agendamento.Application
{
    public class Result:Notifiable
    {
        protected Result()
        {
        }

        protected Result(ICollection<Notification> notifications)
        {
            this.AddNotifications(notifications);
        }

        public void AddNotification(string error)
        {
            this.AddNotification(null, error);
        }

        public ErrorCode? Error { get; set; }
    }
}
