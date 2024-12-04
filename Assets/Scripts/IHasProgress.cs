using System;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<OnProgreessChangedEventArgs> OnProgress_Changed;
    public class OnProgreessChangedEventArgs : EventArgs
    {
        public float ProgreessNormalized;
    }
}
