using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{

    public Camera mainCamera;
    public Canvas canvasObj;

    public Image handImage;

    public int deviceWidth = 576;

    [Range(0f,45f)]
    public float maxAngle = 15f;

    [Range(-45f, 45f)]
    public float defaultAngle = 0f;

    void Start()

    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        // HAND POSITION
        CanvasScaler canvScaler = canvasObj.GetComponent<CanvasScaler>();
        float canvas2scrX = canvScaler.referenceResolution.x / Screen.width;
        float canvas2scrY = canvScaler.referenceResolution.y / Screen.height;

        handImage.rectTransform.anchoredPosition = new Vector2(mousePos.x * canvas2scrX, mousePos.y * canvas2scrY);

        // HAND ROTATION
        float dif = Mathf.Clamp(handImage.rectTransform.anchoredPosition.x - canvScaler.referenceResolution.x/2, - deviceWidth / 2, deviceWidth / 2);

        float angle = Mathf.Clamp(2 * dif / deviceWidth, -1.0f, 1.0f) * maxAngle;

        handImage.rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, defaultAngle - angle));

        if (Input.GetMouseButtonDown(0))
        {
            handImage.GetComponent<Animator>().SetTrigger("Touch");
        }
        if (Input.GetMouseButtonUp(0))
        {
            handImage.GetComponent<Animator>().SetTrigger("UnTouch");
        }

    }

}
