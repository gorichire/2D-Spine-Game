using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmTimingController : MonoBehaviour
{

    public float frameTime = 1f / 30f; // 30fps ����
    public Animator aniWren;

    public bool isInputTiming { get; private set; } = false;
    public float targetInputTime { get; private set; } = 0f;

    private bool isPlaying = false;

    void Start()
    {
        
    }

    public void PlayPattern1()
    {
        if (!isPlaying)
            StartCoroutine(PlayPattern1Coroutine());
    }

    public void PlayPattern2()
    {
        if (!isPlaying)
            StartCoroutine(PlayPattern2Coroutine());
    }

    IEnumerator PlayPattern1Coroutine()
    {
        isPlaying = true;

        yield return WaitFrames(8);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC ��� 1");
        yield return WaitFrames(1);

        yield return WaitFrames(16);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC ��� 2");
        yield return WaitFrames(1);

        yield return WaitFrames(12); // ���������� 1������ ��

        isInputTiming = true;
        yield return WaitFrames(4); // Fast ����

        targetInputTime = Time.time;
        Debug.Log("��Ȯ�� �Է� Ÿ�̹� (����1)");

        yield return WaitFrames(4); // Slow ����
        isInputTiming = false;

        yield return WaitFrames(1); // ���� �÷��̾� ��� ����
        isPlaying = false;
    }

    IEnumerator PlayPattern2Coroutine()
    {
        isPlaying = true;

        yield return WaitFrames(8);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC ��� 1");
        yield return WaitFrames(1);

        yield return WaitFrames(7);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC ��� 2");
        yield return WaitFrames(1);

        yield return WaitFrames(7);
        aniWren.SetTrigger("sing");
        Debug.Log("NPC ��� 3");
        yield return WaitFrames(1);

        yield return WaitFrames(16); // ���� Ÿ�̹�

        yield return WaitFrames(12); // ���������� 1������ ��

        isInputTiming = true;
        yield return WaitFrames(4); // Fast ����

        targetInputTime = Time.time;
        Debug.Log("��Ȯ�� �Է� Ÿ�̹� (����2)");

        yield return WaitFrames(4); // Slow ����
        isInputTiming = false;

        yield return WaitFrames(1);
        isPlaying = false;
    }

    IEnumerator WaitFrames(int frameCount)
    {
        yield return new WaitForSeconds(frameCount * frameTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
