using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationHandler : MonoBehaviour
{
   [SerializeField] private Animator _animator;

   public void StartMoveAnimation()
   {
      _animator.SetBool("isRunning",true);
   }

   public void StopMoveAnimation()
   {
      _animator.SetBool("isRunning",false);

   }

   public void GetDamageAnimation()
   {
      _animator.SetTrigger("GetDamage");
   }
}
