﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SadBehavior : StateMachineBehaviour
{
    // Variables
    private Animator animator;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        this.animator = animator;
        GameManager.instance.actionOneButton.GetComponentInChildren<TextMeshProUGUI>().text = "Comfort";
        GameManager.instance.actionTwoButton.GetComponentInChildren<TextMeshProUGUI>().text = "Feed";
        GameManager.instance.actionThreeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Send to bed";

        GameManager.instance.virtualPet.ResetUI();
        GameManager.instance.actionOneButton.onClick.AddListener(Comfort);
        GameManager.instance.actionTwoButton.onClick.AddListener(Feed);
        GameManager.instance.actionThreeButton.onClick.AddListener(Sleep);
        GameManager.instance.pass.onClick.AddListener(Pass);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameManager.instance.virtualPet.affectionUI.color != Color.blue || GameManager.instance.virtualPet.affectionUI.color != Color.red)
        {
            GameManager.instance.virtualPet.affectionUI.color = Color.blue;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.instance.virtualPet.affectionUI.color = Color.white;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    void Feed()
    {
        // Lower Entertainment
        animator.SetFloat("entertainment", Mathf.Clamp(animator.GetFloat("entertainment") - 1, 0, 10));

        // Restore hunger
        animator.SetFloat("hunger", animator.GetFloat("hunger") + 4);

        // If over fed
        if (animator.GetFloat("hunger") > 10)
        {
            // Max hunger
            animator.SetFloat("hunger", 10);
            // Lower affection
            animator.SetFloat("affection", Mathf.Clamp(animator.GetFloat("affection") - 3, 0, 10));
        }
        else
        {
            GameManager.instance.virtualPet.Feed();
        }
        GameManager.instance.virtualPet.EndTurn();
    }

    void Comfort()
    {
        // Raise Entertainment
        animator.SetFloat("entertainment", Mathf.Clamp(animator.GetFloat("entertainment") + 3, 0, 10));

        // Raise Entertainment
        animator.SetFloat("affection", Mathf.Clamp(animator.GetFloat("affection") + 1, 0, 10));

        // Lower Energy
        animator.SetFloat("energy", animator.GetFloat("energy") - 2);

        GameManager.instance.virtualPet.EndTurn();
    }

    void Sleep()
    {
        // Lower Entertainment
        animator.SetFloat("entertainment", Mathf.Clamp(animator.GetFloat("entertainment") - 1, 0, 10));

        GameManager.instance.virtualPet.Sleep();
    }

    void Pass()
    {
        // Lower Entertainment
        animator.SetFloat("entertainment", Mathf.Clamp(animator.GetFloat("entertainment") - 3, 0, 10));

        // Lower Affection
        animator.SetFloat("affection", Mathf.Clamp(animator.GetFloat("affection") - 1, 0, 10));

        // Lower Energy
        animator.SetFloat("energy", animator.GetFloat("energy") - 1);
        GameManager.instance.virtualPet.EndTurn();
    }
}
