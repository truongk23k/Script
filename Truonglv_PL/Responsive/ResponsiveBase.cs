

using UnityEngine;

public abstract class ResponsiveBase : MonoBehaviour
{

  public virtual void Start()
  {
    ResizeScreen(ResponsiveManager.Instance.screenType);
    Initialize();
  }

  public virtual void Initialize()
  {
    GameEvent.OnResizeScreen.AddListener(ResizeScreen);
  }

  private void ResizeScreen(ScreenType screenType)
  {
    switch (screenType)
    {
      case ScreenType.Vertical:
        OnVertical();
        break;
      case ScreenType.VerticalTall:
        OnVerticalTall();
        break;
      case ScreenType.Tablet:
        OnTablet();
        break;
      case ScreenType.Horizontal:
        OnHorizontal();
        break;
    }
  }
  public abstract void OnHorizontal();
  public abstract void OnVertical();
  public abstract void OnVerticalTall();

  public abstract void OnTablet();


}


// public interface IResponsive
// {
//   public void Initialize();
//   public void OnHorizontal();
//   public void OnVertical();
//   public void OnTablet();
// }
