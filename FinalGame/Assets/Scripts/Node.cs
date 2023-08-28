using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Node : MonoBehaviour
{
    
    public Color hoverColor;
    public Color notEnoughSPColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject mage;
    [HideInInspector]
    public MageBlueprint mageBlueprint;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (mage != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
        {
            return;
        }

        BuildMage(buildManager.GetMageToBuild());
    }

    void BuildMage(MageBlueprint blueprint)
    {
        if (PlayerStats.skillPoints < blueprint.cost)
        {
            Debug.Log("U broke");
            return;
        }

        PlayerStats.skillPoints -= blueprint.cost;

        GameObject _mage = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        mage = _mage;

        mageBlueprint = blueprint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition() - new Vector3(36, 0, 0), Quaternion.identity);
        Destroy(effect, 5f);

        Debug.Log("Mage Built");
    }

    public void SellMage()
    {
        PlayerStats.skillPoints += mageBlueprint.GetSellAmount();

        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition() - new Vector3(36, 0, 0), Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(mage);
        mageBlueprint = null;
    }

    void OnMouseEnter()
    {
        if (!buildManager.CanBuild)
        {
            return;
        }

        if (buildManager.HasSkillPoints)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughSPColor;
        }     
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
