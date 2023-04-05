using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] float _camMoveSpeed;
    [SerializeField] float _camRotationSpeed;
    [SerializeField] Vector3 moveOffSet;
    [SerializeField] Vector3 RotationOffSet;
    [SerializeField] Transform camTarget;

    private void LateUpdate() {
        CamMovement();
        CamRotation();
    }
    void CamMovement(){
        Vector3 targetPos = new Vector3();
        targetPos = camTarget.TransformPoint(moveOffSet);

        transform.position = Vector3.Lerp(transform.position, targetPos, _camMoveSpeed);
    }
    void CamRotation(){
        var dir =  camTarget.position - transform.position;
        var rotation = new Quaternion();
        rotation = Quaternion.LookRotation(dir + RotationOffSet, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation,rotation,_camRotationSpeed );
    }
}
