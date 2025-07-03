using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 150;
    public int currentHealth;

    public TextMeshProUGUI vidaTexto;
    private bool isDead = false;

    public Transform spawnPoint; 
    private PlayerMovement movementScript;
    private CharacterController charController;
    private NavMeshAgent navAgent;

    private void Start()
    {
        currentHealth = maxHealth;
        ActualizarUI();

        movementScript = GetComponent<PlayerMovement>();
        charController = GetComponent<CharacterController>();
        navAgent = GetComponent<NavMeshAgent>();

        if (spawnPoint == null)
        {
            Debug.LogWarning("No hay spawnPoint asignado. Usando posición inicial del jugador.");
            spawnPoint = new GameObject("DefaultSpawnPoint").transform;
            spawnPoint.position = transform.position;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        ActualizarUI();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    void ActualizarUI()
    {
        if (vidaTexto != null)
            vidaTexto.text = "Vida: " + currentHealth;
    }

    void Die()
    {
        isDead = true;

        
        movementScript.enabled = false;

        
        if (navAgent != null)
        {
            navAgent.enabled = false;
        }

       
        if (charController != null)
        {
            charController.enabled = false;
        }

        
        Invoke("Respawn", 2f);
    }

    void Respawn()
    {
      
        
        transform.position = spawnPoint.position;

       
        if (charController != null)
        {
            charController.enabled = true;
        }

        if (navAgent != null)
        {
            navAgent.enabled = true;
            navAgent.Warp(spawnPoint.position); 
        }

        movementScript.enabled = true;

       
        currentHealth = maxHealth;
        ActualizarUI();
        isDead = false;
    }
}
