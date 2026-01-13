using System;
using System.Collections.Generic;
using System.Text;
using World;

public interface IScorable
{
    public int Score { get; }

    public void AddScore()
    {
        WorldController.AddScore(Score);
    }
}
