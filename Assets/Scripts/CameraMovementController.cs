using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovementController : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    private GameObject map;
    // Start is called before the first frame update
    void Start()
    {
        GlobalEventManager.Instance.onMapChanged += UpdateMap;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            //Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = new Vector3(Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x),
                                             Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y),
                                             transform.position.z);
        }
    }
    public void UpdateMap()
    {
        map = MapController.currentMap;
        calculateCameraLimits();
    }
    void calculateCameraLimits()
    {
        Vector3 tileMapSize = map.GetComponentInChildren<Tilemap>().size;
        if (map != null && map != default)
        {
            var halfHeight = Camera.main.orthographicSize;
            var halfWidth = halfHeight * Camera.main.aspect;
            var cellSize = map.GetComponentInChildren<Tilemap>().cellSize;
            float xCellsLimit = halfWidth / cellSize.x;
            float yCellsLimit = halfHeight / cellSize.y;

            Vector3 min = map.transform.GetChild(0).gameObject.GetComponent<Tilemap>().cellBounds.min;
            Vector3 max = map.transform.GetChild(0).gameObject.GetComponent<Tilemap>().cellBounds.max;

            maxPosition = new Vector2(max.x - xCellsLimit, max.y - yCellsLimit);
            minPosition = new Vector2(min.x + xCellsLimit, min.y + yCellsLimit);
            if (tileMapSize.x < halfWidth * 2)
            {
                // TODO: SET MAX Y MIN DE LA X A LA MITAD.
                maxPosition.x = min.x + ((max.x - min.x)/2);
                minPosition.x = min.x + ((max.x - min.x)/2);
            } else
            {
                maxPosition.x = max.x - xCellsLimit;
                minPosition.x = min.x + xCellsLimit;
            }
            if (tileMapSize.y < halfHeight * 2)
            {
                // TODO: SET MAX Y MIN DE LA Y A LA MITAD.
                maxPosition.y = min.y + ((max.y - min.y)/2);
                minPosition.y = min.y + ((max.y - min.y)/2);
            } else
            {
                maxPosition.y = max.y - yCellsLimit;
                minPosition.y = min.y + yCellsLimit;
            }
        }
    }
    private void OnDestroy()
    {
        GlobalEventManager.Instance.onMapChanged -= UpdateMap;
    }
}
