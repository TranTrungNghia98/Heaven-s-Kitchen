using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedClearCounter;
    }

    [SerializeField] Transform kitchenParentTopPoint;
    private KitchenObject kitchenObject;

    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private Vector3 lastInteractDirection;

    private bool isWalking;
    private BaseCounter selectedClearCounter;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("You should not create more than a player");
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAfternateAction += GameInput_OnInteractAfternateAction;
    }

    private void GameInput_OnInteractAfternateAction(object sender, EventArgs e)
    {
        selectedClearCounter.Interactnate(this);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedClearCounter != null)
        {
            selectedClearCounter.Interact(this);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerHandleMovements();
        PlayerHandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void PlayerHandleMovements()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = Time.deltaTime * moveSpeed;
        float playerRadius = 0.7f;
        float playerHeight = 2.0f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            // Cannot move towards / backward

            // Player try to move to the left/right
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0);
            canMove = moveDirection.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            // Only Move to left/right
            if (canMove)
            {
                moveDirection = moveDirectionX.normalized;
            }

            else
            {
                // Cannot move left/right

                // Player try to move toward / backward
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z);
                canMove = moveDirection.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                // Only move toward / backward
                if (canMove)
                {
                    moveDirection = moveDirectionZ.normalized;
                }

                else
                {
                    // Cannot move any direction
                }
            }


        }

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        float rotateSpeed = 10.0f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    private void PlayerHandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        // Change interact direction when player press move buttons
        if (inputVector != Vector2.zero)
        {
            lastInteractDirection = new Vector3(inputVector.x, 0, inputVector.y);
        }

        float interactDistance = 2.0f;

        // Check is player interacting with something or not
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                if(selectedClearCounter !=  clearCounter)
                {
                    SetSelectedCounter(clearCounter);
                }
            }

            else
            {
                SetSelectedCounter(null);
            }
        }

        else
        {
            SetSelectedCounter(null);
        }

    }

    private void SetSelectedCounter(BaseCounter baseCounter)
    {
        this.selectedClearCounter = baseCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs 
        { 
            selectedClearCounter = baseCounter 
        }
        );
    }

    public Transform GetKitchenObjectFollowPoint()
    {
        return kitchenParentTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
