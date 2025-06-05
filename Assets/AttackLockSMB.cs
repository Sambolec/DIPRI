using UnityEngine;

public class AttackLockSMB : StateMachineBehaviour
{
    // Called when entering the attack state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var attack = animator.GetComponent<PlayerAttack>();
        if (attack != null && attack.movementScript != null)
            attack.movementScript.canMove = false;
    }

    // Called when exiting the attack state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var attack = animator.GetComponent<PlayerAttack>();
        if (attack != null && attack.movementScript != null)
            attack.movementScript.canMove = true;
    }
}
