using UnityEngine;

namespace PB.USER
{
    public class PPlayerPrefs
    {
        const string FIRST_LAUNCH = "first_launch";
        const string IS_TUTORIAL_SHOWED = "is_tutorial_showed";

        const string MUTE_SOUND = "mute_sound";
        const string MUTE_BGM = "mute_bgm";
        const string MUTE_SFX = "mute_sfx";


        public static bool FirstLaunch
        {
            get => Get(FIRST_LAUNCH, true);
            set => Set(FIRST_LAUNCH, value);
        }

        public static bool IsTutorialShowed
        {
            get => Get(IS_TUTORIAL_SHOWED, false);
            set => Set(IS_TUTORIAL_SHOWED, value);
        }


        public static bool MuteSound
        {
            get => Get(MUTE_SOUND, false);
            set => Set(MUTE_SOUND, value);
        }

        public static bool MuteBgm
        {
            get => Get(MUTE_BGM, false);
            set => Set(MUTE_BGM, value);
        }

        public static bool MuteSfx
        {
            get => Get(MUTE_SFX, false);
            set => Set(MUTE_SFX, value);
        }

        public static void ResetAll()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        #region Get and Set Methods
        private static void Set(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        private static void Set(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        private static void Set(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        private static void Set(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
            PlayerPrefs.Save();
        }

        private static int Get(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        private static float Get(string key, float defaultValue = 0f)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }

        private static string Get(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        private static bool Get(string key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key, defaultValue ? 1 : 0) == 1;
        }
        #endregion
    }
}


