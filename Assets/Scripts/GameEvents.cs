using System;

public static class GameEvents
{
    public static Action PlayerHit;
    public static Action<float> TimeUpdated;
    public static Action GameOver;
}
