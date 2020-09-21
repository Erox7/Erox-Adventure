using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletAttack : MonoBehaviour
{
    public float damage;
    GridLayout currentMap;
    Vector3Int lastWorldPosition;
    public Vector3 bulletObjective;
    private Vector3 movement;
    public float speed = 6f;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventManager.Instance.onMapChanged += UpdateMap;
        currentMap = MapController.currentMap.GetComponent<GridLayout>();
        lastWorldPosition = currentMap.WorldToCell(transform.position);
    }

    public void UpdateMap()
    {
        currentMap = MapController.currentMap.GetComponent<GridLayout>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3Int currentPosition = currentMap.WorldToCell(transform.position);
        if (lastWorldPosition != currentPosition)
        {
            EnemyEventsManager.Instance.MakeProjectileDamage(currentPosition, damage);
            lastWorldPosition = currentPosition;
        }
        movement = bulletObjective - transform.position;
        transform.Translate(movement.normalized * speed * Time.deltaTime);
    }
}
