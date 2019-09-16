using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterConsoleTables.Common
{
    public interface IBasicBuilder<T> where T : new()
    {
        IBasicBuilder<T> With(Action<T> action);
        T Build();
    }

    public class BasicBuilder<T> : IBasicBuilder<T> where T : new()
    {
        List<Action<T>> _actions;

        public BasicBuilder()
        {
            _actions = new List<Action<T>>();
        }

        public IBasicBuilder<T> With(Action<T> action)
        {
            _actions.Add(action);
            return this;
        }

        public T Build()
        {
            var config = new T();
            foreach (var action in _actions)
            {
                action.Invoke(config);
            }

            return config;
        }
    }
}
