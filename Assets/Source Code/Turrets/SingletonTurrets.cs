using UnityEngine;

public class SingletonTurrets : MonoBehaviour
{
    private GameObject turret;
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
    public void SetTurretBuilding(GameObject tur)
    {
        this.turret = tur;
    }
    public GameObject InstantiateTurretsAt(Vector3 pos)
    {
        GameObject res = Instantiate(this.turret, pos, this.turret.transform.rotation);
        res.transform.SetParent(this.gameObject.transform, false);
        return res;
    }
}
