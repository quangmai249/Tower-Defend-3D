using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] GameObject target;

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
        this.target = SelectTarget.StartSelectTargetWithRange(this.gameObject.transform.position, turretStats.RangeTurret, GameObjectTagManager.TagEnemy);

        if (this.target == null || gameManager.IsGameOver == true || gameManager.IsGameWinLevel == true)
        {
            return;
        }
        else
        {
            this.gameObject.transform.LookAt(this.target.transform.position);
        }
    }
    public bool IsActiveEffects()
    {
        if (this.target == null || gameManager.IsGamePause == true || gameManager.IsGameOver == true || gameManager.IsGameWinLevel == true)
        {
            return false;
        }
        return true;
    }
    public GameObject GetTarget()
    {
        return this.target;
    }
}
