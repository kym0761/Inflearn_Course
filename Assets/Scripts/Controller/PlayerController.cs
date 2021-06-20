using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _Speed = 5.0f;
    [SerializeField]
    float _LookSpeed = 5.0f;


   // bool MoveToDest = false;
    Vector3 DestPos = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        //Managers.Input.KeyAction -= OnKeyBoard; // 1번만 적용되는 것을 보장하기 위해서 사용.
        //Managers.Input.KeyAction += OnKeyBoard;

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

       // Managers.Resource.InstantiateFromPath("UI/Canvas_Button");

    }

    // float ratio;

    public enum PlayerState
    {
        Idle,
        Moving,
        Die
    }

    PlayerState playerState = PlayerState.Idle;
    //float animationRatio = 0.0f;


    void UpdateIdle()
    {
        //Idle Animation
        //animationRatio = Mathf.Lerp(animationRatio, 0, 10.0f * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        //anim.SetFloat("Wait_Run_Ratio", animationRatio);
        //anim.Play("WAIT_RUN");

        anim.SetFloat("Speed", 0);
    }

    void UpdateMoving()
    {
        Vector3 dir = DestPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            playerState = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_Speed * Time.deltaTime, 0, dir.magnitude);

            transform.position += dir.normalized * moveDist;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _LookSpeed * Time.deltaTime);

            //transform.LookAt(DestPos);
        }


        //Moving Animation

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", _Speed);



    }

    void UpdateDie()
    { 
    
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerState)
        {
            case PlayerState.Idle:
                UpdateIdle();
                break;

            case PlayerState.Moving:
                UpdateMoving();
                break;

            case PlayerState.Die:
                UpdateDie();
                break;
        }
    }


    //void OnKeyBoard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), _LookSpeed * Time.deltaTime);

    //        transform.position += Vector3.forward * Time.deltaTime * _Speed;
    //    }

    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), _LookSpeed * Time.deltaTime);

    //        transform.position += Vector3.back * Time.deltaTime * _Speed;
    //    }

    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), _LookSpeed * Time.deltaTime);

    //        transform.position += Vector3.left * Time.deltaTime * _Speed;
    //    }

    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), _LookSpeed * Time.deltaTime);

    //        transform.position += Vector3.right * Time.deltaTime * _Speed;
    //    }


    //    MoveToDest = false;

    //}

    void OnMouseClicked(Define.MouseEvent moustevt) 
    {
        if(playerState == PlayerState.Die)
        {
            return;
        }


        Debug.Log("OnMouseClicked");


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        //int mask = (1 << 8) | (1<<9);
        LayerMask maskval = /*LayerMask.GetMask("Monster") |*/ LayerMask.GetMask("Floor");

        if (Physics.Raycast(ray, out hit, 100.0f, maskval))
        {
            DestPos = hit.point;
            playerState = PlayerState.Moving;

            //Debug.Log($"RayCast : {hit.collider.gameObject.name}");
        }

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);


    }

}
