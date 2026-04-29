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
    private int randomMoveCounter = 0;
    private int lastRandomDirection = 0;
    public UI Ui;
    public Sounds Sounds;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Update()
    {
        if (isPlastered)
        {
            MoveRandomly();
        }
    }

    public void Reset()
    {
        isPlastered = false;
        isDrunk  = false;
        ChangeToSoberSprite();
        spriteRenderer.flipX = false;
        transform.position = Vector3.zero;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "beer")
        {
            Sounds.PlayBeerSound();
            GetDrunk();
            Destroy(other.gameObject);
        }
        if(other.tag == "bone")
        {
            Sounds.PlayBoneSound();
            ScoreKeeper.AddPoint();
            Ui.SetScoreText(ScoreKeeper.GetScore());
            Destroy(other.gameObject);
        }
        if(other.tag == "pill")
        {
            Sounds.PlayPillSound();
            SoberUp();
            Destroy(other.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "moonshine")
        {
            Sounds.PlayMoonshineSound();
            GetPlastered();
            Destroy(other.gameObject);
        }
    }
    
    private void MoveRandomly()
    {
        int direction = lastRandomDirection; 
        
        if (randomMoveCounter == 0)
        {
            direction = Random.Range(0, 4);
            lastRandomDirection = direction;
            randomMoveCounter = Random.Range(20, 60);
        }
        switch (direction)
            {
                case 0:
                    Move(new Vector2(1, 0));
                    break;
                case 1:
                    Move(new Vector2(-1, 0));
                    break;
                case 2:
                    Move(new Vector2(0, 1));
                    break;
                case 3:
                    Move(new Vector2(0, -1));
                    break;
            }
            randomMoveCounter = randomMoveCounter - 1;
    }

    public void MoveManually(Vector2 direction)
    {
        if (isPlastered)
            return;
        
        Move(direction);
    }

    private void GetPlastered()
    {
        isPlastered = true;
        ChangeToDrunkSprite();
        StartSoberingUp();
    }
    
    private void GetDrunk()
    {
        isDrunk = true;
        ChangeToDrunkSprite();
        StartSoberingUp();
    }
    private Vector2 ApplyDrunkness(Vector2 direction)
    {
        if (isDrunk)
        {
            direction.x = direction.x * -1;
            direction.y = direction.y * -1;
        }
        return direction;
    }
    
    private void SoberUp()
    {
        ChangeToSoberSprite();
        isDrunk = false;
        isPlastered = false;
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

    private void ChangeToSoberSprite()
    {
        spriteRenderer.sprite = SoberSprite;
    }

    private void ChangeToDrunkSprite()
    {
        spriteRenderer.sprite = DrunkSprite;
    }

    public void Move(Vector2 direction)
    {
        direction = ApplyDrunkness(direction);
        FaceCorrectDirection(direction);
        Vector2 movementAmount = 5f * direction * Time.deltaTime;
        spriteRenderer.transform.Translate(movementAmount.x, movementAmount.y, 0);
        spriteRenderer.transform.position = SpriteTools.ConstrainToScreen(spriteRenderer);
    }

    private void FaceCorrectDirection(Vector2 direction)
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
