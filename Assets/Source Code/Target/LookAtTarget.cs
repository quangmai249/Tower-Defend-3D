using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] string enemyTag = "Enemy";

    private GameManager gameManager;
    private TurretStats turretStats;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Start()
    {
        this.turretStats = this.gameObject.transform.GetComponent<Turrets>().GetTurretStats();
    }
    void Update()
    {
        this.turretStats = this.gameObject.GetComponent<Turrets>().GetTurretStats();
        this.target = SelectTarget.StartSelectTarget(this.gameObject.transform.position, turretStats.RangeTurret, this.enemyTag);

        if (this.target == null || gameManager.IsGameOver == true || gameManager.IsGameWinLevel == true)
        {
            return;
        }
        else
        {
            this.gameObject.transform.LookAt(this.target.transform.position);
        }
    }
    public GameObject GetTarget()
    {
        return this.target;
    }
}
