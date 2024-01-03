using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class EnemyManager : MonoBehaviour
    {
        public Vector3 responsePos { get; set; }
        public float startTime { get; set; } = 5.0f;
        [field: SerializeField, Header("적이 리스폰될 시간")]
        public float responseTime { get; set; } = 2.0f;
        public float tempResponseTime { get; set; }
        [field: SerializeField, Header("여기에 리스폰할 적을 넣어주세요")]
        public GameObject Enemy { get; set; }
        public GameData gameData { get; set; }
        

        // Start is called before the first frame update
        void Start()
        {
            gameData = this.GetComponent<GameData>();
            
        }

        // Update is called once per frame
        void Update()
        {
            if (tempResponseTime > responseTime)
            {
                tempResponseTime = 0;
                CreateEnemy();
            }
            else
            {
                tempResponseTime += Time.deltaTime;
            }

        }

        void CreateEnemy()
        {
            
            int x = Random.Range(gameData.minInterval, gameData.maxInterval);
            int z = Random.Range(gameData.minInterval, gameData.maxInterval);

            if(Random.Range(0,2) == 0)
            {
                x = -x;
            }
            if(Random.Range(0,2) == 0)
            {
                z = -z;
            }

            Vector3 pos = new Vector3(x, 0, z);
            Instantiate(Enemy, pos, Quaternion.identity);
        }
    }
}
