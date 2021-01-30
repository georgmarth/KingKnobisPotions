using System.Collections;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private bool _shouldReset;
    [SerializeField] private Renderer _spriteRenderer;
    public int _id;

    private bool _dragging;
    private bool _onCauldron;
    private Camera _camera;
    private Vector3 _initialPosition;

    private IngredientsAnimator _animator;
    

    private void Start()
    {
        _camera = Camera.main;
        _initialPosition = transform.position;
        _animator = new IngredientsAnimator { _animator = GetComponent<Animator>() };
        
        MessageBus.Instance.Subscribe<DropIngredientEvent>(evt => PutInCauldron(evt.Ingredient));
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
        _spriteRenderer.material.color = Color.yellow;
        _dragging = true;
        MessageBus.Instance.Publish(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        _spriteRenderer.material.color = Color.red;
        _dragging = false;

        if (_onCauldron)
            PublishDestroyCommand();
        else if (_shouldReset)
            transform.position = _initialPosition;
    }

    private void PublishDestroyCommand()
    {
        MessageBus.Instance.Publish(new DropIngredientEvent {Ingredient = this});
    }

    private void PutInCauldron(Ingredient ingredient) {
        if (this == ingredient)     
        { 
        _animator.PlayDropingInCauldron();
        StartCoroutine("DestroyIngredient");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Cauldron"))
            return;

        _spriteRenderer.material.color = Color.magenta;
        _onCauldron = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Cauldron"))
            return;

        _spriteRenderer.material.color = Color.yellow;
        _onCauldron = false;
    }



    IEnumerator DestroyIngredient() {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}

