using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private Timer shootTimer;
    private Rigidbody2D rb2d;
    private float rotationSpeed = ConfigurationData.GetData().ShipRotationSpeed;
    private float forceMagnitude = ConfigurationData.GetData().ShipForceMagnitude;
    private float reverseForceMagnitude = .1f;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        shootTimer = gameObject.AddComponent<Timer>();
        shootTimer.TargetTime = 0.1f; // set a constant reload time
    }

    private bool frameFlag; // prevents multiple inputs taken in a single frame
    private bool shootFlag = true; // a slight time gap between consecutive shoots

    private void Update()
    {
        float input1 = Input.GetAxis("Horizontal");
        float input2 = Input.GetAxis("Vertical");
        float input3 = Input.GetAxis("Fire1");

        if (!frameFlag)
        {
            frameFlag = true;
            if (input1 < 0)
                gameObject.transform.Rotate(Vector3.back, -rotationSpeed);
            else if (input1 > 0)
                gameObject.transform.Rotate(Vector3.back, rotationSpeed);

            if (input2 != 0)
                AddForce(input2);

            if (input3 > 0 && shootFlag)
            {
                ReverseForce();
                Shoot(GameManager.GetBullet());
                shootFlag = false;
                shootTimer.ScheduleTask(() => { shootFlag = true; });
            }
        }
        else frameFlag = false;
    }

    private void AddForce(float input)
    {
        float angle = gameObject.transform.localEulerAngles.z * Mathf.PI / 180;
        rb2d.AddForce(input * forceMagnitude * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)), ForceMode2D.Force);
    }

    private void ReverseForce()
    {
        float angle = gameObject.transform.localEulerAngles.z * Mathf.PI / 180;
        rb2d.AddForce(-reverseForceMagnitude * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)), ForceMode2D.Impulse);
    }

    private void Shoot(GameObject bullet)
    {
        AudioManager.Play(AudioName.Shoot);

        Transform t = transform;
        bullet.transform.position = t.position;
        bullet.transform.rotation = t.rotation;
        bullet.GetComponent<Bullet>().AddForce(t.eulerAngles.z * Mathf.PI / 180);
    }
}