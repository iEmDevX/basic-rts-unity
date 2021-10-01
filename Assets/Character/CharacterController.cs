using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

public enum UnitTeam
{
    Player,
    Enemy,
}

public class CharacterController : MonoBehaviour, CharacterInterface
{
    public string id;
    public UnitTeam unitTeam;

    public string characterName;
    public int baseDamage;
    public int maxHP;
    public int maxMana;
    public int manaHealRate;// in millisec
    public int manaHealValue;// add per round
    public float attackSpeed;

    public int cerrentHP;
    public int cerrentMana = 0;

    [SerializeField] Slider sliderHp;
    [SerializeField] Slider sliderMana;

    public Skill skill1;
    public Skill skill2;

    public bool isDie { get; private set; } = false;

    private UiManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
    }

    public void InitCharacter(Character character, UnitTeam unitTeam, string id)
    {

        this.id = id;
        this.unitTeam = unitTeam;
        characterName = character.characterName;
        baseDamage = character.baseDamage;
        attackSpeed = character.acttackSpeed;

        // Set Mana State
        maxMana = character.maxMana;
        cerrentMana = 0;

        // Set HP State
        maxHP = character.maxHP;
        cerrentHP = maxHP;

        // Set HP Bar
        sliderHp.maxValue = maxHP;

        // Set mana
        sliderHp.value = cerrentHP;
        manaHealRate = character.manaHealRate;
        manaHealValue = character.manaHealValue;
        sliderMana.maxValue = maxMana;

        skill1 = character.skill1;
        skill2 = character.skill2;

        if (UnitTeam.Player.Equals(unitTeam))
        {
            _ = CallBasicManaHeal();
        }
        else
        {
            sliderMana.gameObject.SetActive(false);
        }

    }

    private void OnMouseDown()
    {
        uiManager.SetCerrent(gameObject);
    }

    private async Task CallBasicManaHeal()
    {
        while (!isDie)
        {
            await basicManaHeal();
        }
    }

    private async Task basicManaHeal()
    {
        await Task.Delay(manaHealRate);
        UpdateMana(cerrentMana + manaHealValue);
    }

    public void HpHeal(int healValue)
    {
        if (isDie) return;
        UpdateHP(cerrentHP + healValue);
    }

    public bool TakeDamage(int damage)
    {
        if (damage < 0 || cerrentHP < 0)
        {
            return isDie;
        }

        UpdateHP(cerrentHP - damage);

        if (cerrentHP <= 0) SetDie();
        return isDie;
    }

    public bool ReduceMana(int damage)
    {
        UpdateMana(cerrentMana - damage);
        return true;
    }

    private void UpdateHP(int newHP)
    {
        if (newHP < 0) newHP = 0;
        if (newHP > maxHP) newHP = maxHP;

        this.cerrentHP = newHP;
        this.sliderHp.value = this.cerrentHP;
    }

    private void UpdateMana(int newMana)
    {
        if (newMana < 0) newMana = 0;
        if (newMana > maxMana) newMana = maxMana;

        this.cerrentMana = newMana;
        this.sliderMana.value = this.cerrentMana;
    }

    private void SetDie()
    {
        isDie = true;
        gameObject.SetActive(false);
    }

    public async void UseBasicHit()
    {
        float valueRotate = 40;
        if (UnitTeam.Player.Equals(unitTeam))
        {
            valueRotate = -40;
        }
        var meshObj = gameObject.transform.Find("Mesh");
        meshObj.Rotate(0.0f, 0.0f, valueRotate, Space.World);

        await Task.Delay(500);
        meshObj.Rotate(0.0f, 0.0f, valueRotate * -1, Space.World);
    }

    public void UseSkill1()
    {
        if (skill1 == null || isDie) return;
        if (SkillType.AllTarget.Equals(skill1.skillType))
        {
            skill1.UseSkill(gameObject);
        }
        else if (SkillType.AllBuff.Equals(skill1.skillType))
        {
            Debug.Log("Use Skill1");
            skill1.UseSkill(gameObject);
        }
        else if (SkillType.MyBuff.Equals(skill1.skillType))
        {
            skill1.UseSkill(gameObject);
        }
    }

    public void UseSkill2()
    {
        if (skill2 == null || isDie) return;
        if (SkillType.AllTarget.Equals(skill2.skillType))
        {
            skill2.UseSkill(gameObject);
        }
        else if (SkillType.AllBuff.Equals(skill2.skillType))
        {
            skill2.UseSkill(gameObject);
        }
        else if (SkillType.MyBuff.Equals(skill2.skillType))
        {
            skill2.UseSkill(gameObject);
        }
    }

    public void BuffMyDamage(int addDamage)
    {
        if (addDamage < 0) return;
        baseDamage += addDamage;
    }

    public void DebuffMyDamage(int difDamage)
    {
        if (difDamage < 0) return;
        if (baseDamage - difDamage < 1)
        {
            baseDamage = 1;
        }
        else
        {
            baseDamage -= difDamage;
        }
    }

}

public interface CharacterInterface
{
    public void InitCharacter(Character character, UnitTeam unitTeam, string id);
    public bool TakeDamage(int dmg);
    public void HpHeal(int healValue);
    public void UseBasicHit();
    public void UseSkill1();
    public void UseSkill2();
}