using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrtajMeUtil
{
    abstract public class Subject
    {
        List<IObserver> _observerList = new List<IObserver>();

        public void addObserver(IObserver inObserver)
        {
            _observerList.Add(inObserver);
        }

        public void notifyObservers()
        {
            foreach (IObserver obs in _observerList)
                obs.UpdateEx();
        }

        public void removeObserver(IObserver obs)
        {
            if (_observerList.Contains(obs))
                _observerList.Remove(obs);
        }
    }
}
