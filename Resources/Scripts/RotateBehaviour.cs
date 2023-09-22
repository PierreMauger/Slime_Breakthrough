using System;

public class RotateBehaviour : CustomComponent
{
    public float speed = 5.0f;

    private float time = 0.0f;
    private Transform transform;

    public void onInit()
    {
        transform = this.entity.getComponent<Transform>();
    }

    public void onUpdate(float deltaTime)
    {
        time += deltaTime;

        Vector3 tmpRot = transform.rotation;
        tmpRot.y += deltaTime * speed;
        tmpRot.y = (tmpRot.y + 90) % 180 - 90;
        transform.rotation = tmpRot;
    }
}
