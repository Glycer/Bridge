using System.Collections;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;

    private void Start()
    {
        StartCoroutine(BLARGH());
    }

    public void Launch(Vector3 position, Vector3 direction)
    {
        GameObject shot = Instantiate(projectile);

        Projectile _projectile = shot.GetComponent<Projectile>();

        _projectile.transform.position = position;
        _projectile.transform.eulerAngles = direction;
        _projectile.Activate();

        _projectile.GetComponent<Rigidbody>().AddForce(0, 0, _projectile.speed);
    }

    IEnumerator BLARGH()
    {
        while (true)
        {
            Launch(transform.position, transform.eulerAngles);
            yield return new WaitForSeconds(1);
        }
    }
}
