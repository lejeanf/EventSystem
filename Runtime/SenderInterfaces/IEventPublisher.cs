using UnityEngine;

namespace jeanf.EventSystem
{
    public interface IEventPublisher
    {
        Component Source { get; }
    }

    public static class EventPublisherExtensions
    {
        public static void Publish(this IEventPublisher self, System.Action raise)
        {
            EventDiagnostics.PushSender(self);
            try { raise?.Invoke(); }
            finally { EventDiagnostics.PopSender(self); }
        }
    }
}
