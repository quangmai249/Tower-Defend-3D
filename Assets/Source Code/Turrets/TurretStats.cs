using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurretStats
{
    private int levelTurret;
    private float priceTurret;
    private float priceUpgradeTurret;
    private float priceSellTurret;
    private float rangeTurret;
    private float damagedTurret;
    private float rateTurret;
    public TurretStats(float priceTurret, float priceUpgradeTurret, float priceSellTurret, float rangeTurret, float damagedTurret, int levelTurret, float rateTurret)
    {
        this.priceTurret = priceTurret;
        this.priceUpgradeTurret = priceUpgradeTurret;
        this.priceSellTurret = priceSellTurret;
        this.rangeTurret = rangeTurret;
        this.damagedTurret = damagedTurret;
        this.levelTurret = levelTurret;
        this.rateTurret = rateTurret;
    }
    public float PriceTurret { get => priceTurret; set => priceTurret = value; }
    public float PriceUpgradeTurret { get => priceUpgradeTurret; set => priceUpgradeTurret = value; }
    public float PriceSellTurret { get => priceSellTurret; set => priceSellTurret = value; }
    public float RangeTurret { get => rangeTurret; set => rangeTurret = value; }
    public float DamagedTurret { get => damagedTurret; set => damagedTurret = value; }
    public int LevelTurret { get => levelTurret; set => levelTurret = value; }
    public float RateTurret { get => rateTurret; set => rateTurret = value; }
}
