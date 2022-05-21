using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private UnityEngine.UI.Image healthBar;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float playerHealth;
    private float damageElapsedTime;
    private float damageTimeDelay = 1.0f;
    private bool invincible = false;

    public enum damageSources { nothing = 0, lava = 20, bullet = 10 }

    void Awake()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<UnityEngine.UI.Image>();
        playerHealth = maxHealth;
    }

    private void Update()
    {
        damageElapsedTime += Time.deltaTime;
    }

    public void UpdateHealth(damageSources source)
    {
        if(invincible)
        {
            return;
        }
        switch (source)
        {
            case damageSources.nothing:
                break;
            case damageSources.lava:
                if (damageElapsedTime >= damageTimeDelay)
                {
                    playerHealth -= (int)damageSources.lava;
                    Debug.Log("Damage From Lava: -20% Health");
                    damageElapsedTime = 0;
                }
                break;
            case damageSources.bullet:
                if (damageElapsedTime >= damageTimeDelay)
                {
                    playerHealth -= (int)damageSources.bullet;
                    Debug.Log("Damage From Bullet: -10% Health");
                    damageElapsedTime = 0;
                }
                break;
        }

        if (playerHealth <= 0)
        {
            invincible = true;
            GameController.GameCon.Death();
        }

        healthBar.fillAmount = playerHealth / 100;
    }
 
    public void ResetMaxHealth()
    {
        playerHealth = maxHealth;
    }
}
