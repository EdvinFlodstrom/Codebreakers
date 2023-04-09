using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject wall;

    private bool target1Hit;
    private bool target2Hit;
    private bool target3Hit;

    void Update()
    {
        if (target1Hit && target2Hit && target3Hit)
        {
            if (wall != null)
            {
                MoveWall();
                if (wall.transform.position.y < -7.25f) Destroy(wall);
            }
        }
    }
    public void Targets(int _targetNum)
    {
        if (_targetNum == 1) target1Hit = true;
        if (_targetNum == 2) target2Hit = true;
        if(_targetNum == 3) target3Hit = true;
    }
    private void MoveWall()
    {
        wall.transform.position = new Vector3(wall.transform.position.x, wall.transform.position.y - Time.deltaTime * 1.25f, wall.transform.position.z);
    }
}
