using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearanceMeleeAttackAnim : MeleeAttackAnim
{
    [SerializeField] private float maxA;
    [SerializeField] private float startA;

    private SpriteRenderer spriteRenderer;
    private float FROM_256_TO_1 = 256;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, startA / FROM_256_TO_1);
    }
    public override void StartAnim()
    {
        StartCoroutine("Anim");
    }

    private IEnumerator Anim()
    {
        float t = 0;
        Color color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, startA);
        while (t < 1)
        {
            color.a = (startA + (maxA - startA) * t * t) / FROM_256_TO_1;
            spriteRenderer.color = color;
            t += Time.deltaTime / AnimTime;
            yield return null;
        }

        MeleeAttack.MeleeAttackGiveDamage.GiveDamage();
    }
}
