using System;

public static class GameEvent
{
    public static Action GameCompleteEvent;
    public static Action<int> EnemySpawnEvent;
    public static Action<int> EnemyDeadEvent;
    public static Action<int> WaveDoneEvent;
    public static Action<int> NextWaveEvent;
    public static Action SkipShoppingEvent;
    public static Action<float> ShoppingTimeChangeEvent;
}