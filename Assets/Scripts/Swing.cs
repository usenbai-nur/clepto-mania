using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] private int mouseIndex = 0;
    [SerializeField] private SpriteRenderer jewelryPlaceholder;
    private Animator handAnim;

    public bool handIsBusy;
    void Start()
    {
        handAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(mouseIndex))
        {
            //cooldown?
            handAnim.SetTrigger("ToSwing");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (handIsBusy)
            return;

        if (collision.gameObject.CompareTag("Jewelry"))
        {
            handIsBusy = true;
            jewelryPlaceholder.sprite = collision.gameObject.GetComponent<SpriteRenderer>().sprite;
            GameManager.instance.fillingBag(collision.gameObject.GetComponent<Jewelry>().cost);
            Destroy(collision.gameObject);
            //Steal
            //Destroy Obj
            //Increase cleptomania value in speccial bar, showing that chracter's desire is accomplished
            //maybe show floating text in corner of screen somwhere
            //Save data about object for the later work

        }
    }

    public void DeleteJewelry()
    {
        if (jewelryPlaceholder.sprite != null)
            jewelryPlaceholder.sprite = null;
        handIsBusy = false;
    }
}