using System.Collections;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Projectile projectile;

    private void Start()
    {
        //StartCoroutine(BLARGH());
    }

    // TODO: Fix tracking parameter, it should be a projectile variable
    public void Launch(Vector3 position, Vector3 direction, bool tracking = false, GameObject targetDirection = null)
    {
        GameObject shot = Instantiate(projectile.gameObject);

        Projectile _projectile = shot.GetComponent<Projectile>();

        _projectile.transform.position = position;
        _projectile.transform.eulerAngles = direction;
        _projectile.Activate();

        if (!tracking)
            _projectile.GetComponent<Rigidbody>().AddForce(_projectile.transform.forward * 100 * projectile.speed);
        else
            _projectile.StartTracking(targetDirection);
    }

    /*IEnumerator BLARGH()
    {
        while (true)
        {
            Launch(transform.position, transform.eulerAngles);
            yield return new WaitForSeconds(1);
        }
    }*/
}
