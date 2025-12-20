using System;

public interface IPausable
{
    void OnPause();
    void OnResume();

    public void Attach(ref Action pause, ref Action resume)
    {
        pause += OnPause;
        resume += OnResume;
    }
}