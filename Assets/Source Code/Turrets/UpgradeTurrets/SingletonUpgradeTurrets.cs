using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonUpgradeTurrets : MonoBehaviour
{
    public static SingletonUpgradeTurrets Instance;
    [SerializeField] GameObject upgradeTurret;
    private GameObject go;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
        this.go = Instantiate(this.upgradeTurret);
        this.go.gameObject.SetActive(true);
        this.go.transform.SetParent(this.gameObject.transform, false);
    }
    private void Start()
    {
        this.go.SetActive(false);
    }
    public void SetActiveShopTurrets(bool _b, Vector3 pos)
    {
        this.go.gameObject.transform.position = pos;
        this.go.SetActive(_b);
    }
}
