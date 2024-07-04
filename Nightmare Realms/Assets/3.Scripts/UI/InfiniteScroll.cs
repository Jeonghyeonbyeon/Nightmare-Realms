using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    left = -1,
    right = 1
}

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float resetPositionX;
    [SerializeField] private float startPositionX;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(new Vector3((int)direction * speed * Time.deltaTime, 0, 0));

        if (direction == Direction.left && transform.position.x < resetPositionX)
        {
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
        else if (direction == Direction.right && transform.position.x > resetPositionX)
        {
            transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        }
    }
}