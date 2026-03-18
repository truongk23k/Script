

using UnityEngine;

public class CameraResponsive : MonoBehaviour
{
    public float orthoSizeV = 0;
    public float orthoSizeVTall = 0;

    public float orthoSizeH = 0;
    public float orthoSizeTab = 0;

    private void Start()
    {
        ResizeScreen(ResponsiveManager.Instance.screenType);
        GameEvent.OnResizeScreen.AddListener(ResizeScreen);
    }
    private void ResizeScreen(ScreenType screenType)
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null) return;

        switch (screenType)
        {
            case ScreenType.Vertical:
                mainCamera.orthographicSize = orthoSizeV;
                break;
            case ScreenType.VerticalTall:
                mainCamera.orthographicSize = orthoSizeVTall;
                break;
            case ScreenType.Tablet:
                mainCamera.orthographicSize = orthoSizeTab;
                break;
            case ScreenType.Horizontal:
                mainCamera.orthographicSize = orthoSizeH;
                break;
        }
    }

    private void OnDestroy()
    {
        GameEvent.OnResizeScreen.RemoveListener(ResizeScreen);
    }
}
