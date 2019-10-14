using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameConroller : MonoBehaviour
{
    [SerializeField]
    private GameObject _car,_ballsPrefab,_rowsPrefab;
    public GameObject _rePlayPage;
    public CameraFollow cameraFollow;
    public BoolenOperation boolenOperation;
    private CarController carController;
    private bool gamaEndControl;
    private GameObject balls, rows;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        carController = _car.GetComponent<CarController>();
        rows = GameObject.Find("Rows");
        balls = GameObject.Find("Balls");
    }

    // Update is called once per frame
    void Update()
    {
        if (carController.counter >= 4 && !gamaEndControl)
            StartCoroutine(GameEnd());
    }

    public void closeRePlayPage()
    {
        _rePlayPage.SetActive(false);
    }

    public void rePlayPageOpen()
    {
        _rePlayPage.SetActive(true);
    }

    void resetObjects()
    {
        rows = GameObject.Find("Rows");
        balls = GameObject.Find("Balls");
    }

    public void rePlayGame()
    {
        // Toplar ve Satır objeleri tekrar kolanıyor.
        GameObject ballsClone = Instantiate(_ballsPrefab, balls.transform.position, balls.transform.rotation);
        GameObject rowsClone =  Instantiate(_rowsPrefab, rows.transform.position, rows.transform.rotation);
        rowsClone.transform.SetParent(rows.transform.parent);
        rowsClone.name = rows.name;
        ballsClone.name = balls.name;
        boolenOperation.SendMessage("rowsAttach",rowsClone.transform);
        cameraFollow.SendMessage("ballAttach",ballsClone.transform);
        rows.SetActive(false);
        balls.SetActive(false);
        _rePlayPage.SetActive(false);
        gamaEndControl = false;
        carController.counter = 0;
        resetObjects();
    }


    IEnumerator GameEnd()
    {
        gamaEndControl = true;
        yield return new WaitForSeconds(2);
        rePlayPageOpen();
    }
}
