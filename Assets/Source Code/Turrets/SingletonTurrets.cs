using UnityEngine;

public class SingletonTurrets : MonoBehaviour
{
    [SerializeField] GameObject turret;
    public static SingletonTurrets Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.name} is not single!");
            return;
        }
        Instance = this;
    }
    public GameObject InstantiateTurretsAt(GameObject location)
    {
        return Instantiate(this.turret, location.transform.position, this.turret.transform.rotation);
    }
}
