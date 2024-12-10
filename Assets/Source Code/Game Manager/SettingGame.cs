using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SettingGame : MonoBehaviour
{
    [Header("Toggle")]
    [SerializeField] Toggle toggleFullScreen;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI textVolumeMusic;
    [SerializeField] TextMeshProUGUI textVolumeFXSound;
    [SerializeField] GameObject panelSetting;

    [Header("Stats")]
    [SerializeField] float volumeMusic;
    [SerializeField] float volumeFXSound;
    [SerializeField] int resolutionScreenWidth;
    [SerializeField] int resolutionScreenHeight;

    private Setting setting;
    private void Start()
    {
        this.setting = JsonUtility.FromJson<Setting>(PlayerPrefs.GetString("Setting Game"));
        this.SetValueSetting();

        this.textVolumeMusic.text = $"{Mathf.Round(this.setting.volumeMusic * 100f).ToString()}";
        this.textVolumeFXSound.text = $"{Mathf.Round(this.setting.volumeFXSound * 100f).ToString()}";

        this.panelSetting.gameObject.SetActive(false);
    }
    public void ButtonSetting()
    {
        this.panelSetting.gameObject.SetActive(true);
        this.textVolumeMusic.text = $"{Mathf.Round(this.setting.volumeMusic * 100f).ToString()}";
        this.textVolumeFXSound.text = $"{Mathf.Round(this.setting.volumeFXSound * 100f).ToString()}";
    }
    public void ButtonSaveSetting()
    {
        this.setting = new Setting(this.toggleFullScreen.isOn, this.volumeMusic, this.volumeFXSound, this.resolutionScreenWidth, this.resolutionScreenHeight);
        PlayerPrefs.SetString("Setting Game", JsonUtility.ToJson(this.setting));
        PlayerPrefs.Save();
        this.SetValueSetting();
        this.panelSetting.gameObject.SetActive(false);
    }
    public void ButtonIncreaseVolumeMusic()
    {
        if (this.volumeMusic >= 1)
            this.volumeMusic = 1;
        else
            this.volumeMusic += 0.1f;
        this.textVolumeMusic.text = $"{Mathf.Round(this.volumeMusic * 100f).ToString()}";
    }
    public void ButtonDecreaseVolumeMusic()
    {
        if (this.volumeMusic < 0.1f)
            this.volumeMusic = 0;
        else
            this.volumeMusic -= 0.1f;
        this.textVolumeMusic.text = $"{Mathf.Round(this.volumeMusic * 100f).ToString()}";
    }
    public void ButtonIncreaseVolumeFXSound()
    {
        if (this.volumeFXSound >= 1)
            this.volumeFXSound = 1;
        else
            this.volumeFXSound += 0.1f;
        this.textVolumeFXSound.text = $"{Mathf.Round(this.volumeFXSound * 100f).ToString()}";
    }
    public void ButtonDecreaseVolumeFXSound()
    {
        if (this.volumeFXSound < 0.1f)
            this.volumeFXSound = 0;
        else
            this.volumeFXSound -= 0.1f;
        this.textVolumeFXSound.text = $"{Mathf.Round(this.volumeFXSound * 100f).ToString()}";
    }
    public void ButtonDefaultSetting()
    {
        this.setting = new Setting(true, 0.5f, 0.5f, 1920, 1080);
        PlayerPrefs.SetString("Setting Game", JsonUtility.ToJson(this.setting));
        PlayerPrefs.Save();
        this.SetValueSetting();
        this.panelSetting.gameObject.SetActive(false);
    }
    private void SetValueSetting()
    {
        this.toggleFullScreen.isOn = setting.isToggleFullScreen;

        this.volumeMusic = setting.volumeMusic;
        this.volumeFXSound = setting.volumeFXSound;

        this.resolutionScreenHeight = setting.resolutionScreenHeight;
        this.resolutionScreenWidth = setting.resolutionScreenWidth;
    }
}
