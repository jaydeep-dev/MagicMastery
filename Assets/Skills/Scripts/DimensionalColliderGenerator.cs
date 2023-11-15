using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionalColliderGenerator : MonoBehaviour
{
    [SerializeField] GameObject dimensionColliderObject;
    void Awake()
    {
        AddDimensionalColliders();
    }

    void AddDimensionalColliders()
    {
        var cam = Camera.main;
        float x = cam.orthographicSize * 2 * cam.aspect;
        float y = cam.orthographicSize * 2;
        float colliderThickness = 6;
        var topCollider = dimensionColliderObject.AddComponent<BoxCollider2D>();
        topCollider.size = new Vector2(x + colliderThickness * 2, colliderThickness);
        topCollider.offset = new Vector2(0, (y + colliderThickness) / 2);

        var bottomCollider = dimensionColliderObject.AddComponent<BoxCollider2D>();
        bottomCollider.size = new Vector2(x + colliderThickness * 2, colliderThickness);
        bottomCollider.offset = new Vector2(0, -(y + colliderThickness) / 2);

        var leftCollider = dimensionColliderObject.AddComponent<BoxCollider2D>();
        leftCollider.size = new Vector2(colliderThickness, y + colliderThickness * 2);
        leftCollider.offset = new Vector2(-(x + colliderThickness) / 2, 0);

        var rightCollider = dimensionColliderObject.AddComponent<BoxCollider2D>();
        rightCollider.size = new Vector2(colliderThickness, y + colliderThickness * 2);
        rightCollider.offset = new Vector2((x + colliderThickness) / 2, 0);
    }
}
