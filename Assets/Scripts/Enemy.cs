using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Silly
{
    public class Enemy : MonoBehaviour
    {
        [field: SerializeField]
        public float moveSpeed { get; set; } = 5f;
        public float attackSpeed { get; set; } = 2.0f;
        public float tempAttackSpeed { get; set; }
        public int damage { get; set; } = 10;
        public int hp { get; set; } = 30;
        public Transform Player { get; set; }

        

        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.Find("Player").transform;
            
        }

        // Update is called once per frame
        void Update()
        {
            if(Vector3.Distance(Player.position, this.transform.position) > 1)
            {
                Move();
            }
            else
            {
                if(tempAttackSpeed > attackSpeed)
                {
                    tempAttackSpeed = 0;
                    Player.GetComponent<Player>().Hit(damage);
                }
            }
        }

        void Move()
        {
            this.transform.Translate(Vector3.forward * moveSpeed * Time.smoothDeltaTime);
            this.transform.rotation = Quaternion.LookRotation(Player.position - this.transform.position);
        }

        public void Hit(int damage)
        {
            hp -= damage;

            if(hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
