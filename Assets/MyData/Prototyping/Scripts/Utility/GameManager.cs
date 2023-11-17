using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public static bool EnabledMusic 
    {
        get => PlayerPrefs.GetInt(nameof(EnabledMusic), 1) == 1; 
        set => PlayerPrefs.SetInt(nameof(EnabledMusic), value ? 1 : 0); 
    }
    
    public static bool EnabledSFX 
    {
        get => PlayerPrefs.GetInt(nameof(EnabledSFX), 1) == 1;
        set => PlayerPrefs.SetInt(nameof(EnabledSFX), value ? 1 : 0);
    }

    public static bool IsGodMode { get; set; }
}
