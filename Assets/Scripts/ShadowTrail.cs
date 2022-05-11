using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTrail : MonoBehaviour
{
    //Eu tinha comentado esse script antes mas apaguei os comments e acabei salvando D:
    
    /*Script responsável pela caldinha do spectro, disponibilizei o link do video em <links de ajuda> e
    explico como esse script funciona no GDD*/

    public int length;
    public LineRenderer lineRend;
    public Vector3[] segmentPoses;
    Vector3[] segmentVelocity;
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    void Start()
    {
        lineRend.positionCount = length;
        segmentPoses = new Vector3[length];
        segmentVelocity = new Vector3[length];
    }
    void Update()
    {
        segmentPoses[0] = targetDir.position;

        for (int i= 1; i < segmentPoses.Length;i++){
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i-1] + 
            targetDir.right*targetDist, ref segmentVelocity[i], smoothSpeed);
        }
        lineRend.SetPositions(segmentPoses);
    }
}
