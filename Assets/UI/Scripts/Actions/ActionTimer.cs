using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO reformat code
namespace UI
{
    public class ActionTimer : MonoBehaviour
    {
        public static ActionTimer instance;
        private void Awake()
        {
            instance = this;
        }
        public IEnumerator SpawnQueue()
        {
            if (ActionFrame.instance.spawningQueueTimer.Count > 0)
            {
                Debug.Log($"Waiting for {ActionFrame.instance.spawningQueueTimer[0]}");
                yield return new WaitForSeconds(ActionFrame.instance.spawningQueueTimer[0]);
                Debug.Log($"Spawned");
                ActionFrame.instance.Spawn();
                ActionFrame.instance.spawnQueue.Remove(ActionFrame.instance.spawnQueue[0]);
                ActionFrame.instance.spawningQueueTimer.Remove(ActionFrame.instance.spawningQueueTimer[0]);
                ActionFrame.instance.spawnTypes.Remove(ActionFrame.instance.spawnTypes[0]);

                if (ActionFrame.instance.spawningQueueTimer.Count > 0)
                {
                    StartCoroutine(SpawnQueue());
                }
            }
        }
    }
}