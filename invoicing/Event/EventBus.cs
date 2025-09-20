namespace invoicing.Event
{
    public class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type))
            {
                _subscribers[type] = new List<Delegate>();
            }
            _subscribers[type].Add(handler);
        }

        public void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (_subscribers.ContainsKey(type))
            {
                _subscribers[type].Remove(handler);
                if (_subscribers[type].Count == 0)
                {
                    _subscribers.Remove(type);
                }
            }
        }

        public void Publish<T>(T eventMessage)
        {
            var type = typeof(T);
            if (_subscribers.ContainsKey(type))
            {
                // 建立複本避免迭代中修改
                foreach (var handler in new List<Delegate>(_subscribers[type]))
                {
                    ((Action<T>)handler)?.Invoke(eventMessage);
                }
            }
        }
    }
}
