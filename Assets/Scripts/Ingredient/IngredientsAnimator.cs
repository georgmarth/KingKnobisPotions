using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsAnimator
{
    public Animator _animator;


    public void PlayDropingInCauldron() => _animator.SetTrigger("DropInCauldron");

}
