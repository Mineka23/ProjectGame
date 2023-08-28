
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Build Manage in scene.");
            return;
        }
        instance = this;
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private MageBlueprint mageToBuild;
    private Node selectedNode;

    public NodeUI nodeUI;

    public bool CanBuild { get { return mageToBuild != null; } }
    public bool HasSkillPoints { get { return PlayerStats.skillPoints >= mageToBuild.cost; } } 

    public void SelectMageToBuild(MageBlueprint mage)
    {
        mageToBuild = mage;
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        
        selectedNode = node;
        mageToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public MageBlueprint GetMageToBuild()
    {
        return mageToBuild;
    }
}
