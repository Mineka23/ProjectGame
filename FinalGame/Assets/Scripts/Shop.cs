using UnityEngine;

public class Shop : MonoBehaviour
{
    public MageBlueprint standardMage;
    public MageBlueprint iceMage;
    public MageBlueprint iceTotem;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardMage()
    {
        Debug.Log("Standard Mage Selected");
        buildManager.SelectMageToBuild(standardMage);
    }

    public void SelectIceMage()
    {
        Debug.Log("Ice Mage Selected");
        buildManager.SelectMageToBuild(iceMage);
    }

    public void SelectIceTotem()
    {
        Debug.Log("Ice Totem Selected");
        buildManager.SelectMageToBuild(iceTotem);
    }
}
