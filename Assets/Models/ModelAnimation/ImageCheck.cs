using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCheck : MonoBehaviour
{
    private Animator mAnimator;
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (mAnimator != null)
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                mAnimator.SetTrigger("RGTrigger");
            }
        }*/
    }

    public void ArriveScaleUp()
    {
        //Trigger for red ginger
        mAnimator.SetTrigger("RGTrigger");
        mAnimator.ResetTrigger("RGReverse");

        //Trigger for fern
        mAnimator.SetTrigger("FTrigger");
        mAnimator.ResetTrigger("FReverse");

        //Trigger for tobacco
        mAnimator.SetTrigger("TTrigger");
        mAnimator.ResetTrigger("TReverse");

        //Trigger for pepper
        mAnimator.SetTrigger("PTrigger");
        mAnimator.ResetTrigger("PReverse");
    }
    
    public void ArriveScaleDown()
    {
        //Trigger for red ginger
        mAnimator.SetTrigger("RGReverse");
        mAnimator.ResetTrigger("RGTrigger");

        //Trigger for fern
        mAnimator.SetTrigger("FReverse");
        mAnimator.ResetTrigger("FTrigger");

        //Trigger for tobacco
        mAnimator.SetTrigger("TReverse");
        mAnimator.ResetTrigger("TTrigger");

        //Trigger for pepper
        mAnimator.SetTrigger("PReverse");
        mAnimator.ResetTrigger("PTrigger");
    }
}
