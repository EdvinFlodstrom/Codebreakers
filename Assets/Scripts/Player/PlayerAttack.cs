using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDamage;
    [SerializeField] private GameObject[] lasers;
    [SerializeField] public Transform playerPosition;


    private float attackWait = Mathf.Infinity;

    void Start()
    {
        
    }

    void Update()
    {
        attackWait += Time.deltaTime;

        if ((Input.GetMouseButton(0) || (Input.GetKey(KeyCode.LeftShift))) && (attackWait > attackCooldown))
        {
            //Fungerar
            attackWait = 0;
            lasers[Attack(attackDamage)].transform.position = playerPosition.position;
        }
    }
    private int Attack(float _damage)
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].SetActive(true);
            if (!lasers[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
