using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private GameObject[] objects;
    private GameObject[] prefabObjects;
    private int num;

    public void Initialize(int num, string name)
    {
        objects = new GameObject[num];
        prefabObjects = new GameObject[1];
        prefabObjects[0] = Resources.Load<GameObject>(name);
        for (int i = 0; i < num; i++)
            objects[i] = GetNewObject();
    }

    public void Initialize(int num, string[] names)
    {
        objects = new GameObject[num];
        prefabObjects = new GameObject[names.Length];
        for (int i = 0; i < prefabObjects.Length; i++)
            prefabObjects[i] = Resources.Load<GameObject>(name);
        for (int i = 0; i < num; i++)
        {
            objects[i] = GetNewObject();
        }
    }

    private GameObject GetNewObject()
    {
        GameObject obj = Instantiate(prefabObjects[Random.Range(0, prefabObjects.Length)], Vector3.up, Quaternion.identity);
        obj.SetActive(false);
        return obj;
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (!objects[i].activeSelf)
            {
                objects[i].SetActive(true);
                return objects[i];
            }
        }

        Debug.Log("not available");
        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}