using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public Transform holdPosition; // El pozisyonu
    private GameObject currentObject; // �u an al�nabilir obje
    private bool isHoldingObject = false; // Oyuncu bir obje tutuyor mu

    void Update()
    {
        // Obje alma
        if (currentObject != null && Input.GetKeyDown(KeyCode.E) && !isHoldingObject)
        {
            Pickup(currentObject);
        }
        // Obje b�rakma
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

        // Rigidbody ayarlar�
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true; // Fizik etkilerini kald�r
    }

    private void Drop()
    {
        if (currentObject != null)
        {
            isHoldingObject = false;
            currentObject.transform.SetParent(null); // Parent'� kald�r

            // Rigidbody ayarlar�
            Rigidbody rb = currentObject.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = false; // Fizik etkilerini geri getir

            currentObject = null; // �u anki objeyi s�f�rla
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup")) // Al�nabilir objeyi alg�la
        {
            currentObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup")) // Obje alan�ndan ��karsa
        {
            if (!isHoldingObject) // E�er obje tutulmuyorsa
            {
                currentObject = null;
            }
        }
    }
}
