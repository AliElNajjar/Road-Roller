using System;

public class EventManager : Singleton<EventManager>
{
    public Action<float> OnInputDetected;
}
