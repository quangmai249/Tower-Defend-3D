using UnityEngine;
public class GameStatsLevel : MonoBehaviour
{
    protected GameStats SetStatsLevel(string s)
    {
        switch (s)
        {
            case "LEVEL_1":
                return new GameStats(2000, 10, 1, 2);
            case "LEVEL_2":
                return new GameStats(2500, 15, 1, 3);
            case "LEVEL_3":
                return new GameStats(5000, 20, 1, 4);
            case "LEVEL_4":
                return new GameStats(10000, 100, 1, 3);
            case "LEVEL_5":
                return new GameStats(10000, 100, 1, 3);
            case "LEVEL_6":
                return new GameStats(10000, 100, 1, 3);
            case "LEVEL_7":
                return new GameStats(10000, 100, 1, 3);
            default:
                break;
        }
        return null;
    }
}
