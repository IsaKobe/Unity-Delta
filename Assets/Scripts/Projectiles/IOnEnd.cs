using System;

public interface IOnEnd<T>
{
    public Action<T> onEnd { get; set; }
}