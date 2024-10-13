using System.Collections;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    private Player player;

    private SpriteRenderer sr;
    private float currentGravity = 3f;
    [SerializeField] private GameObject bubble;

    [SerializeField] private int maxHealth;
    private int currentHealth;

    #region Harmful effects
    public float airJump { get; private set; } = 0.1f;
    private float currentAirJump;
    public bool noJump { get; set; }
    #endregion

    private void Start()
    {
        player = GetComponent<Player>();
        sr = GetComponent<SpriteRenderer>();


        currentAirJump = airJump;
        currentHealth = maxHealth;

        bubble.SetActive(false);
    }

    public void takeDame()
    {
        player.isHit();
        currentHealth--;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die() => player.Die();

    public void recoverHealth()
    {
        currentHealth++;
        if(currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void reverseDirection()
    {
        player.setUpPlayer(true , currentGravity);
        StartCoroutine(colorChange());
    }
    public void jumpAir() => player.activeJumpingAir();

    private IEnumerator stopJumpIn(float _second) // hiệu ứng gây hại: Xóa khả năng nhảy
    {
        noJump = true;
        sr.color = Color.blue;
        Debug.Log("Ngừng nhảy");
        yield return new WaitForSeconds(_second);
        noJump = false;
        sr.color = new Color(1, 1, 1, 1);
        Debug.Log("Được phép nhảy");
    }

    private IEnumerator increaseFallSpeedIn(float _second) // hiệu ứng gây hại: Rơi nhanh hơn khi ở trạng thái trên không
    {
        airJump = 1f;
        sr.color = Color.grey;
        yield return new WaitForSeconds(_second);
        airJump = currentAirJump;
        sr.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator colorChange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(1);
        sr.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator removeGravityBy(float _second)
    {
        player.setUpPlayer(false, 0);
        bubble.SetActive(true);
        Debug.Log("Không trọng lực");
        yield return new WaitForSeconds(_second);
        player.setUpPlayer(false, currentGravity);
        bubble.SetActive(false);
        Debug.Log("Trả trọng lực");
    }
}
