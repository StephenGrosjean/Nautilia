using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Borders : MonoBehaviour
{

    [SerializeField] private Transform upBorder, downBorder, leftBorder, rightBorder;
    [SerializeField] private float offset;
    Vector2 topLeft, topRight, bottomLeft, bottomRight;
    private Vector2 screenSize;

    //Draw screen border
#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(topLeft, .5f);
        Gizmos.DrawWireSphere(topRight, .5f);
        Gizmos.DrawWireSphere(bottomLeft, .5f);
        Gizmos.DrawWireSphere(bottomRight, .5f);

    }
#endif

    //Set colliders position to stop bullets from escaping the screen (Save a lot of FPS)
    void Start()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        topLeft = new Vector2(-screenSize.x, screenSize.y);
        topRight = new Vector2(screenSize.x, screenSize.y);
        bottomLeft = new Vector2(-screenSize.x, -screenSize.y);
        bottomRight = new Vector2(screenSize.x, -screenSize.y);

        upBorder.transform.position = new Vector2(0, topLeft.y + offset);
        downBorder.transform.position = new Vector2(0, bottomLeft.y - offset);
        leftBorder.transform.position = new Vector2(topLeft.x - offset, 0);
        rightBorder.transform.position = new Vector2(topRight.x + offset, 0);
    }

}
