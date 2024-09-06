using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BugLog : MonoBehaviour // this is the main class that manages the bug log system
{
    [System.Serializable]
    public class Bug // a class that defines each bugs properties
    {
        public string bugName; //the name of the bug
        public int bugsCaught = 0; // the amount of bugs caught
        public int bugsRequiredToUnlock; // the amount of bugs you need to catch before it gets unlocked
        public Button bugButton; // a ui button that will enabled when the entry is unlocked
        public GameObject bugEntry; // a reference to the ui for each bug entry
        public bool isUnlocked = false;
    }

    public List<Bug> bugList; // the list of different types of bugs
    public GameObject bugLogUI; // the bug log ui element
    public Button bugLogButton; // this is for the button that opens the bug log
    public Sprite defaultButtonSprite; // the default sprite for bug log button
    public Sprite newBugCaughtSprite; // the sprite that the button will change to when a new bug is unlock
    public AudioClip unlockSound;
    public AudioSource audioSource;
    private bool isNewBugUnlocked = false; // making sure no bugs are already unlocked

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bugLogUI.SetActive(false); // this is hiding the bug log at the start of the game
        foreach (Bug bug in bugList)
        {
            if (bug.bugsCaught < bug.bugsRequiredToUnlock)
            {
                bug.bugButton.interactable = false; // disabling the button if the bug hasnt been unlocked yet
                bug.bugEntry.SetActive(false); // this is setting the entry in the bug log to false
            }
            else
            {
                UnlockBugEntry(bug); // if the bug is unlocked it will unlock the entry in the bug log
            }
        }

        bugLogButton.GetComponent<Image>().sprite = defaultButtonSprite; // making sure the bug log button is set to the default sprite which i set in the inspector
        bugLogButton.onClick.AddListener(OnBugLogButtonClick); // this handles the clicks of the button
    }

    public void OnBugCaught(string bugName) // this method is called when a bug is caught in the game
    {
        foreach (Bug bug in bugList) // this is going through the bug list and finding the correct bug name with the bug that been caught
        {
            if (bug.bugName == bugName) // if the name of bug caught is the same as the bug in the list
            {
                bug.bugsCaught++; //then it adds one to bugs caught
                if (!bug.isUnlocked && bug.bugsCaught >= bug.bugsRequiredToUnlock)
                {
                    UnlockBugEntry(bug);
                    UpdateBugLogButton(); // Update button sprite when a new bug is unlocked
                }
                break;
            }
        }
    }

    private void CheckBugUnlockStatus(Bug bug) //this method is checking to see if the player has caught enough of a certain bug to unlock it.
    {
        if (bug.bugsCaught >= bug.bugsRequiredToUnlock) //if the number of bugs caught is greater or equal to (>= this is what that means) it then called the unlock bug entry method
        {
            UnlockBugEntry(bug); //this unlocks the bug entry
            UpdateBugLogButton(); // this changes the bug log button
        }
    }

    private void UnlockBugEntry(Bug bug) 
    {
        bug.bugButton.interactable = true; // this makes the button interactable
        bug.bugEntry.SetActive(true);// shows the bugs entry in the bug log
        bug.isUnlocked = true;
        audioSource.PlayOneShot(unlockSound);
    } 
       

    private void UpdateBugLogButton()
    {
        isNewBugUnlocked = true; // this marks that a new bug is unlocked
        bugLogButton.GetComponent<Image>().sprite = newBugCaughtSprite; //this changes the bug log button sprite to show the player they have caught enough of a certain bug to unlock it in the bug log
    }

    public void OnBugLogButtonClick() //this method is called when the player clicks on the bug log button
    {
        if (isNewBugUnlocked) 
        {
            bugLogButton.GetComponent<Image>().sprite = defaultButtonSprite; // when the player opens the bug log it will reset it to its default sprite
            isNewBugUnlocked = false; // sets the button back to false
        }

        OpenBugLog(); // this is calling the method to open the bug log
    }

    public void OpenBugLog()
    {
        bugLogUI.SetActive(true); 
        Time.timeScale = 0f; //if the bug is open this pauses the game using the time.timescalse function
    }

    public void CloseBugLog()
    {
        bugLogUI.SetActive(false);
        Time.timeScale = 1f; //if the bug log is closed then the game starts again
    }
}
