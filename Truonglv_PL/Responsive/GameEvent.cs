

using UnityEngine.Events;

public static class GameEvent
{
    private static UnityEvent<ScreenType> _onResizeScreen = new UnityEvent<ScreenType>();
    public static UnityEvent<ScreenType> OnResizeScreen => _onResizeScreen;

    private static UnityEvent _onUserFirstTouch = new UnityEvent();
    public static UnityEvent OnUserFirstTouch => _onUserFirstTouch;

    private static UnityEvent _onLoadedLevel = new UnityEvent();
    public static UnityEvent OnLoadedLevel => _onLoadedLevel;

    private static UnityEvent _onEndLevel = new UnityEvent();
    public static UnityEvent OnEndLevel => _onLoadedLevel;


}
