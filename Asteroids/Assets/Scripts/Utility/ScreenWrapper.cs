using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    void OnBecameInvisible()
    {
        ScreenData.CheckScreenSizeChanged();

        Transform t = transform;
        float colliderRadius = gameObject.GetComponent<CircleCollider2D>().radius * t.localScale.x;
           // must take value according to transform scale, otherwise shows unexpected behavior
        float offset = .94f;
        Vector2 location = t.position;

        if (location.x < ScreenData.Left - colliderRadius)
            location.x = ScreenData.Right + colliderRadius * offset;
        else if (location.x > ScreenData.Right + colliderRadius)
            location.x = ScreenData.Left - colliderRadius * offset;
        if (location.y > ScreenData.Top + colliderRadius)
            location.y = ScreenData.Bottom - colliderRadius * offset;
        else if (location.y < ScreenData.Bottom - colliderRadius)
            location.y = ScreenData.Top + colliderRadius * offset;

        transform.position = location;
    }
}