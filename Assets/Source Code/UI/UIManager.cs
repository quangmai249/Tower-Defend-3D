using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textGold;
    private GameManager gameManager;
    public static UIManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"{this.gameObject.name} is NOT SINGLE!");
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        textGold.text = $"$ {gameManager.GetGold().ToString()}";
    }
}
