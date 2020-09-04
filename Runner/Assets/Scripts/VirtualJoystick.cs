using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image backgroundImage;
    private Image joystickImage;
    private Vector3 inputVector;

    private void Start()
    {
        backgroundImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        joystickImage.color = new Color32(80, 80, 80, 255);
        OnDrag(eventData);

    }
    public void OnPointerUp(PointerEventData eventData)
    {

        joystickImage.color = new Color32(150, 150, 150, 255);
        inputVector = Vector3.zero;
        joystickImage.rectTransform.anchoredPosition = inputVector;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform,
            eventData.position, eventData.pressEventCamera, out position))
        {
            position.x = (position.x / backgroundImage.rectTransform.sizeDelta.x);
            position.y = (position.y / backgroundImage.rectTransform.sizeDelta.y);

            inputVector = new Vector3(position.x * 2 + 1, 0, position.y * 2 - 1);
            if (inputVector.magnitude > 1)
            {
                inputVector = inputVector.normalized;
            }
            //backgroundImage.rectTransform.sizeDelta.x / 3  -  - divided by for the visual so the knob will not go too far, but the actual value will remain [-1, 1]
            joystickImage.rectTransform.anchoredPosition =
                new Vector3(inputVector.x * (backgroundImage.rectTransform.sizeDelta.x / 3), inputVector.z * (backgroundImage.rectTransform.sizeDelta.y / 3), 0);

        }


    }

    public float Horizontal
    {
        get
        {
            if (inputVector.x != 0)
            {
                return inputVector.x;
            }
            else
            {
                return Input.GetAxis("Horizontal");
            }
        }
    }
    public float Vertical
    {
        get
        {
            if (inputVector.z != 0)
            {
                return inputVector.z;
            }
            else
            {
                return Input.GetAxis("Vertical");
            }
        }
    }

}
