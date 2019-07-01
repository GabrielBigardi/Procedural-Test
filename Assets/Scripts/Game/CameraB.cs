using UnityEngine;

public class CameraB : MonoBehaviour
{
	
	public static CameraB Instance;
	
    public GameObject target;
    public Vector3 targetPos;
    public float moveSpeed;

    public BoxCollider2D boundBox;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private Camera theCamera;
    private float halfHeight;
    private float halfWidth;


	void Awake()
	{
		if(Instance == null){
			Instance = this;
		}else if(Instance != this){
			Destroy(gameObject);
		}
		
	}

    void Start()
    {
        //minBounds = boundBox.bounds.min;
        //maxBounds = boundBox.bounds.max;
        //
        //theCamera = GetComponent<Camera>();
        //halfHeight = theCamera.orthographicSize;
        //halfWidth = halfHeight * Screen.width / Screen.height;

    }

    void FixedUpdate()
    {

        if (target == null) return;

        targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);

        float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
	
	public void SetBounds(BoxCollider2D newBounds)
	{
		boundBox = newBounds;
		
		minBounds = boundBox.bounds.min;
		maxBounds = boundBox.bounds.max;

        theCamera = GetComponent<Camera>();
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }
}
