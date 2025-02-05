using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SettingsHandling : MonoBehaviour
{
	[field: SerializeField] public Toggle FullScreen { get; private set; }
	[field: SerializeField] public TMP_Dropdown QualityDropdown { get; private set; }
	[field: SerializeField] public TMP_Dropdown ResolutionDropdown { get; private set; }
	[field: SerializeField] public TMP_Dropdown LanguageDropdown { get; private set; }
	public List<Resolution> InMenuResolutions { get; private set; }
	public double HighestRefreshRate { get; private set; }
	Resolution[] resolutions;
	private List<Locale> locales;
	private List<QualitySettings> qualitySettings;
	private void Awake()
	{
		//InMenuResolutions = new List<Resolution>();
		//resolutions = Screen.resolutions;
		//GetMaxRefresh();
		//SetUpResolutionSetting();
		SetUpLanguageSetting();

		QualityDropdown.value = QualitySettings.GetQualityLevel();
		QualityDropdown.onValueChanged.AddListener(ChangeGraphicsQuality);
	}

	private void OnDestroy()
	{
		QualityDropdown.onValueChanged.RemoveListener(ChangeGraphicsQuality);
	}
	private void SetUpResolutionSetting()
	{
		ResolutionDropdown.ClearOptions();
		List<string> options = new List<string>();
		int temp = 0;
		for (int i = 0; i < resolutions.Length; i++)
		{
			if (resolutions[i].refreshRateRatio.value != HighestRefreshRate) continue;
			string op = resolutions[i].width + " x " + resolutions[i].height;
			options.Add(op);
			InMenuResolutions.Add(resolutions[i]);
			if (resolutions[i].width == Screen.currentResolution.width &&
			    resolutions[i].height == Screen.currentResolution.height)
			{
				temp = i;
			}
		}
		ResolutionDropdown.AddOptions(options);
		ResolutionDropdown.RefreshShownValue();
		ResolutionDropdown.value = temp;
	}
	private async void SetUpLanguageSetting()
	{
		LanguageDropdown.ClearOptions();
		List<string> options = new List<string>();
		int temp = 0;
		await LocalizationSettings.InitializationOperation.Task;
		locales = LocalizationSettings.AvailableLocales.Locales;
		
		for (int i = 0; i < locales.Count; i++)
		{
			options.Add(locales[i].LocaleName);
			if (LocalizationSettings.SelectedLocale == locales[i])
			{
				temp = i;
			}
		}
		LanguageDropdown.AddOptions(options);
		LanguageDropdown.RefreshShownValue();
		LanguageDropdown.value = temp;
	}
	private void GetMaxRefresh()
	{
		foreach (var resolution in resolutions)
		{
			if (HighestRefreshRate < resolution.refreshRateRatio.value)
				HighestRefreshRate = resolution.refreshRateRatio.value;
		}
	}

	//Settings Functions
	public void ChangeGraphicsQuality(int value)
	{
		if (QualitySettings.GetQualityLevel() == value) return;
		QualitySettings.SetQualityLevel(value);
	}
	public void ChangeFullScreen(bool value)
	{
		if (Screen.fullScreen == value) return;
		Screen.fullScreen = value;
	}
	public void ChangeResolution(int value)
	{
		if (InMenuResolutions[value].width == Screen.currentResolution.width &&
				InMenuResolutions[value].height == Screen.currentResolution.height)
			return;
		Screen.SetResolution(InMenuResolutions[value].width, InMenuResolutions[value].height, Screen.fullScreenMode, InMenuResolutions[value].refreshRateRatio);
	}
	public async void ChangeLanguage(int value)
	{
		await LocalizationSettings.InitializationOperation.Task;
		if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.Locales[value]) return;
		LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[value];
	}
	public void ChangeSettings()
	{
		ChangeGraphicsQuality(QualityDropdown.value);
		ChangeLanguage(LanguageDropdown.value);
		//ChangeResolution(ResolutionDropdown.value);
		//ChangeFullScreen(FullScreen.isOn);
	}
}
