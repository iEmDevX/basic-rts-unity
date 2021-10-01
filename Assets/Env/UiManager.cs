using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject skill1UI;
    [SerializeField] GameObject skill2UI;

    private CharacterController cerrentUnit;

    Color activeColor = Color.yellow;
    Color unactiveColor = Color.gray;

    public void SetCerrent(CharacterController gameObject)
    {
        cerrentUnit = gameObject;
        SetUI();
    }

    private void SetUI()
    {
        // Skill1
        skill1UI.transform.Find("Image").GetComponent<Image>().sprite = cerrentUnit.skill1.skillImg;
        skill1UI.GetComponentInChildren<Text>().text = cerrentUnit.skill1.useMana.ToString();

        // Skill2
        skill2UI.transform.Find("Image").GetComponent<Image>().sprite = cerrentUnit.skill2.skillImg;
        skill2UI.GetComponentInChildren<Text>().text = cerrentUnit.skill2.useMana.ToString();

        activeColor.a = 0.4f;
        unactiveColor.a = 0.5f;
    }

    public void onClickSkill1()
    {
        cerrentUnit.UseSkill1();
    }

    public void onClickSkill2()
    {
        cerrentUnit.UseSkill2();
    }

    private void Update()
    {
        var skillImg1 = skill1UI.transform.Find("Image").GetComponent<Image>();
        var skillImg2 = skill2UI.transform.Find("Image").GetComponent<Image>();

        // Skill 1
        if (cerrentUnit.skill1.useMana > cerrentUnit.cerrentMana || cerrentUnit.isDie)
        {
            skill1UI.GetComponent<Image>().color = unactiveColor;
            skillImg1.color = unactiveColor;
        }
        else
        {
            skill1UI.GetComponent<Image>().color = activeColor;
            skillImg1.color = Color.white;
        }

        // Skill 2
        if (cerrentUnit.skill2.useMana > cerrentUnit.cerrentMana || cerrentUnit.isDie)
        {
            skill2UI.GetComponent<Image>().color = unactiveColor;
            skillImg2.color = unactiveColor;
        }
        else
        {
            skill2UI.GetComponent<Image>().color = activeColor;
            skillImg2.color = Color.white;
        }
    }

    internal void SetCerrent(GameObject gameObject)
    {
        this.cerrentUnit = gameObject.GetComponent<CharacterController>();
        SetUI();
    }
}
