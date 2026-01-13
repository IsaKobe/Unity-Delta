using System;
using World;

public interface IPausable
{
    void OnPause();
    void OnResume();


    public void Attach(TimeController timeController)
    {
        timeController.onPause += OnPause;
        timeController.onResume += OnResume;
    }
    public void Detach(TimeController timeController)
    {
        timeController.onPause -= OnPause;
        timeController.onResume -= OnResume;
    }
}