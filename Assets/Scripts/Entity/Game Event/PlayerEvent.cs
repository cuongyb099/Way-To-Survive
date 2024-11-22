using System;

public static class PlayerEvent
{
    public static Action<float, float> OnHeathChange;
    public static Action<float, float> OnMaxHeathChange;
    public static Action<float, float> OnMaxManaChange;
    public static Action<float, float> OnManaChange;
	public static Action<AttributeType, float ,float> OnInitStatusBar;
}
