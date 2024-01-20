using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BottleHeight
{
    Short,
    Medium,
    Tall,
}

public enum BottleRadius
{
    Small,
    Medium,
    Large,
}

public enum BottleLid
{
    Pink,
    LightBrown,
    DarkBrown,
}

public enum LiquidAmount
{
    Quarter,
    Half,
    Full,
}

public enum BottleTag
{
    LightPurple,
    Purple,
    Blue,
}

public class Bottle : MonoBehaviour
{
    [SerializeField]
    private BottleHeight bottleHeight;
    [SerializeField] 
    private BottleRadius bottleRadius;
    [SerializeField]
    private BottleLid bottleLid;
    [SerializeField]
    private LiquidAmount liquidAmount;
    [SerializeField]
    private BottleTag bottleTag;

    #region Bottle SpriteRenderer
    [SerializeField]
    private SpriteRenderer bottleLidSR;
    [SerializeField]
    private SpriteRenderer waterAmountSR;
    [SerializeField]
    private SpriteRenderer bottleTagSR;

    #endregion

    #region Bottle Sprite

    [SerializeField]
    List<Sprite> bottleLidSprites;
    [SerializeField]
    List<Sprite> liquidAmountSprites;
    [SerializeField]
    List<Sprite> bottleTagSprites;

    #endregion

    [SerializeField]
    private ParticleSystem x;
    [SerializeField]
    private ParticleSystem o;

    public void SetBottleTag(BottleTag tag)
    {
        this.bottleTag = tag;
        this.bottleTagSR.sprite = bottleTagSprites[(int)tag];
     
    }

    public void SetBottleLid(BottleLid lid)
    {
        this.bottleLid = lid;
        this.bottleLidSR.sprite = bottleLidSprites[(int)lid];
    }

    public void SetLiquidAmount(LiquidAmount amount)
    {
        this.liquidAmount = amount;
        this.waterAmountSR.sprite = liquidAmountSprites[(int)amount];
    }
    
    public BottleHeight GetBottleHeight()
    {
        return bottleHeight;
    }

    public BottleRadius GetBottleRadius()
    {
        return bottleRadius;
    }

    public BottleLid GetBottleLid()
    {
        return bottleLid;
    }

    public LiquidAmount GetLiquidAmount()
    {
        return liquidAmount;
    }

    public BottleTag GetBottleTag()
    {
        return bottleTag;
    }

    public void Wrong()
    {
        x.Play();
    }

    public void Correct()
    {
        o.Play();
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.IsGameStart())
        {
            BottleOrderBy orderBy = GameManager.instance.GetGameMode();
            switch (orderBy)
            {
                case BottleOrderBy.HEIGHT:
                    {
                        for(int i = 0; i < this.transform.parent.childCount; i++)
                        {
                            if (GameManager.instance.GetOrderMode() == Order.Asc)
                            {
                                if ((int)bottleHeight > (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetBottleHeight())
                                {
                                    Wrong();
                                    return;
                                }
                            }
                            else if (GameManager.instance.GetOrderMode() == Order.Desc)
                            {
                                if ((int)bottleHeight < (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetBottleHeight())
                                {
                                    Wrong();
                                    return;
                                }
                            }
                        }
                        GameManager.instance.CheckIndex((int)bottleHeight, this.gameObject);
                        break;
                    }
                case BottleOrderBy.RADIUS:
                    {
                        for (int i = 0; i < this.transform.parent.childCount; i++)
                        {
                            if (GameManager.instance.GetOrderMode() == Order.Asc)
                            {
                                if ((int)bottleRadius > (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetBottleRadius())
                                {
                                    Wrong();
                                    return;
                                }
                            }
                            else if (GameManager.instance.GetOrderMode() == Order.Desc)
                            {
                                if ((int)bottleRadius < (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetBottleRadius())
                                {
                                    Wrong();
                                    return;
                                }
                            }
                        }
                        GameManager.instance.CheckIndex((int)bottleRadius, this.gameObject);
                        break;
                    }
                case BottleOrderBy.LID:
                    {
                        for (int i = 0; i < this.transform.parent.childCount; i++)
                        {
                            if (GameManager.instance.GetOrderMode() == Order.Asc)
                            {
                                if ((int)bottleLid > (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetBottleLid())
                                {
                                    Wrong();
                                    return;
                                }
                            }
                            else if (GameManager.instance.GetOrderMode() == Order.Desc)
                            {
                                if ((int)bottleLid < (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetBottleLid())
                                {
                                    Wrong();
                                    return;
                                }
                            }
                        }
                        GameManager.instance.CheckIndex((int)bottleLid, this.gameObject);
                        break;
                    }
                case BottleOrderBy.LIQUID:
                    {
                        for(int i = 0; i < this.transform.parent.childCount; i++)
                        {
                            if (GameManager.instance.GetOrderMode() == Order.Asc)
                            {
                                if ((int)liquidAmount > (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetLiquidAmount())
                                {
                                    Wrong();
                                    return;
                                }
                            }
                            else if (GameManager.instance.GetOrderMode() == Order.Desc)
                            {
                                if ((int)liquidAmount < (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetLiquidAmount())
                                {
                                    Wrong();
                                    return;
                                }
                            }
                        }
                        GameManager.instance.CheckIndex((int)liquidAmount, this.gameObject);
                        break;
                    }
                case BottleOrderBy.TAG:
                    {
                        for (int i = 0; i < this.transform.parent.childCount; i++)
                        {
                            if (GameManager.instance.GetOrderMode() == Order.Asc)
                            {
                                if ((int)bottleTag > (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetBottleTag())
                                {
                                    Wrong();
                                    return;
                                }
                            }else if (GameManager.instance.GetOrderMode() == Order.Desc)
                            {
                                if ((int)bottleTag < (int)this.transform.parent.GetChild(i).GetComponent<Bottle>().GetBottleTag())
                                {
                                    Wrong();
                                    return;
                                }
                            }
                        }
                        GameManager.instance.CheckIndex((int)bottleTag, this.gameObject);
                        break;
                    }
            }
        }
    }
}
