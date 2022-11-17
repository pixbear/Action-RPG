using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] float axeMoveSpeed;   // 도끼 이동 스피드
    [SerializeField] float axeRotSpeed;    // 도끼 회전 스피드

    [SerializeField] int QSkillDamage;     // Q 스킬 데미지
    [SerializeField] ParticleSystem qEffect;

    void Update()
    {
        // 도끼 회전
        transform.Rotate(-Vector3.forward * axeRotSpeed * Time.deltaTime);
    }

    // 도끼 이동
    public void ThrowMove(Vector3 _dir)
    {
        transform.position += _dir.normalized * axeMoveSpeed * Time.deltaTime;
    }

    // 도끼 컴백
    public void ThrowBack(Transform _pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, _pos.position + Vector3.up * 1, axeMoveSpeed * Time.deltaTime);
    }

    // 도끼 삭제
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBlue"))
        {
            int _damage = Random.Range(QSkillDamage, QSkillDamage + 5);
            other.GetComponent<Enemy>().TakeDamage(_damage);
            qEffect.Play();

            
        }
    }
}
