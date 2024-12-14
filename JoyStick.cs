using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : MonoBehaviour
{
    private static JoyStick instance;
    public static JoyStick Instant => instance;

    private Vector2 direction = Vector2.zero;
    public Vector2 DIR => direction.normalized;

    [SerializeField] Transform dot;
    Vector2? mousePos = null;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dot.localPosition = Vector2.zero;
            mousePos = null;
            direction = Vector2.zero;
        }

        if (Input.GetMouseButtonDown(0))
            this.transform.position = Input.mousePosition;

        if (!Input.GetMouseButton(0))
            return;
        mousePos = Input.mousePosition;
        if (mousePos == null)
            return;
        Debug.DrawLine(this.transform.position, (Vector3)mousePos, Color.green);
        direction = (Vector3)mousePos - this.transform.position;

        if (direction.magnitude <= 200)
        {
            dot.position = Input.mousePosition;
            return;
        }

        float angle = Mathf.Atan2(direction.y, direction.x);

        Vector2 pos;
        pos.x = Mathf.Cos(angle) * 200;
        pos.y = Mathf.Sin(angle) * 200;

        dot.localPosition = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 200);
    }
}
