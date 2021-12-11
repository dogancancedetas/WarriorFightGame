using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public float health = 100;

    private float xDeath = -90;
    private float deathSmooth = 0.9f;
    private float rotateTime = 0.23f;
    private bool playerDied;
    public bool isPlayer;

    [SerializeField]
    private Image healthUI;
    [HideInInspector]
    public bool shieldActivated;
    private CharacterSoundFX soundFX;

    private void Awake()
    {
        soundFX = GetComponentInChildren<CharacterSoundFX>();
    }

    private void Update()
    {
        if (playerDied)
        {
            RotateAfterDeath();
        }    
    }

    public void ApplyDamage(float damage)
    {
        if (shieldActivated)
        {
            return;
        }

        health -= damage;

        if (healthUI != null)
        {
            healthUI.fillAmount = health / 100;
        }

        if (health <= 0)
        {
            soundFX.Die();
            GetComponent<Animator>().enabled = false;

            StartCoroutine(AllowRotate());

            if (isPlayer)
            {
                GetComponent<PlayerMove>().enabled = false;
                GetComponent<PlayerAttackInput>().enabled = false;

                //player is not the parent game object for the camera anymore
                Camera.main.transform.SetParent(null);

                GameObject.FindGameObjectWithTag(Tags.ENEMY_TAG).GetComponent<EnemyController>().enabled = false;
            }
            else
            {
                GetComponent<EnemyController>().enabled = false;
                GetComponent<NavMeshAgent>().enabled = false;
            }
        }
    }

    void RotateAfterDeath()
    {
        transform.eulerAngles = new Vector3(Mathf.Lerp(transform.eulerAngles.x, xDeath, Time.deltaTime * deathSmooth),
            transform.eulerAngles.y, transform.eulerAngles.z);
    }
    
    IEnumerator AllowRotate()
    {
        playerDied = true;

        yield return new WaitForSeconds(rotateTime);

        playerDied = false;
    }
}
