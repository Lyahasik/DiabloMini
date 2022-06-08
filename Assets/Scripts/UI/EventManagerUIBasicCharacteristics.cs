using System;

public class EventManagerUIBasicCharacteristics
{
    public static Action<int> OnReduceHealth;
    public static Action<int> OnIncreaseRage;
    public static Action<int> OnReduceRage;

    public static Action OnBufActivate;
    public static Action OnBufDeactivate;

    public static Action OnDiePlayer;

    public static void ReduceHealth(int value)
    {
        OnReduceHealth?.Invoke(value);
    }

    public static void IncreaseRage(int value)
    {
        OnIncreaseRage?.Invoke(value);
    }

    public static void ReduceRage(int value)
    {
        OnReduceRage?.Invoke(value);
    }

    public static void BufActivate()
    {
        OnBufActivate?.Invoke();
    }

    public static void BufDeactivate()
    {
        OnBufDeactivate?.Invoke();
    }

    public static void DiePlayer()
    {
        OnDiePlayer?.Invoke();
    }
}
