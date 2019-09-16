using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleury.Agendamento.Application;
using Flunt.Notifications;

namespace Fleury.Agendamento.Infrastructure.Http
{
    public class Resultado : Notifiable
    {
        protected Resultado()
        {
        }

        protected Resultado(ICollection<Notification> notifications)
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
