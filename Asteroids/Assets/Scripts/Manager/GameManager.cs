using System;
using UnityEngine;

public class GameManager : MonoBehaviour
    {
        private static ObjectPool bulletPool, rockPool;
        private int bulletPoolCapacity = ConfigurationData.GetData().BulletPoolCapacity;
        private int rockPoolCapacity = ConfigurationData.GetData().RockPoolCapacity;

        private void Awake()
        {
            ConfigurationData.GetData();
            ScreenData.Initialize();
            
            bulletPool = gameObject.AddComponent<ObjectPool>();
            bulletPool.Initialize(bulletPoolCapacity, "Bullet");

            rockPool = gameObject.AddComponent<ObjectPool>();
            rockPool.Initialize(rockPoolCapacity, "Rock");

            gameObject.AddComponent<ParticleRenderer>();

            GameObject ship = Resources.Load<GameObject>("Ship");
            Instantiate(ship, Vector3.zero, Quaternion.identity);
        }

        public static GameObject GetBullet()
        {
            return bulletPool.GetObject();
        }

        public static void ReturnBullet(GameObject bullet)
        {
            bulletPool.ReturnObject(bullet);
        }

        public static GameObject GetRock()
        {
            return rockPool.GetObject();
        }

        public static void ReturnRock(GameObject rock)
        {
            rock.transform.localScale = new Vector3(1, 1, 1);
            rockPool.ReturnObject(rock);
        }

        public void Pause()
        {
            MenuManager.GoTo(MenuName.Pause);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Escape))
                MenuManager.GoTo(MenuName.Pause);
        }
    }
