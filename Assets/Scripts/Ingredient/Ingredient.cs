using System.Collections;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private bool _shouldReset;
    [SerializeField] public SpriteRenderer _spriteRenderer;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private AudioSource _splashAudio;
    [SerializeField] private AudioClip[] _splashClips;
    public IngredientData IngredientData;

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
        _dragging = true;
        MessageBus.Instance.Publish(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        _dragging = false;

        if (_onCauldron)
            PublishDestroyCommand();
        else if (_shouldReset)
            transform.position = _initialPosition;
    }

    private void PublishDestroyCommand()
    {
        MessageBus.Instance.Publish(new DropIngredientEvent {Ingredient = this});
        PutInCauldron();
    }

    private void PutInCauldron() 
    {
        _animator.PlayDropingInCauldron();
        StartCoroutine("DestroyIngredient");
        _collider.enabled = false;
        _splashAudio.clip = _splashClips.SelectRandom();
        _splashAudio.PlayDelayed(0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Cauldron"))
            return;

        _onCauldron = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Cauldron"))
            return;

        _onCauldron = false;
    }



    IEnumerator DestroyIngredient() {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}

