using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using UnityEditor.Animations;
public class PlayerMoveControl : MonoBehaviour
{
    private Animator animator;
    private Vector2 playerInputVec;
    private bool isRunning = false;
    private bool isAiming = false;
    private bool isHandleGun = false;
    private Vector3 playerMovement;
    private Transform playerTransform; //替换transform优化代码运行效率

    public int temp = 0;
    private float rotateSpeed = 1000f;
    private float currentSpeed;
    private float targetSpeed;
    private float walkSpeed = 2.0f;
    private float runSpeed = 3.6f;

    public GameObject rifleOnBack;
    public GameObject rifleInHand;

    public enum EnableRifleType{
        BackRifle,
        HandRifle
    }

    public AnimatorStateInfo animatorStateInfo;
    private void Start() {
        animator = GetComponent<Animator>();
        playerTransform = transform;
        rifleOnBack.SetActive(true);
        rifleInHand.SetActive(false);
    }
    private void Update(){
        RotatePlayer();
        MovePlayer();
        GetRifleIdle();
    }
    public void GetPlayerMoveInput(InputAction.CallbackContext context){
        playerInputVec = context.ReadValue<Vector2>();
    }
    public void GetPlayerRunInput(InputAction.CallbackContext context){
        isRunning = context.ReadValue<float>() > 0 ? true : false;
    }
    private void RotatePlayer(){
        if(playerInputVec.Equals(Vector2.zero))
            return;
        playerMovement.x = playerInputVec.x;
        playerMovement.z = playerInputVec.y;

        Quaternion targetRotation = Quaternion.LookRotation(playerMovement, Vector3.up);
        playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
    private void MovePlayer(){
        targetSpeed = isRunning ? runSpeed : walkSpeed;
        targetSpeed *= playerInputVec.magnitude;//摇杆没动时就默认不动
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 0.5f);
        animator.SetFloat("Vertical Speed", currentSpeed);
    }
    public void GetRifleIdle(){
        if(temp == 1){
            animatorStateInfo = animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Arms"));
            if(animatorStateInfo.IsName("Rifle Idle")){
                isHandleGun = true;
                animator.SetBool("isHandleGun",true);
            }
        }
        else if(temp == 0){
            isHandleGun = false;
            animator.SetBool("isHandleGun",false);
        }
    }
    public void GrabAndPutBackRifle(InputAction.CallbackContext context){
        temp = (temp + 1) % 2; // 判断是拿枪还是放回, 1为拿, 0为放
        if(temp == 1){
            animator.SetBool("isGrabRifle",true);
            animator.SetBool("isPutBackRifle",false);
            Debug.Log("拿出枪");
        }
        else if(temp == 0){
            animator.SetBool("isGrabRifle",false);
            animator.SetBool("isPutBackRifle",true);
            Debug.Log("放回背上");
        }
    }
    public void GetPlayerAim(InputAction.CallbackContext context){
        if(context.action.phase == InputActionPhase.Started){
            animator.SetBool("isAiming", true);
            isAiming = true;
            if(isAiming && isHandleGun) IKTargetsController.Instance.IKTargetsAiming();
        }
        else if(context.action.phase == InputActionPhase.Canceled){
            animator.SetBool("isAiming", false);
            isAiming = false;
            if(!isAiming && isHandleGun) IKTargetsController.Instance.IKTargetsIdle();
        }
    }
    public void EnableRifle(EnableRifleType rifleType){
        switch(rifleType){
            case EnableRifleType.BackRifle:
                rifleOnBack.SetActive(true);
                rifleInHand.SetActive(false);
                Debug.Log("激活背上的枪");
                break;
            case EnableRifleType.HandRifle: 
                rifleOnBack.SetActive(false);
                rifleInHand.SetActive(true);
                Debug.Log("激活手上的枪");
                break;
        }
    }
}
