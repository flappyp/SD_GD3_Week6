using UnityEngine;

public class Bug : MonoBehaviour
{
    public AudioClip caughtSound;
    public string bugName; // Name of the bug type
    private BugLog bugLogManager; // holds a reference to the buglog component, this is used o interact with the bug log script
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bugLogManager = FindObjectOfType<BugLog>(); // this is finding the buglog in the hierachy 

        if (bugLogManager == null)
        {
            Debug.LogError("BugLog not found in the scene.");
        }
    }

    private void OnMouseDown() //this method is called when the player clicks on the bug
    {
        if (bugLogManager != null)
        {
            audioSource.PlayOneShot(caughtSound);
            bugLogManager.OnBugCaught(bugName); // this is calling the On Bug Caught method in the bug log script and adding to the amount of bugs needed to unlock it
            
            Destroy(gameObject); // destroying the bug 
        }
        else
        {
            Debug.LogWarning("BugLogManager is not assigned.");
        }
    }
}
