using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Silly
{
    public class Player : MonoBehaviour
    {

        const float half = 0.5f;

        [field: SerializeField, Header("����")]
        public Vector3 dir { get; set; }
        [field: SerializeField]
        public float moveSpeed { get; set; } = 5.0f;
        public int hp = 100;
        
        public float tempTextureUpdateTime { get; set; }
        [field: SerializeField, Header("�ִϸ��̼� ���� ����")]
        public float textureUpdateTime { get; set; } = 2.0f;
        public int textureNum { get; set; }
        [field: SerializeField, Header("������ �ִϸ��̼��� ���� �ؽ���")]
        public Texture2D[] textures { get; set; }

        public GameObject childPlayer { get; set; }

        public Vector3 centerVector { get; set; }
        public Vector3 lookVector { get; set; }
        public Vector3 lookDir { get; set; }
        public float tempFireTime { get; set; }
        public float fireTime { get; } = 0.1f;
        
        public Joystick leftJoystick { get; set; }
        public Joystick rightJoystick { get; set; }
        [field: SerializeField, Header("���⿡ �Ѿ� �������� �־��ּ���")]
        public GameObject bullet { get; set; }
        public Transform bulletPoint { get; set; }




        // Start is called before the first frame update
        void Start()
        {
            childPlayer = this.transform.Find("childPlayer").gameObject;
            bulletPoint = this.transform.Find("bulletPoint");
        }

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        void Move()
        {
            if(Application.platform != RuntimePlatform.Android)
            {
                if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    Debug.Log(dir);
                    AnimMove();
                }
                else
                {
                    dir = new Vector3(0, 0, 0);
                }
                // �߰����� ã��(�÷��̾��� ��ġ)
                centerVector = new Vector3(Screen.width * half, 0, Screen.height * half);
                // ���콺 ��ġ ��
                lookVector = Input.mousePosition;
                // �ٶ� ��ġ
                lookVector = new Vector3(lookVector.x, 0, lookVector.y);

                lookDir = lookVector - centerVector;
                Fire();
            }
            else
            {
                float joyX = leftJoystick.Horizontal;
                float joyY = leftJoystick.Vertical;

                if( joyX != 0 || joyY != 0 )
                {
                    dir = new Vector3(joyX, 0, joyY);
                    AnimMove();
                }
                else
                {
                    dir = new Vector3(0, 0, 0);
                }
            }

            this.transform.Translate(dir * moveSpeed * Time.smoothDeltaTime, Space.World);
            this.transform.rotation = Quaternion.LookRotation(lookDir);
        }

        // �����̴� �ִϸ��̼� Texture ����
        void AnimMove()
        {
            if(tempTextureUpdateTime > textureUpdateTime)
            {
                tempTextureUpdateTime = 0;

                textureNum++;

                if(textureNum > textures.Length - 1)
                {
                    textureNum = 0;
                }

                childPlayer.GetComponent<MeshRenderer>().material.mainTexture = textures[textureNum];
            }
            else
            {
                tempTextureUpdateTime += Time.deltaTime;
            }
        }

        void Fire()
        {
            if (Input.GetMouseButton(0))
            {
                if(tempFireTime > fireTime)
                {
                    tempFireTime = 0;
                    Debug.Log(bulletPoint);
                    Debug.Log(bullet);
                    GameObject tempBullet = Instantiate(bullet, bulletPoint.position, Quaternion.LookRotation(lookDir));
                }
                else if(tempFireTime < fireTime)
                {
                    tempFireTime += Time.deltaTime;
                }
            }
        }

        public void Hit(int damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                //Destroy(this.gameObject);
                Time.timeScale = 0;
            }
        }
    }
}