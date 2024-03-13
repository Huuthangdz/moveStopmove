using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : character
{
    [SerializeField] GameObject canvasDie;

    private CounterTime counter = new CounterTime();
    
    // Start is called before the first frame update
    void Start()
    {
        
        OnInit();
        changeAnim("Idile");
        switch (Random.Range(1, 5))
        {
            case 1: ChangeColor(ColorType.red); break;
            case 2: ChangeColor(ColorType.yellow); break;
            case 3: ChangeColor(ColorType.green); break;
            case 4: ChangeColor(ColorType.icon); break;

            default: Debug.Log("f"); break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeapon(PlayerPrefs.GetInt("Weapon"));
        PlayerPrefs.GetInt("Weapon");
        BulletPrefab = GameController.Ins.GetCurrentBullet(PlayerPrefs.GetInt("Weapon"));
        if (isdead == true)  
        {
            if ( Input.GetMouseButtonDown(0))
            {
                counter.Excute();
            }
            if (Input.GetMouseButton(0) && JoyStick.direct != Vector3.zero || Input.GetKey(KeyCode.UpArrow))
            {
                Vector3 nextPoint = transform.position + JoyStick.direct * speed * Time.deltaTime;

                if (CanMove(nextPoint)) changeAnim("Run");
                {
                    transform.position = checkGround(nextPoint);
                }
                if (JoyStick.direct != Vector3.zero)
                {
                    body.forward = JoyStick.direct;
                }
            }
            else
            {
                counter.Excute();
            }
            if (Input.GetMouseButtonUp(0))
            {
                character target = GetTargetInRange();
                if (target != null)
                {
                    changeAnim("attack");
                    OnAttack();
                    RemoveTaget(target);
                }
                else
                {
                    changeAnim("Idle");
                }
            }
        }
    }


    public override void OnAttack()
    {
        base.OnAttack();
        counter.Start(Throw, 0.2f);

    }
    public override void OnDead()
    {
        base.OnDead();
        GameController.Ins.PlayerDead();
        canvasDie.SetActive(true);
    }
    public override void OnInit()
    {
        skin.ChangeWeapon(PlayerPrefs.GetInt("weapon"));
        skin.ChangePant(PlayerPrefs.GetInt("Pants"));
        base.OnInit();
    }
}
