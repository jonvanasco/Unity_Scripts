using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int Respawn;
    private GameObject player;
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    //private Animator anim;
    private bool dead;

    private void Awake()
    {
        currentHealth = startingHealth;
        //anim = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //anim.SetTrigger("hurt");
            //iframes
        }
        else
        {
            if (!dead)
            {
                //anim.SetTrigger("die");
                //GetComponent<PlayerMovement>().enabled = false;
                dead = true;
                SceneManager.LoadScene(Respawn);
                Destroy(this.gameObject);
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
}