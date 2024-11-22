using System;

public static class GameEvent
{
    public static Action CallbackGameComplete;
    public static Action<int> CallbackEnemyAmountChange;
    public static Action<int> CallbackWaveDone;
    public static Action<int> CallbackNextWave;
}