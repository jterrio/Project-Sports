using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseSkillScript : MonoBehaviour {

    public Image toolbarImage;
    public Image menuImage;

    public GameObject[] targets;

    public float[] damage;
    public float[] healing;
    public float[] range;

    public int damageRank;
    public int healingRank;
    public int rangeRank;

    public float cooldown;


    void TargetEnemies() {
        targets = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void TargetTeammates() {
        targets = GameObject.FindGameObjectsWithTag("Teammate");
    }


}
