using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Builders
{
    public abstract class Builder<T, TItem>
        where T : Builder<T, TItem>
        where TItem : new()
    {
        protected TItem State;

        protected Builder()
        {
            Init();
        }

        public virtual TItem Build()
        {
            return State;
        }
        public T With(Action<TItem> operation)
        {
            operation.Invoke(State);
            return (T)this;
        }

        private void Init()
        {
            State = new TItem();
        }

        public TItem Get()
        {
            return State;
        }

        public TItem As(TItem item)
        {
            return State = item;
        }

        public T For(Action<T> operation)
        {
            operation.Invoke((T)this);
            return (T)this;
        }

    }
}
