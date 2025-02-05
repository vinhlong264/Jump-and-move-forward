using Extension;
using System.Collections;
using UnityEngine;

public class PlayerStats : Singleton<PlayerStats>
{
    private Player player;
    private SpriteRenderer sr;
    private float currentGravity = 3f;
    [SerializeField] private GameObject bubble;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    #region Harmful effects
    public float airJump { get; private set; } = 0.1f;
    private float currentAirJump;
    public bool noJump { get; set; }
    #endregion

    [SerializeField] GameObject UI_YouLose;


    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();
        currentAirJump = airJump;
        currentHealth = maxHealth;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bubble.SetActive(false);
        Observer.Instance.Notify(ActionType.Health, currentHealth);
    }

    public void takeDame()
    {
        player.isHit();
        currentHealth--;
        Observer.Instance.Notify(ActionType.Health, currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        player.Die();
        UI_YouLose.SetActive(true);
        Time.timeScale = 0f;
    }

    public void recoverHealth()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Observer.Instance.Notify(ActionType.Health, currentHealth);
    }

    public void jumpAir()
    {
        player.activeJumpingAir();
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void ReverseDirection()
    {
        player.isOppositeDirection = true;       
    }
}
