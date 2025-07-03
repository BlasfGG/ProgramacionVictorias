using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EJERCICIO
/// 
/// 1.- La cantidad total de personajes que van a aparecer debe estar definida por la variable charactersPerRound, sin embargo la cantidad de personajes que puede haber en la escena
/// a la vez, esta limitada por la variable maxCharacterCountInScene
/// 
/// 2.- Cuando mates a un enemigo, si aun quedan personajes por aparecer durante esa ronda, que sigan apareciendo hasta llegar al limite definido por charactersPerRound.
/// 
/// 3.- Una vez hayan muerto la cantidad de personajes marcados en charactersPerRound, deben de iniciar otra ronda, cada vez sumandole 2 personajes más.
/// 
/// 4.- La primer ronda debe iniciar en  6 personajes.
/// </summary>
public class Spawn : MonoBehaviour
{

    [SerializeField] private int charactersPerRound; // Esta variable indica la cantidad máxima de enemigos durante una ronda. Es decir, para que acabe la ronda, tu debes de matar
    // a esta cantidad de enemigos.
    [SerializeField] private int charactersKilled; // Esta variable te revisa cuantos personajes ya spawneaste, de el total que va a haber en la ronda 

    [SerializeField] private GameObject characterToSpawn; // El personaje/enemigo a spawnear
    [SerializeField] private Transform[] spawnPoints; // Puntos de transform donde pueden aparecer los personajes

    [SerializeField] private int maxCharacterCountInScene; // Maximo de personajes ACTIVOS en la escena
    [SerializeField] private int maxCharacterInstancesInQueue; // Maximo de personajes DISPONIBLES para usarse

    [SerializeField] private float spawnRate; // Cada cuanto spawnean los personajes

    [SerializeField] Queue<GameObject> characterQueue; // Esta va a ser la fila de los personajes

    [SerializeField] int charactersInScene = 0; // Cantidad de personajes que estan activos en la escena

    //[SerializeField] private float characterLifeTime; // Variable que usamos de prueba, para que los personajes murireran cada x tiempo

    private void Start()
    {
        StartPool();
    }

    private void StartPool()
    {
        characterQueue = new Queue<GameObject>(); // Inicializamos la fila

        for (int i = 0; i < maxCharacterInstancesInQueue; i++)
        {
            GameObject instance = Instantiate(characterToSpawn); // Instancia los personajes que tendremos a disposicion
            instance.SetActive(false); // Desactiva
            characterQueue.Enqueue(instance); // Agrega a la fila
        }

        StartCoroutine(SpawnCharacters());
    }

    private IEnumerator SpawnCharacters()
    {                                    //      5                  5 
        yield return new WaitUntil(()=> charactersInScene < maxCharacterCountInScene);
        //                 0
        for (int i = charactersInScene; i < maxCharacterCountInScene; i++)
        {

                yield return new WaitForSeconds(spawnRate); // 3
                GameObject character = characterQueue.Dequeue(); // Sacamos a un personaje de la fila, y lo guardamos en una variable temporal
                character.SetActive(true);
                int randomSpawn = RandomSpawnPoint();
                character.transform.position = spawnPoints[randomSpawn].position;
                character.transform.rotation = spawnPoints[randomSpawn].rotation;
                charactersInScene++;
                //StartCoroutine(KillCharacter(character));    
        }

        StartCoroutine(SpawnCharacters());
    }

    private int RandomSpawnPoint()
    {
        return Random.Range(0, spawnPoints.Length); // 10 // 0,9
    }
    //private IEnumerator KillCharacter(GameObject characterToKill)
    //{
    //    yield return new WaitForSeconds(characterLifeTime); // 3
    //    characterToKill.SetActive(false);
    //    characterQueue.Enqueue(characterToKill);
    //    charactersInScene--;
    //}

    public void OnCharacterKilled(GameObject killedCharacter)
    {
        killedCharacter.SetActive(false);
        characterQueue.Enqueue(killedCharacter);
        charactersInScene--;
    }
    

}
