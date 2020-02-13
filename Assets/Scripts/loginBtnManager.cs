using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loginBtnManager : MonoBehaviour
{
    public GameObject SignUpBtn;
    public GameObject LoginBtn;
    public GameObject rightArrow;
    public GameObject SignupTxt;
    public GameObject dropDown;
    public GameObject roleTxt;


    public void signupActivate()
    {
        SignUpBtn.SetActive(true);
        LoginBtn.SetActive(false);
        rightArrow.SetActive(false);
        SignupTxt.SetActive(false);
        roleTxt.SetActive(true);
        dropDown.SetActive(true);
    }

    public void signupDeactivate()
    {
        SignUpBtn.SetActive(false);
        LoginBtn.SetActive(true);
        rightArrow.SetActive(true);
        SignupTxt.SetActive(true);
        roleTxt.SetActive(false);
        dropDown.SetActive(false);
    }
}
