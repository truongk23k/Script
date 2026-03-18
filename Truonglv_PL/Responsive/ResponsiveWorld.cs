

using UnityEngine;

public class ResponsiveWorld : ResponsiveBase
{
  public Vector3 positionVertical;
  public Vector3 positionVerticalTall;
  public Vector3 positionHorizontal;
  public Vector3 positionTablet;

  public override void Start()
  {
    base.Start();
  }
  public override void OnHorizontal()
  {

    transform.localPosition = positionHorizontal;
  }
  public override void OnVerticalTall()
  {
    transform.localPosition = positionVerticalTall;
  }
  public override void OnVertical()
  {

    transform.localPosition = positionVertical;
  }

  public override void OnTablet()
  {

    transform.localPosition = positionTablet;
  }
}
