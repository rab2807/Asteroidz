using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    private Rigidbody2D rb2d;
    private float minForceMagnitude = ConfigurationData.GetData().RockMinForceMagnitude;
    private float maxForceMagnitude = ConfigurationData.GetData().RockMaxForceMagnitude;
    private float minTorque = ConfigurationData.GetData().RockMinTorque;
    private float maxTorque = ConfigurationData.GetData().RockMaxTorque;
    private int type;
    private GamePlayMenu hud;

    protected void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        float angle = Random.Range(0, 2 * Mathf.PI);
        float mag = Random.Range(minForceMagnitude, maxForceMagnitude);
        rb2d.AddForce(mag * new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)), ForceMode2D.Impulse);
        rb2d.AddTorque(Random.Range(minTorque, maxTorque));
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];

        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<GamePlayMenu>();
    }

    public int Type
    {
        get => type;
        set => type = value;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CollisionWork(other.gameObject);
    }

    private void CollisionWork(GameObject obj)
    {
        ParticleRenderer.EmitParticles(gameObject);

        if (obj.GetComponent<Bullet>() != null)
        {
            AudioManager.Play(AudioName.Rock);
            hud.UpdateScore(4 - type);
            GameManager.ReturnBullet(obj);
            if (type == 1) SpawnTwoRocks(2);
            else if (type == 2)
                if (Random.Range(0, 1.0f) < 0.33f)
                    SpawnTwoRocks(3);
        }
        else if (obj.GetComponent<Ship>() != null)
        {
            AudioManager.Play(AudioName.GameOver);
            MenuManager.GoTo(MenuName.GameOver);
        }

        GameManager.ReturnRock(gameObject);
    }

    private void SpawnTwoRocks(int type)
    {
        GameObject r1 = RockSpawner.SpawnRock(type), r2 = RockSpawner.SpawnRock(type);
        r1.transform.position = transform.position;
        r2.transform.position = transform.position;

        void AddForce(GameObject r)
        {
            float magnitude = Random.Range(.7f, 2.0f);
            float angle = Random.Range(0, 2 * Mathf.PI);
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            r.GetComponent<Rigidbody2D>().AddForce(magnitude * direction, ForceMode2D.Impulse);
        }

        AddForce(r1);
        AddForce(r2);
    }
}