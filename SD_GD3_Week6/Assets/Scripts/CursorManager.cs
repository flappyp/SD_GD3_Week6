using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursorTexture;  // Default cursor sprite
    public Texture2D bugCursorTexture;      // Cursor sprite when over a bug
    private Vector2 hotspot = new Vector2(0, 0); // this defines the point within the cursor where the click is registered

    private void Start()
    {
        
        Cursor.SetCursor(defaultCursorTexture, hotspot, CursorMode.Auto); // this setting the cursor to its default sprite, the cursor mode auto lets unity automatically handle the cursors look based on the platform its being played on
    }

    private void Update()
    {
        // Raycast to detect if the cursor is over a bug
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // this is converting the mouse position from screen space pixels to world space within the game
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero); // this is checking whats underneath the cursor

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Bug"))
            {
                
                Cursor.SetCursor(bugCursorTexture, hotspot, CursorMode.Auto); // if the cursor is over a game object with the tag of bug then it will change to the net cursor
            }
            else
            {
                
                Cursor.SetCursor(defaultCursorTexture, hotspot, CursorMode.Auto); // if its not over a bug it will change back to the default cursor
            }
        }
        else
        {
            
            Cursor.SetCursor(defaultCursorTexture, hotspot, CursorMode.Auto); // this changes the cursor back to the default sprite when its over nothing
        }
    }
}
