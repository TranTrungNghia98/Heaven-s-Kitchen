using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedClearCounter;
    }

    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    private Vector3 lastInteractDirection;

    private bool isWalking;
    private ClearCounter selectedClearCounter;

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
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedClearCounter != null)
        {
            selectedClearCounter.Interact();
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
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

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
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

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
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
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

    private void SetSelectedCounter(ClearCounter clearCounter)
    {
        this.selectedClearCounter = clearCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs 
        { 
            selectedClearCounter = clearCounter 
        }
        );
    }

}
