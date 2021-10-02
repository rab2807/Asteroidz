using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = ConfigurationData.GetData().BulletSpeed;
    private float angle;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 position = transform.position;
        position.x += speed * Mathf.Cos(angle);
        position.y += speed * Mathf.Sin(angle);
        transform.position = position;
    }

    public void AddForce(float angle)
    {
        this.angle = angle;
    }

    private void OnBecameInvisible()
    {
        GameManager.ReturnBullet(gameObject);
    }
}