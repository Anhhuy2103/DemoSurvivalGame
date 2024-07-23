using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineManager : MonoBehaviour
{

    public GameObject newGameInTroCamera;
    public GameObject MeetNPCCamera;
    public PlayableDirector NewGameIntroTimeLine;
    public PlayableDirector FirstMeetNPCIntroTimeLine;
    public static TimeLineManager Instance { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        newGameInTroCamera.SetActive(false);
        MeetNPCCamera.SetActive(false);
    }
    public void NewGameTimeLine()
    {

        if (newGameInTroCamera != null)
        {
            newGameInTroCamera.gameObject.SetActive(true);
            NewGameIntroTimeLine.Play();
            StartCoroutine(DestroyTimeLine_newIntro());
        }
    }
    private IEnumerator DestroyTimeLine_newIntro()
    {
        yield return new WaitForSeconds(5f);
        newGameInTroCamera.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        Destroy(newGameInTroCamera);

    }

    public void FirstMeetNPCTimeLine()
    {
        if (MeetNPCCamera != null)
        {
            MeetNPCCamera.gameObject.SetActive(true);
            FirstMeetNPCIntroTimeLine.Play();
            StartCoroutine(DestroyTimeLine_FirstMeetIntro());
        }
    }
    private IEnumerator DestroyTimeLine_FirstMeetIntro()
    {
        yield return new WaitForSeconds(5f);
        MeetNPCCamera.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        Destroy(MeetNPCCamera);

    }
}
