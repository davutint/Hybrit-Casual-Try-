using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{

    public RectTransform joystickOutline;
    public RectTransform joystickButton;
    public float moveFactor;
    private Vector3 move;
    private bool canControlJoystick;
    private Vector3 tapPosition;

    private void Start()
    {
        HideJoystick();
    }

    private void Update()
    {
        if (canControlJoystick)
        {
            ControlJoystick();

        }
    }

    private void ShowJoystick()//GÖSTER
    {

        joystickOutline.gameObject.SetActive(true);
        canControlJoystick = true;
    }

    private void HideJoystick()//SAKLA
    {
        joystickOutline.gameObject.SetActive(false);
        canControlJoystick = false;
        move = Vector3.zero;
    }

    public void TappedOnJoystickZone()//PARMAK KONTROL VE JOYSTİCK POZİSYON AYARLAMA
    {
        tapPosition = Input.mousePosition;
        joystickOutline.position = tapPosition;
        //when finger on screen, then joystick show up
        ShowJoystick();
        Debug.Log("CALISTI");
    }

    public void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - tapPosition;

        float moveMagnitute = direction.magnitude * moveFactor / Screen.width;
        moveMagnitute = Mathf.Min(moveMagnitute, joystickOutline.rect.width / 2);

        move = direction.normalized * moveMagnitute;

        Vector3 targetPos = tapPosition + move;

        joystickButton.position = targetPos;

        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }

    public Vector3 GetMovePosition()
    {
        return move;

    }
}
