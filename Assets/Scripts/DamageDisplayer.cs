using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDisplayer : MonoBehaviour
{
    private const string ColorProp = "_BaseColor";

    public string enemyTag;
    [Range(0, 1)]
    public float iterTime = 0.2f;
    public uint repeats = 3;
    public SkinnedMeshRenderer renderer;

    private Coroutine routine;
    private Color baseColor;
    private WaitForSeconds wait;

    private void Start() {
        baseColor = renderer.material.GetColor(ColorProp);
    }


    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == enemyTag) {
            if (routine != null)
                StopCoroutine(routine);
            routine = StartCoroutine(DamageRoutine());
        }
    }

    public IEnumerator DamageRoutine() {
        wait = new WaitForSeconds(iterTime);

        for (int i = 0; i < repeats; i++) {
            renderer.material.SetColor(ColorProp, Color.red);
            yield return wait;
            renderer.material.SetColor(ColorProp, baseColor);
            yield return wait;
        }

        routine = null;
    }
}
