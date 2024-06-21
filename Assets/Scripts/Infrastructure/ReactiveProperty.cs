using System;

namespace Infrastructure
{
    public class ReactiveProperty<T>
    {
        private T _value;

        public event Action<T> OnChangeEvent; 

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChangeEvent?.Invoke(_value);
            }
        }

        public ReactiveProperty(T value)
        {
            _value = value;
        }
    }
}