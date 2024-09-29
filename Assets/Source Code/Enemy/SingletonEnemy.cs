using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    public static SingletonEnemy Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    public GameObject InstantiateTurretsAt(Vector3 pos)
    {
        return Instantiate(this.enemy, pos, this.enemy.transform.rotation);
    }
}
