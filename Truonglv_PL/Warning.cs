
using UnityEngine;

public class Warning : MonoBehaviour
{

    private void Start()
    {
        //GameManager.OnFinishGame += HideTimer;
    }
    private void HideTimer()
    {
        gameObject.SetActive(false);
    }
}
