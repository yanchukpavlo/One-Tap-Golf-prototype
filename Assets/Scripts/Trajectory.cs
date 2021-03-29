using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField] int dotsNumber;
    [SerializeField] Transform donsParent;
    [SerializeField] GameObject dotsPrefab;
    [SerializeField] float distanceBetween;

    Transform[] dots;
    float timeStamp;
    Vector2 tempPos;

    void Start()
    {
        Hide();
        InitializeDots();
    }

    void InitializeDots()
    {
        dots = new Transform[dotsNumber];
        for (int i = 0; i < dotsNumber; i++)
        {
            dots[i] = Instantiate(dotsPrefab, null).transform;
            dots[i].parent = donsParent.transform;
        }
    }

    public void UpdateDots(Vector3 ballPos, float force)
    {
        timeStamp = distanceBetween;
        for (int i = 0; i < dotsNumber; i++)
        {
            tempPos.x = (ballPos.x + force * timeStamp);
            tempPos.y = (ballPos.y + force * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2;
            
            dots[i].position = tempPos;
            timeStamp += distanceBetween;
        }
    }

    public void Show()
    {
        donsParent.gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        donsParent.gameObject.SetActive(false);
    }
}
