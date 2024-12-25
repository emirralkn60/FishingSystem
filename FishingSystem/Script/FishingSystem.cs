using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum waterSource
{
    Lake,
    River,
    Ocean
}
public class FishingSystem : MonoBehaviour
{
    FishingRod fishingRod;
    public static FishingSystem instance { get; set; }
    public List<FishData> lakeFishList; // alabalik
    public List<FishData> riverFishList; //somon
    public List<FishData> oceanFishList;// ton
    public bool isThereABite;
    bool hasPulled;
    public static event Action OnFishingEnd;
    public GameObject minigame;
    public PlayerMovement playerMovement;
    public Transform GeciciEndofRop;
    Animator fishingAnim;

    public GameObject FishCaughtPanel;
    public GameObject FishMissingPanel;
    public Animator fishCaughtAnimator;
    private void Awake()
    {
        if(instance!=null && instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        fishingRod = FindObjectOfType<FishingRod>();
        fishingAnim= FindObjectOfType<FishingRod>().gameObject.GetComponent<Animator>();
        GeciciEndofRop.position =fishingRod.endof_of_rope.transform.position;
        GeciciEndofRop.rotation = fishingRod.endof_of_rope.transform.rotation;
        GeciciEndofRop.localScale = fishingRod.endof_of_rope.transform.localScale;    
    }
    public void StartFishing(waterSource waterSource)
    {
        StartCoroutine(FishingCoroutine(waterSource));
    }

    IEnumerator FishingCoroutine(waterSource waterSource)
    {

        yield return new WaitForSeconds(3f);
        FishData fish = calculateBite(waterSource);

        if(fish.fishName=="NoBite")
        {
            Debug.LogWarning("No Fish Caught");
            StartCoroutine(ShowPanelForDuration(FishMissingPanel, 1f));
            EndFishing();

        }
        else
        {
            Debug.LogWarning(fish.fishName+ "isBiting" );
            StartCoroutine(StartFishStruggle(fish));
        }
    }

    IEnumerator StartFishStruggle(FishData fish)
    {
        isThereABite = true;
        while(!hasPulled)
        {
            yield return null;
        }
        StartMiniGame();
    }

    private void StartMiniGame()
    {
        playerMovement.enabled = false;
        minigame.SetActive(true);

    }

    public void SetHasPulled()
    {
      hasPulled = true; 
    }

    private void EndFishing()
    {

        isThereABite = false;
        hasPulled = false;

        eskiHalineGetir();
        OnFishingEnd?.Invoke();

    }


    private FishData calculateBite(waterSource waterSource)
    {
        List<FishData> availableFish = GetAvailableFish(waterSource); // mevcut balýklarýn listesini ekliyoruz

        float totalProbability = 0f;
        // ton %5(0-4) somon %20 (5-24) nobite %10(24-34)
        foreach (FishData fish in availableFish) // balýklarýn tüm olasýklarýný toplucak
        {
            totalProbability+=fish.probability;
        }
        // 1 ile  balýklarýn tüm olasýklarý arasýnda +1 ekleyerek random bir olasýlýk bulcak 
        int randomValue=UnityEngine.Random.Range(0,Mathf.FloorToInt(totalProbability)+1);
        Debug.Log("Random value is" + randomValue);

        float cumulativeProbability = 0f;
        foreach(FishData fish in availableFish)
        {
            cumulativeProbability+=fish.probability; // ayný iþlem
            if(randomValue<=cumulativeProbability) // karþýlaþtýrma yapýcak
            {
                return fish;
            }
        }
        return null;

    }



    private List<FishData> GetAvailableFish(waterSource waterSource)
    {
        switch(waterSource)
        {
            case waterSource.Lake:
                return lakeFishList;
            case waterSource.River:
                return riverFishList;
            case waterSource.Ocean:
                return oceanFishList;
            default:
                return null;
        }
    }

    internal void EndMiniGame(bool success)
    {
        playerMovement.enabled = true;
        minigame.gameObject.SetActive(false);

        if (success)
        {
            Debug.Log("Fish Caught");
            fishCaughtAnimator.Play("FishCaughtAnim");
            StartCoroutine(ShowPanelForDuration(FishCaughtPanel, 1f)); // Fish Caught Paneli 2 saniye aç
        }

        EndFishing();
    }

    private IEnumerator ShowPanelForDuration(GameObject panel, float duration)
    {
        panel.SetActive(true); // Paneli aç
        yield return new WaitForSeconds(duration); // Belirtilen süre kadar bekle
        panel.SetActive(false); // Paneli kapat
    }
    void eskiHalineGetir()
    {
        fishingRod.endof_of_rope.transform.position = GeciciEndofRop.position;
        fishingRod.endof_of_rope.transform.rotation = GeciciEndofRop.rotation;
        fishingRod.endof_of_rope.transform.localScale = GeciciEndofRop.localScale;

        fishingAnim.SetBool("EndFish", true);
        fishingRod.isCasted= false;
        fishingRod.isPulling = false;
    }
}
