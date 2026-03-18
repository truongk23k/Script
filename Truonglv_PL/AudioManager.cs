using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public List<AudioSource> musicSources;
    public List<AudioSource> sfxSources;

    [Header("Audio Clips")]
    public List<AudioClip> musicClips;
    public List<AudioClip> sfxClips;

    private int musicIndex = 0; // Lưu vị trí AudioSource tiếp theo cho Music
    private int sfxIndex = 0;   // Lưu vị trí AudioSource tiếp theo cho SFX

    [LunaPlaygroundField]
    public bool bgmOn = true;
    public GameObject srcBGM;

    private bool musicStarted = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

       
    }

    private void Start()
    {
        CheckBGM();
    }

    private void CheckBGM()
    {
        if (bgmOn)
        {
            if (srcBGM != null)
                srcBGM.SetActive(true);
        }
        else
        {
            if (srcBGM != null)
            {
                srcBGM.SetActive(false);
                var audioSrc = srcBGM.GetComponent<AudioSource>();
                audioSrc.volume = 0f;
                if (audioSrc != null && audioSrc.isPlaying)
                    audioSrc.Stop();
            }
        }
    }

    void Update()
    {
        if (!musicStarted && Input.anyKeyDown)
        {
            CheckBGM();
            musicStarted = true;
        }
    }

    public void PlayMusic(string clipName, bool loop = true)
    {
        AudioClip clip = musicClips.Find(c => c.name == clipName);
        if (clip == null)
        {
            Debug.LogWarning("Music clip không tồn tại: " + clipName);
            return;
        }

        AudioSource source = GetAvailableSource(musicSources, ref musicIndex);

        if (source == null) return;

        source.clip = clip;
        source.loop = loop;
        source.Play();
    }

    public void PlaySFX(string clipName, float timeDelayComplete)
    {
        StartCoroutine(PlaySFXC(clipName, timeDelayComplete));
    }

    IEnumerator PlaySFXC(string clipName, float timeDelayComplete)
    {
        AudioClip clip = sfxClips.Find(c => c.name == clipName);
        if (clip == null)
        {
            Debug.LogWarning("SFX clip không tồn tại: " + clipName);
            yield break;
        }

        yield return new WaitForSeconds(timeDelayComplete);

        AudioSource source = GetAvailableSource(sfxSources, ref sfxIndex);

        if (source == null) yield break;

        source.clip = clip;
        source.loop = false;
        source.Play();
    }

    public void PlaySFXWithTime(string clipName, float timeDelayClipStart, float time)
    {
        StartCoroutine(PlaySFXWithTimeC(clipName, timeDelayClipStart, time));
    }

    IEnumerator PlaySFXWithTimeC(string clipName, float timeDelayClipStart, float time)
    {
        AudioClip clip = sfxClips.Find(c => c.name == clipName);
        if (clip == null)
        {
            Debug.LogWarning("SFX clip không tồn tại: " + clipName);
            yield break;
        }

        yield return new WaitForSeconds(timeDelayClipStart);

        AudioSource source = GetAvailableSource(sfxSources, ref sfxIndex);

        if (source == null) yield break;

        source.clip = clip;
        source.loop = true;
        source.Play();

        // Nếu time <= 0 thì tự động lấy theo clip.length
        float waitTime = (time > 0) ? time : clip.length;

        yield return new WaitForSeconds(waitTime);

        // Chỉ stop nếu vẫn đang phát clip này
        if (source.clip == clip && source.isPlaying)
        {
            source.Stop();
        }
    }

    /// Lấy AudioSource rảnh, nếu không có thì chọn cái tiếp theo trong danh sách
    private AudioSource GetAvailableSource(List<AudioSource> sources, ref int index)
    {
        foreach (var src in sources)
        {
            if (!src.isPlaying)
                return src;
        }

        return null;
    }


    public void StopSFX(string sfxName, float timeDelayComplete = 0.1f)
    {
        StartCoroutine(StopSFXC(sfxName, timeDelayComplete));
    }

    IEnumerator StopSFXC(string sfxName, float timeDelayComplete)
    {
        yield return new WaitForSeconds(timeDelayComplete);
        foreach (var s in sfxSources)
        {
            if (s.isPlaying && s.clip != null && s.clip.name == sfxName)
            {
                Debug.Log(s.gameObject.name);
                s.clip = null;
                s.Stop();
            }
        }
    }


    /// Dừng toàn bộ nhạc nền
    public void StopBackgroundMusic()
    {
        foreach (var source in musicSources)
        {
            if (source.isPlaying)
                source.Stop();
        }
    }

    public void PauseAudio()
    {
        foreach (var source in musicSources)
        {
            if (source.isPlaying)
                source.Pause();
        }

        foreach (var s in sfxSources)
        {
            if (s.isPlaying)
                s.Pause();
        }
    }

    public void ResumeAudio()
    {
        foreach (var source in musicSources)
        {
            if (source != null && !source.isPlaying && source.time > 0f)
                source.UnPause();
        }

        foreach (var s in sfxSources)
        {
            if (s != null && !s.isPlaying && s.time > 0f)
                s.UnPause();
        }
    }

    public void PlaySFXIfNotPlaying(string clipName, float timeDelayComplete)
    {
        StartCoroutine(PlaySFXIfNotPlayingC(clipName, timeDelayComplete));
    }

    private IEnumerator PlaySFXIfNotPlayingC(string clipName, float timeDelayComplete)
    {
        AudioClip clip = sfxClips.Find(c => c.name == clipName);
        if (clip == null)
        {
            Debug.LogWarning("SFX clip không tồn tại: " + clipName);
            yield break;
        }

        yield return new WaitForSeconds(timeDelayComplete);

        // Nếu đã có source đang phát clip này thì bỏ qua, không phát lại
        foreach (var src in sfxSources)
        {
            if (src.isPlaying && src.clip == clip)
                yield break;
        }

        AudioSource source = GetAvailableSource(sfxSources, ref sfxIndex);
        if (source == null) yield break;

        source.clip = clip;
        source.loop = false;
        source.Play();
    }
}
