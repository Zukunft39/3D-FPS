using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
public class PlayerMoveControl : MonoBehaviour
{
    private Animator animator;
    private Vector2 playerInputVec;
    private bool isRunning;
    private bool isHandleGun;
    private bool isAiming;
    private Vector3 playerMovement;
    private Transform playerTransform; //替换transform优化代码运行效率

    private float rotateSpeed = 1000f;
    private float currentSpeed;
    private float targetSpeed;
    private float walkSpeed = 2.0f;
    private float runSpeed = 3.6f;
    
    private void Start() {
        animator = GetComponent<Animator>();
        playerTransform = transform;
    }
    private void Update(){
        RotatePlayer();
        MovePlayer();
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
    public void GetPlayerHandleGun(InputAction.CallbackContext context){
        if(isHandleGun == false) { 
            animator.SetBool("isHandleGun", true);
            isHandleGun = true; 
        }
        else { 
            animator.SetBool("isHandleGun", false);
            isHandleGun = false;
        }
    }
    public void GetPlayerAim(InputAction.CallbackContext context){
        if(context.action.phase == InputActionPhase.Started){
            animator.SetBool("isAiming", true);
            
        }
        else if(context.action.phase == InputActionPhase.Canceled){
            animator.SetBool("isAiming", false);
            
        }
    }
    public void GetPlayerFire(InputAction.CallbackContext context){
        if(context.action.phase == InputActionPhase.Performed){
            animator.SetBool("Fire",true);
        }
        else if(context.action.phase == InputActionPhase.Canceled){
            animator.SetBool("Fire",false);
        }
    }
    public void EnableRifle(EnableRifleType rifleType){
        switch(rifleType){
            case EnableRifleType.BackRifle:
                Debug.Log("激活背上的枪");
                break;
            case EnableRifleType.HandRifle: 
                Debug.Log("激活手上的枪");
                break;
        }
    }
    public enum EnableRifleType{
        BackRifle,
        HandRifle
    }
}
