using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

//TODO reformat code
namespace Buildings
{
    public class SpawnTimer : MonoBehaviour
    {
        public static SpawnTimer instance;

        private UnitSpawnQueue spawnList;
        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            spawnList = gameObject.GetComponent<UnitSpawnQueue>();
        }
        public IEnumerator SpawnQueue()
        {
            Debug.Log(spawnList.spawningQueueTimer);
            if (spawnList.spawningQueueTimer.Count > 0)
            {
                Debug.Log($"Waiting for {spawnList.spawningQueueTimer[0]}");
                yield return new WaitForSeconds(spawnList.spawningQueueTimer[0]);
                Debug.Log($"Spawned");
                spawnList.Spawn();

                //ActionFrame.instance.spawnTMP.Remove(ActionFrame.instance.spawnTMP[0]);

                spawnList.spawnQueue.Remove(spawnList.spawnQueue[0]);
                spawnList.spawningQueueTimer.Remove(spawnList.spawningQueueTimer[0]);
                spawnList.spawnTypes.Remove(spawnList.spawnTypes[0]);

                if (spawnList.spawningQueueTimer.Count > 0)
                {
                    StartCoroutine(SpawnQueue());
                }
            }
        }
    }
}