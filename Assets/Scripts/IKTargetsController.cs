using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IKTargetsController : MonoBehaviour
{
    /// <summary>
    /// 单例控制IKTargets
    /// </summary>
    private static IKTargetsController instance;

    // 获取单例实例的属性
    public static IKTargetsController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<IKTargetsController>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("IKTargetsController");
                    instance = singletonObject.AddComponent<IKTargetsController>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this; // 这里的this指代当前实例化的单例对象
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Ik 权重转换
    /// </summary>
    public Transform leftHandTarget;
    public Transform leftHandTargetAim;
    public Transform leftHandTargetIdle;
    public Transform leftHandTargetBack;
    public Transform rightHandTarget;
    public Transform rightHandTargetAim;
    public Transform rightHandTargetIdle;
    public Transform rightHandTargetBack;
    public void IKTargetsIdle(){   
        leftHandTarget.position = leftHandTargetIdle.position;
        leftHandTarget.rotation = leftHandTargetIdle.rotation;
        rightHandTarget.position = rightHandTargetIdle.position;
        rightHandTarget.rotation = rightHandTargetIdle.rotation;
    }
    public void IKTargetsAiming(){
        leftHandTarget.position = leftHandTargetAim.position;
        leftHandTarget.rotation = leftHandTargetAim.rotation;
        rightHandTarget.position = rightHandTargetAim.position;
        rightHandTarget.rotation = rightHandTargetAim.rotation;
    }
    public void IKTargetsBack(){
        leftHandTarget.position = leftHandTargetBack.position;
        leftHandTarget.rotation = leftHandTargetBack.rotation;
        rightHandTarget.position = rightHandTargetBack.position;
        rightHandTarget.rotation = rightHandTargetBack.rotation;
    }
}
