using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode mode = Define.CameraMode.QuarterView;
    [SerializeField]
    Vector3 deltaVec = new Vector3(0,0,0);
    [SerializeField]
    GameObject playerCharacter = null;

    Vector3 Pivot = new Vector3(0.0f, 2.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (mode == Define.CameraMode.QuarterView)
        {



            RaycastHit hit;
            bool result = Physics.Raycast(playerCharacter.transform.position+ new Vector3(0.0f, 0.001f, 0.0f), deltaVec, out hit, deltaVec.magnitude, LayerMask.GetMask("Wall"));
            if (result)
            {
                Debug.Log($"Camera Raycast : {hit.collider.gameObject.name}");
                float dist = (hit.point - playerCharacter.transform.position).magnitude * 0.8f;
                transform.position = playerCharacter.transform.position + deltaVec.normalized * dist + Pivot;
            }
            else
            {
                transform.position = playerCharacter.transform.position + deltaVec + Pivot;
                transform.LookAt(playerCharacter.transform);
            }
        }



    }

    public void SetQuaterview(Vector3 delta)
    {

        mode = Define.CameraMode.QuarterView;
        deltaVec = new Vector3(0, 6.0f, -5.0f);
    }


}
