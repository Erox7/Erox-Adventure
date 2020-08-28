using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireAttack : MonoBehaviour
{
    public float damage;
    GridLayout currentMap;
    Vector3Int lastWorldPosition;
    public Vector2 orientation;
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventManager.Instance.onMapChanged += UpdateMap;
        currentMap = MapController.currentMap.GetComponent<GridLayout>();
        lastWorldPosition = currentMap.WorldToCell(transform.position);
    }
    public void UpdateMap ()
    {
        currentMap = MapController.currentMap.GetComponent<GridLayout>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3Int currentPosition = currentMap.WorldToCell(transform.position);
        if (lastWorldPosition != currentPosition)
        {
            EnemyEventsManager.Instance.TakeProjectileDamage(currentPosition);
            lastWorldPosition = currentPosition;
        }
        // Si la posicion actual es distinta a la ultima que tengo guardada
        // Envío una señal para que los enemigos en aquella posicion pillen daño
        transform.Translate(orientation.normalized * speed * Time.deltaTime);
    }

    /*
     Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
    if (screenPosition.y > Screen.height || screenPosition.y < 0)
    {
        Die(); Destroy el objeto
    }
    }
 
     */
}
