using System;
using World;

public interface IPausable
{
    void OnPause();
    void OnResume();


    public void Attach(TimeController timeController)
    {
        Attach(ref timeController.onPause, ref timeController.onResume);
    }

    void Attach(ref Action pause, ref Action resume)
    {
        pause += OnPause;
        resume += OnResume;
    }
}