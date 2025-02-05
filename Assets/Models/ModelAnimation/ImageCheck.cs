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

        //Trigger for aloe vera
        mAnimator.SetTrigger("AVTrigger");
        mAnimator.ResetTrigger("AVReverse");

        //Trigger for turmeric
        mAnimator.SetTrigger("TurTrigger");
        mAnimator.ResetTrigger("TurReverse");

        //Trigger for mahkota dewa
        mAnimator.SetTrigger("MDTrigger");
        mAnimator.ResetTrigger("MDReverse");

        //Trigger for sirih
        mAnimator.SetTrigger("SirTrigger");
        mAnimator.ResetTrigger("SirReverse");
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

        //Trigger for aloe vera
        mAnimator.SetTrigger("AVReverse");
        mAnimator.ResetTrigger("AVTrigger");

        //Trigger for turmeric
        mAnimator.SetTrigger("TurReverse");
        mAnimator.ResetTrigger("TurTrigger");

        //Trigger for mahkota dewa
        mAnimator.SetTrigger("MDReverse");
        mAnimator.ResetTrigger("MDTrigger");

        //Trigger for sirih
        mAnimator.SetTrigger("SirReverse");
        mAnimator.ResetTrigger("SirTrigger");
    }
}
