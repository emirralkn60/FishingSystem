using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform holdPosition; // El pozisyonu
    private GameObject currentObject; // Þu an alýnabilir obje
    private bool isHoldingObject = false; // Oyuncu bir obje tutuyor mu

    void Update()
    {
        // Obje alma
        if (currentObject != null && Input.GetKeyDown(KeyCode.E) && !isHoldingObject)
        {
            Pickup(currentObject);
        }
        // Obje býrakma
        else if (isHoldingObject && Input.GetKeyDown(KeyCode.E))
        {
            Drop();
        }
    }

    private void Pickup(GameObject obj)
    {
        isHoldingObject = true;
        obj.transform.SetParent(holdPosition); // El pozisyonuna ata

        // Verilen pozisyon ve rotasyonu ayarla
        obj.transform.localPosition = new Vector3(0.5f, 0.234f, 0.8f);
        obj.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        // Rigidbody ayarlarý
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true; // Fizik etkilerini kaldýr
    }

    private void Drop()
    {
        if (currentObject != null)
        {
            isHoldingObject = false;
            currentObject.transform.SetParent(null); // Parent'ý kaldýr

            // Rigidbody ayarlarý
            Rigidbody rb = currentObject.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = false; // Fizik etkilerini geri getir

            currentObject = null; // Þu anki objeyi sýfýrla
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup")) // Alýnabilir objeyi algýla
        {
            currentObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup")) // Obje alanýndan çýkarsa
        {
            if (!isHoldingObject) // Eðer obje tutulmuyorsa
            {
                currentObject = null;
            }
        }
    }
}
