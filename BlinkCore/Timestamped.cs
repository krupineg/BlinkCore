namespace BlinkCore
{
    public class Timestamped<T>
    {
        public long Timestamp { get; set; }
        public T Value { get; set; }

        public Timestamped(T value, long timestamp)
        {
            Value = value;
            Timestamp = timestamp;
        }
    }
}