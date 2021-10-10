using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public Vector2 vector;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player && player.GetComponent<PlayerControllerScript>().absorber)
		{
            //El player está absorbiendo
            refreshPlayerVector();
            absorbido();
		}

        rb.velocity = Vector2.left * 1f;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        //SE HACE DESDE ACÁ, PORQUE SINO, EL TRIGGER SOLO DETECTA UN OBJETO POR FRAME.
        if(collision.gameObject.name=="absorb")
		{
            print("El enemigo chocó con " + collision.gameObject.name);
            player = collision.gameObject.transform.parent.gameObject;
        }
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        //SE HACE DESDE ACÁ, PORQUE SINO, EL TRIGGER SOLO DETECTA UN OBJETO POR FRAME.
        if (collision.gameObject.name == "absorb")
        {
            print("El enemigo se alejó de " + collision.gameObject.name);
            player = null;
            vector = Vector2.zero;
        }
    }

    public void absorbido()
	{
        this.rb.velocity += vector;
	}

    public void refreshPlayerVector()
	{
        vector = new Vector2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y);
    }
}
