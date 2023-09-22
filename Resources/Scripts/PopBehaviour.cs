using System;

public class PopBehaviour : CustomComponent
{
    public float directionX = 0.0f;
    public float directionY = 0.0f;
    public float directionZ = 0.0f;
    public float frequency = 5.0f;

    private float time = 0.0f;
    private Transform transform;
    private Vector3 origin;

    public void onInit()
    {
        transform = this.entity.getComponent<Transform>();
        origin = transform.position;
    }

    public void onUpdate(float deltaTime)
    {
        time += deltaTime;

        Vector3 tmpPos = transform.position;
        float displacement = MathF.Sin(time * frequency) < 0.0f ? 0.0f : MathF.Sin(time * frequency);
        tmpPos.x = origin.x + displacement * directionX;
        tmpPos.y = origin.y + displacement * directionY;
        tmpPos.z = origin.z + displacement * directionZ;
        transform.position = tmpPos;
    }
}
