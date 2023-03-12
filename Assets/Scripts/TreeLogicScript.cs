using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLogicScript : MonoBehaviour
{
    // Status des Baums (wie gro�)
    private Sprite currentSprite;
    // Anfangs-Sprite
    [SerializeField] Sprite startSprite;
    // Liste aller Sprites um darauf zuzugreifen
    [SerializeField] Sprite[] spriteListTrees;
    // X Positionen der B�ume
    [SerializeField] float[] xPositionTrees;
    // Y Positionen der B�ume
    [SerializeField] float[] yPositionTrees;
    // Wie schnell soll der Baum �ber die Zeit wachsen
    [SerializeField] float growingSpeed;
    // Wieviele B�ume sollen zun�chste gespawnt werden
    [SerializeField] int treesToSpawn;
    // Prefab f�r B�ume mit SpriteRenderer Component bisher
    [SerializeField] GameObject prefabTree;

    //Liste der Instanziierten B�ume, um einfacher darauf zuzugreifen
    private List<GameObject> trees = new List<GameObject>();
    // Um die Wachstumsrate variabel zu halten
    private float randomFloatExtraTime;

    private Dictionary<int, TreeComponent.State> _states = new Dictionary<int, TreeComponent.State>()
    {
        { 0, TreeComponent.State.Small },
        { 1, TreeComponent.State.Medium },
        { 2, TreeComponent.State.Large }
    };


    // Start is called before the first frame update
    void Start()
    {
        SpawnTree(treesToSpawn, xPositionTrees, yPositionTrees);
    }

    // Coroutine um das Wachsen der B�ume zu steuern 
    IEnumerator TreeGrow(float timeBetweenStatus, GameObject tree,
        SpriteRenderer spriteRendererTree, Sprite[] spritesTreesList, TreeComponent treeComponent)
    {
        Debug.Log("Gameobject" + tree + "Extra Zeit" + randomFloatExtraTime);
        for (int i = 0; i < spritesTreesList.Length; i++)
        {
            randomFloatExtraTime = Random.Range(0, 10f);
            yield return new WaitForSeconds(timeBetweenStatus + randomFloatExtraTime);
            spriteRendererTree.sprite = spritesTreesList[i];
            treeComponent.SetState(_states[i]);
        }
        yield break;
    }

    // Methode um die B�ume zu spawnen
    private void SpawnTree(int numberOfTrees, float[] x, float[] y)
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            GameObject tree = Instantiate(prefabTree, new Vector3(x[i], y[i], 0), Quaternion.identity);
            SpriteRenderer spriteRenderer = tree.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = startSprite;
            trees.Add(tree);
            StartCoroutine(TreeGrow(growingSpeed, tree, spriteRenderer, spriteListTrees, tree.GetComponent<TreeComponent>()));
        }
    }
}
