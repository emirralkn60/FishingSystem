using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;
    float YRotation = 0f;

    void Start()
    {
        //�mleci ekran�n ortas�na kilitleyip g�r�nmez hale getiriyoruz
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            //x ekseni etraf�ndaki d�n��� kontrol ediyoruz (Yukar� ve a�a�� bak)
            xRotation -= mouseY;

            //d�nd�rmeyi s�k��t�r�yoruz, b�ylece a��r� d�nmeyelim (ger�ek hayatta oldu�u gibi)
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //y ekseni etraf�ndaki d�n��� kontrol ediyoruz (Yukar� ve a�a�� bak)
            YRotation += mouseX;

            //her iki rotasyonu da uyguluyoruz
            transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
        
        

    }
}
