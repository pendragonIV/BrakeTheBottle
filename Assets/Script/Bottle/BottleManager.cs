using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BottleManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> bottlesPrefab;
    [SerializeField]
    private Transform bottleContainer;

    [SerializeField]
    List<GameObject> initialized;
    GameObject bottlePrefab;
    [SerializeField]
    private List<object> lidDecorations;
    [SerializeField]
    private List<object> tagDecorations;
    [SerializeField]
    private List<object> liquidDecorations;

    private void Awake()
    {
        initialized = new List<GameObject>();
        lidDecorations = new List<object>();
        tagDecorations = new List<object>();
        liquidDecorations = new List<object>();
    }

    public void DestroyAllBottle()
    {
        for(int i = bottleContainer.childCount - 1; i >=0; i--)
        {
            Destroy(bottleContainer.GetChild(i).gameObject);
        }
    }

    public bool IsAllBottleBreaked()
    {
        return bottleContainer.childCount == 0;
    }

    public void InitBottle(BottleOrderBy bottleOrderBy, int numberOfBottle)
    {
        float startPostionX = -3f;
        if (bottleOrderBy == BottleOrderBy.HEIGHT || bottleOrderBy == BottleOrderBy.RADIUS)
        {
            InitByBottle(startPostionX, numberOfBottle);
        }
        else
        {
            InitByDecoration(startPostionX, numberOfBottle);
        }

    }

    private void InitByBottle(float startPostionX, int numberOfBottle)
    {
        initialized.Clear();

        for (int i = 0; i < numberOfBottle; i++)
        {
            bottlePrefab = bottlesPrefab[UnityEngine.Random.Range(0, bottlesPrefab.Count)];

            initialized.ForEach(bottle =>
            {
                while(bottle == bottlePrefab)
                {
                    bottlePrefab = bottlesPrefab[UnityEngine.Random.Range(0, bottlesPrefab.Count)];
                }
            });
            
            initialized.Add(bottlePrefab);

            GameObject bottle = Instantiate(bottlePrefab, new Vector3(startPostionX, -2.7f, -3f), Quaternion.identity);
            bottle.transform.position= new Vector3(bottle.transform.position.x, bottle.transform.position.y + 1, bottle.transform.position.z);
            bottle.transform.DOMove(new Vector3(startPostionX, -2.7f, -3f), .2f).OnComplete(()=>
            {
                bottle.transform.localScale = new Vector3(1.2f, .8f, 1);
                bottle.transform.DOScale(new Vector3(1, 1, 1), .3f);
            });
            bottle.name = i.ToString();
            bottle.transform.SetParent(bottleContainer);
            startPostionX += 6f / (numberOfBottle - 1);
        }

        ChangeDecoration();
    }

    private void InitByDecoration(float startPostionX, int numberOfBottle)
    {
        bottlePrefab = bottlesPrefab[UnityEngine.Random.Range(0, bottlesPrefab.Count)];

        for (int i = 0; i < numberOfBottle; i++)
        {
            GameObject bottle = Instantiate(bottlePrefab, new Vector3(startPostionX, -2.7f, -3f), Quaternion.identity);
            bottle.transform.position = new Vector3(bottle.transform.position.x, bottle.transform.position.y + 1, bottle.transform.position.z);
            bottle.transform.DOMove(new Vector3(startPostionX, -2.7f, -3f), .2f).OnComplete(() =>
            {
                bottle.transform.localScale = new Vector3(1.2f, .8f, 1);
                bottle.transform.DOScale(new Vector3(1, 1, 1), .3f);
            });
            bottle.name = i.ToString();
            bottle.transform.SetParent(bottleContainer);
            startPostionX += 6f / (numberOfBottle - 1);
        }

        ChangeDecoration();
    }

    private void ChangeDecoration()
    {
        lidDecorations.Clear();
        tagDecorations.Clear();
        liquidDecorations.Clear();

        object decoration;
        foreach (Transform bottle in bottleContainer)
        {
            //LID
            decoration = UnityEngine.Random.Range(0, Enum.GetValues(typeof(BottleLid)).Length);
            lidDecorations.ForEach(deco =>
            {
                while ((BottleLid)decoration == (BottleLid)deco)
                {
                    decoration = UnityEngine.Random.Range(0, Enum.GetValues(typeof(BottleLid)).Length);
                }
            });
            lidDecorations.Add(decoration);
            bottle.GetComponent<Bottle>().SetBottleLid((BottleLid)decoration);

            //TAG
            decoration = (BottleTag)UnityEngine.Random.Range(0, Enum.GetValues(typeof(BottleTag)).Length);
            tagDecorations.ForEach(deco =>
            {
                while ((BottleTag)decoration == (BottleTag)deco)
                {
                    decoration = UnityEngine.Random.Range(0, Enum.GetValues(typeof(BottleTag)).Length);
                }
            });
            tagDecorations.Add(decoration);
            bottle.GetComponent<Bottle>().SetBottleTag((BottleTag)decoration);

            //LIQUID
            decoration = (LiquidAmount)UnityEngine.Random.Range(0, Enum.GetValues(typeof(LiquidAmount)).Length);
            liquidDecorations.ForEach(deco =>
            {
                while ((LiquidAmount)decoration == (LiquidAmount)deco)
                {
                    decoration = UnityEngine.Random.Range(0, Enum.GetValues(typeof(LiquidAmount)).Length);
                }
            });
            liquidDecorations.Add(decoration);
            bottle.GetComponent<Bottle>().SetLiquidAmount((LiquidAmount)decoration);
        }
    }


}
