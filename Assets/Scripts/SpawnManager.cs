using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]    private GameObject _enemyPrefab;

    [SerializeField]    private GameObject _enemyContainer;
    [SerializeField]    private GameObject _powerUpContainer;

    [SerializeField]    private GameObject[] powerups;

    private bool _stopSpawning = false;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }


    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        while (_stopSpawning == false) //Пока Player не сообщит что _stopSpawning == true
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 6, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(5f);
        
        while (_stopSpawning == false) //Пока Player не сообщит что _stopSpawning == true
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8.5f, 8.5f), 6, 0);
            int randomPowerUp = Random.Range(0, 3); // !!!random integer number between min [inclusive] and max [EXCLUSIVE]!!!
            GameObject newPowerUp = Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            yield return new WaitForSeconds(Random.Range(3f, 8f));
        }
    }

    //Корутина теперь работает не бесконечно.
    //Её работу прерывает метод OnPlayerDeath (изменяет значение переменной bool), вызываемый из скрипта Player.
    //Player обращается SM через переменную, которой присваиваем значение SM.
    //Для этого ищем по "названию" объекта в юнити и достаём объект (скрипт SM) через GetComponent.

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

