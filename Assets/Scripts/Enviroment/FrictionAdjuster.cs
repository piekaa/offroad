using UnityEngine;
using System.Collections.Generic;

public class FrictionAdjuster : MonoBehaviour
{
    public PiekaMaterialFloatTable MaterialFrictionTable;

    private int collidingMaterialsCount = 0;

    private PiekaMaterial myMaterial;

    private PhysicsMaterial2D myPhysicsMaterial;

    private Collider2D myCollider;

    protected void Start()
    {
        myMaterial = GetComponent<ObjectWithMaterial>().PiekaMaterial;
        myCollider = GetComponent<Collider2D>();
        myPhysicsMaterial = myCollider.sharedMaterial;
        myPhysicsMaterial.friction = MaterialFrictionTable.Default;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var objectWithMaterial = col.gameObject.GetComponent<ObjectWithMaterial>();
        if (objectWithMaterial != null)
        {
            var otherMaterial = objectWithMaterial.PiekaMaterial;
            var friction = MaterialFrictionTable.GetFloat(myMaterial, otherMaterial);
            myPhysicsMaterial.friction = friction;
            collidingMaterialsCount++;
            myCollider.sharedMaterial = myPhysicsMaterial;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        var objectWithMaterial = col.gameObject.GetComponent<ObjectWithMaterial>();
        if (objectWithMaterial != null)
        {
            collidingMaterialsCount--;
            if (collidingMaterialsCount == 0)
            {
                myPhysicsMaterial.friction = MaterialFrictionTable.Default;
                myCollider.sharedMaterial = myPhysicsMaterial;
            }
        }
    }
}