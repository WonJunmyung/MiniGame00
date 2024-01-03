using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class GameData : MonoBehaviour
    {
        public int mapSize { get; set; } = 40;
        public int cornerInterval = 5;
        public int minInterval { get; set; } = 30;
        public int maxInterval { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            maxInterval = mapSize - cornerInterval;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
