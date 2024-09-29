using UnityEngine;

public class SingletonTurrets : MonoBehaviour
{
    [SerializeField] GameObject turret;
    public static SingletonTurrets Instance;
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
        return Instantiate(this.turret, pos, this.turret.transform.rotation);
    }
}
