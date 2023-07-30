using UnityEngine;

public class PlayerInteractionUiEnabler : MonoBehaviour
{
    [SerializeField] private GameObject fenceUiObject;
    [SerializeField] private GameObject treeUiObject;
    [SerializeField] private GameObject mineUiObject;
    [SerializeField] private GameObject statueUiObject;

    private void OnTriggerStay2D(Collider2D other)
    {
        SetActiveFoLabels(LayerMask.LayerToName(other.gameObject.layer), true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        string layer = LayerMask.LayerToName(other.gameObject.layer);
        if (layer == "FenceTrigger" || layer == "Tree" 
        || layer == "Stone" || layer == "Statue")
        {
            statueUiObject.SetActive(false);
            treeUiObject.SetActive(false);
            mineUiObject.SetActive(false);
            fenceUiObject.SetActive(false);
        }
    }

    private void SetActiveFoLabels(string layer, bool value)
    {
        switch(layer)
        {
            case "FenceTrigger":
                fenceUiObject.SetActive(value);
                treeUiObject.SetActive(!value);
                mineUiObject.SetActive(!value);
                statueUiObject.SetActive(!value);
                break;
            case "Tree":
                treeUiObject.SetActive(value);
                fenceUiObject.SetActive(!value);
                mineUiObject.SetActive(!value);
                statueUiObject.SetActive(!value);
                break;
            case "Stone":
                mineUiObject.SetActive(value);
                treeUiObject.SetActive(!value);
                fenceUiObject.SetActive(!value);
                statueUiObject.SetActive(!value);
                break;
            case "Statue":
                statueUiObject.SetActive(value);
                treeUiObject.SetActive(!value);
                mineUiObject.SetActive(!value);
                fenceUiObject.SetActive(!value);
                break;
            default:
                statueUiObject.SetActive(!value);
                treeUiObject.SetActive(!value);
                mineUiObject.SetActive(!value);
                fenceUiObject.SetActive(!value);
                break;

        }
    }
}
