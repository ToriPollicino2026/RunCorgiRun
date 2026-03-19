using UnityEngine;
using System.Collections;

public class corgi : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isDrunk = false;
    public Sprite DrunkSprite;
    public Sprite SoberSprite;
    private Coroutine soberUpCoroutine;
    private bool isPlastered = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "beer");
        {
            GetDrunk();
            Destroy(other.gameObject);
        }
        if(other.tag == "bone");
        {
            Destroy(other.gameObject);
        }
        if(other.tag == "pill");
        {
            SoberUp();
            Destroy(other.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "moonshine")
        {
            GetPlastered();
            Destroy(other.gameObject);
        }
    }

    private void GetPlastered()
    {
        isPlastered = true;
        ChangeToDrunkSprite();
        StartSoberingUp();
    }

    public void Update()
    {
        if (isPlastered)
        {
            MoveRandomly();
        }
    }

    private void MoveRandomly()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                Move(new Vector2(1,0));
                break;
            case 1:
                Move(new Vector2(-1,0));
                break;
            case 2:
                Move(new Vector2(0,1));
                break;
            case 3:
                Move(new Vector2(0,-1));
                break;
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
        isPlastered = false;
    }

    private void ChangeToSoberSprite()
    {
        spriteRenderer.sprite = SoberSprite;
    }

    private void ChangeToDrunkSprite()
    {
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
