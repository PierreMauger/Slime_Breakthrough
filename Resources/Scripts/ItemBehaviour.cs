using System;

public class ItemBehaviour : CustomComponent
{
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
        tmpRot.y += deltaTime * 30;
        tmpRot.y = (tmpRot.y + 90) % 180 - 90;
        transform.rotation = tmpRot;
    }
}
