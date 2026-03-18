

public static class PlayableAPI
{
  public static void GoToStore()
  {
    Luna.Unity.Playable.InstallFullGame();
  }

  public static void GameEnded()
  {
    Luna.Unity.LifeCycle.GameEnded();
  }

  public static void LogEventFailed()
  {
    Luna.Unity.Analytics.LogEvent(Luna.Unity.Analytics.EventType.LevelFailed);
  }

  public static void LogEventWin()
  {
    Luna.Unity.Analytics.LogEvent(Luna.Unity.Analytics.EventType.LevelWon);
  }

  public static void LogEventStart()
  {
    Luna.Unity.Analytics.LogEvent(Luna.Unity.Analytics.EventType.LevelStart);
  }

  public static void CustomEvent(string eventName, int Value)
  {
    Luna.Unity.Analytics.LogEvent(eventName, Value);
  }
}
