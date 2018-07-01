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
        [SerializeField]
        private GameObject[] _prefabs;
        [SerializeField]
        private GameObject[] _obstacles;
        private IRecyclable[] _recyclables;
        private GameObject _obstacleParent;
        [SerializeField]
        private Transform _spawnPoint;

        [SerializeField]
        private float _delay = 2;
        [SerializeField]
        private bool _active = true;
        public bool active { get { return _active; } set { _active = value; } }
        [SerializeField]
        private Vector2 _delayRange = new Vector2(1, 2);

        // Use this for initialization
        void Start()
        {
            _obstacleParent = new GameObject("Obstacles");
            CreateObstacles();
            ResetDelay();
            StartCoroutine(EnemyGeneratoLoop());
        }

        private void ResetDelay()
        {
            _delay = Random.Range(_delayRange.x, _delayRange.y);
        }

        void CreateObstacles()
        {
            _recyclables = new IRecyclable[_prefabs.Length];
            _obstacles = new GameObject[_prefabs.Length];

            for (int i = 0; i < _obstacles.Length; i++)
            {
                _obstacles[i] = Instantiate(_prefabs[i], _obstacleParent.transform);
                _recyclables[i] = _obstacles[i].GetComponent<IRecyclable>();
                _recyclables[i].Shutdown();
            }
        }

        void SpawnObstacle(int index)
        {
            _obstacles[index].transform.position = _spawnPoint.position;
            _obstacles[index].SetActive(true);
            _recyclables[index].Restart();
        }

        public void DisableAllObstacles()
        {
            for (int i = 0; i < _obstacles.Length; i++)
            {
                _recyclables[i].Shutdown();
                _obstacles[i].SetActive(false);
            }
        }

        IEnumerator EnemyGeneratoLoop()
        {
            yield return new WaitForSeconds(_delay);
            if (_active)
            {
                var newTransform = transform;
                SpawnObstacle(GetAvailablePrefab());
            }
            StartCoroutine(EnemyGeneratoLoop());
        }

        private int GetAvailablePrefab()
        {
            int index = Random.Range(0, _obstacles.Length);
            while (_obstacles[index].activeSelf)
            {
                index = Random.Range(0, _obstacles.Length);
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
