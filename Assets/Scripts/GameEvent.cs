using System;

public static class GameEvent
{
    public static Action GameCompleteEvent;
    //Trả Về số kẻ dịch hiện tại
    public static Action<int> EnemySpawnEvent;
    //Trả Về số kẻ dịch hiện tại
    public static Action<int> EnemyDeadEvent;
    //Trả về wave hiện tại
    public static Action<int> WaveDoneEvent;
    //Trả về wave hiện tại
    public static Action<int> NextWaveEvent;
    //?.Invoke hàm này để skipshopping Time
    public static Action SkipShoppingEvent;
    //Trả về thời gian đếm ngoại của shoppingTimer
    public static Action<float> ShoppingTimeChangeEvent;
}