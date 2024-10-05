using System.Collections;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    private Player player;

    #region Harmful effects
    public float airJump { get; private set; } = 0.1f;
    private float currentAirJump;
    public bool noJump { get; private set; }
    #endregion

    private void Start()
    {
        player = GetComponent<Player>();
        currentAirJump = airJump;
    }

    public void takeDame()
    {
        player.StartCoroutine("isKnockBack");
    }
    private void Die() => player.Die();

    public void reverseDirection() => player.setUpPlayer(true);
    public void jumpAir() => player.ativeJumpingAir();

    private IEnumerator isNoJump(float _second) // hiệu ứng gây hại: Xóa khả năng nhảy
    {
        noJump = true;
        Debug.Log("Ngừng nhảy");
        yield return new WaitForSeconds(_second);
        noJump = false;
        Debug.Log("Được phép nhảy");
    }

    private IEnumerator increaseFallSpeed(float _second) // hiệu ứng gây hại: Rơi nhanh hơn khi ở trạng thái trên không
    {
        airJump = 1f;
        yield return new WaitForSeconds(_second);
        airJump = currentAirJump;
    }
}
