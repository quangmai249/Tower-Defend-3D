using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonShopTurrets : MonoBehaviour
{
    public static SingletonShopTurrets Instance;
    [SerializeField] GameObject shopTurrets;
    [SerializeField] float yPos = 2f;
    private GameObject go;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
        this.go = Instantiate(this.shopTurrets);
        this.go.gameObject.SetActive(true);
        this.go.transform.SetParent(this.gameObject.transform, false);
    }
    private void Start()
    {
        this.go.SetActive(false);
    }
    public void SetActiveShopTurrets(bool _b, Vector3 pos)
    {
        this.go.gameObject.transform.position = new Vector3(pos.x, yPos, pos.z);
        this.go.SetActive(_b);
    }
}
