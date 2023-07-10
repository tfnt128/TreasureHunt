using UnityEngine;

public class mannequinManager : MonoBehaviour
{
    [SerializeField] private GameObject mannequin1;
    [SerializeField] private GameObject mannequin2;
    [SerializeField] private bool appers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (appers)
            {
                mannequin1.SetActive(true);
                if (mannequin2 != null)
                {
                    mannequin1.SetActive(true);
                }
            }
            else
            {
                mannequin1.SetActive(false);
                if (mannequin2 != null)
                {
                    mannequin1.SetActive(false);
                }
            }
        }
    }
}