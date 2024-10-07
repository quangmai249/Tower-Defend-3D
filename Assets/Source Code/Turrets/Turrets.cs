using UnityEngine;

public class Turrets : MonoBehaviour
{
    [SerializeField] float priceTurrets = 100f;
    [SerializeField] float range = 6f;
    [SerializeField] GameObject nodeBuilding;

    private Renderer rend;
    private Color color;

    private SingletonBuilding singletonBuilding;
    private SingletonUpgradeTurrets singletonUpgradeTurrets;
    private GameManager gameManager;
    private UIManager uiManager;
    private void Awake()
    {
        singletonBuilding = SingletonBuilding.Instance;
        singletonUpgradeTurrets = SingletonUpgradeTurrets.Instance;
        gameManager = GameManager.Instance;
        uiManager = UIManager.Instance;
    }
    private void Start()
    {
        if (gameManager.GetGold() < GetPriceGameObject())
        {
            GameObject nodeBuilding = singletonBuilding.InstantiateAt(this.gameObject.transform.position);
            nodeBuilding.transform.parent = this.gameObject.transform.parent.transform;
            Destroy(this.gameObject);
            Debug.Log("Not enough money!");
            return;
        }
        else
        {
            gameManager.SetGold(-GetPriceGameObject());
        }

        this.rend = GetComponent<Renderer>();
        this.color = this.rend.material.color;
    }
    private void OnMouseEnter()
    {
        rend.material.color = Color.green;
    }
    private void OnMouseExit()
    {
        rend.material.color = this.color;
    }
    private void OnMouseDown()
    {
        singletonUpgradeTurrets.SetActiveShopTurrets(true, new Vector3(this.gameObject.transform.position.x, -2f, this.transform.position.z));
        return;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, range);
    }
    private float GetPriceGameObject()
    {
        return this.priceTurrets;
    }
}
