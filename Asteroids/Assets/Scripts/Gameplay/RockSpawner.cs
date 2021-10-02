using UnityEngine;
using Random = UnityEngine.Random;

public class RockSpawner : MonoBehaviour
{
    private Timer timer;
    private int maxBigRockNum = ConfigurationData.GetData().MAXBigRockOnScreen;

    private void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        Generate();
        ThrowRock(SpawnRock(1));
        ThrowRock(SpawnRock(1));
    }

    private void Generate()
    {
        int bigRockNum = GameObject.FindGameObjectsWithTag("BigRock").Length;
        if (bigRockNum < maxBigRockNum)
            ThrowRock(SpawnRock(1));
        timer.ScheduleTask(Random.Range(3.0f, 5), Generate);
    }
    
    public static GameObject SpawnRock(int type)
    {
        GameObject rock = GameManager.GetRock();
        if (type == 1) rock.tag = "BigRock";
        
        float s;
        switch (type)
        {
            case 1:
                s = Random.Range(.8f, 1);
                break;
            case 2:
                s = Random.Range(.4f, .5f);
                break;
            default:
                s = Random.Range(.3f, .33f);
                break;
        }
        rock.GetComponent<Rock>().Type = type;

        rock.transform.localScale = new Vector3(s, s, 1);
        return rock;
    }

    private void ThrowRock(GameObject rock)
    {
        if (rock == null) return;

        float colRadius = rock.GetComponent<CircleCollider2D>().radius * rock.transform.localScale.x;
        float magnitude = Random.Range(.7f, 2.0f);
        float angle = Random.Range(0, 2 * Mathf.PI);

        int side = Random.Range(1, 5);
        switch (side)
        {
            case 1:
                ResetPosition(rock, ScreenData.Left - colRadius, Random.Range(ScreenData.Bottom, ScreenData.Top));
                break;
            case 2:
                ResetPosition(rock, ScreenData.Right + colRadius, Random.Range(ScreenData.Bottom, ScreenData.Top));
                break;
            case 3:
                ResetPosition(rock, Random.Range(ScreenData.Left, ScreenData.Right), ScreenData.Bottom - colRadius);
                break;
            default:
                ResetPosition(rock, Random.Range(ScreenData.Left, ScreenData.Right), ScreenData.Top + colRadius);
                break;
        }

        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        rock.GetComponent<Rigidbody2D>().AddForce(magnitude * direction, ForceMode2D.Impulse);
    }

    private void ResetPosition(GameObject rock, float x, float y)
    {
        Vector3 pos = rock.transform.position;
        pos.x = x;
        pos.y = y;
        rock.transform.position = pos;
    }
}