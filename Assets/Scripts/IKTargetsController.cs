using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IKTargetsController : MonoBehaviour
{
    public Transform leftHandTarget;
    public Transform rightHandTarget;
    public void IKTargetsDefault(){
        rightHandTarget.transform.eulerAngles = new Vector3(-18.293f, 4.307f, 12.566f);
        rightHandTarget.transform.position = new Vector3(0.044f, -0.11f, -0.047f);
        leftHandTarget.transform.eulerAngles = new Vector3(35.091f, -18.14f, -261.433f);
        leftHandTarget.transform.position = new Vector3(0.134f, 0.019f, 0.105f);
    }
    public void IKTargetsAiming(){
        rightHandTarget.transform.eulerAngles = new Vector3(-0.714f, 15.956f, -9.107f);
        rightHandTarget.transform.position = new Vector3(0.121f, -0.016f, -0.108f);
        leftHandTarget.transform.eulerAngles = new Vector3(31.81f, -14.754f, -272.62f);    
        leftHandTarget.transform.position = new Vector3(0.102f, 0.214f, -0.028f);  
    }
}
