using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLogicScript : MonoBehaviour
{
    // Status des Baums (wie groß)
    private Sprite currentSprite;
    // Anfangs-Sprite
    [SerializeField] Sprite startSprite;
    // Liste aller Sprites um darauf zuzugreifen
    [SerializeField] Sprite[] spriteListTrees;
    // X Positionen der Bäume
    [SerializeField] float[] xPositionTrees;
    // Y Positionen der Bäume
    [SerializeField] float[] yPositionTrees;
    // Wie schnell soll der Baum über die Zeit wachsen
    [SerializeField] float growingSpeed;
    // Wieviele Bäume sollen zunächste gespawnt werden
    [SerializeField] int treesToSpawn;
    // Prefab für Bäume mit SpriteRenderer Component bisher
    [SerializeField] GameObject prefabTree;

    //Liste der Instanziierten Bäume, um einfacher darauf zuzugreifen
    private List<GameObject> trees = new List<GameObject>();
    // Um die Wachstumsrate variabel zu halten
    private float randomFloatExtraTime;


    // Start is called before the first frame update
    void Start()
    {
        SpawnTree(treesToSpawn, xPositionTrees, yPositionTrees);
    }

    // Coroutine um das Wachsen der Bäume zu steuern 
    IEnumerator TreeGrow(float timeBetweenStatus, GameObject tree,
        SpriteRenderer spriteRendererTree, Sprite[] spritesTreesList)
    {
        Debug.Log("Gameobject" + tree + "Extra Zeit" + randomFloatExtraTime);
        for (int i = 0; i < spritesTreesList.Length; i++)
        {
            randomFloatExtraTime = Random.Range(0, 10f);
            yield return new WaitForSeconds(timeBetweenStatus + randomFloatExtraTime);
            spriteRendererTree.sprite = spritesTreesList[i];
        }
        yield break;
    }

    // Methode um die Bäume zu spawnen
    private void SpawnTree(int numberOfTrees, float[] x, float[] y)
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            GameObject tree = Instantiate(prefabTree, new Vector3(x[i], y[i], 0), Quaternion.identity);
            SpriteRenderer spriteRenderer = tree.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = startSprite;
            trees.Add(tree);
            StartCoroutine(TreeGrow(growingSpeed, tree, spriteRenderer, spriteListTrees));
        }
    }
}
