using UnityEngine;

public class AttackSMB : StateMachineBehaviour
{
    // Reference to the SwordDamage script (assign in Inspector or auto-find)
    public SwordDamage swordDamage;

    // Called when entering the attack state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Auto-find the SwordDamage script if not assigned
        if (swordDamage == null)
            swordDamage = animator.GetComponentInChildren<SwordDamage>();
        
        if (swordDamage != null)
            swordDamage.EnableDamage();
    }

    // Called when exiting the attack state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (swordDamage != null)
            swordDamage.DisableDamage();
    }
}
