using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputJudgeTwo : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Animator playerAnimator;

    public Direction expectedDirection; // 현재 맞춰야 할 방향 (외부에서 세팅)
    public Direction trapDirection;

    private bool inputReceived = false;

    void Start()
    {
        trapDirection = (Direction)Random.Range(0, 4); // 랜덤 설정
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) CheckInput(Direction.Up);
        else if (Input.GetKeyDown(KeyCode.DownArrow)) CheckInput(Direction.Down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) CheckInput(Direction.Left);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) CheckInput(Direction.Right);
    }



    void CheckInput(Direction input)
    {
        if (input == trapDirection)
        {
            Debug.Log("함정 방향 입력! 실패 처리");

            TriggerTrapAnimation(input);
            playerHealth.TakeDamage();
            inputReceived = true;
            return;
        }

        if (input == expectedDirection)
        {
            Debug.Log("정확한 입력!");
            TriggerSuccessAnimation(input);
            inputReceived = true;
        }
        else
        {
            Debug.Log("틀린 방향! 체력 감소");
            TriggerFailAnimation(input);
            playerHealth.TakeDamage();
            inputReceived = true;
        }
    }

    void TriggerTrapAnimation(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                playerAnimator.SetTrigger("UpTrap");
                break;
            case Direction.Down:
                playerAnimator.SetTrigger("DownTrap");
                break;
            case Direction.Left:
                playerAnimator.SetTrigger("LeftTrap");
                break;
            case Direction.Right:
                playerAnimator.SetTrigger("RightTrap");
                break;
        }
    }

    void TriggerSuccessAnimation(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                playerAnimator.SetTrigger("UpSuccess");
                break;
            case Direction.Down:
                playerAnimator.SetTrigger("DownSuccess");
                break;
            case Direction.Left:
                playerAnimator.SetTrigger("LeftSuccess");
                break;
            case Direction.Right:
                playerAnimator.SetTrigger("RightSuccess");
                break;
        }
    }

    void TriggerFailAnimation(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
                playerAnimator.SetTrigger("UpFail");
                break;
            case Direction.Down:
                playerAnimator.SetTrigger("DownFail");
                break;
            case Direction.Left:
                playerAnimator.SetTrigger("LeftFail");
                break;
            case Direction.Right:
                playerAnimator.SetTrigger("RightFail");
                break;
        }
    }

    IEnumerator CheckIdleMissAfterDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (!inputReceived && expectedDirection != trapDirection)
        {
            Debug.Log("입력 없음 – IdleMiss 발생!");
            playerAnimator.SetTrigger("IdleMiss");
            playerHealth.TakeDamage();
        }
    }

    public void SetExpectedDirection(Direction dir)
    {
        expectedDirection = dir;
        inputReceived = false;

        StartCoroutine(CheckIdleMissAfterDelay(0.6f)); // 타이밍은 BPM 기준으로 조절
    }
}
