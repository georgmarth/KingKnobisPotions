using System;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private bool _shouldReset; 
    
    private bool _dragging;
    private bool _onCauldron;
    private Camera _camera;
    private Vector3 _initialPosition;

    private void Start()
    {
        _camera = Camera.main;
        _initialPosition = transform.position;
        
        MessageBus.Subscribe<DestroyCommand>(_ => PutInCauldron());
    }

    private void Update()
    {
        DragPotion();
    }

    private void DragPotion()
    {
        if (_dragging)
        {
            var currentMousePosition = Input.mousePosition;
            var worldPosition = _camera.ScreenToWorldPoint(new Vector3(currentMousePosition.x, currentMousePosition.y,
                -_camera.transform.position.z));
            transform.position = worldPosition;
        }
    }

    private void OnMouseDown()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        _dragging = true;
        MessageBus.Publish(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        GetComponent<Renderer>().material.color = Color.red;
        _dragging = false;

        if (_onCauldron)
            PublishDestroyCommand();
        else if (_shouldReset)
            transform.position = _initialPosition;
    }

    private void PublishDestroyCommand()
    {
        MessageBus.Publish(new DestroyCommand {Ingredient = this});
    }

    private void PutInCauldron()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Cauldron"))
            return;
        
        GetComponent<Renderer>().material.color = Color.magenta;
        _onCauldron = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Cauldron"))
            return;

        GetComponent<Renderer>().material.color = Color.yellow;
        _onCauldron = false;
    }
}
