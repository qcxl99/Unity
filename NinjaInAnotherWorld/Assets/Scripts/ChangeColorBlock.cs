using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumColor
{
    white,
    red,
    blue,
    
}
public class ChangeColorBlock : MonoBehaviour
{
    EnumColor color;
    new SpriteRenderer renderer;

    public GameObject Item;
    public Transform ItemPoint;
    public GameObject TerrainDis;
    public GameObject OtherBlock;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(color != EnumColor.red || OtherBlock.GetComponent<ChangeColorBlock>().color != EnumColor.red)
        {
            TerrainDis.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Hit"))
        {
            switch (color)
            {
                case EnumColor.white:
                    {
                        renderer.color = Color.red;
                        color = EnumColor.red;
                        break;
                    }
                case EnumColor.red:
                    {
                        Vector4 blue = new Vector4(0, 0.5f, 1f, 1);
                        renderer.color = blue;
                        color = EnumColor.blue;
                        break;
                    }
                case EnumColor.blue:
                    {
                        renderer.color = Color.white;
                        color = EnumColor.white;
                        break;
                    }
            }
            if (color == EnumColor.red && color == OtherBlock.GetComponent<ChangeColorBlock>().color)
            {
                StartCoroutine(TerrainDisappear());
            }
            else if (color == EnumColor.blue && color == OtherBlock.GetComponent<ChangeColorBlock>().color)
            {
                TerrainDis.SetActive(true);
                Instantiate(Item, ItemPoint.position, Quaternion.identity);
            }

        }
    }
    IEnumerator TerrainDisappear()
    {
        yield return new WaitForSeconds(3);
        TerrainDis.SetActive(false);
/*        yield return new WaitForSeconds(3);
        TerrainDis.SetActive(true);*/
    }
    IEnumerator CreatItem()
    {
        yield return new WaitForSeconds(3);

    }
}
