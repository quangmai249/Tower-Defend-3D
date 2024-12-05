public class EnemyStats
{
    private float enemyHP;
    private float enemySpeed;
    private float enemyRewardGold;
    public EnemyStats(float enemyHP, float enemySpeed, float enemyRewardGold)
    {
        this.EnemyHP = enemyHP;
        this.EnemySpeed = enemySpeed;
        this.EnemyRewardGold = enemyRewardGold;
    }

    public float EnemyHP { get => enemyHP; set => enemyHP = value; }
    public float EnemySpeed { get => enemySpeed; set => enemySpeed = value; }
    public float EnemyRewardGold { get => enemyRewardGold; set => enemyRewardGold = value; }
}
