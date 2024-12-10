using UnityEngine;
public class Setting
{
    public bool isToggleFullScreen;
    public float volumeMusic;
    public float volumeFXSound;
    public Setting(bool isToggleFullScreen, float volumeMusic, float volumeFXSound)
    {
        this.isToggleFullScreen = isToggleFullScreen;
        this.volumeMusic = volumeMusic;
        this.volumeFXSound = volumeFXSound;
    }
}
