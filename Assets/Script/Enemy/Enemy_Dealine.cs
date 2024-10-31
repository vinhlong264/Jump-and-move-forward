using System.Collections;
using UnityEngine;

public class Enemy_Dealine : MonoBehaviour
{
    [SerializeField] private GameObject bullet;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newBulet = Instantiate(bullet,transform.position, Quaternion.identity);

            if(newBulet != null)
            {
                newBulet.GetComponent<Angry_bullet>().AttackTarget(CharacterStatus.Instance.transform, 10f);
            }
        }
    }
}
