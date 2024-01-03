using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Camera : MonoBehaviour
    {
        public Transform Player { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.Find("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            this.transform.position = new Vector3(Player.position.x, this.transform.position.y, Player.position.z);
        }
    }
}
