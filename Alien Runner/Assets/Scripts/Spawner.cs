using UnityEngine;
using System.Collections;

/*
* AUTHOR: Harrison Hough   
* COPYRIGHT: Harrison Hough 
* VERSION: 1.0
* SCRIPT: Spawner Class
*/

namespace AlienRunner
{
    /// <summary>
    /// 
    /// </summary>
    public class Spawner : MonoBehaviour
    {

        public GameObject[] prefabs;
        private GameObject[] obstacles;
        private IRecyclable[] recyclables;
        private GameObject obstacleParent;
        [SerializeField]
        private Transform spawnPoint;

        public float delay = 2;
        public bool active = true;
        public Vector2 delayRange = new Vector2(1, 2);
        // Use this for initialization
        void Start()
        {
            obstacleParent = new GameObject("Obstacles");
            CreateObstacles();
            ResetDelay();
            StartCoroutine(EnemyGeneratoLoop());
        }

        private void ResetDelay()
        {
            delay = Random.Range(delayRange.x, delayRange.y);
        }

        void CreateObstacles()
        {
            recyclables = new IRecyclable[prefabs.Length];
            obstacles = new GameObject[prefabs.Length];

            for (int i = 0; i < obstacles.Length; i++)
            {
                obstacles[i] = Instantiate(prefabs[i], obstacleParent.transform);
                recyclables[i] = obstacles[i].GetComponent<IRecyclable>();
                recyclables[i].Shutdown();
            }
        }

        void SpawnObstacle(int index)
        {
            obstacles[index].transform.position = spawnPoint.position;
            obstacles[index].SetActive(true);
            recyclables[index].Restart();
        }

        public void DisableAllObstacles()
        {
            for (int i = 0; i < obstacles.Length; i++)
            {
                recyclables[i].Shutdown();
                obstacles[i].SetActive(false);
            }
        }

        IEnumerator EnemyGeneratoLoop()
        {
            yield return new WaitForSeconds(delay);
            if (active)
            {
                var newTransform = transform;
                SpawnObstacle(GetAvailablePrefab());
            }
            StartCoroutine(EnemyGeneratoLoop());
        }

        private int GetAvailablePrefab()
        {
            int index = Random.Range(0, obstacles.Length);
            while (obstacles[index].activeSelf)
            {
                index = Random.Range(0, obstacles.Length);
            }
            return index;
        }

        public GameObject SpawnObject(GameObject prefab)
        {
            GameObject go = Instantiate(prefab);
            return go;
        }
    }
}
