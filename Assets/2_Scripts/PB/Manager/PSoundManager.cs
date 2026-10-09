using System;
using System.Collections.Generic;
using UnityEngine;
using PB.SYSTEM;
using PB.UTILS;
using PB.USER;

namespace PB.MANAGER
{
    public class PSoundManager : PMonoSingleton<PSoundManager>
    {
        [Serializable]
        public class SFX
        {
            public string Key;
            [Range(0, 1)]
            public float Volume = 1f;
            public AudioClip Clip;
        }

        [Serializable]
        public class BGM
        {
            public string Key;
            [Range(0, 1)]
            public float Volume = 1f;
            public AudioClip Clip;
        }

        [Header("BGM")]
        [SerializeField] List<BGM> bgms;

        [Header("SFX")]
        [SerializeField] List<SFX> sfxs;

        [Header("Player")]
        private AudioSource _bgmPlayer;
        private AudioSource[] _sfxPlayers;

        private bool _isSoundMuted;
        private bool _isBgmMuted;
        private bool _isSfxMuted;

        protected override void Awake()
        {
            base.Awake();
            InitBGM();
            InitSFX();
        }

        private void Start()
        {
            _isSoundMuted = PPlayerPrefs.MuteSound;
            _isBgmMuted = PPlayerPrefs.MuteBgm;
            _isSfxMuted = PPlayerPrefs.MuteSfx;
        }
        public void MuteSound(bool mute)
        {
            MuteBgm(mute);
            MuteSfx(mute);
            _isSoundMuted = mute;
            PPlayerPrefs.MuteSound = mute;
            PLog.Log($"Sound muted: {mute}");
        }

        #region Initialize
        private void InitBGM()
        {
            var bgm = new GameObject("BgmPlayer");
            bgm.transform.SetParent(transform);
            _bgmPlayer = bgm.AddComponent<AudioSource>();
            _bgmPlayer.playOnAwake = false;
            _bgmPlayer.loop = true;
        }

        private void InitSFX()
        {
            var sfxPlayerCount = 10;
            _sfxPlayers = new AudioSource[sfxPlayerCount];
            for (int i = 0; i < sfxPlayerCount; i++)
            {
                var sfx = new GameObject($"SfxPlayer{i}");
                sfx.transform.SetParent(transform);
                _sfxPlayers[i] = sfx.AddComponent<AudioSource>();
                _sfxPlayers[i].playOnAwake = false;
                _sfxPlayers[i].loop = false;
            }
        }
        #endregion


        #region BGM
        public void PlayBgm(string key)
        {
            if (key == null || key == string.Empty)
            {
                PLog.Error("BGM key is null or empty.");
                return;
            }

            var bgm = bgms.Find(x => x.Key == key);
            if (bgm == null)
            {
                PLog.Error($"BGM with key '{key}' not found.");
                return;
            }

            _bgmPlayer.clip = bgm.Clip;
            _bgmPlayer.volume = bgm.Volume;
            _bgmPlayer.mute = _isSoundMuted || _isBgmMuted;
            _bgmPlayer.Play();
        }

        public void PlayBgm(AudioClip clip, float volume = 1f)
        {
            if (clip == null)
            {
                PLog.Error("BGM clip is null.");
                return;
            }

            _bgmPlayer.clip = clip;
            _bgmPlayer.volume = Mathf.Clamp01(volume);
            _bgmPlayer.mute = _isSoundMuted || _isBgmMuted;
            _bgmPlayer.Play();
        }

        public void StopBgm()
        {
            _bgmPlayer.Stop();
        }

        public void SetBgmVolume(float volume)
        {
            _bgmPlayer.volume = Mathf.Clamp01(volume);
        }

        public void MuteBgm(bool mute)
        {
            _bgmPlayer.mute = mute;
            _isBgmMuted = mute;
            PPlayerPrefs.MuteBgm = mute;
            PLog.Log($"BGM muted: {mute}");
        }
        #endregion


        #region SFX
        public void PlaySfx(string key)
        {
            if (key == null || key == string.Empty)
            {
                PLog.Error("SFX key is null or empty.");
                return;
            }

            SFX sfx = null;
            sfx = sfxs.Find(x => x.Key == key);
            if (sfx == null)
            {
                PLog.Error($"SFX with key '{key}' not found.");
                return;
            }

            foreach (var sfxPlayer in _sfxPlayers)
            {
                if (!sfxPlayer.isPlaying)
                {
                    sfxPlayer.clip = sfx.Clip;
                    sfxPlayer.volume = sfx.Volume;
                    sfxPlayer.mute = _isSoundMuted || _isSfxMuted;
                    sfxPlayer.Play();
                    return;
                }
            }
        }

        public void PlaySfx(AudioClip clip, float volume = 1f)
        {
            if (clip == null)
            {
                PLog.Error("SFX clip is null.");
                return;
            }

            foreach (var sfxPlayer in _sfxPlayers)
            {
                if (!sfxPlayer.isPlaying)
                {
                    sfxPlayer.clip = clip;
                    sfxPlayer.volume = Mathf.Clamp01(volume);
                    sfxPlayer.mute = _isSoundMuted || _isSfxMuted;
                    sfxPlayer.Play();
                    return;
                }
            }
        }

        public void SetSfxVolume(float volume)
        {
            foreach (var sfxPlayer in _sfxPlayers)
            {
                sfxPlayer.volume = Mathf.Clamp01(volume);
            }
        }

        public void MuteSfx(bool mute)
        {
            foreach (var sfxPlayer in _sfxPlayers)
            {
                sfxPlayer.mute = mute;
            }

            _isSfxMuted = mute;
            PPlayerPrefs.MuteSfx = mute;
            PLog.Log($"SFX muted: {mute}");
        }
        #endregion
    }
}
