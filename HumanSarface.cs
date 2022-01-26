using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using DG.Tweening;

public class HumanSarface : MonoBehaviour
{
    #region All Types Of Character Movement Behavioures.......!!

    public void SimpleForowardMovementOfTheCharacter(GameObject character,float speed)
    {
        character.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    public void CharacterSwarvMovement(Touch touch, GameObject character, float swarvSpeed,float characterSpeed)
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                character.transform.position = new Vector3(
                        character.transform.position.x + touch.deltaPosition.x * swarvSpeed,
                        character.transform.position.y,
                        character.transform.position.z
                    );
            }
            SimpleForowardMovementOfTheCharacter(character,characterSpeed);
        }
    }
    public void CharacterButtonMovement(GameObject character,float speed)
    {
        //Use OnButtonDown Event Into The Editor.....
        SimpleForowardMovementOfTheCharacter(character,speed);
    }
    public void NPC__CharacterMovementTowardsMultiPositions(GameObject character,Transform[] target,int currentTargetNumber,float speed,Vector3 direction, Quaternion rotGoal, float rotationSpeed)
    {
       

        if (Vector3.Distance(transform.position, target[currentTargetNumber].position) >= 0.5f)
        {
            Vector3 pos = Vector3.MoveTowards(transform.position, target[currentTargetNumber].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
            direction = (target[currentTargetNumber].position) - transform.position;

            rotGoal = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, rotationSpeed * Time.deltaTime);
        }
        else currentTargetNumber = (currentTargetNumber + 1) % target.Length;
    }


    #region Top Down Camera Follower......




    //This Top Down Control Will Handeled By Control Freak2.... Have To Reacharch This .......
    public void TopDownCharacterMovement(GameObject character,float playerSpeed)
    {
        //float inputX = ControlFreak2.CF2Input.GetAxis("Horizontal") * playerSpeed;
        //float inputY = ControlFreak2.CF2Input.GetAxis("Vertical") * playerSpeed;
        //Vector3 direction = new Vector3(inputX, 0, inputY).normalized;
        //Vector3 lookAtPosition = character.transform.position + direction;
        //transform.LookAt(lookAtPosition);
        //character.transform.Translate(direction * playerSpeed, Space.World);
    }
    #endregion

    #endregion

    #region Wapone Controlling for Humanoide Characters........!!

    public void ShootWithAim()
    {

    }

    #endregion

    //public void PlayerEffector(GameObject player,int playerHeadSize)
    //{
    //    if(this.gameObject.tag)
    //}


}
