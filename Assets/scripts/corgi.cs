using UnityEngine;
using System.Collections;

public class corgi : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isDrunk = false;
    public Sprite DrunkSprite;
    public Sprite SoberSprite;
    private Coroutine soberUpCoroutine;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "beer");
        {
            GetDrunk();
            Destroy(other.gameObject);
        }
        if(other.tag == "bone");
        {
            
        }
        if(other.tag == "pill");
        {
            
        }
    }

    private void GetDrunk()
    {
        isDrunk = true;
        ChangeToDrunkSprite();
        StartSoberingUp();
    }

    private Vector2 ApplyDrunkeness(Vector2 direction)
    {
        if (isDrunk)
        {
            direction.x = direction.x * -1;
            direction.y = direction.y * -1;
        }
        return direction;
    }

    private void StartSoberingUp()
    {
        if (soberUpCoroutine != null)
        {
         StopCoroutine(soberUpCoroutine);   
        }
        soberUpCoroutine = StartCoroutine(CountdownUntilSober());
    }

    IEnumerator CountdownUntilSober()
    {
        yield return new WaitForSeconds(GameParameters.CorgiDrunkSeconds);
        SoberUp();
    }

    private void SoberUp()
    {
        ChangeToSoberSprite();
        isDrunk = false;
    }

    private void ChangeToSoberSprite()
    {
        spriteRenderer.sprite = SoberSprite;
    }

    private void ChangeToDrunkSprite()
    {
        print("drunk");
        spriteRenderer.sprite = DrunkSprite;
    }

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Move(Vector2 direction)
    {
        direction = ApplyDrunkeness(direction);
        FaceCorrecrDirection(direction);
        Vector2 movementAmount = 5f * direction * Time.deltaTime;
        spriteRenderer.transform.Translate(movementAmount.x, movementAmount.y, 0);
        spriteRenderer.transform.position = SpriteTools.ConstrainToScreen(spriteRenderer);
    }

    private void FaceCorrecrDirection(Vector2 direction)
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public Vector3 GetPosition()
    {
        return spriteRenderer.transform.position;
    }
}
