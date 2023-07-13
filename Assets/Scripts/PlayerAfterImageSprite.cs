using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImageSprite : MonoBehaviour
{
    public float AlphaMultiplier;
    public float AlphaSet;
    public float ActiveTime;

    private Transform _player;

    private float _timeActivated;
    private float _alpha;
    private SpriteRenderer _sprite;
    private SpriteRenderer _playerSprite;
    private Color _color;

    private void OnEnable()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerSprite = _player.GetComponent<SpriteRenderer>();
        _alpha = AlphaSet;
        _sprite.sprite = _playerSprite.sprite;
        transform.position = _player.position;
        transform.rotation = _player.rotation;
        _timeActivated = Time.time;
    }

    private void Update()
    {
        _alpha *= AlphaMultiplier;
        _color = new Color(1f, 1f, 1f, _alpha);
        _sprite.color = _color;
        if (Time.time >= _timeActivated + ActiveTime)
        {
            PlayerAfterImagePool.Instance.AddToPool(gameObject);
        }
    }
}
