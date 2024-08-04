using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : NPCBaseFSM
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (opponent != null) 
        {
            var targetDirection = opponent.transform.position - NPC.transform.position;
            float lookAhead = targetDirection.magnitude / (movementSpeed + opponent.GetComponent<Drive>().speed);
            Vector3 lookPosition = (opponent.transform.position + opponent.transform.forward * lookAhead);
            var direction = NPC.transform.position - lookPosition;
            NPC.transform.rotation = Quaternion.Slerp(NPC.transform.rotation,
                                          Quaternion.LookRotation(direction),
                                          rotationSpeed * Time.deltaTime);
            NPC.transform.Translate(0.0f, 0.0f, Time.deltaTime * movementSpeed);
        }
    }
}
