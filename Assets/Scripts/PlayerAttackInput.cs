using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackInput : MonoBehaviour
{
    private CharacterAnimations playerAnimation;
    public GameObject attackPoint;
    private PlayerShield shield;
    private CharacterSoundFX soundFX;

    void Awake()
    {
        playerAnimation = GetComponent<CharacterAnimations>();
        shield = GetComponent<PlayerShield>();
        soundFX = GetComponentInChildren<CharacterSoundFX>();
    }

    // Update is called once per frame
    void Update()
    {
        //Defend when J pressed
        if (Input.GetKeyDown(KeyCode.J))
        {
            playerAnimation.Defend(true);
            shield.ActivateShield(true);
        }

        //Release defence when j is released
        if (Input.GetKeyUp(KeyCode.J))
        {
            playerAnimation.UnFreezeAnimation();
            playerAnimation.Defend(false);
            shield.ActivateShield(false);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (Random.Range(0,2) > 0)
            {
                playerAnimation.Attack_1();
                soundFX.Attack1();
            }
            else
            {
                playerAnimation.Attack_2();
                soundFX.Attack2();
            }
        }
    }

    void ActivateAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void DeactivateAttackPoint()
    {
        if(attackPoint.activeInHierarchy)
        attackPoint.SetActive(false);
    }
}
