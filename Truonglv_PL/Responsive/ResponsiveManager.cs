

using UnityEngine;

public class ResponsiveManager : Singleton<ResponsiveManager>
{
    public ScreenType screenType = ScreenType.Vertical;

    private float widthScreen = 0;

    override protected void Awake()
    {
        base.Awake();
        UpdateScreenType();
    }

    private void Update()
    {
        if (Screen.width != widthScreen)
        {
            UpdateScreenType();
        }
    }
    private void UpdateScreenType()
    {
        widthScreen = Screen.width;
        float heightScreen = Screen.height;

        float aspectRatio = (float)widthScreen / heightScreen;
        if (aspectRatio <= 0.5f)
        {
            screenType = ScreenType.VerticalTall; // dọc dài (9:19.5, 10:21...)
        }
        else if (aspectRatio <= 0.7f)
        {
            screenType = ScreenType.Vertical; // dọc (9:16, 10:19...)
        }
        else if (aspectRatio > 0.7f && aspectRatio <= 1.6f)
        {
            screenType = ScreenType.Tablet; // 4:3, 3:2, các tỷ lệ trung gian
        }
        else
        {
            screenType = ScreenType.Horizontal; // 16:9, 21:9...
        }

        GameEvent.OnResizeScreen?.Invoke(screenType);
    }
}


public enum ScreenType
{
    Vertical,
    Tablet,
    Horizontal,
    VerticalTall
}
